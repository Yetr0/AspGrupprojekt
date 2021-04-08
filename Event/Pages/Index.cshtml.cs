using Event.Context;
using Event.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DatabaseContext _context;
        private readonly UserManager<MyUser> _userManager;
        private readonly SignInManager<MyUser> _signInManager;


        public IndexModel(
             ILogger<IndexModel> logger,
             DatabaseContext context,
             UserManager<MyUser> userManager,
              SignInManager<MyUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
             _signInManager = signInManager;
        }
        public async Task OnGetAsync(bool? seedDb)
        {
            if (seedDb ?? false)
            {
                await _context.ResetAndSeedAsync(_userManager);
            }
           
        }
    }

}
