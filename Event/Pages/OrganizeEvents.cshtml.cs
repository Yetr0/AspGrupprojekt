using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Event.Context;
using Event.Models;
using Microsoft.AspNetCore.Authorization;

namespace Event.Pages
{
    [Authorize(Roles = "Organizer, Admin")]
    public class OrganizeEventsModel : PageModel
    {
        private readonly Event.Context.DatabaseContext _context;
        [BindProperty(SupportsGet = true)]
        public string AddedEvent { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Edited { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Deleted { get; set; }

        public OrganizeEventsModel(Event.Context.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Events> Events { get;set; }

        public IActionResult OnGet()
        {
            
            Events = _context.Events.Where(e => e.Organizer.UserName == User.Identity.Name).ToList();
            return Page();
        }
    }
}
