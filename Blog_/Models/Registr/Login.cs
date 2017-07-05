using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog_.Models.Registr
{
    public class Login
    {
        [Required]
        public string LoginName { get; set; }

        [Required]
        public string Role { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}