//the current url we are located at!
var ourToDoApi = "/api/Todo";
$(document).ready(function () {

    populatePageData();

    //what happens whe someone clicks "Add"
    $('button').click(function () {
        //get today's date
        var now = new Date();

        //create a data structure that we'll send to the API 
        var newTodoItem = {
            createdBy: "TechTalkCanada",
            description: $("input[name=task]").val(), //get the input from the text box
            createdDate: now,
            dueDate: new Date(now.getTime() + 24 * 60 * 60 * 1000) //set the due date to tomorrow
        }

        $('#todo').append("<ul>"  + newTodoItem.createdBy + " has to " + newTodoItem.description + " by " + newTodoItem.dueDate 
             + " <a href='#' class='close' aria-hidden='true'>&times;</a></ul>");
        saveNewTodoItem(newTodoItem);
    });

    //This is what happens when we click the x on the todo item
    $("body").on('click', '#todo a', function () {
        //get the html of the todo item we clicked
        var todoId = $(this).closest("ul").attr('id');
        //find the id of the todo item
        var id = todoId.substring("todo-".length);
        //remove the item from the browser
        $(this).closest("ul").remove();
        //JOAN TODO
        //delete the item in the API
        deleteTodoItem(id);
    });
});

function saveNewTodoItem(todoItem) {
    $.ajax({
        accepts: "application/json",
        url: ourToDoApi,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        method: "POST",
        data: JSON.stringify(todoItem),
    })
    .done(function (response) {
        console.log("Great success!");
        console.log(response);
    })
    .fail(function (err) {
        alert("Oh boy we done goofed");
        console.log(err);
    })
};

function deleteTodoItem(id) {
    //OH NO HOW DO I DO THIS
};

function populatePageData() {

    $.ajax({
        accepts: "application/json",
        url: ourToDoApi,
        method: "GET"
    })
    .done(function (response) {
        console.log(response);
        for (var i = 0; i < response.length; i++) {
            $('#todo').append("<ul id='todo-"+response[i].id+"'>" + response[i].createdBy + " has to " + response[i].description + " by " + response[i].dueDate +
                " <a href='#' class='close' aria-hidden='true'>&times;</a></ul>");
        }
    })
    .fail(function (err) {
        alert("Oh boy we done goofed");
        console.log(err);
    });
};