function toggleTab(evt, tabName) {
    document.getElementById("searchBox").style.display = "block";
    document.getElementById("createContact").style.display = "block";
    document.getElementById("reloadContacts").style.display = "block";
    document.getElementById("createService").style.display = "none";
    document.getElementById("reloadServices").style.display = "none";
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(tabName).style.display = "block";
    evt.currentTarget.className += " active";
}
document.getElementById("defaultTab").click();

function tabOptions() {
    document.getElementById("searchBox").style.display = "none";
    document.getElementById("reloadContacts").style.display = "none";
    document.getElementById("reloadServices").style.display = "block";
    document.getElementById("createService").style.display = "block";
    document.getElementById("createContact").style.display = "none";
}