using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public Membership Membership { get; set; }

        //So, for performance matters when we dont want the whole membership object, we juste need the ID we do this .. ?
        public byte MembershipId { get; set; }

        [Min18YearsValidation]
        public DateTime? Birthdate { get; set; }
    }
}