using System.Net;
using System.Net.Sockets;
using System.Text;

using var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

var host = IPAddress.Parse("127.0.0.1");
var port = 7888;
var endpoint = new IPEndPoint(host, port);

socket.Bind(endpoint);

socket.Listen(10);

Console.WriteLine($"Listening on port {port}...");

while (true)
{
    // Accept incoming connection
    Socket clientSocket = socket.Accept();

    // Handle the accepted socket
    HandleClient(clientSocket);
}

void HandleClient(Socket clientSocket)
{
    // Handle the connection (you can add your logic here)
    // For example, read data from the client
    byte[] buffer = new byte[1024];
    int bytesRead = clientSocket.Receive(buffer);
    string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
    Console.WriteLine($"Received data: {receivedData}");

    string[] lines = receivedData.Split("\r\n"); // Split all lines by new line

    string[] requestLine = lines[0].Split(' '); // Split the first line by space

    string method = requestLine[0]; // GET, POST, PUT, DELETE, etc.
    string path = requestLine[1]; // The path of the request like /index.html
    string httpVersion = requestLine[2]; // HTTP version like HTTP/1.1

    if (method == "GET")
    {
        // ...
        // handle the request based on the method and path 
        // by using backend application server logic
        // ...
    }

    // Send a simple HTTP response back to the client
    string response = "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\nHello, World!";
    byte[] responseBytes = Encoding.ASCII.GetBytes(response);
    clientSocket.Send(responseBytes);

    // Close the client socket
    clientSocket.Close();
}