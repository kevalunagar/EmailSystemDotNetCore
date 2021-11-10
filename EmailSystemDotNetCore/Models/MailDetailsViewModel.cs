using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Models
{
    public class MailDetailsViewModel
    {
        public Mail mail { get; set; }
        public IEnumerable<ReplyMail> replyMails { get; set; }
    }
}
