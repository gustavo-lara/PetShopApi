using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
	public class Produto 
	{
	public Guid ProdutoId { get; set; }
	[Required(ErrorMessage = "O nome do produto é obrigatório!")]
	[Display(Name = "Nome do Produto")]
	[StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do produto deve ter entre 3 e 100 caracteres!")]
	public string ProdutoNome { get; set;}
	[Required(ErrorMessage = "O valor do produto é obrigatório!")]
	[Display(Name = "Valor do Produto")]
	public double ProdutoValor { get; set; }
	[Required(ErrorMessage = "A categoria do produto é obrigatória!")]
	[Display(Name = "Categoria do Produto")]
	public string ProdutoCategoria { get; set;}
	[Required(ErrorMessage = "A entrada do produto é obrigatória!")]
	[Display(Name = "Entrada do Produto")]
	public DateTime ProdutoEntrada { get; set; }
	[Required(ErrorMessage = "A saída do produto é obrigatória!")]
	[Display(Name = "Saída do Produto")]
	public DateTime ProdutoSaida { get; set;}
	[Display(Name = "Quantidade de Produtos")]
	public double ProdutoQtnd { get; set; }
	public Guid FornecedorId { get; set; }
	public Fornecedor? Fornecedor { get; set; }
	}
}
