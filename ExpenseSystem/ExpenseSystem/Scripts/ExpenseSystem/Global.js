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