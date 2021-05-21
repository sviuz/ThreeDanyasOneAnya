using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assets
{
    class NetworkData
    {
        private static NetworkData data;
        private Socket client;
        private IPAddress endip;
        private IPEndPoint endPoint;
        public ServerMessage Query(ServerMessage message)
        {
            endip = IPAddress.Loopback;
            client = new Socket(endip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            endPoint = new IPEndPoint(IPAddress.Loopback, 80);
            client.Connect(endPoint);
            int size = 10 * 1024 * 1024;
            byte[] request;
            string request_string = JsonConvert.SerializeObject(message);
            request = Encoding.Default.GetBytes(request_string);
            client.Send(request);
            byte[] response = new byte[size];
            client.Receive(response);
            string response_string = Encoding.Default.GetString(response);
            ServerMessage result = JsonConvert.DeserializeObject<ServerMessage>(response_string);
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            return result;
        }
    }
}
