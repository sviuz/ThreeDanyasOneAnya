using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MessengerServer
{
    public class ThreadStatus
    {
        public bool IsOn { get; set; }
        public int Clients { get; set; }
        public TcpListener Server { get; set; }
    }
}
