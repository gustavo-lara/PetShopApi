using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AnimalsController : ControllerBase
	{
		private readonly MyContext _context;

		public AnimalsController(MyContext context)
		{
			_context = context;
		}

		// GET: api/Animals
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Animal>>> GetAnimais()
		{
			if (_context.Animais == null)
			{
				return NotFound();
			}
			return await _context.Animais.ToListAsync();
		}

		// GET: api/Animals/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Animal>> GetAnimal(Guid id)
		{
			if (_context.Animais == null)
			{
				return NotFound();
			}
			var animal = await _context.Animais.FindAsync(id);

			if (animal == null)
			{
				return NotFound();
			}

			return animal;
		}

		// GET: api/Animais/name
		[HttpGet("GetByName/{name}")]
		public async Task<ActionResult<IEnumerable<Animal>>> GetClienteByName(string name)
		{
			if (_context.Animais == null)
			{
				return NotFound();
			}

			var animal = await _context.Animais.Where(c => c.AnimalNome.Contains(name)).ToListAsync();

			if (animal == null)
			{
				return NotFound();
			}
			return Ok(animal);
		}

		// PUT: api/Animals/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAnimal(Guid id, Animal animal)
		{
			if (id != animal.AnimalId)
			{
				return BadRequest();
			}

			_context.Entry(animal).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AnimalExists(id))
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

		// POST: api/Animals
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
		{
			if (_context.Animais == null)
			{
				return Problem("Entity set 'MyContext.Animais'  is null.");
			}

			if (animal.AnimalIdade < 0)
			{
				return BadRequest("O Animal não pode ter idade negativa!");
			}

			_context.Animais.Add(animal);
			await _context.SaveChangesAsync();


			return CreatedAtAction("GetAnimal", new { id = animal.AnimalId }, animal);
		}


		// DELETE: api/Animals/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAnimal(Guid id)
		{
			if (_context.Animais == null)
			{
				return NotFound();
			}
			var animal = await _context.Animais.FindAsync(id);
			if (animal == null)
			{
				return NotFound();
			}

			_context.Animais.Remove(animal);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool AnimalExists(Guid id)
		{
			return (_context.Animais?.Any(e => e.AnimalId == id)).GetValueOrDefault();
		}
	}
}