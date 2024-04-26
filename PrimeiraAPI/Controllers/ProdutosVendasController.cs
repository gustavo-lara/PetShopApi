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
    public class ProdutosVendasController : ControllerBase
    {
        private readonly MyContext _context;

        public ProdutosVendasController(MyContext context)
        {
            _context = context;
        }

        // GET: api/ProdutosVendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoVenda>>> GetProdutosVendas()
        {
            if (_context.ProdutosVendas == null)
            {
                return NotFound();
            }
            return await _context.ProdutosVendas.ToListAsync();
        }

        // GET: api/ProdutosVendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoVenda>> GetProdutoVenda(Guid id)
        {
            if (_context.ProdutosVendas == null)
            {
                return NotFound();
            }
            var produtoVenda = await _context.ProdutosVendas.FindAsync(id);

            if (produtoVenda == null)
            {
                return NotFound();
            }

            return produtoVenda;
        }

        // PUT: api/ProdutosVendas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdutoVenda(Guid id, ProdutoVenda produtoVenda)
        {
            if (id != produtoVenda.ProdutoVendaId)
            {
                return BadRequest();
            }

            _context.Entry(produtoVenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoVendaExists(id))
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

        // POST: api/ProdutosVendas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProdutoVenda>> PostProdutoVenda(ProdutoVenda produtoVenda)
        {
            if (_context.ProdutosVendas == null)
            {
                return Problem("Entity set 'MyContext.ProdutosVendas'  is null.");
            }
            // Buscar o produto no banco de dados
            var produtoBanco = await _context.Produtos.FindAsync(produtoVenda.ProdutoId);
            if (produtoBanco == null)
            {
                return NotFound("Produto não encontrado");
            }

            // Verificar se a qunatidade do produto é suficiente
            if (produtoBanco.ProdutoQtnd < produtoVenda.QntdVenda)
            {
                return BadRequest("Quantidade insuficiente");
            }
            //Calcular valor do item
            var valorItem = produtoBanco.ProdutoValor * produtoVenda.QntdVenda;
            //Atualizando o valor da venda
            var venda = await _context.Vendas.FindAsync(produtoVenda.VendaId);
            //Atualizando o valor da venda
            venda!.ValorVenda += valorItem;
            //Atualizando a qunatidade do produto
            produtoBanco.ProdutoQtnd -= produtoVenda.QntdVenda;

            _context.Produtos.Update(produtoBanco);
            _context.Vendas.Update(venda);
            _context.ProdutosVendas.Add(produtoVenda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdutoVenda", new { id = produtoVenda.ProdutoVendaId }, produtoVenda);
        }

        // DELETE: api/ProdutosVendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdutoVenda(Guid id)
        {
            if (_context.ProdutosVendas == null)
            {
                return NotFound();
            }
            var produtoVenda = await _context.ProdutosVendas.FindAsync(id);
            if (produtoVenda == null)
            {
                return NotFound();
            }

            _context.ProdutosVendas.Remove(produtoVenda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoVendaExists(Guid id)
        {
            return (_context.ProdutosVendas?.Any(e => e.ProdutoVendaId == id)).GetValueOrDefault();
        }
    }
}
