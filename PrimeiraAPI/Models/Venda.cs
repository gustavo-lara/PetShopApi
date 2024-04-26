using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
	public class Venda 
	{
		public Guid VendaId { get; set; }
		[Required(ErrorMessage = "A data da venda é obrigatória!")]
		[Display(Name = "Data da Venda")]
		public DateTime VendaData { get; set; }
		[Display(Name = "Valor da Venda")]
		public double? ValorVenda { get; set; }
		public Guid ClienteId { get; set; }
		public Guid? PagamentoId { get; set; }
		public Guid? ServicoId { get; set; }
		public Cliente? Cliente { get; set; }	
		public Pagamento? Pagamento { get; set; }
		public Servico? Servico { get; set; }
	}
}
