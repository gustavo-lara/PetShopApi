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
    public class VacinacoesController : ControllerBase
    {
        private readonly MyContext _context;

        public VacinacoesController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Vacinacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacinacao>>> GetVacinacoes()
        {
            if (_context.Vacinacoes == null)
            {
                return NotFound();
            }
            return await _context.Vacinacoes.ToListAsync();
        }

        // GET: api/Vacinacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vacinacao>> GetVacinacao(Guid id)
        {
            if (_context.Vacinacoes == null)
            {
                return NotFound();
            }
            var vacinacao = await _context.Vacinacoes.FindAsync(id);

            if (vacinacao == null)
            {
                return NotFound();
            }

            return vacinacao;
        }

        // PUT: api/Vacinacoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacinacao(Guid id, Vacinacao vacinacao)
        {
            if (id != vacinacao.VacinacaoId)
            {
                return BadRequest();
            }

            _context.Entry(vacinacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacinacaoExists(id))
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

        // POST: api/Vacinacoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vacinacao>> PostVacinacao(Vacinacao vacinacao)
        {
            if (_context.Vacinacoes == null)
            {
                return Problem("Entity set 'MyContext.Vacinacoes'  is null.");
            }
            _context.Vacinacoes.Add(vacinacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVacinacao", new { id = vacinacao.VacinacaoId }, vacinacao);
        }




        // POST: api/Vacinacoes/teste
        [HttpPost("Vacinacoes/teste")]
        public async Task<ActionResult<Vacinacao>> AgendarVacinaAsync(Vacinacao vacinacao)
        {
            DateTime data = vacinacao.DataVacinacao;

            // Verifica se a data já passou
            if (data < DateTime.Today)
            {
                // Manuseie o cenário de data inválida (por exemplo, exiba uma mensagem de erro)
                return Problem("A vacinação não pode ser agendada para uma data passada.");
            }

            // Verifica se a data é o dia anterior ao atual
            if (data == DateTime.Today.AddDays(-1))
            {
                // Manuseie o cenário de data inválida (por exemplo, exiba uma mensagem de erro)
                return Problem("A vacinação não pode ser agendada para o dia anterior ao atual.");
            }

            _context.Vacinacoes.Add(vacinacao);
            await _context.SaveChangesAsync();
            return Ok("data:" + data);

        }




        // DELETE: api/Vacinacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacinacao(Guid id)
        {
            if (_context.Vacinacoes == null)
            {
                return NotFound();
            }
            var vacinacao = await _context.Vacinacoes.FindAsync(id);
            if (vacinacao == null)
            {
                return NotFound();
            }

            _context.Vacinacoes.Remove(vacinacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VacinacaoExists(Guid id)
        {
            return (_context.Vacinacoes?.Any(e => e.VacinacaoId == id)).GetValueOrDefault();
        }
    }
}
