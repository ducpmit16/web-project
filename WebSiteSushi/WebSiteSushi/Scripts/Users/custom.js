$(document).ready(function ($) {
    // get the element with id="defaultOpen" and click on it
    document.getElementById("defaultOpen").click();
    document.getElementById("defaultOpenz").click();

    $('a.scrollTo').click(function () {
        openPage(event, "page-nav");
        $('body').scrollTo($(this).attr('href'), 1000);
        return false;
    });

    $('#back-to-top').click(function () {
        $("html, body").animate({ scrollTop: 0 }, 1000);
        return false;
    });

    if (screen.width < 680) {
        $('.res-screen').css("display", "none");
    }

});

function openPage(evt, pageName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(pageName).style.display = "block";
    evt.currentTarget.className += " active";
}

function openMenu(evt, menuName) {
    var i, tabcontentz, tablinksz;
    tabcontentz = document.getElementsByClassName("tabcontentz");
    for (i = 0; i < tabcontentz.length; i++) {
        tabcontentz[i].style.display = "none";
    }
    tablinksz = document.getElementsByClassName("tablinksz");
    for (i = 0; i < tablinksz.length; i++) {
        tablinksz[i].className = tablinksz[i].className.replace(" active", "");
    }
    document.getElementById(menuName).style.display = "block";
    evt.currentTarget.className += " active";
}

window.onscroll = function () { Scroll() };
function Scroll() {
    if ($(window).scrollTop() > 3000) {
        $('div#back-to-top').show("slow");
    }
    else {
        $('div#back-to-top').hide("slow");
    }

}
