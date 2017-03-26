$(document).ready(function () {
    document.getElementById("defaultOpenTab").click();

    $('a.toggle').click(function () {
        $('div.sidebar-nav').toggle("slow");
        $('#main').css("margin-left", 0);
    });

    if ($(window).width() <= 960) {
        $('div.sidebar-nav').toggle("fast");
        $('#main').css("margin-left", 0);
    }

    document.getElementById("defaultOpenMenuTab").click();
});

function openTab(evt, tabName) {
    var i, tab_content, tab_links;
    tab_content = document.getElementsByClassName("tab_content");
    for (i = 0; i < tab_content.length; i++) {
        tab_content[i].style.display = "none";
    }
    tab_links = document.getElementsByClassName("tab_links");
    for (i = 0; i < tab_links.length; i++) {
        tab_links[i].className = tab_links[i].className.replace(" active", "");
    }
    document.getElementById(tabName).style.display = "block";
    evt.currentTarget.className += " active";
}

function openTabMenu(evt, tabMenuName) {
    var i, tabMenuContent, tabMenuLinks;
    tabMenuContent = document.getElementsByClassName("tabMenuContent");
    for (i = 0; i < tabMenuContent.length; i++) {
        tabMenuContent[i].style.display = "none";
    }
    tabMenuLinks = document.getElementsByClassName("tabMenuLinks");
    for (i = 0; i < tabMenuLinks.length; i++) {
        tabMenuLinks[i].className = tabMenuLinks[i].className.replace(" active", "");
    }
    document.getElementById(tabMenuName).style.display = "block";
    evt.currentTarget.className += " active";
}