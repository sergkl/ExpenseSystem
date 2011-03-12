//When document will be loaded
$(document).ready(function () {
    //Attaching tree view component for list of tags
    $('#tagsTree').treeview();

    //Attaching onClick event for tags in the tree
    $('#tagsTree li .tagValue').click(function (event) {
        event.stopPropagation();
        tagClicked($(this).parent()[0]);
    });

    //Attaching onClick event when we click on the "Add tag" button
    $('#addTagButton').click(addTag);
    
    //Attaching onClick event when we click on the "Edit tag" button
    $('#editTagButton').click(editTag);

    //Attaching onClick event when we click on the "Delete tag" button
    $('#deleteTagButton').click(deleteTag);
});

//Function adds new template of expense record to the expense records table
function addExpenseRecord() {
    //Setting default values for expense record
    var newExpenseRecord = { Description: "", Price: "0", DateStamp: "" };
    
    //Adding new expense record on the expense records table. This operation will affect only for client side. We still don't create an expense record on the server side.
    var expenseRecordTemplate = $("#expenseRecordTemplate").tmpl(newExpenseRecord);
    $('#addExpenseRecordArea').before(expenseRecordTemplate);
    assignExpenseRecordEvents(expenseRecordTemplate);
    editExpenseRecord(expenseRecordTemplate);
}

//Function deletes expense record from the system
function deleteExpenseRecord(recordRow) {
    var expenseRecordId = $(recordRow).attr("id");
    //Delete expense record on the server side
    if (expenseRecordId != "") {
        $.ajax({
            url: "/Expense/DeleteExpenseRecord",
            type: "POST",
            data: { expenseRecordId: expenseRecordId.replace("expenseRecord_", "") },
            dataType: "json",
            success: function (data) {
                //If deleting was successfull then we delete record from client side (So we delete row from expense record table)
                if (!data.IsError) {
                    recordRow.remove();
                }
                else {
                    //Otherwise we show the error messages.
                    alert(joinStrings(result.Errors));
                }
            },
            error: function () {
                alert('Server is busy. Try again later.');
            }
        });
    }
    else {
        recordRow.remove();
    }
}

//Function edits expense record
function editExpenseRecord(recordRow) {
    //Displaying the "Save" button (for the row of the expense records table) instead "Edit" one.
    $('.editExpenseRecordButton', recordRow).addClass("hidden");
    $('.saveExpenseRecordButton', recordRow).removeClass("hidden");

    //Creating input html controls for editing description, price and date stamp
    var valueContainers = ["tdDescription", "tdPrice", "tdDateStamp"];
    for (var i = 0; i < valueContainers.length; i++) {
        $('.' + valueContainers[i] + ' span', recordRow).hide();
        descriptionInput = $('<input type="text" />');
        descriptionInput.val($('.' + valueContainers[i] + ' span', recordRow).text());
        descriptionInput.addClass('fullFill');
        $('.' + valueContainers[i], recordRow).append(descriptionInput);
        if (valueContainers[i] == "tdDateStamp") {
            descriptionInput.datepicker();
        }
    }
}

