using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MessengerServer
{
    public class Server
    {
        ThreadStatus status;
        public int Clients { get; set; } = 0;
        public Server(ref object ts) 
        {
            status = ts as ThreadStatus;
        }
        public void Run()
        {
            string ip = "127.0.0.1";
            int port = 80;
            status.Server = new TcpListener(IPAddress.Parse(ip), port);
            status.Server.Start();
            while(status.IsOn)
            {
                try
                {
                    TcpClient client = status.Server.AcceptTcpClient();
                    Clients++;
                    HandleClient handle = new HandleClient();
                    handle.Start(client);
                }
                catch (Exception ex) { }
            }
        }
    }
}
