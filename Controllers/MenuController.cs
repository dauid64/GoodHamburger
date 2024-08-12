using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodHamburger.Data;
using GoodHamburger.Models;

namespace GoodHamburger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly Context _context;

        public MenuController(Context context)
        {
            _context = context;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<Dictionary<string, IEnumerable<object>>>> GetMenu()
        {
            List<Sandwich> sandwiches = await _context.Sandwiches.ToListAsync();
            List<Extra> extras = await _context.Extras.ToListAsync();

            var menu = new Dictionary<string, IEnumerable<object>>
            {
                { "sandwiches", sandwiches },
                { "extras", extras }
            };

            return menu;
        }
    }
}
