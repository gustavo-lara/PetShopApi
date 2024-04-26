using Microsoft.AspNetCore.Mvc;

namespace PrimeiraAPI.Models
{
	public class ProdutoVenda 
	{
	public Guid ProdutoVendaId { get; set; }
	public Guid ProdutoId { get; set; }
	public Guid VendaId { get; set; }
	public double QntdVenda { get; set; } 
	public Venda? Venda { get; set; }
	public Produto? Produto { get; set; }
	}
}
