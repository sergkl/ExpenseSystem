//When document will be ready 
$(document).ready(function () {
    //We attach tree view jquery component for list of results
    $('#tagsTree').treeview();

    //Attaches date picker for date range (Start and End date)
    $('#startDate').datepicker();
    $('#startDate').datepicker("option", "dateFormat", 'mm/dd/yy');
    $('#endDate').datepicker();
    $('#endDate').datepicker("option", "dateFormat", 'mm/dd/yy');
});