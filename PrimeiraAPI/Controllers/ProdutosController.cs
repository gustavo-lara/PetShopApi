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
	public class ProdutosController : ControllerBase
	{
		private readonly MyContext _context;

		public ProdutosController(MyContext context)
		{
			_context = context;
		}

		// GET: api/Produtos
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
		{
			if (_context.Produtos == null)
			{
				return NotFound();
			}
			return await _context.Produtos.ToListAsync();
		}

		// GET: api/Produtos/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Produto>> GetProduto(Guid id)
		{
			if (_context.Produtos == null)
			{
				return NotFound();
			}
			var produto = await _context.Produtos.FindAsync(id);

			if (produto == null)
			{
				return NotFound();
			}

			return produto;
		}

		// PUT: api/Produtos/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduto(Guid id, Produto produto)
		{
			if (id != produto.ProdutoId)
			{
				return BadRequest();
			}

			_context.Entry(produto).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProdutoExists(id))
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

		// GET: api/Produtos/name
		[HttpGet("GetByName/{name}")]

		public async Task<ActionResult<Produto>> GetProdutoByName(string Nome)
		{
			if (_context.Produtos == null)
			{
				return NotFound();
			}
			var produto = await _context.Produtos.Where(c => c.ProdutoNome.Contains(Nome)).ToListAsync();

			if (Nome == null)
			{
				return NotFound();
			}

			return Ok(Nome);
		}

		// GET: api/Produtos/categoria
		[HttpGet("GetProductsByCategory/{categoria}")]
		public async Task<ActionResult<IEnumerable<Produto>>> GetProductsByCategory(string categoria)
		{
			if (_context.Produtos == null)
			{
				return NotFound();
			}

			var produtos = await _context.Produtos.Where(p => p.ProdutoCategoria.Contains(categoria)).ToListAsync();

			if (produtos.Count == 0)
			{
				return NotFound();
			}

			return Ok(produtos);

		}

		[HttpGet("GetVendaByQuantidade/{Quantidade}")]
		public async Task<ActionResult<IEnumerable<Produto>>> GetVendasByValor(double qntd)
		{
			if (_context.Produtos == null)
			{
				return NotFound();
			}

			var vendas = await _context.Produtos.Where(p => p.ProdutoQtnd == qntd).ToListAsync();

			if (!vendas.Any())
			{
				return NotFound("Não há produtos com está qunatidade!");
			}

			return Ok(vendas);
		}

		// POST: api/Produtos
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Produto>> PostProduto(Produto produto)
		{
			if (_context.Produtos == null)
			{
				return Problem("Favor cadastrar todos os campos");
			}

			if (produto.ProdutoQtnd < 0)
			{
				return Problem("Quantidade em estoque não pode ser negativo!");
			}

			if (produto.ProdutoValor < 0)
			{
				return BadRequest("O preço do produto não pode ser negativo!");
			}

			_context.Produtos.Add(produto);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, produto);
		}

		// DELETE: api/Produtos/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduto(Guid id)
		{
			if (_context.Produtos == null)
			{
				return NotFound();
			}
			var produto = await _context.Produtos.FindAsync(id);
			if (produto == null)
			{
				return NotFound();
			}

			_context.Produtos.Remove(produto);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ProdutoExists(Guid id)
		{
			return (_context.Produtos?.Any(e => e.ProdutoId == id)).GetValueOrDefault();
		}

		[HttpGet("GetProdutosByValor/{valor}")]
		public async Task<ActionResult<IEnumerable<Produto>>> GetProdutosByValor(double valor)
		{
			if (_context.Produtos == null)
			{
				return NotFound();
			}

			var produtos = await _context.Produtos.Where(p => p.ProdutoValor == valor).ToListAsync();

			if (!produtos.Any())
			{
				return NotFound("Não há produtos com este valor!");
			}

			return Ok(produtos);
		}
	}
}
