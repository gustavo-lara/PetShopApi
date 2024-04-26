using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Azure.Core;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PrimeiraAPI.Data;
using PrimeiraAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PrimeiraAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientesController : ControllerBase
	{
		private readonly MyContext _context;

		public ClientesController(MyContext context)
		{
			_context = context;
		}

		// GET: api/Clientes
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
		{
			if (_context.Clientes == null)
			{
				return NotFound();
			}
			return await _context.Clientes.ToListAsync();
		}

		// GET: api/Clientes/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Cliente>> GetCliente(Guid id)
		{
			if (_context.Clientes == null)
			{
				return NotFound();
			}
			var cliente = await _context.Clientes.FindAsync(id);

			if (cliente == null)
			{
				return NotFound();
			}

			return cliente;
		}
		// GET: api/Clientes/name
		[HttpGet("GetByName/{name}")]
		public async Task<ActionResult<IEnumerable<Cliente>>> GetClienteByName(string name)
		{
			if (_context.Clientes == null)
			{
				return NotFound();
			}

			var cliente = await _context.Clientes.Where(c => c.ClienteNome.Contains(name)).ToListAsync();

			if (cliente == null)
			{
				return NotFound();
			}
			return Ok(cliente);
		}

		// PUT: api/Clientes/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCliente(Guid id, Cliente cliente)
		{
			if (id != cliente.ClienteId)
			{
				return BadRequest();
			}

			_context.Entry(cliente).State = EntityState.Modified;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ClienteExists(id))
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

		// POST: api/Clientes
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
		{
			if (_context.Clientes == null)
			{
				return Problem("Entity set 'MyContext.Clientes'  is null.");
			}

			// Verificar se o email já existe
			if (_context.Clientes.Any(c => c.ClienteEmail == cliente.ClienteEmail))
			{
				return BadRequest("O email informado já está em uso.");
			}

			// Verificar se o CPF já existe
			if (_context.Clientes.Any(c => c.ClienteCPF == cliente.ClienteCPF))
			{
				return BadRequest("O CPF informado já está em uso.");
			}

			// Se os dados forem únicos, salvar o cliente
			_context.Clientes.Add(cliente);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCliente", new { id = cliente.ClienteId }, cliente);
		}

		// DELETE: api/Clientes/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCliente(Guid id)
		{
			if (_context.Clientes == null)
			{
				return NotFound();
			}
			var cliente = await _context.Clientes.FindAsync(id);
			if (cliente == null)
			{
				return NotFound();
			}

			_context.Clientes.Remove(cliente);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ClienteExists(Guid id)
		{
			return (_context.Clientes?.Any(e => e.ClienteId == id)).GetValueOrDefault();
		}
	}
}
