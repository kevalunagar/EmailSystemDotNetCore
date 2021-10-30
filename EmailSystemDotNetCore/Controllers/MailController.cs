using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Inbox()
        {
            return View();
        }
    }
}
