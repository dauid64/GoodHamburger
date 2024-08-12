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
    public class ExtraController : ControllerBase
    {
        private readonly Context _context;

        public ExtraController(Context context)
        {
            _context = context;
        }

        // GET: api/Extra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Extra>>> GetExtras()
        {
            return await _context.Extras.ToListAsync();
        }
    }
}
