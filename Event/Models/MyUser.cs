using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event.Models
{
  
    public class MyUser : IdentityUser
    {
        ICollection<Events> Event { get; set; }
    }
}
