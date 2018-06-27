$(document).ready(function () {
    $('td').each(function () {
        if (parseFloat($(this).html()) < 0) {
            $(this).attr("style", "color:red");
            $(this).html("(" + Math.abs(parseFloat($(this).html())) + ")");
        }
    });

    $('.util').each(function () {
        if (parseFloat($(this).html()) > 0) {
            $(this).attr("style", "color:blue");
        }
        else {
            $(this).attr("style", "color:red");
        }
        $(this).html("$" + parseFloat($(this).html()));
    });

    $('.percent').each(function () {
        $(this).html($(this).html() + "%");
    });
});