
var connUrl = "https://localhost:44328/signalr";
var hubProxy = null;

function main() {
    $(function () {
        console.log("Setting up Control Panel")
        setup();
    })
}

function setup() {
    $.connection.hub.url = connUrl;
    hubProxy = $.connection.MainHub;
    hubProxy.client.OnPing = function (data) {
        // console.log(data);
        // console.log("Ping Recieved!");
    }
    $.connection.hub.start().done(function () {
        console.log("Connection is ok!");
        refresh();
        setInterval(refresh, 5000);
    });
}

function startServer() {
    console.log("Starting server");
    hubProxy.server.adminStartGameServer(getServerIdFromHtml()).done(function () {
        
    });
}

function getServerIdFromHtml() {
    var value = document.getElementById("server_id_input").value;
    return value;
}

function refresh() {

    if (hubProxy == null) {
        console.log("Hub is null. Not refreshing");
        return;
    }

    // console.log("Refreshing");

    hubProxy.server.requestPing().done(function (value) {
        // console.log("Value: " + value);
    });
}

main()




