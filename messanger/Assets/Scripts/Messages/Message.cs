using System;

namespace Messages {
    public class Message {
        public string id;
        public string message;
        public string ReceiverId;//id получателя сообщения
        public string SenderId;//id отправителя чата
        private DateTime SendingDate;//полное время отправления

        public string GetMessageDate() => SendingDate.Date.ToShortDateString();
        public string GetMessageTime() => SendingDate.ToShortTimeString();
    }
}