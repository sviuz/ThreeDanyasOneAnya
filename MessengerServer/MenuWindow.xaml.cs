using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessengerServer.Database;

namespace MessengerServer
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        MainWindow MainWindow_parent;
        ThreadStatus ts = new ThreadStatus() { IsOn = true, Clients = 0 };
        public MenuWindow(MainWindow parent)
        {
            InitializeComponent();
            MainWindow_parent = parent;
            StartServer();
        }
        public void StartServer()
        {
            Thread server_thread = new Thread(new ParameterizedThreadStart(Process));
            server_thread.Start(ts);
        }
        public void Process(object ts_)
        {
            Server server = new Server(ref ts_);
            server.Run();
        }
        private void Menu_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ts.IsOn = false;
            ts.Server.Stop();
            MainWindow_parent.IsEnabled = true;
            using(MessengerDBContext ctx = new MessengerDBContext())
            {
                ctx.Logs.Add(new Log()
                {
                    EventTypeId = (int)EventTypeId.server_shutdown_event,
                    Message = ctx.EventTypes.Where(et => et.Id == (int)EventTypeId.server_shutdown_event).First().Name,
                    Time = DateTime.Now
                });
                ctx.SaveChanges();
            }
        }
    }
}
