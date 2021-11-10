using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Models
{
    public class MailViewModel
    {
        [Required(ErrorMessage ="Email required")]
        [EmailAddress]
        [Display(Name ="To")]
        public string receiverUserEmail { get; set; }
       
        public string subject { get; set; }

        [DataType(DataType.MultilineText)]
        public string description { get; set; }
    }
}
