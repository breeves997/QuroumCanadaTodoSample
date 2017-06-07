using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace QCalTechTalkTodo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //here we are adding the in memory database so that it is available to our API controllers
            //also, adding a bit of extra data 
            services.AddSingleton<InMemoryDatabase>(new InMemoryDatabase()
            {
                Data = new Dictionary<long, TodoItem>()
                {
                    [1] = new TodoItem
                    {
                        CreatedBy = "Ben Reeves",
                        CreatedDate = DateTime.UtcNow,
                        Description = "Take over the world with robots",
                        DueDate = DateTime.UtcNow.AddMonths(2),
                        Id = 1
                    },
                    [2] = new TodoItem
                    {
                        CreatedBy = "Joan Javillonar",
                        CreatedDate = DateTime.UtcNow,
                        Description = "Build a web app from scratch",
                        DueDate = DateTime.UtcNow.AddDays(1),
                        Id = 2
                    }
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //this tells the application to use index.html as the default web page for our application
            app.UseDefaultFiles();
            //this tells the application that we're going to be sending raw files from the filesystem!
            //In our case, we will be sending back just the index.html file without doing any server pre-processing
            app.UseStaticFiles();
            //This tells the application that we are going to be using the microsoft MVC patterns to handle things like
            //routing, api responses, server rendering, etc!
            app.UseMvc();
        }
    }
}
