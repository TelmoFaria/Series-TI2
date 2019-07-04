$(document).ready(function () {
    var topHatHeight = $("#topHat").height();
    var trigger = $("#options");
    var sidebar = $(".sidebar_nav");

    sidebar.css('top', topHatHeight);
    trigger.click(function () {
        sidebar.toggleClass("sidebar-is-open");
    });

});