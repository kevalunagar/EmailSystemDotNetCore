using EmailSystemDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Controllers
{
    public class MailBaseController : Controller
    {
        private readonly AppDbContext appDbContext;

        public MailBaseController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    
                    var user = appDbContext.Users.SingleOrDefault(u => u.UserName == username);
                    string fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                    ViewData.Add("FullName", fullName);
                    ViewData.Add("imagePath", user.ImagePath);
                    ViewData.Add("email", user.Email);
                }
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
