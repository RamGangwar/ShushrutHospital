function GetRoute(action) {
    var directory = "";
    var host = document.location.origin;
    var url = host + (directory != "" ? "/" + directory : "") + action;
    return url;
}
function redirectPage(url) {
    var seconds = 2;
    setInterval(function () {
        seconds--;
        if (seconds == 0) {
            window.location = url;
        }
    }, 1000);
}