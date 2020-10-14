
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
    
    $.connection.hub.start().done(function () {
        console.log("Connection is ok!");
        refresh();
        setInterval(refresh, 3000);
        //hubProxy.client.OnPing = function (data) {
            
        //}
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

function resetServer() {
    console.log("Reseting server START");
    hubProxy.server.resetUniverseFakeData().done(function () {
        console.log("Reseting server FINISHED");
    });
}

function getAllPlayerDatas() {
    hubProxy.server.requestAllPlayerDatas().done(function (playerDatas) {

        var tableRef = document.getElementById('player_data_table').getElementsByTagName('tbody')[0];

        for (var i = tableRef.rows.length-1; i >= 0; i--) {
            tableRef.deleteRow(i);
        }

        for (var i = 0; i < playerDatas.length; i++) {
            var data = playerDatas[0]; 
            var rowhtml = `
                <tr>
                    <th scope="row">${i}</th>
                        <td>${data.Username}</td>
                        <td>${data.PlayerName}</td>
                        <td>${data.Id}</td>
                    </tr>
                    `
            var newRow = tableRef.insertRow(tableRef.rows.length);
            newRow.innerHTML = rowhtml;
        }
    });
}

function refresh() {

    var connectionStatus = document.getElementById('server_status');

    var state = $.signalR.connectionState

    if (hubProxy == null) {
        connectionStatus.innerHTML = "Hub Connection: Offline";
        return;
    }

    connectionStatus.innerHTML = "Hub Connection: Online :)";

    // console.log("Refreshing");
    getAllPlayerDatas();
    hubProxy.server.requestPing().done(function (value) {
        // console.log("Value: " + value);
    });
}

main()




