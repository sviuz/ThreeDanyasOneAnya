using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Security.Cryptography;
using MessengerServer.Database;

namespace MessengerServer
{
    public enum Roles
    {
        superadmin = 1,
        admin,
        moderator,
        user
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click_Web(object sender, RoutedEventArgs e)
        {
            string uri = (sender as Hyperlink).NavigateUri.OriginalString;
            Process.Start(new ProcessStartInfo("cmd", $"/c start {uri}"));
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            bool isOk = false;
            using (MessengerDBContext ctx = new MessengerDBContext())
            {
                var superadmin = ctx.Users.Where(u => u.RoleId == (int)Roles.superadmin).ToList().Last();
                var hash = Salt.GetHash(Encoding.Default.GetBytes(tbPassword.Password));
                if (superadmin.Username == tbUsername.Text.Trim(new char[] { ' ', '\t' }) 
                    && hash.SequenceEqual(superadmin.PasswordHash))
                {
                    isOk = true;
                }
                ctx.Logs.Add(new Log()
                {
                    EventTypeId = (int)EventTypeId.server_start_event,
                    Message = ctx.EventTypes.Where(et => et.Id == (int)EventTypeId.server_start_event).First().Name,
                    Time = DateTime.Now
                });
                ctx.SaveChanges();
            }
            if(isOk)
            {
                MenuWindow form = new MenuWindow(this);
                this.IsEnabled = false;
                form.Show();
            }
        }
    }
}
