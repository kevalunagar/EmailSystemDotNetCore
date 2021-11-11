using EmailSystemDotNetCore.Models;
using EmailSystemDotNetCore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Controllers
{
    [Authorize]
    public class MailController : Controller
    {
        private readonly UserManager<UserModel> userManager;
        private readonly IUserRepository userRepository;
        private readonly AppDbContext appDbContext;
        private readonly IMailRepository mailRepository;
        private readonly IWebHostEnvironment hostEnvironment;

        public MailController(UserManager<UserModel> userManager
            ,IUserRepository userRepository
            ,AppDbContext appDbContext
            ,IMailRepository mailRepository
            ,IWebHostEnvironment hostEnvironment)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.appDbContext = appDbContext;
            this.mailRepository = mailRepository;
            this.hostEnvironment = hostEnvironment;
        }
        public IActionResult Inbox()
        {
            var user = userRepository.getLoggedUser(User).Result;
            IEnumerable<Mail> mails= mailRepository.getReceiveMails(user.Email);
            return View(mails);
        }

        public IActionResult starredBtnClick(string id)
        {
            mailRepository.starredMail(id);
            return RedirectToAction("Inbox", "Mail");
        }
        public IActionResult deleteBtnClick(string id)
        {
            mailRepository.deleteMail(id);
            return RedirectToAction("Inbox", "Mail");
        }

        public IActionResult markAsReadClick(string id)
        {
            mailRepository.markAsReadMail(id);
            return RedirectToAction("Inbox", "Mail");
        }

        public IActionResult Starred()
        {
            var user = userRepository.getLoggedUser(User).Result;
            IEnumerable<Mail> mails= mailRepository.getStarredMails(user.Email);
            return View(mails);
        }

        [HttpGet]
        public IActionResult Compose(string id,string btn)
        {
            if(id!=null && btn != null)
            {
                Mail mail= mailRepository.getMail(id);
                if (mail == null)
                {
                    ReplyMail replyMail= mailRepository.getOneReplyMail(id);
                    MailViewModel model1 = new MailViewModel
                    {
                        receiverUserEmail = btn == "reply" ? replyMail.SenderUserModel.Email : null,
                        Subject = btn == "reply" ? replyMail.mail.Subject : null,
                        Description = btn == "forward" ? replyMail.Description : null,
                    };
                    return View(model1);
                }

                MailViewModel model = new MailViewModel
                {
                    receiverUserEmail = btn=="reply"?mail.SenderUserModel.Email:null,
                    Subject = btn=="reply"? mail.Subject:null,
                    Description=btn=="forward"?mail.Description:null,
                };
                return View(model);
            }
            return View();
        }

        public IActionResult SentMails()
        {
            var user = userRepository.getLoggedUser(User).Result;
            IEnumerable<Mail> mails = mailRepository.getSentMails(user.Email);
            return View(mails);
        }

        [HttpPost]
        public IActionResult Compose(MailViewModel model,string id,string btn)
        {
            if (ModelState.IsValid)
            {
                if (id != null && btn.Equals("reply"))
                {
                    Mail replyOfMail = mailRepository.getMail(id);
                    if (replyOfMail == null)
                    {
                        ReplyMail replyMail1= mailRepository.getOneReplyMail(id);
                        ReplyMail replyMail2 = new ReplyMail
                        {
                            Id = Guid.NewGuid().ToString(),
                            SenderUserModel = userRepository.getLoggedUser(User).Result,
                            ReceiverUserModel = userRepository.getUserByEmail(model.receiverUserEmail).Result,
                            Date = DateTime.Now,
                            Description = model.Description,
                            MarkAsRead = false,
                            mail = replyMail1.mail
                        };
                        appDbContext.ReplyMails.Add(replyMail2);
                        appDbContext.SaveChanges();
                        return RedirectToAction("Details", "Mail", new { id = replyMail1.mail.Id });

                    }
                    ReplyMail replyMail = new ReplyMail
                    {
                        Id = Guid.NewGuid().ToString(),
                        SenderUserModel = userRepository.getLoggedUser(User).Result,
                        ReceiverUserModel = userRepository.getUserByEmail(model.receiverUserEmail).Result,
                        Date = DateTime.Now,
                        Description = model.Description,
                        MarkAsRead = false,
                        mail = replyOfMail
                    };
                    appDbContext.ReplyMails.Add(replyMail);
                    appDbContext.SaveChanges();
                    return RedirectToAction("Details", "Mail", new { id = id });
                }
                Mail mail = new Mail
                {
                    Id = Guid.NewGuid().ToString(),
                    SenderUserModel = userRepository.getLoggedUser(User).Result,
                    ReceiverUserModel = userRepository.getUserByEmail(model.receiverUserEmail).Result,
                    Date = DateTime.Now,
                    Subject = model.Subject != null ? model.Subject : "(No Subject)",
                    Description = model.Description,
                    MarkAsRead = false,
                    starred = false
                };
                appDbContext.Mails.Add(mail);
                appDbContext.SaveChanges();
                return RedirectToAction("Inbox", "Mail");
            }
            return View(model);
        }

        public IActionResult Details(string id)
        {
            
            Mail mail= mailRepository.getMail(id);
            if (userRepository.getLoggedUser(User).Result.Id == mail.ReceiverUserModel.Id)
            {
                mailRepository.markAsReadMail(id);
            }
            IEnumerable<ReplyMail> replyMails = mailRepository.getReplyMail(mail.Id);
            MailDetailsViewModel model = new MailDetailsViewModel
            {
                mail = mail,
                replyMails = replyMails
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            UserModel user= userRepository.getLoggedUser(User).Result;
            UserViewModel model = new UserViewModel
            {
                FirstName=user.FirstName,
                LastName=user.LastName,
                imagePath=user.ImagePath,
                DateOfBirth=user.DateOfBirth
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(UserViewModel model)
        {
            UserModel user = userRepository.getLoggedUser(User).Result;
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.ImageFile != null)
                {
                    string uploadsFolder= Path.Combine(hostEnvironment.WebRootPath, "Images");
                    uniqueFileName= Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filePath= Path.Combine(uploadsFolder, uniqueFileName);
                    model.ImageFile.CopyTo(new FileStream(filePath,FileMode.Create));
                }
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.ImagePath = uniqueFileName;
                user.DateOfBirth = model.DateOfBirth;

                await userManager.UpdateAsync(user);
            }
            UserViewModel model1 = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                imagePath = user.ImagePath,
                DateOfBirth = user.DateOfBirth
            };
            return View(model1);
        }
    }
}
