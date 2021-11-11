using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Models
{
    public class AppDbContext:IdentityDbContext<UserModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<ReplyMail> ReplyMails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //many to many relationships
            modelBuilder.Entity<Mail>()
                .HasOne<UserModel>(m => m.ReceiverUserModel)
                .WithMany(u => u.ReceiveMails)
                .HasForeignKey(m => m.ReceiverUserModelId);

            modelBuilder.Entity<Mail>()
                        .HasOne<UserModel>(m => m.SenderUserModel)
                        .WithMany(u => u.SentMails)
                        .HasForeignKey(m => m.SenderUserModelId);
            modelBuilder.Entity<ReplyMail>()
                .HasOne<Mail>(r => r.mail)
                .WithMany(m => m.replyMails)
                .HasForeignKey(r => r.mailId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
