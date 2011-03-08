$(document).ready(function () {
    $('#tagsTree').treeview();

    $('#tagsTree li .tagValue').click(function (event) {
        event.stopPropagation();
        tagClicked($(this).parent()[0]);
    });

    $('#addTagButton').click(addTag);
    $('#editTagButton').click(editTag);
    $('#deleteTagButton').click(deleteTag);
});

function addExpenseRecord() {
    var newExpenseRecord = { Description: "", Price: "0", DateStamp: "" };
    var expenseRecordTemplate = $("#expenseRecordTemplate").tmpl(newExpenseRecord);
    $('#addExpenseRecordArea').before(expenseRecordTemplate);
    assignExpenseRecordEvents(expenseRecordTemplate);
    editExpenseRecord(expenseRecordTemplate);
}

function deleteExpenseRecord(recordRow) {
    var expenseRecordId = $(recordRow).attr("id");
    if (expenseRecordId != "") {
        $.ajax({
            url: "/Expense/DeleteExpenseRecord",
            type: "POST",
            data: { expenseRecordId: expenseRecordId.replace("expenseRecord_", "") },
            dataType: "json",
            success: function (data) {
                if (!data.IsError) {
                    recordRow.remove();
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
    else {
        recordRow.remove();
    }
}

function editExpenseRecord(recordRow) {
    $('.editExpenseRecordButton', recordRow).addClass("hidden");
    $('.saveExpenseRecordButton', recordRow).removeClass("hidden");

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

function saveExpenseRecord(recordRow) {
    //new values
    var description = $('.tdDescription input', recordRow).val();
    var price = $('.tdPrice input', recordRow).val().replace(",", ".");
    var dateStamp = $('.tdDateStamp input', recordRow).val();
    var tagId = $('#tagsTree .selected').attr("id").replace("tagId_", "");

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

    var currencyChecker = /^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$/;
    if (!currencyChecker.test(price)) {
        alert("Enter a valid amount spent value");
        return;
    }

    if (isNaN(Date.parse(dateStamp))) {
        alert("Enter a valid date stamp");
        return;
    }

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
    $.ajax({
        url: "/Expense/" + actionName,
        type: "POST",
        data: dataToSend,
        dataType: "json",
        success: function (data) {
            if (!data.IsError) {
                if (typeof (data.Id) != "undefined") {
                    $(recordRow).attr("id", "expenseRecord_" + data.Id);
                }

                $('.editExpenseRecordButton', recordRow).removeClass("hidden");
                $('.saveExpenseRecordButton', recordRow).addClass("hidden");

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

function assignExpenseRecordEvents(row) {
    $('.deleteExpenseRecordButton', row).click(function () { deleteExpenseRecord($(this).parent().parent()); });
    $('.editExpenseRecordButton', row).click(function () { editExpenseRecord($(this).parent().parent()); });
    $('.saveExpenseRecordButton', row).click(function () { saveExpenseRecord($(this).parent().parent()); });
}

function tagClicked(tag) {
    $('#tagsTree li').removeClass("selected");
    $(tag).addClass("selected");
    loadExpensesRecords($(tag).attr("id").replace("tagId_", ""), false);
}

function editTag() {
    selectedLi = $('#tagsTree .selected');
    var valueArea = selectedLi.find('> .tagValue');
    valueArea.hide();
    editingControl = $('<input class="editing" type="text" value="' + valueArea.text() + '" />');
    valueArea.after(editingControl);
    editingControl.blur(function () { cancelEditing(this) });
    editingControl.keyup(function (e) {
        if (e.keyCode == 13) {
            cancelEditing(this);
        }
    });
    editingControl.focus();
}

function deleteTag() {
    selectedLi = $('#tagsTree .selected');
    $.post("/Tag/DeleteTag",
            'tagId=' + selectedLi.attr("id").replace("tagId_", ""),
            function (result) {
                if (!result.IsError) {
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
                    alert(joinStrings(result.Errors));
                }
            },
            "json");
}

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

function joinStrings(strings) {
    var result = "";
    for (var i = 0; i < strings.length; i++) {
        result += strings[i] + ";";
    }
    return result;
}

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
