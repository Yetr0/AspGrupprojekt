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
using Microsoft.AspNetCore.Authorization;

namespace Event.Pages
{
    [Authorize]
    public class JoinEventModel : PageModel
    {

        private readonly DatabaseContext _context;
        private readonly UserManager<MyUser> _userManager;
        public bool Joined { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }

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

            Event = await _context.Events.Include(e => e.Attendees).FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
            {
                return NotFound();
            }
            var user = _userManager.GetUserAsync(User).Result;
            if(Event.Attendees != null)
            {
                if (Event.Attendees.Contains(user))
                {
                    Joined = true;
                }
            }
            else
            {
                Joined = false;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {

            var userId = _userManager.GetUserId(User);

            var user = await _context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.MyEvents)
               .FirstOrDefaultAsync();

            Event = await _context.Events.Where(e => e.Id == id).FirstOrDefaultAsync();

            user.MyEvents.Remove(Event);

            await _context.SaveChangesAsync();

            Joined = false;

            return Page();




        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.Include(e => e.Attendees).FirstOrDefaultAsync(m => m.Id == id);

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
            Joined = true;
            return Page();
        }
        
    }
}
    
