using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        //So, for performance matters when we dont want the whole membership object, we juste need the ID we do this .. ?
        public byte MembershipId { get; set; }

        //[Models.Min18YearsValidation]
        public DateTime? Birthdate { get; set; }

        public MembershipDto Membership { get; set; }
    }
}