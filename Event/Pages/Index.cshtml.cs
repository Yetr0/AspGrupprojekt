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

        public IndexModel(
            ILogger<IndexModel> logger,
            DatabaseContext context,
            UserManager<MyUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
    }

}