//Save expense record in the system
function saveExpenseRecord(recordRow) {
    //New values
    var description = $('.tdDescription input', recordRow).val();
    var price = $('.tdPrice input', recordRow).val().replace(",", ".");
    var dateStamp = $('.tdDateStamp input', recordRow).val();
    var tagId = $('#tagsTree .selected').attr("id").replace("tagId_", "");

    //Verifying the values
    var errorsList = "Please provide: ";
    var errorPresent = false;
    if ($.trim(description) == "") {
        errorsList += "Description;";
        errorPresent = true;
    }
    if ($.trim(price) == "") {
        errorsList += "Price;";
        errorPresent = true;
    }
    if ($.trim(dateStamp) == "") {
        errorsList += "Date;";
        errorPresent = true;
    }
    if (errorPresent) {
        alert(errorsList);
        return;
    }

    //Check currency format
    var currencyChecker = /^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$/;
    if (!currencyChecker.test(price)) {
        alert("Enter a valid amount spent value");
        return;
    }

    //Check date format
    if (isNaN(Date.parse(dateStamp))) {
        alert("Enter a valid date stamp");
        return;
    }

    //Forming json structure with expense record data to send it to the server
    var dataToSend;
    var actionName;
    if ($(recordRow).attr("id") == "") { //new record
        actionName = "AddExpenseRecord";
        dataToSend = { description: description, price: price, tagId: tagId, dateStamp: dateStamp };
    }
    else {
        actionName = "EditExpenseRecord";
        dataToSend = { description: description, price: price, tagId: tagId, dateStamp: dateStamp, expenseRecordId: $(recordRow).attr("id").replace("expenseRecord_", "") };
    }

    //Send json structure to the server
    $.ajax({
        url: "/Expense/" + actionName,
        type: "POST",
        data: dataToSend,
        dataType: "json",
        success: function (data) {
            //If we don't have any errors it signals then saving was successfull
            if (!data.IsError) {
                //We set new identifier for expense record row
                if (typeof (data.Id) != "undefined") {
                    $(recordRow).attr("id", "expenseRecord_" + data.Id);
                }

                //Show "Edit" button, instead of "Save" one.
                $('.editExpenseRecordButton', recordRow).removeClass("hidden");
                $('.saveExpenseRecordButton', recordRow).addClass("hidden");

                //Copy data from inputs to the table cells value
                var valueContainers = ["tdDescription", "tdPrice", "tdDateStamp"];
                for (var i = 0; i < valueContainers.length; i++) {
                    $('.' + valueContainers[i] + ' span', recordRow).text($('.' + valueContainers[i] + ' input', recordRow).val());
                    $('.' + valueContainers[i] + ' input', recordRow).remove();
                    $('.' + valueContainers[i] + ' span', recordRow).show();
                }
            }
            else {
                alert(joinStrings(result.Errors));
            }
        },
        error: function () {
            alert('Server is busy. Try again later.');
        }
    });
}

//Assign expense records rows events
function assignExpenseRecordEvents(row) {
    //For "Delete" button
    $('.deleteExpenseRecordButton', row).click(function () { deleteExpenseRecord($(this).parent().parent()); });
    
    //For "Edit" button
    $('.editExpenseRecordButton', row).click(function () { editExpenseRecord($(this).parent().parent()); });
    
    //For "Save" button
    $('.saveExpenseRecordButton', row).click(function () { saveExpenseRecord($(this).parent().parent()); });
}

//Function load expense records by clicked tag
function tagClicked(tag) {
    $('#tagsTree li').removeClass("selected");
    $(tag).addClass("selected");
    loadExpensesRecords($(tag).attr("id").replace("tagId_", ""), false);
}

//Function edits tag name.
function editTag() {
    //Get selected tag li
    selectedLi = $('#tagsTree .selected');
    var valueArea = selectedLi.find('> .tagValue');
    valueArea.hide();
    
    //Adding input html control to put new tag name
    editingControl = $('<input class="editing" type="text" value="' + valueArea.text() + '" />');
    valueArea.after(editingControl);
    
    //Assigning events when we should save new name for the tag
    editingControl.blur(function () { cancelEditing(this) });
    editingControl.keyup(function (e) {
        if (e.keyCode == 13) {
            cancelEditing(this);
        }
    });
    editingControl.focus();
}

//Function deletes tag from server and then client side
function deleteTag() {
    selectedLi = $('#tagsTree .selected');
    $.post("/Tag/DeleteTag",
            'tagId=' + selectedLi.attr("id").replace("tagId_", ""),
            function (result) {
                //If execution result was successfull then we remove branch of tag from client side
                if (!result.IsError) {
                    //Operations like changing clas collapsable, expandble, lastCollapsable, etc. They need to display corrent images near tree (for lines)
                    var ul = $(selectedLi).parent();
                    if (ul.children().length == 1) {    //so it is the last item in the branch.
                        ul.parent().find('.hitarea').remove();
                        ul.parent().removeClass("collapsable"); 
                        ul.parent().removeClass("expandable");
                        if (ul.parent().hasClass("lastCollapsable") || ul.parent().hasClass("lastExpandable")) {
                            ul.parent().attr("class", "last");
                        }
                        ul.remove();
                    }
                    else {
                        $(selectedLi).remove();
                    }
                }
                else {
                    //If execution result was failed. Then we show error messages
                    alert(joinStrings(result.Errors));
                }
            },
            "json");
}

