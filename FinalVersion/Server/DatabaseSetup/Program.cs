using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseSetup.Database;
using System.Security.Cryptography;
using System.IO;

namespace DatabaseSetup
{
    public class Program
    {
        public static byte[] Salt { get; set; }
        public static byte[] GetHash(byte[] data)
        {
            byte[] result;
            using(var hmac = new HMACSHA256(Salt))
            {
                result = hmac.ComputeHash(data);
            }
            return result;
        }
        public static void Main(string[] args)
        {
            string filename = @".\..\..\..\Storage\salt";
            Salt = File.ReadAllBytes(filename);
            using (MessengerDBContext ctx = new MessengerDBContext())
            {
                if(ctx.Roles.Count() > 0)
                {
                    Console.WriteLine("Database has been already created. Press any key to exit.");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("Database hasn't been created yet.");
                    Console.WriteLine("1 - create dev version [default]");
                    Console.WriteLine("2 - create release version");
                    bool result = int.TryParse(Console.ReadLine(), out int action);
                    Console.WriteLine("Creating database...");
                    #region event types
                    Console.WriteLine("Adding event types...");
                    EventType server_start_event = new EventType() { Name = "Server Start" };
                    ctx.EventTypes.Add(server_start_event);
                    EventType server_shutdown_event = new EventType() { Name = "Server Shutdown" };
                    ctx.EventTypes.Add(server_shutdown_event);
                    EventType superadmin_created_event = new EventType() { Name = "SuperAdmin Created" };
                    ctx.EventTypes.Add(superadmin_created_event);
                    EventType admin_created_event = new EventType() { Name = "Admin Created" };
                    ctx.EventTypes.Add(admin_created_event);
                    EventType sign_in_event = new EventType() { Name = "Sign In" };
                    ctx.EventTypes.Add(sign_in_event);
                    EventType sign_out_event = new EventType() { Name = "Sign Out" };
                    ctx.EventTypes.Add(sign_out_event);
                    EventType user_sign_up_event = new EventType() { Name = "User Sign Up" };
                    ctx.EventTypes.Add(user_sign_up_event);
                    EventType user_report_event = new EventType() { Name = "User Report" };
                    ctx.EventTypes.Add(user_report_event);
                    EventType user_ban_event = new EventType() { Name = "User Ban" };
                    ctx.EventTypes.Add(user_ban_event);
                    EventType user_unban_event = new EventType() { Name = "User Unban" };
                    ctx.EventTypes.Add(user_unban_event);
                    EventType moderator_report_event = new EventType() { Name = "Moderator Report" };
                    ctx.EventTypes.Add(moderator_report_event);
                    EventType moderator_ban_event = new EventType() { Name = "Moderator Ban" };
                    ctx.EventTypes.Add(moderator_ban_event);
                    EventType moderator_unban_event = new EventType() { Name = "Moderator Unban" };
                    ctx.EventTypes.Add(moderator_unban_event);
                    EventType admin_report_event = new EventType() { Name = "Admin Report" };
                    ctx.EventTypes.Add(admin_report_event);
                    EventType admin_ban_event = new EventType() { Name = "Admin Ban" };
                    ctx.EventTypes.Add(admin_ban_event);
                    EventType admin_unban_event = new EventType() { Name = "Admin Unban" };
                    ctx.EventTypes.Add(admin_unban_event);
                    EventType personal_info_edit_event = new EventType() { Name = "Personal Info Edit" };
                    ctx.EventTypes.Add(personal_info_edit_event);
                    EventType friend_add_event = new EventType() { Name = "Friend Add" };
                    ctx.EventTypes.Add(friend_add_event);
                    EventType friend_delete_event = new EventType() { Name = "Friend Delete" };
                    ctx.EventTypes.Add(friend_delete_event);
                    EventType role_change_event = new EventType() { Name = "Role Change" };
                    ctx.EventTypes.Add(role_change_event);
                    EventType type_add_event = new EventType() { Name = "Type Add" };
                    ctx.EventTypes.Add(type_add_event);
                    EventType format_add_event = new EventType() { Name = "Format Add" };
                    ctx.EventTypes.Add(format_add_event);
                    EventType media_add_event = new EventType() { Name = "Media Add" };
                    ctx.EventTypes.Add(media_add_event);
                    EventType media_delete_event = new EventType() { Name = "Media Delete" };
                    ctx.EventTypes.Add(media_delete_event);
                    EventType chat_create_event = new EventType() { Name = "Chat Create" };
                    ctx.EventTypes.Add(chat_create_event);
                    EventType chat_edit_event = new EventType() { Name = "Chat Edit" };
                    ctx.EventTypes.Add(chat_edit_event);
                    EventType chat_delete_event = new EventType() { Name = "Chat Delete" };
                    ctx.EventTypes.Add(chat_delete_event);
                    EventType message_send_event = new EventType() { Name = "Message Send" };
                    ctx.EventTypes.Add(message_send_event);
                    EventType message_edit_event = new EventType() { Name = "Message Edit" };
                    ctx.EventTypes.Add(message_edit_event);
                    EventType message_delete_event = new EventType() { Name = "Message Delete" };
                    ctx.EventTypes.Add(message_delete_event);
                    ctx.SaveChanges();
                    Console.WriteLine("Event types added");
                    #endregion
                    #region roles
                    Console.WriteLine("Adding roles...");
                    Role superadmin_role = new Role() { Name = "SuperAdmin" };
                    Role admin_role = new Role() { Name = "Admin" };
                    Role moderator_role = new Role() { Name = "Moderator" };
                    Role user_role = new Role() { Name = "User" };
                    ctx.Roles.Add(superadmin_role);
                    ctx.Roles.Add(admin_role);
                    ctx.Roles.Add(moderator_role);
                    ctx.Roles.Add(user_role);
                    ctx.SaveChanges();
                    Console.WriteLine("Roles have been successfully added.");
                    #endregion
                    #region superadmin
                    Console.WriteLine("Adding superadmin...");
                    User superadmin = new User()
                    {
                        Username = "superadmin",
                        PasswordHash = GetHash(Encoding.Default.GetBytes("me5_b3MS_0xLWnd")),
                        Email = "superadmin@3d1a.com",
                        Role = superadmin_role,
                        ImagePath = @".\..\..\..\Storage\User Images\superadmin.jpg",
                        IsOnline = true,
                        IsBanned = false
                    };
                    ctx.Users.Add(superadmin);
                    ctx.Logs.Add(new Log()
                    { 
                        EventType = superadmin_created_event,
                        Message = superadmin_created_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    superadmin.IsOnline = true;
                    ctx.Logs.Add(new Log()
                    {
                        EventType = server_start_event,
                        Message = server_start_event.Name,
                        Time = DateTime.Now
                    });
                    ctx.SaveChanges();
                    Console.WriteLine("Superadmin has been successfully added.");
                    #endregion
                    #region types
                    Console.WriteLine("Adding media types...");
                    Database.Type image_type = new Database.Type() { Name = "Image" };
                    ctx.Types.Add(image_type);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = type_add_event,
                        Message = image_type.Name + " " + type_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Database.Type audio_type = new Database.Type() { Name = "Audio" };
                    ctx.Types.Add(audio_type);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = type_add_event,
                        Message = audio_type.Name + " " + type_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Database.Type video_type = new Database.Type() { Name = "Video" };
                    ctx.Types.Add(video_type);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = type_add_event,
                        Message = video_type.Name + " " + type_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Database.Type file_type = new Database.Type() { Name = "File" };
                    ctx.Types.Add(file_type);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = type_add_event,
                        Message = file_type.Name + " " + type_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    ctx.SaveChanges();
                    Console.WriteLine("Media types have been successfully added.");
                    #endregion
                    #region formats
                    Console.WriteLine("Adding media formats...");
                    Format jpeg_format = new Format() { Name = "JPEG", Extension = ".jpg.jpeg.jpe.jif.jfif.jfi", Type = image_type };
                    ctx.Formats.Add(jpeg_format);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = format_add_event,
                        Message = jpeg_format.Name + " " + format_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Format png_format = new Format() { Name = "PNG", Extension = ".png", Type = image_type };
                    ctx.Formats.Add(png_format);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = format_add_event,
                        Message = png_format.Name + " " + format_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Format bmp_format = new Format() { Name = "BMP", Extension = ".bmp.dib", Type = image_type };
                    ctx.Formats.Add(bmp_format);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = format_add_event,
                        Message = bmp_format.Name + " " + format_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Format gif_format = new Format() { Name = "GIF", Extension = ".gif", Type = image_type };
                    ctx.Formats.Add(gif_format);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = format_add_event,
                        Message = gif_format.Name + " " + format_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Format tiff_format = new Format() { Name = "TIFF", Extension = ".tiff.tif", Type = image_type };
                    ctx.Formats.Add(tiff_format);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = format_add_event,
                        Message = tiff_format.Name + " " + format_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Format webp_format = new Format() { Name = "WebP", Extension = ".webp", Type = image_type };
                    ctx.Formats.Add(webp_format);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = format_add_event,
                        Message = webp_format.Name + " " + format_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Format svg_format = new Format() { Name = "SVG", Extension = ".svg", Type = image_type };
                    ctx.Formats.Add(svg_format);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = format_add_event,
                        Message = svg_format.Name + " " + format_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Format mp3_format = new Format() { Name = "MP3", Extension = ".mp3", Type = audio_type };
                    ctx.Formats.Add(mp3_format);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = format_add_event,
                        Message = mp3_format.Name + " " + format_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Format wma_format = new Format() { Name = "WMA", Extension = ".wma", Type = audio_type };
                    ctx.Formats.Add(wma_format);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = format_add_event,
                        Message = wma_format.Name + " " + format_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Format flac_format = new Format() { Name = "FLAC", Extension = ".flac", Type = audio_type };
                    ctx.Formats.Add(flac_format);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = format_add_event,
                        Message = flac_format.Name + " " + format_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    Format mp4_format = new Format() { Name = "MP4", Extension = ".mp4", Type = video_type };
                    ctx.Formats.Add(mp4_format);
                    ctx.Logs.Add(new Log()
                    {
                        EventType = format_add_event,
                        Message = mp4_format.Name + " " + format_add_event.Name + " by " + superadmin.Username,
                        Time = DateTime.Now
                    });
                    ctx.SaveChanges();
                    Console.WriteLine("Media formats have been successfully added.");
                    #endregion
                    #region dev version
                    if (!(result && action == 2))
                    {
                        #region admins
                        Console.WriteLine("Adding admins...");
                        User admin_lao = new User()
                        {
                            Username = "lao",
                            PasswordHash = GetHash(Encoding.Default.GetBytes("nsk30_fwTun3_n989x")),
                            Email = "lao@3d1a.com",
                            Role = admin_role,
                            ImagePath = @".\..\..\..\Storage\User Images\lao.jpg",
                            IsOnline = false,
                            IsBanned = false
                        };
                        ctx.Users.Add(admin_lao);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = admin_created_event,
                            Message = admin_lao.Username + " " + admin_created_event.Name + " by " + superadmin.Username,
                            Time = DateTime.Now
                        });
                        admin_lao.IsOnline = true;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_in_event,
                            Message = admin_lao.Username + " " + sign_in_event.Name,
                            Time = DateTime.Now
                        });
                        User admin_lise = new User()
                        {
                            Username = "lise",
                            PasswordHash = GetHash(Encoding.Default.GetBytes("_n3bj9eSRjnk3_74tus")),
                            Email = "lise@3d1a.com",
                            Role = admin_role,
                            ImagePath = @".\..\..\..\Storage\User Images\lise.jpg",
                            IsOnline = false,
                            IsBanned = false
                        };
                        ctx.Users.Add(admin_lise);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = admin_created_event,
                            Message = admin_lise.Username + " " + admin_created_event.Name + " by " + superadmin.Username,
                            Time = DateTime.Now
                        });
                        admin_lise.IsOnline = true;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_in_event,
                            Message = admin_lise.Username + " " + sign_in_event.Name,
                            Time = DateTime.Now
                        });
                        User admin_nuri = new User()
                        {
                            Username = "nuri",
                            PasswordHash = GetHash(Encoding.Default.GetBytes("nHGvhe7_nbkje_v4Rujkl")),
                            Email = "nuri@3d1a.com",
                            Role = admin_role,
                            ImagePath = @".\..\..\..\Storage\User Images\nuri.jpg",
                            IsOnline = false,
                            IsBanned = false
                        };
                        ctx.Users.Add(admin_nuri);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = admin_created_event,
                            Message = admin_nuri.Username + " " + admin_created_event.Name + " by " + superadmin.Username,
                            Time = DateTime.Now
                        });
                        admin_nuri.IsOnline = true;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_in_event,
                            Message = admin_nuri.Username + " " + sign_in_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        Console.WriteLine("Admins have been successfully added.");
                        #endregion
                        #region configuring admins
                        Console.WriteLine("Configuring admins...");
                        superadmin.Friends.Add(admin_lao);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = friend_add_event,
                            Message = superadmin.Username + " " + friend_add_event.Name + " " + admin_lao.Username,
                            Time = DateTime.Now
                        });
                        superadmin.Friends.Add(admin_lise);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = friend_add_event,
                            Message = superadmin.Username + " " + friend_add_event.Name + " " + admin_lise.Username,
                            Time = DateTime.Now
                        });
                        superadmin.Friends.Add(admin_nuri);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = friend_add_event,
                            Message = superadmin.Username + " " + friend_add_event.Name + " " + admin_nuri.Username,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        admin_lao.Friends.Add(superadmin);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = friend_add_event,
                            Message = admin_lao.Username + " " + friend_add_event.Name + " " + superadmin.Username,
                            Time = DateTime.Now
                        });
                        admin_lao.Friends.Add(admin_lise);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = friend_add_event,
                            Message = admin_lao.Username + " " + friend_add_event.Name + " " + admin_lise.Username,
                            Time = DateTime.Now
                        });
                        admin_lao.Friends.Add(admin_nuri);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = friend_add_event,
                            Message = admin_lao.Username + " " + friend_add_event.Name + " " + admin_nuri.Username,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        admin_lise.Friends.Add(superadmin);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = friend_add_event,
                            Message = admin_lise.Username + " " + friend_add_event.Name + " " + superadmin.Username,
                            Time = DateTime.Now
                        });
                        admin_lise.Friends.Add(admin_lao);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = friend_add_event,
                            Message = admin_lise.Username + " " + friend_add_event.Name + " " + admin_lao.Username,
                            Time = DateTime.Now
                        });
                        admin_lise.Friends.Add(admin_nuri);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = friend_add_event,
                            Message = admin_lise.Username + " " + friend_add_event.Name + " " + admin_nuri.Username,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        admin_nuri.Friends.Add(superadmin);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = friend_add_event,
                            Message = admin_nuri.Username + " " + friend_add_event.Name + " " + superadmin.Username,
                            Time = DateTime.Now
                        });
                        admin_nuri.Friends.Add(admin_lao);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = friend_add_event,
                            Message = admin_nuri.Username + " " + friend_add_event.Name + " " + admin_lao.Username,
                            Time = DateTime.Now
                        });
                        admin_nuri.Friends.Add(admin_lise);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = friend_add_event,
                            Message = admin_nuri.Username + " " + friend_add_event.Name + " " + admin_lise.Username,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        Chat admin_chat = new Chat()
                        {
                            Name = "Admins' chat",
                            ImagePath = @".\..\..\..\Storage\Chats\Images\1.jpg",
                        };
                        admin_chat.Users.Add(superadmin);
                        admin_chat.Users.Add(admin_lao);
                        admin_lao.Chats.Add(admin_chat);
                        admin_chat.Users.Add(admin_lise);
                        admin_chat.Users.Add(admin_nuri);
                        ctx.Chats.Add(admin_chat);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = chat_create_event,
                            Message = admin_chat.Name + " " + chat_create_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        admin_chat.Messages.Add(new Message()
                        {
                            Chat = admin_chat,
                            Text = "Welcome to the admin chat, friends! This project, led by 'Three Danyas One Anya' team," +
                            " is finally presented to the public. We're the admins here. To tell the truth, there's not much we're supposed to do:" +
                            "check logs, view online statistics, add some media types etc. Our primary goal is to be the local police. Thankfully, we aren't " +
                            "going to handle all conflicts on the platform. Moderators are chosen among usual users to keep them in order. But, if some " +
                            "moderator starts acting inappropriately, we will ban their account after receving report from other moderator. You, as admins, can " +
                            "also report each other for rule violation. And your fate will be decided by me (^_~)",
                            User = superadmin,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = superadmin.Username + " " + message_send_event.Name + " " + admin_chat.Name,
                            Time = DateTime.Now
                        });
                        admin_chat.Messages.Add(new Message()
                        {
                            Chat = admin_chat,
                            Text = "Hi, everyone! I guess that's really cool messenger. Can't wait to get started with my tasks. Are we going to discuss here " +
                            "only work-related topics, or can we be more informal?",
                            User = admin_lao,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = admin_lao.Username + " " + message_send_event.Name + " " + admin_chat.Name,
                            Time = DateTime.Now
                        });
                        admin_chat.Messages.Add(new Message()
                        {
                            Chat = admin_chat,
                            Text = "Hi there! Guess we shouldn't flood this chat with unrelated topics. So, basically, our work starts when first users register here?",
                            User = admin_lise,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = admin_lise.Username + " " + message_send_event.Name + " " + admin_chat.Name,
                            Time = DateTime.Now
                        });
                        admin_chat.Messages.Add(new Message()
                        {
                            Chat = admin_chat,
                            Text = "Hi! Pleased to start working there (^ ^) I don't fully understand, how we're supposed to communicate with usual users and moderators " +
                            "as long as we're not seen to any non-admins here. At least, we can ban some unruly moderator after receiving report, but how moderators are assigned, in the first place?",
                            User = admin_nuri,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = admin_nuri.Username + " " + message_send_event.Name + " " + admin_chat.Name,
                            Time = DateTime.Now
                        });
                        admin_chat.Messages.Add(new Message()
                        {
                            Chat = admin_chat,
                            Text = "Lise is right, we shouldn't flood this chat. I'm gonna create another one for more informal purposes *^_^* And yes, our " +
                            "work only starts when somebody begin do something here. Moderators can contact any of us via email and then get this role, if any of us " +
                            "is willing to do so. Guess, they shoul be persistive if they want some power~",
                            User = superadmin,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = superadmin.Username + " " + message_send_event.Name + " " + admin_chat.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        Chat b_admin_chat = new Chat()
                        {
                            Name = "/b/admins/",
                            ImagePath = @".\..\..\..\Storage\Chats\Images\2.jpg",
                        };
                        b_admin_chat.Users.Add(superadmin);
                        ctx.Chats.Add(b_admin_chat);
                        admin_lao.Chats.Add(b_admin_chat);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = chat_create_event,
                            Message = b_admin_chat.Name + " " + chat_create_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        admin_chat.Messages.Add(new Message()
                        {
                            Chat = admin_chat,
                            Text = "Recently created new chat for us - /b/admins/. See you there!",
                            User = superadmin,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = superadmin.Username + " " + message_send_event.Name + " " + admin_chat.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        b_admin_chat.Users.Add(admin_lao);
                        b_admin_chat.Users.Add(admin_lise);
                        b_admin_chat.Users.Add(admin_nuri);
                        ctx.SaveChanges();
                        b_admin_chat.Messages.Add(new Message()
                        {
                            Chat = b_admin_chat,
                            Text = "Here some free sheech can be tolerated, I guess",
                            Media = new Media()
                            {
                                Type = image_type,
                                Path = @".\..\..\..\Storage\Chats\Media\Images\1.jfif"
                            },
                            User = superadmin,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = superadmin.Username + " " + message_send_event.Name + " " + b_admin_chat.Name,
                            Time = DateTime.Now
                        });
                        b_admin_chat.Messages.Add(new Message()
                        {
                            Chat = b_admin_chat,
                            Text = "Fine! Any Kevin MacLeod fans there?",
                            Media = new Media()
                            {
                                Type = audio_type,
                                Path = @".\..\..\..\Storage\Chats\Media\Audio\1.mp3"
                            },
                            User = admin_lise,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = admin_lise.Username + " " + message_send_event.Name + " " + b_admin_chat.Name,
                            Time = DateTime.Now
                        });
                        b_admin_chat.Messages.Add(new Message()
                        {
                            Chat = b_admin_chat,
                            Text = "Yup! Also just chill vibes video~",
                            Media = new Media()
                            {
                                Type = video_type,
                                Path = @".\..\..\..\Storage\Chats\Media\Video\1.mp4"
                            },
                            User = admin_lao,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = admin_lao.Username + " " + message_send_event.Name + " " + b_admin_chat.Name,
                            Time = DateTime.Now
                        });
                        b_admin_chat.Messages.Add(new Message()
                        {
                            Chat = b_admin_chat,
                            Text = "Cool stuff there. Can anyone pls help me with some linux script? It did something with my computer, not sure what happened...",
                            Media = new Media()
                            {
                                Type = file_type,
                                Path = @".\..\..\..\Storage\Chats\Media\Files\1.sh"
                            },
                            User = admin_nuri,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = admin_nuri.Username + " " + message_send_event.Name + " " + b_admin_chat.Name,
                            Time = DateTime.Now
                        });
                        b_admin_chat.Messages.Add(new Message()
                        {
                            Chat = b_admin_chat,
                            Text = "Wow, how funny, Mr. First-To-Be-Banned-For-Malware",
                            User = admin_lao,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = admin_lao.Username + " " + message_send_event.Name + " " + b_admin_chat.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        ctx.Reports.Add(new Report()
                        {
                            Initiator = admin_lao,
                            ReportedUser = admin_nuri,
                            Reason = "Middle-school trolling with rm -rf",
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = admin_report_event,
                            Message = admin_lao.Username + " " + admin_report_event.Name + " " + admin_nuri.Username,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        admin_nuri.IsBanned = true;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = admin_ban_event,
                            Message = superadmin.Username + " " + admin_ban_event.Name + " " + admin_nuri.Username,
                            Time = DateTime.Now
                        });
                        admin_nuri.IsOnline = false;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_out_event,
                            Message = admin_nuri.Username + " " + sign_out_event.Name,
                            Time = DateTime.Now
                        });
                        b_admin_chat.Messages.Add(new Message()
                        {
                            Chat = b_admin_chat,
                            Text = "So... I didn't expect it will happen so quickly. Still, bye, nuri!",
                            User = superadmin,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = superadmin.Username + " " + message_send_event.Name + " " + b_admin_chat.Name,
                            Time = DateTime.Now
                        });
                        b_admin_chat.Messages.Add(new Message()
                        {
                            Chat = b_admin_chat,
                            Text = "Huh, nuri cannot read this anymore. Can he came back some time after?",
                            User = admin_lise,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = admin_lise.Username + " " + message_send_event.Name + " " + b_admin_chat.Name,
                            Time = DateTime.Now
                        });
                        b_admin_chat.Messages.Add(new Message()
                        {
                            Chat = b_admin_chat,
                            Text = "Yeah, if he emails me and can make some points",
                            User = superadmin,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = superadmin.Username + " " + message_send_event.Name + " " + b_admin_chat.Name,
                            Time = DateTime.Now
                        });
                        b_admin_chat.Messages.Add(new Message()
                        {
                            Chat = b_admin_chat,
                            Text = "But who cares...",
                            User = admin_lao,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = admin_lao.Username + " " + message_send_event.Name + " " + b_admin_chat.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        Console.WriteLine("Admins have been configured.");
                        #endregion
                        #region configuring moderators...
                        Console.WriteLine("Configuring moderators...");
                        User moderator_libri = new User()
                        {
                            Username = "libri",
                            PasswordHash = GetHash(Encoding.Default.GetBytes("_h4k_h4rKJBf3_n4")),
                            Email = "libri@3d1a.com",
                            Role = user_role,
                            ImagePath = @".\..\..\..\Storage\User Images\libri.jpg",
                            IsOnline = true,
                            IsBanned = false
                        };
                        ctx.Users.Add(moderator_libri);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = user_sign_up_event,
                            Message = moderator_libri.Username + " " + user_sign_up_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        moderator_libri.IsOnline = false;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_out_event, 
                            Message = moderator_libri.Username + " " + sign_out_event.Name,
                            Time = DateTime.Now
                        });
                        moderator_libri.Role = moderator_role;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = role_change_event,
                            Message = moderator_libri.Username + " " + role_change_event.Name + " " + moderator_libri.Role.Name,
                            Time = DateTime.Now
                        });
                        moderator_libri.IsOnline = true;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_in_event,
                            Message = moderator_libri.Username + " " + sign_in_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        User moderator_gull = new User()
                        {
                            Username = "gull",
                            PasswordHash = GetHash(Encoding.Default.GetBytes("nbjkVHje_6784b_hTDw")),
                            Email = "gull@3d1a.com",
                            Role = user_role,
                            ImagePath = @".\..\..\..\Storage\User Images\gull.jpg",
                            IsOnline = true,
                            IsBanned = false
                        };
                        ctx.Users.Add(moderator_gull);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = user_sign_up_event,
                            Message = moderator_gull.Username + " " + user_sign_up_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        moderator_gull.IsOnline = false;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_out_event,
                            Message = moderator_gull.Username + " " + sign_out_event.Name,
                            Time = DateTime.Now
                        });
                        moderator_gull.Role = moderator_role;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = role_change_event,
                            Message = moderator_gull.Username + " " + role_change_event.Name + " " + moderator_gull.Role.Name,
                            Time = DateTime.Now
                        });
                        moderator_gull.IsOnline = true;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_in_event,
                            Message = moderator_gull.Username + " " + sign_in_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        Chat moderator_chat = new Chat()
                        {
                            Name = "Moderators' chat",
                            ImagePath = @".\..\..\..\Storage\Chats\Images\3.jpg"
                        };
                        ctx.Chats.Add(moderator_chat);
                        moderator_chat.Users.Add(moderator_libri);
                        moderator_chat.Users.Add(moderator_gull);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = chat_create_event,
                            Message = moderator_chat.Name + " " + chat_create_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        moderator_chat.Messages.Add(new Message()
                        {
                            Chat = moderator_chat,
                            Text = "Hi! Wanna talk about NFT?",
                            Media = new Media()
                            {
                                Type = image_type, 
                                Path = @".\..\..\..\Storage\Chats\Media\Images\2.webp"
                            },
                            User = moderator_gull,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = moderator_gull.Username + " " + message_send_event.Name + " " + moderator_chat.Name,
                            Time = DateTime.Now
                        });
                        moderator_chat.Messages.Add(new Message()
                        {
                            Chat = moderator_chat,
                            Text = "Maybe you wanna get blocked instead?",
                            User = moderator_libri,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = moderator_libri.Username + " " + message_send_event.Name + " " + moderator_chat.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        ctx.Reports.Add(new Report()
                        {
                            Initiator = moderator_libri,
                            ReportedUser = moderator_gull,
                            Reason = "NFT spamming",
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = moderator_report_event,
                            Message = moderator_libri.Username + " " + moderator_report_event.Name + " " + moderator_gull.Username,
                            Time = DateTime.Now
                        });
                        moderator_gull.IsBanned = true;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = moderator_ban_event,
                            Message = admin_lao.Username + " " + moderator_ban_event.Name + " " + moderator_gull.Username,
                            Time = DateTime.Now
                        });
                        moderator_gull.IsOnline = false;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_out_event,
                            Message = moderator_gull.Username + " " + sign_out_event.Name,
                            Time = DateTime.Now
                        });
                        moderator_chat.Messages.Add(new Message()
                        {
                            Chat = moderator_chat,
                            Text = "Bye!",
                            User = moderator_libri,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = moderator_libri.Username + " " + message_send_event.Name + " " + moderator_chat.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        Console.WriteLine("Moderators have been configured.");
                        #endregion
                        #region configuring users
                        Console.WriteLine("Configuring users...");
                        User user_kitty = new User()
                        {
                            Username = "kitty",
                            PasswordHash = GetHash(Encoding.Default.GetBytes("dmUgj4_jl3h8CGhek_m4")),
                            Email = "kitty@3d1a.com",
                            Role = user_role,
                            ImagePath = @".\..\..\..\Storage\User Images\kitty.jpg",
                            IsOnline = true,
                            IsBanned = false
                        };
                        ctx.Users.Add(user_kitty);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = user_sign_up_event,
                            Message = user_kitty.Username + " " + user_sign_up_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        User user_koi = new User()
                        {
                            Username = "koi",
                            PasswordHash = GetHash(Encoding.Default.GetBytes("bnknfk_4jhb_h48y")),
                            Email = "koi@3d1a.com",
                            Role = user_role,
                            ImagePath = @".\..\..\..\Storage\User Images\koi.jpg",
                            IsOnline = true,
                            IsBanned = false
                        };
                        ctx.Users.Add(user_koi);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = user_sign_up_event,
                            Message = user_koi.Username + " " + user_sign_up_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        User user_david = new User()
                        {
                            Username = "david",
                            PasswordHash = GetHash(Encoding.Default.GetBytes("vmbr_bkh4_hk48DGH3")),
                            Email = "david@3d1a.com",
                            Role = user_role,
                            ImagePath = @".\..\..\..\Storage\User Images\david.jpg",
                            IsOnline = true,
                            IsBanned = false
                        };
                        ctx.Users.Add(user_david);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = user_sign_up_event,
                            Message = user_david.Username + " " + user_sign_up_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        Chat user_chat = new Chat()
                        {
                            Name = "Users' chat",
                            ImagePath = @".\..\..\..\Storage\Chats\Images\4.jpg"
                        };
                        user_chat.Users.Add(user_kitty);
                        user_chat.Users.Add(user_koi);
                        user_chat.Users.Add(user_david);
                        ctx.Chats.Add(user_chat);
                        ctx.Logs.Add(new Log()
                        {
                            EventType = chat_create_event,
                            Message = user_chat.Name + " " + chat_create_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        user_chat.Messages.Add(new Message()
                        {
                            Chat = user_chat,
                            Text = "Hello, everyone!",
                            Media = new Media()
                            {
                                Type = image_type,
                                Path = @".\..\..\..\Storage\Chats\Media\Images\3.jpg"
                            },
                            User = user_kitty,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = user_kitty.Username + " " + message_send_event.Name + " " + user_chat.Name,
                            Time = DateTime.Now
                        });
                        user_chat.Messages.Add(new Message()
                        {
                            Chat = user_chat,
                            Text = "Hi!",
                            Media = new Media()
                            {
                                Type = video_type,
                                Path = @".\..\..\..\Storage\Chats\Media\Video\2.mp4"
                            },
                            User = user_koi,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = user_koi.Username + " " + message_send_event.Name + " " + user_chat.Name,
                            Time = DateTime.Now
                        });
                        user_chat.Messages.Add(new Message()
                        {
                            Chat = user_chat,
                            Text = "Hi!",
                            Media = new Media()
                            {
                                Type = audio_type,
                                Path = @".\..\..\..\Storage\Chats\Media\Audio\2.mp3"
                            },
                            User = user_david,
                            Time = DateTime.Now
                        });
                        ctx.Logs.Add(new Log()
                        {
                            EventType = message_send_event,
                            Message = user_david.Username + " " + message_send_event.Name + " " + user_chat.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        Console.WriteLine("Users have been configured.");
                        #endregion
                        #region sign out
                        user_david.IsOnline = false;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_out_event,
                            Message = user_david.Username + " " + sign_out_event.Name,
                            Time = DateTime.Now
                        });
                        user_koi.IsOnline = false;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_out_event,
                            Message = user_koi.Username + " " + sign_out_event.Name,
                            Time = DateTime.Now
                        });
                        user_kitty.IsOnline = false;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_out_event,
                            Message = user_kitty.Username + " " + sign_out_event.Name,
                            Time = DateTime.Now
                        });
                        moderator_libri.IsOnline = false;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_out_event,
                            Message = moderator_libri.Username + " " + sign_out_event.Name,
                            Time = DateTime.Now
                        });
                        admin_lise.IsOnline = false;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_out_event,
                            Message = admin_lise.Username + " " + sign_out_event.Name,
                            Time = DateTime.Now
                        });
                        admin_lao.IsOnline = false;
                        ctx.Logs.Add(new Log()
                        {
                            EventType = sign_out_event,
                            Message = admin_lao.Username + " " + sign_out_event.Name,
                            Time = DateTime.Now
                        });
                        ctx.SaveChanges();
                        #endregion
                    }
                    else
                    {
                        Console.WriteLine("Release version of database has been added.");
                    }
                    #endregion
                    superadmin.IsOnline = false;
                    ctx.Logs.Add(new Log()
                    {
                        EventType = server_shutdown_event,
                        Message = server_shutdown_event.Name,
                        Time = DateTime.Now
                    });
                    Console.WriteLine("Saving changes...");
                    ctx.SaveChanges();
                    Console.WriteLine("All changes have been successfully saved.");
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
