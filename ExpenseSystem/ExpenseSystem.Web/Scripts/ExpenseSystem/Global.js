//This configuration helps us to display loading panel when we use ajax request.
$.ajaxSetup({
    beforeSend: function () {
        $("#loadingArea").show();
    },
    complete: function () {
        $("#loadingArea").hide();
    },
    success: function () {
        $("#loadingArea").hide();
    },
    error: function () {
        $("#loadingArea").hide();
    }
});

//Variable to store session identifier to prevent CSRF attacks
var gSessionId;
$(document).ready(function () {
    //Session identifier to prevent CSRF attacks
    gSessionId = $('#sessionId').val();
});