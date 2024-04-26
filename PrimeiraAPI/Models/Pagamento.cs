using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
	public class Pagamento 
	{
		public Guid PagamentoId { get; set; }
		[Required(ErrorMessage = "A forma de pagamento é obrigatória!")]
		[Display(Name = "Forma de Pagamento")]
		public string PagamentoForma { get; set; }
		[Required(ErrorMessage = "O valor do pagamento é obrigatório!")]
		[Display(Name = "Valor Pagamento")]
		public double ValorPagamento { get; set; }
		[Required(ErrorMessage = "A data do pagamento é obrigatória!")]
		[Display(Name = "Data do Pagamento")]
		public DateTime PagamentoData { get; set; }
	}
}
