using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Event.Models;
using Event.Context;

namespace Event.Pages
{

    [Authorize(Roles = "Organizer, Admin")]
    public class AddEventsModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<MyUser> _userManager;
        private readonly SignInManager<MyUser> _signInManager;


        public AddEventsModel(DatabaseContext context, UserManager<MyUser> userManager, SignInManager<MyUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Events EventCreated { get; set; }
        public MyUser CurrentUser { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userId = _userManager.GetUserId(User);

            CurrentUser = await _context.MyUsers.Where(u => u.Id == userId).FirstOrDefaultAsync();

            EventCreated.Organizer = CurrentUser;
            await _context.Events.AddAsync(EventCreated);
            await _context.SaveChangesAsync();

            return RedirectToPage("./OrganizeEvents", new { AddedEvent = "true" });
        }
    }
}
