
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/notificationHub") // URL of the SignalR hub
        .build();

    // Start the connection
    connection.start().then(() => {
        console.log("SignalR connection established.");
    }).catch(err => {
        console.error("Error establishing SignalR connection: ", err);
    });

    // Function to handle incoming notifications
    connection.on("ReceiveNotification", (message) => {
        displayNotification(message);
    });

    // Function to display notifications on the web page
    function displayNotification(message) {
        const notificationElement = document.createElement("div");
        notificationElement.textContent = message;
        notificationElement.className = "notification";
        document.getElementById("notifications").appendChild(notificationElement);
    }
