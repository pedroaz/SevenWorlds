
var connUrl = "https://localhost:44328/signalr";
var hub = null;


function main() {
    console.log("Setting up Control Panel")
    setup();
    refresh();
    setInterval(refresh, 5000);
}

function setup() {
    $.connection.hub.url = connUrl;
    hub = $.connection.MainHub;
}

function refresh() {

    if (hub == null) {
        console.log("Hub is null. Not refreshing");
        return;
    }


    console.log("Refreshing");
    
    
}

main()




