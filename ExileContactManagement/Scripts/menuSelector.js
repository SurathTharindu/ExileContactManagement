$(document).ready(function () {
    $("#menu li a").click(function (e) {
        $("#menu li a").each(function () {
            $(this).removeClass("active");
        });

        $(this).addClass("active");
    });
});
