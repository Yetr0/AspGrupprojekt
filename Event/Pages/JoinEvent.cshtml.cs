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
    public class JoinEventModel : PageModel
    {
        private readonly Event.Context.DatabaseContext _context;

        public JoinEventModel(Event.Context.DatabaseContext context)
        {
            _context = context;
        }

        public Events Event { get; set; }

        public MyUser MyUser { get; set; }

    //    public IActionResult OnGet(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        attendee = _context.Attendees.First(a => a.Id == 1);
    //        Event = _context.Events.Include(e => e.Attendees).Include(e => e.Organizer).First(e => e.Id == id);


    //        if (Event == null)
    //        {
    //            return NotFound();
    //        }
    //        return Page();
    //    }
    //    public IActionResult OnPost(int id)
    //    {
    //        attendee = _context.Attendees.First(a => a.Id == 1);
    //        Events events = _context.Events.Include(e => e.Attendees).First(e => e.Id == id);

    //        if (_context.JoinEvent(attendee, events, _context))
    //        {
    //            Event = _context.Events.Include(e => e.Attendees).Include(e => e.Organizer).First(e => e.Id == id);
    //            return Page();
    //        }

    //        Event = _context.Events.Include(e => e.Attendees).Include(e => e.Organizer).First(e => e.Id == id);
    //        return Page();

    //    }

    //    public bool AttendeeJoined(Events Event, Attendee attendee)
    //    {
    //        if (Event.Attendees.Contains(attendee))
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
   }
}
    
