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
    public class EventsModel : PageModel
    {
        private readonly Event.Context.DatabaseContext _context;

        public EventsModel(Event.Context.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Events> Events { get; set; }

        public async Task OnGetAsync()
        {
            Events = await _context.Events.ToListAsync();
        }
    }
}
