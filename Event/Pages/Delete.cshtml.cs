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
    public class DeleteModel : PageModel
    {
        private readonly Event.Context.DatabaseContext _context;

        public DeleteModel(Event.Context.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Events Events { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Events = await _context.Events.FirstOrDefaultAsync(m => m.Id == id && m.Organizer.UserName == User.Identity.Name);

            if (Events == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Events = await _context.Events.FindAsync(id);

            if (Events != null)
            {
                _context.Events.Remove(Events);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./OrganizeEvents", new { Deleted = Events.Title });
        }
    }
}
