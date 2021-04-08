using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Event.Context;
using Event.Models;
using Microsoft.AspNetCore.Identity;

namespace Event.Pages
{
    public class JoinEventModel : PageModel
    {

        private readonly DatabaseContext _context;
        private readonly UserManager<MyUser> _userManager;

        public JoinEventModel(
            DatabaseContext context,
            UserManager<MyUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        public Events Event { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
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

            Event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.MyEvents)
                .FirstOrDefaultAsync();

            if (!user.MyEvents.Contains(Event))
            {
                user.MyEvents.Add(Event);
                await _context.SaveChangesAsync();
            }

            return Page();
        }
    }
}
    
