using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanhosTosasController : ControllerBase
    {
        private readonly MyContext _context;

        public BanhosTosasController(MyContext context)
        {
            _context = context;
        }

        // GET: api/BanhosTosas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BanhoTosa>>> GetBanhosTosas()
        {
          if (_context.BanhosTosas == null)
          {
              return NotFound();
          }
            return await _context.BanhosTosas.ToListAsync();
        }

		// GET: api/BanhosTosas/GetDate/{data}
		[HttpGet("/GetByDate/{data}")]
		public async Task<ActionResult<IEnumerable<BanhoTosa>>> GetBanhosTosasDate(DateTime data)
		{
			if (_context.BanhosTosas == null)
			{
				return NotFound();
			}

			var listaBanhosTosas = await _context.BanhosTosas.Where(b => b.DataBanhoTosa.Date  == data.Date).ToListAsync();

			if (listaBanhosTosas == null)
            {
                return NoContent();
            }

			return Ok(listaBanhosTosas);
		}

		// GET: api/BanhosTosas/5
		[HttpGet("{id}")]
        public async Task<ActionResult<BanhoTosa>> GetBanhoTosa(Guid id)
        {
          if (_context.BanhosTosas == null)
          {
              return NotFound();
          }
            var banhoTosa = await _context.BanhosTosas.FindAsync(id);

            if (banhoTosa == null)
            {
                return NotFound();
            }

            return banhoTosa;
        }

		// PUT: api/BanhosTosas/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutBanhoTosa(Guid id, BanhoTosa banhoTosa)
        {
            if (id != banhoTosa.BanhoTosaId)
            {
                return BadRequest();
            }

            _context.Entry(banhoTosa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BanhoTosaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BanhosTosas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BanhoTosa>> PostBanhoTosa(BanhoTosa banhoTosa)
        {
          if (_context.BanhosTosas == null)
          {
              return Problem("Entity set 'MyContext.BanhosTosas'  is null.");
          }
            _context.BanhosTosas.Add(banhoTosa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBanhoTosa", new { id = banhoTosa.BanhoTosaId }, banhoTosa);
        }

        // DELETE: api/BanhosTosas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanhoTosa(Guid id)
        {
            if (_context.BanhosTosas == null)
            {
                return NotFound();
            }
            var banhoTosa = await _context.BanhosTosas.FindAsync(id);
            if (banhoTosa == null)
            {
                return NotFound();
            }

            _context.BanhosTosas.Remove(banhoTosa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BanhoTosaExists(Guid id)
        {
            return (_context.BanhosTosas?.Any(e => e.BanhoTosaId == id)).GetValueOrDefault();
        }
    }
}
