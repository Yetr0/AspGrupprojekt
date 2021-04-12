using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Event.Context;
using Event.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Event.Pages

{

    [Authorize(Roles = "Admin")]
    public class AdminPageModel : PageModel
    {
        [BindProperty]
        public List<string> PrevOrganizer { get; set; }
        [FromForm(Name = "Organizer")]
        public List<string> Organizer { get; set; }
        public List<AdminPage> UsersAndRoles { get; set; }


        private readonly Event.Context.DatabaseContext _context;
        private readonly UserManager<MyUser> _userManager;

        public AdminPageModel(Event.Context.DatabaseContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            List<AdminPage> results = new List<AdminPage>();
            var Users = _userManager.Users.ToList();
            List<MyUser> organizers = _userManager.GetUsersInRoleAsync("Organizer").Result.ToList();
            foreach (var user in Users)
            {
                bool organizer = organizers.Contains(user) ? true : false;
                results.Add(new AdminPage { User = user, Organizer = organizer });
            }
            UsersAndRoles = results;
            
            
            return Page();
        }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine(Organizer + "\n" + PrevOrganizer);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            foreach (var organizer in Organizer)
            {
                if (!PrevOrganizer.Contains(organizer))
                {
                    var user = await _context.Users.Where(u => u.UserName == organizer).FirstAsync();
                    await _userManager.AddToRoleAsync(user, "Organizer");
                }
            }
            foreach (var prevorg in PrevOrganizer)
            {
                if (!Organizer.Contains(prevorg))
                {
                    var user = _userManager.Users.Where(u => u.UserName == prevorg).First();
                    await _userManager.RemoveFromRoleAsync(user, "Organizer");
                }
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
