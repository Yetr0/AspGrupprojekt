using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event.Models
{
    public class AdminPage
    {
        public MyUser User { get; set; }
        public List<string> Roles { get; set; }
    }
}