//Function saves new name for tag for client and server side
function cancelEditing(editingControl) {
    if ($(editingControl).val() != "") {
        var valueArea = $(editingControl).parent().find("> .tagValue");
        $.post("/Tag/ChangeTagName",
            'tagId=' + valueArea.parent().attr("id").replace("tagId_", "") + '&tagName=' + $(editingControl).val(),
            function (result) {
                if (!result.IsError) {
                    valueArea.text($(editingControl).val());
                    valueArea.show();
                    $(editingControl).remove();
                }
                else {
                    alert(joinStrings(result.Errors));
                }
            },
            "json");
    }
}

//Add new tag for client and server side
function addTag() {
    if ($('#tagsTree').length == 0) {   //If we don't have tree at all.
        $.post("/Tag/AddTag",
            'name=New node&parentId=null',
            function (result) {
                if (!result.IsError) {
                    var newTree = $('<ul id="tagsTree"></ul>');
                    var newLi = $('<li id="tagId_' + result.Id + '"></li>');
                    var newTagValue = $('<div class="tagValue">New node</div>');

                    newLi.append(newTagValue);
                    newTree.append(newLi);
                    newTagValue.click(function (event) {
                        event.stopPropagation();
                        tagClicked($(this).parent()[0]);
                    });

                    $('#chooseTagsLabel').after(newTree);
                    $('#tagsTree').treeview();
                }
                else {
                    alert(joinStrings(result.Errors));
                }
            },
            "json");
    }
    else {
        selectedLi = $('#tagsTree .selected');
        if (selectedLi.length > 0) {

            $.post("/Tag/AddTag",
            'name=New node&parentId=' + selectedLi.attr("id").replace("tagId_", ""),
            function (result) {
                if (!result.IsError) {
                    var newId = "tagId_" + result.Id;
                    var text = "New node";
                    var li = $('<li></li>');
                    var spanValue = $('<div class="tagValue"></div>');
                    spanValue.text(text);
                    li.append(spanValue);
                    li.attr("id", newId);

                    var childUl = selectedLi.find('> ul');

                    //if li hasn't ul, we create it:
                    if (childUl.length == 0) {
                        childUl = $('<ul></ul>');
                        childUl.append(li);
                        selectedLi.append(childUl);
                        $("#tagsTree").treeview({
                            add: selectedLi
                        });
                    }
                    else {
                        childUl.append(li);
                        $("#tagsTree").treeview({
                            add: li
                        });
                    }

                    //Assign event for new node
                    $('#' + newId + ' .tagValue').click(function (event) {
                        event.stopPropagation();
                        tagClicked($(this).parent()[0]);
                    });
                }
                else {
                    alert(joinStrings(result.Errors));
                }
            },
            "json");
        }
        else {
            alert("Choose the parent tag");
        }
    }
}

//Function joins all messages from the passed array and displays them as one entire string. 
//This function is used to display errors in the application
function joinStrings(strings) {
    var result = "";
    for (var i = 0; i < strings.length; i++) {
        result += strings[i] + ";";
    }
    return result;
}

//Load expense records to expense record table.
function loadExpensesRecords(tagId, includeBranches) {
    $.ajax({
        url: "/Expense/GetExpenseRecords",
        type: "POST",   //I used post in this situation, because IE caches the data and after clicking on choosed tag again, expense records will be shown as withouth changing.
        data: { tagId: tagId, includeBranchesResuls: includeBranches },
        dataType: "html",
        success: function (data) {
            $('#expensesRecords').html(data);
            $('.addExpenseRecordButton').click(addExpenseRecord);
            $.each($('tr[class=expenseRecord]'), function () { assignExpenseRecordEvents(this); });
        },
        error: function () {
            alert('Server is busy. Try again later.');
        }
    });
}
