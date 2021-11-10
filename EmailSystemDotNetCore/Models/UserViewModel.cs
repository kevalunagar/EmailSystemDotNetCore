using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage ="firstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="lastName is required")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string imagePath { get; set; }

        [Display(Name = "Profile picture")]
        public IFormFile ImageFile { get; set; }
    }
}
