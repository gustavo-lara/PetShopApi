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
    public class FornecedoresController : ControllerBase
    {
        private readonly MyContext _context;

        public FornecedoresController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Fornecedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedores()
        {
          if (_context.Fornecedores == null)
          {
              return NotFound();
          }
            return await _context.Fornecedores.ToListAsync();
        }

		// GET: api/Fornecedores/name
		[HttpGet("GetByName/{name}")]
		public async Task<ActionResult<IEnumerable<Fornecedor>>> GetClienteByName(string name)
		{
			if (_context.Fornecedores == null)
			{
				return NotFound();
			}

			var fornecedor = await _context.Fornecedores.Where(c => c.FornecedorNome.Contains(name)).ToListAsync();

			if (fornecedor == null)
			{
				return NotFound();
			}
			return Ok(fornecedor);
		}

		// GET: api/Fornecedores/5
		[HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> GetFornecedor(Guid id)
        {
          if (_context.Fornecedores == null)
          {
              return NotFound();
          }
            var fornecedor = await _context.Fornecedores.FindAsync(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return fornecedor;
        }

        // PUT: api/Fornecedores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFornecedor(Guid id, Fornecedor fornecedor)
        {
            if (id != fornecedor.FornecedorId)
            {
                return BadRequest();
            }

            _context.Entry(fornecedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FornecedorExists(id))
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

        // POST: api/Fornecedores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fornecedor>> PostFornecedor(Fornecedor fornecedor)
        {
          if (_context.Fornecedores == null)
          {
              return Problem("Entity set 'MyContext.Fornecedores'  is null.");
          }
            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFornecedor", new { id = fornecedor.FornecedorId }, fornecedor);
        }

        // DELETE: api/Fornecedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFornecedor(Guid id)
        {
            if (_context.Fornecedores == null)
            {
                return NotFound();
            }
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FornecedorExists(Guid id)
        {
            return (_context.Fornecedores?.Any(e => e.FornecedorId == id)).GetValueOrDefault();
        }
    }
}
