
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
        console.log("Ping Recieved!");
    }
    $.connection.hub.start().done(function () {
        console.log("Connection is ok!");
        refresh();
        setInterval(refresh, 5000);
    });
    
}



function refresh() {

    if (hubProxy == null) {
        console.log("Hub is null. Not refreshing");
        return;
    }

    console.log("Refreshing");

    hubProxy.server.requestPing().done(function (value) {
        console.log("Value: " + value);
    });



    
    
}

main()




