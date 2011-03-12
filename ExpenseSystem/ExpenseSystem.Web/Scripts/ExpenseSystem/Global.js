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