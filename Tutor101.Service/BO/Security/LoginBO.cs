using System;
using System.ComponentModel.DataAnnotations;

namespace Tutor101.Service.BO.Security
{
    public class LoginBO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiration { get; set; }
    }
}
