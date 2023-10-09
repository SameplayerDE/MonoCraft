using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main(string[] args)
    {
        TcpListener server = null;
        try
        {
            // Set the IP address and port on which the server will listen
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // Use your desired IP address
            int port = 12345; // Use your desired port number

            server = new TcpListener(ipAddress, port);

            // Start listening for incoming client connections
            server.Start();
            Console.WriteLine("Server started...");

            while (true)
            {
                // Accept a client connection
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected...");

                // Start a new thread to handle the client
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            server?.Stop();
        }
    }

    static void HandleClient(object clientObj)
    {
        TcpClient client = (TcpClient)clientObj;
        NetworkStream stream = client.GetStream();
        StreamWriter writer = new StreamWriter(stream);

        try
        {
            while (true)
            {
                // Get the current time as a string
                string currentTime = DateTime.Now.ToString("HH:mm:ss");

                // Send the current time to the client
                writer.WriteLine(currentTime);
                writer.Flush();

                // Wait for 1 second before sending the next time update
                Thread.Sleep(1000);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Client disconnected: " + ex.Message);
        }
        finally
        {
            writer.Close();
            stream.Close();
            client.Close();
        }
    }
}
