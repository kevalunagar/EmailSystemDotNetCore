using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Models
{
    public class ReplyMail
    {
        public string Id { get; set; }
        public string SenderUserModelId { get; set; }
        public UserModel SenderUserModel { get; set; }
        public string ReceiverUserModelId { get; set; }
        public UserModel ReceiverUserModel { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool MarkAsRead { get; set; }
        public string mailId { get; set; }
        public Mail mail { get; set; }
    }
}
