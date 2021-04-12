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
        public List<AdminPage> UsersAndRoles { get; set; }


        private readonly Event.Context.DatabaseContext _context;
        private readonly UserManager<MyUser> _userManager;

        public AdminPageModel(Event.Context.DatabaseContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            List<AdminPage> results = new List<AdminPage>();
            var Users = _userManager.Users.ToList();
            foreach (var user in Users)
            {
                List<string> roles = _userManager.GetRolesAsync(user).Result.ToList();
                AdminPage obj = new AdminPage();
                results.Add(new AdminPage { User = user, Roles = roles});
            }
            UsersAndRoles = results;
            
            
            return Page();
        }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
