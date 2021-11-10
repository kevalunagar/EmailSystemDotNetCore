using EmailSystemDotNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Repository
{
    public interface IMailRepository
    {
        public IEnumerable<Mail> getReceiveMails(string email);
        public IEnumerable<Mail> getSentMails(string email);
        public Mail getMail(string id);
        public void starredMail(string id);
        public void deleteMail(string id);
        public void markAsReadMail(string id);
        public void updateMail(Mail mail);
        public IEnumerable<Mail> getStarredMails(string email);

        public IEnumerable<ReplyMail> getReplyMail(string id);
        public ReplyMail getOneReplyMail(string id);
    }
}
