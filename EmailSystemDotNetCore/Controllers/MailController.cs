using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Controllers
{
    [Authorize]
    public class MailController : Controller
    {
        public IActionResult Inbox()
        {
            return View();
        }
    }
}
