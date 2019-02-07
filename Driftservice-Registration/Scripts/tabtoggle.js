function toggleTab(evt, tabName) {
    document.getElementById("searchBox").style.display = "block";
    document.getElementById("createBtn").style.display = "none";
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

function tabTools() {
    document.getElementById("searchBox").style.display = "none";
    document.getElementById("createBtn").style.display = "block";
}