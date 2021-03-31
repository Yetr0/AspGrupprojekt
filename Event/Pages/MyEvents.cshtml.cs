using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Event.Context;
using Event.Models;

namespace Event.Pages
{
    public class MyEventsModel : PageModel
    {
        private readonly Event.Context.DatabaseContext _context;

        public MyEventsModel(Event.Context.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Events> Events { get; set; }

        //    public void OnGet()
        //    {
        //        Events = _context.Attendees.Include(a => a.Events).First(a => a.Id == 1).Events;
        //    }
    }
}
