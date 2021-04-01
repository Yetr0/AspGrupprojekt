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
    public class MyEventsModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<MyUser> _userManager;

        public MyEventsModel(
            DatabaseContext context,
            UserManager<MyUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        [TempData]
        public String Message { get; set; }
        public bool ShowMessage => !String.IsNullOrEmpty(Message);


        public IList<Events> Events { get; set; }
        [BindProperty]
        public MyUser MyUser { get; set; }

        public async Task OnGetAsync()
        {

            var userId = _userManager.GetUserId(User);

            var user = await _context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.MyEvents)
               .FirstOrDefaultAsync();


            Events = user.MyEvents;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
          
                var userId = _userManager.GetUserId(User);

                var user = await _context.Users
                   .Where(u => u.Id == userId)
                   .Include(u => u.MyEvents)
                   .FirstOrDefaultAsync();

                Events RemoveEvent = await _context.Event.Where(e => e.Id == id).FirstOrDefaultAsync();

                user.MyEvents.Remove(RemoveEvent);

                await _context.SaveChangesAsync();

            Message = "You have now left the event!";

            return RedirectToPage();
    



        }

    }
}
    