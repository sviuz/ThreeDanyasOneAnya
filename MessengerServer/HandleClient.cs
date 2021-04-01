using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MessengerServer.Database;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace MessengerServer
{
    enum EventTypeId
    {
        server_start_event = 1,
        server_shutdown_event,
        superadmin_created_event,
        admin_created_event,
        sign_in_event,
        sign_out_event,
        user_sign_up_event,
        user_report_event,
        user_ban_event,
        user_unban_event,
        moderator_report_event,
        moderator_ban_event,
        moderator_unban_event,
        admin_report_event,
        admin_ban_event,
        admin_unban_event,
        personal_info_edit_event,
        friend_add_event,
        friend_delete_event,
        role_change_event,
        type_add_event,
        format_add_event,
        media_add_event,
        media_delete_event,
        chat_create_event,
        chat_edit_event,
        chat_delete_event,
        message_send_event,
        message_edit_event,
        message_delete_event
    }
    public class HandleClient
    {
        TcpClient client;
        public HandleClient() { }
        public void Start(TcpClient socket)
        {
            client = socket;
            Thread thread = new Thread(Process);
            thread.Start();
        }
        private void Process()
        {
            bool isUp = true;
            int size = 1024 * 1024 * 1024;
            byte[] request = new byte[size];
            byte[] response = null;
            bool isWS = false;
            while(isUp)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    stream.Read(request, 0, size);
                    string request_string = Encoding.Default.GetString(request);
                    if (!isWS && Regex.IsMatch(request_string, "^GET", RegexOptions.IgnoreCase)) 
                    {
                        string swk = Regex.Match(request_string, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();
                        string swka = swk + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
                        byte[] swkaSHA1 = SHA1.Create().ComputeHash(Encoding.Default.GetBytes(swka));
                        string swkaSHA1Base64 = Convert.ToBase64String(swkaSHA1);
                        response = Encoding.Default.GetBytes(
                            "HTTP/1.1 101 Switching Protocols\r\n" +
                            "Connection: Upgrade\r\n" +
                            "Upgrade: websocket\r\n" +
                            "Sec-WebSocket-Accept: " + swkaSHA1Base64 + "\r\n\r\n");
                        isWS = true;
                        stream.Write(response, 0, response.Length);
                        stream.Flush();
                        continue;
                    }
                    if(isWS)
                    {
                        ServerMessage request_object = JsonConvert.DeserializeObject<ServerMessage>(request_string);
                        ServerMessage response_object = new ServerMessage();
                        using (MessengerDBContext ctx = new MessengerDBContext())
                        {
                            if (request_object.ActionId == (int)Actions.SignUp)
                            {
                                if (ctx.Users.Where(u => u.Username == request_object.Users.First().Username).Count() == 0
                                   && ctx.Users.Where(u => u.Email == request_object.Users.First().Email).Count() == 0)
                                {
                                    string filename = @".\..\..\..\Storage\User Images\";
                                    filename += request_object.Filename;
                                    File.WriteAllBytes(filename, request_object.File);
                                    User user = new User()
                                    {
                                        Username = request_object.Users.First().Username,
                                        PasswordHash = Salt.GetHash(Encoding.Default.GetBytes(request_object.Password)),
                                        Email = request_object.Users.First().Email,
                                        RoleId = (int)Roles.user,
                                        ImagePath = filename,
                                        IsOnline = true,
                                        IsBanned = false
                                    };
                                    ctx.Users.Add(user);
                                    ctx.Logs.Add(new Log()
                                    {
                                        EventTypeId = (int)EventTypeId.user_sign_up_event,
                                        Message = user.Username + " " + ctx.EventTypes.Where(e => e.Id == (int)EventTypeId.user_sign_up_event).First().Name,
                                        Time = DateTime.Now
                                    });
                                    ctx.SaveChanges();
                                    response_object.isOk = true;
                                    response_object.Users.Add(user);
                                }
                                else
                                {
                                    response_object.isOk = false;
                                }
                            }
                            else if (request_object.ActionId == (int)Actions.SignIn)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.SignOut)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.GetPersonalInfo)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.SetPersonalInfo)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.GetUsersList)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.AddFriend)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.DelFriend)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.GetFriendsList)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.SendReport)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.GetReportsList)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.SetModerator)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.Ban)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.Unban)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.GetChatsList)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.AddChat)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.EditChat)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.DelChat)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.GetMessages)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.AddMessage)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.EditMessage)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.DelMessage)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.AddFormat)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.CreateAdmin)
                            {
                                ;
                            }
                            else if (request_object.ActionId == (int)Actions.GetLogs)
                            {
                                ;
                            }
                            else
                            {
                                response_object.isOk = false;
                            }
                            string response_string = JsonConvert.SerializeObject(response_object);
                            response = Encoding.Default.GetBytes(response_string);
                            stream.Write(response, 0, response.Length);
                            stream.Flush();
                        }
                    }
                }
                catch(Exception ex){ }
            }
        }
    }
}
