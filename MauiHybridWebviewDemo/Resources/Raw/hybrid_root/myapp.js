function SendMessageToCSharp() {
    var message = document.getElementById('messageInput').value;
    HybridWebView.SendRawMessageToDotNet(message);
}

function OpenCamera() {
    HybridWebView.SendRawMessageToDotNet("open_camera");
}

function DisplayPhoto(path) {
    path = 'data:Image/jpeg;base64,' + path;
    document.getElementById('photo').src = path;
}
