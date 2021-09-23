using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace eProject.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            Tickets = new HashSet<Ticket>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public int? MemberShip { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
