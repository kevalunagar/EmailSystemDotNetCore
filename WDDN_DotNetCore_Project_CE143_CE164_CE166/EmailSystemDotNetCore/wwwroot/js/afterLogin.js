$(document).ready(function () {
    $(".hamburger").click(function () {
        $(".wrapper").toggleClass("collapse-c");
    });

    var loc = window.location.href; // returns the full URL
    if (/Compose/.test(loc)) {
        $('#compose').addClass('active-btn');
    }
    else if (/Inbox/.test(loc)) {
        $('#inbox').addClass('active-btn');
    }
    else if (/Starred/.test(loc)) {
        $('#starred').addClass('active-btn');
    }
    else if(/SentMails/.test(loc)) {
        $('#sent').addClass('active-btn');
    }
})

//$(document).ready(function () {

//    $(".wrapper .sidebar-c ul li a.clicked").removeClass("clicked");
//    $(".wrapper .sidebar-c ul li a").click(function () {
//        $(".wrapper .sidebar-c ul li a").addClass("clicked");
//    });
//})