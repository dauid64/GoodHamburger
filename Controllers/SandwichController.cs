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
    public class SandwichController : ControllerBase
    {
        private readonly Context _context;

        public SandwichController(Context context)
        {
            _context = context;
        }

        // GET: api/Sandwich
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sandwich>>> GetSandwiches()
        {
            return await _context.Sandwiches.ToListAsync();
        }
    }
}
