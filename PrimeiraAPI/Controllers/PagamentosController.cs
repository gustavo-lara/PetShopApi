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
    public class PagamentosController : ControllerBase
    {
        private readonly MyContext _context;

        public PagamentosController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Pagamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamentos()
        {
          if (_context.Pagamentos == null)
          {
              return NotFound();
          }
            return await _context.Pagamentos.ToListAsync();
        }


		// GET: api/Pagamentos/5
		[HttpGet("{id}")]
        public async Task<ActionResult<Pagamento>> GetPagamento(Guid id)
        {
          if (_context.Pagamentos == null)
          {
              return NotFound();
          }
            var pagamento = await _context.Pagamentos.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound();
            }

            return pagamento;
        }


        [HttpGet("GetByName/{FormadePagamneto}")]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetClienteByForma(string Forma)
        {
            if (_context.Pagamentos == null)
            {
                return NotFound();
            }

            var forma = await _context.Pagamentos.Where(c => c.PagamentoForma.Contains(Forma)).ToListAsync();

            if (forma == null)
            {
                return NotFound();
            }
            return Ok(forma);
        }

        // PUT: api/Pagamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagamento(Guid id, Pagamento pagamento)
        {
            if (id != pagamento.PagamentoId)
            {
                return BadRequest();
            }

            _context.Entry(pagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagamentoExists(id))
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

        // POST: api/Pagamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pagamento>> PostPagamento(Pagamento pagamento)
        {
          if (_context.Pagamentos == null)
          {
              return Problem("Entity set 'MyContext.Pagamentos'  is null.");
          }

			if (pagamento.ValorPagamento <= 0)
			{
				return BadRequest("O pagamento não pode ser negativo!");
			}

			_context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPagamento", new { id = pagamento.PagamentoId }, pagamento);
        }

        // DELETE: api/Pagamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagamento(Guid id)
        {
            if (_context.Pagamentos == null)
            {
                return NotFound();
            }
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null)
            {
                return NotFound();
            }

            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagamentoExists(Guid id)
        {
            return (_context.Pagamentos?.Any(e => e.PagamentoId == id)).GetValueOrDefault();
        }
    }
}
