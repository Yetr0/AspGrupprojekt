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

        public IList<Events> Events { get; set; }

        public async Task OnGetAsync()
        {

            var userId = _userManager.GetUserId(User);

            var user = await _context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.MyEvents)
               .FirstOrDefaultAsync();


            Events = user.MyEvents;
        }
    }

}
    