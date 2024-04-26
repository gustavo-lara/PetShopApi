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
    public class ConsultasController : ControllerBase
    {
        private readonly MyContext _context;

        public ConsultasController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Consultas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsultas()
        {
            if (_context.Consultas == null)
            {
                return NotFound();
            }
            return await _context.Consultas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Consulta>> GetConsulta(Guid id)
        {
            if (_context.Consultas == null)
            {
                return NotFound();
            }
            var consulta = await _context.Consultas.FindAsync(id);

            if (consulta == null)
            {
                return NotFound();
            }

            return consulta;
        }


        [HttpGet("GetByDate/{data}")]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsultasDate(DateTime data)
        {
            if (_context.Consultas == null)
            {
                return NotFound();
            }

            var listaConsultas = await _context.Consultas.Where(b => b.DataConsulta.Date == data.Date).ToListAsync();

            if (listaConsultas == null)
            {
                return NoContent();
            }

            return Ok(listaConsultas);
        }


        // PUT: api/Consultas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsulta(Guid id, Consulta consulta)
        {
            if (id != consulta.ConsultaId)
            {
                return BadRequest();
            }

            _context.Entry(consulta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultaExists(id))
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

        // POST: api/Consultas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Consulta>> PostConsulta(Consulta consulta)
        {
            if (_context.Consultas == null)
            {
                return Problem("Entity set 'MyContext.Consultas'  is null.");
            }
            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsulta", new { id = consulta.ConsultaId }, consulta);
        }


        // POST: api/Consultas/teste
        [HttpPost("Consultas/teste")]
        public async Task<ActionResult<Consulta>> AgendarConsultaAsync(Consulta consulta)
        {
            DateTime data = consulta.DataConsulta;

            // Verifica se a data já passou
            if (data < DateTime.Today)
            {
                // Manuseie o cenário de data inválida (por exemplo, exiba uma mensagem de erro)
                return Problem("A Consulta não pode ser agendada para uma data passada.");
            }

            // Verifica se a data é o dia anterior ao atual
            if (data == DateTime.Today.AddDays(-1))
            {
                // Manuseie o cenário de data inválida (por exemplo, exiba uma mensagem de erro)
                return Problem("A Consulta não pode ser agendada para o dia anterior ao atual.");
            }

            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();
            return Ok("data:" + data);

        }


        // DELETE: api/Consultas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsulta(Guid id)
        {
            if (_context.Consultas == null)
            {
                return NotFound();
            }
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }

            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Consultas/Datetime
        [HttpDelete]
        public async Task<IActionResult> DeleteConsultaByDateTime(DateTime data)
        {
            if (_context.Consultas == null)
            {
                return NotFound();
            }
            var consulta = await _context.Consultas.FindAsync(data);
            if (consulta == null)
            {
                return NotFound();
            }

            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsultaExists(Guid id)
        {
            return (_context.Consultas?.Any(e => e.ConsultaId == id)).GetValueOrDefault();
        }
    }
}
