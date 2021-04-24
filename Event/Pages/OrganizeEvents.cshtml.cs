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
using Microsoft.AspNetCore.Identity;

namespace Event.Pages
{
    [Authorize(Roles = "Organizer, Admin")]
    public class OrganizeEventsModel : PageModel
    {
        private readonly Event.Context.DatabaseContext _context;

        private readonly UserManager<MyUser> _userManager;
        [BindProperty(SupportsGet = true)]
        public string AddedEvent { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Edited { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Deleted { get; set; }

        public OrganizeEventsModel(
            DatabaseContext context,
            UserManager<MyUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Events> Events { get;set; }

        public IActionResult OnGet()
        {
            var userId = _userManager.GetUserId(User);

            Events = _context.Events.Where(e => e.Organizer.Id == userId).ToList();
            return Page();
        }
    }
}
