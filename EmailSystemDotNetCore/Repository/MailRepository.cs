using EmailSystemDotNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Repository
{
    public class MailRepository : IMailRepository
    {
        private readonly AppDbContext appDbContext;

        public MailRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public void updateMail(Mail mail)
        {
            appDbContext.Mails.Update(mail);
            appDbContext.SaveChanges();
        }

        public void deleteMail(string id)
        {
            appDbContext.Mails.Remove(getMail(id));
            appDbContext.SaveChanges();
        }

        public Mail getMail(string id)
        {
            return appDbContext.Mails.Where(m => m.Id == id)
                .Include(m => m.SenderUserModel)
                .Include(m=>m.ReceiverUserModel)
                .FirstOrDefault();
        }

        public IEnumerable<ReplyMail> getReplyMail(string id)
        {
            return appDbContext.ReplyMails
                .Where(r => r.mailId == id)
                .Include(r => r.SenderUserModel);
        }
        public ReplyMail getOneReplyMail(string id)
        {
            return appDbContext.ReplyMails
                .Where(r => r.Id == id)
                .Include(r => r.SenderUserModel)
                .Include(r=>r.ReceiverUserModel)
                .Include(r=>r.mail)
                .FirstOrDefault();
        }

        public IEnumerable<Mail> getReceiveMails(string email)
        {
            return appDbContext.Mails
                .OrderByDescending(m => m.Date)
                .Where(m => m.ReceiverUserModel.Email == email)
                .Include(m => m.SenderUserModel);
        }

        public IEnumerable<Mail> getSentMails(string email)
        {
            return appDbContext.Mails
                .OrderByDescending(m => m.Date)
                .Where(m => m.SenderUserModel.Email == email)
                .Include(m => m.ReceiverUserModel);
        }

        public void markAsReadMail(string id)
        {
            Mail mail = getMail(id);
            mail.MarkAsRead = !mail.MarkAsRead;
            updateMail(mail);
        }

        public void starredMail(string id)
        {
            Mail mail = getMail(id);
            mail.starred = !mail.starred;
            updateMail(mail);
        }

        public IEnumerable<Mail> getStarredMails(string email)
        {
            return appDbContext.Mails.OrderByDescending(m => m.Date)
                .Where(m => ((m.SenderUserModel.Email == email || m.ReceiverUserModel.Email == email) && m.starred == true))
                .Include(m => m.SenderUserModel);
        }
    }
}
