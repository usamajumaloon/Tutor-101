using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tutor101.Common.Utility;

namespace Tutor101.Data.Entities
{
    [Table("Users")]
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public string CreatedById { get; set; }
        public DateTime? EditedOn { get; set; }
        public string EditedById { get; set; }
        public Enums.RecordState RecordState { get; set; }

        public User()
        {
            RecordState = Enums.RecordState.Active;
            CreatedOn = DateTime.UtcNow;
            EditedOn = DateTime.UtcNow;
        }
    }
}