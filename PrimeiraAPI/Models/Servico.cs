using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
	public class Servico 
	{
		public Guid ServicoId { get; set; }
		[Required(ErrorMessage = "O valor do serviço é obrigatório!")]
		[Display(Name = "Valor do Serviço")]
		public double ValorServico { get; set; }
		public Guid ConsultaId { get; set; }
		public Guid BanhoTosaId { get; set; }
		public Guid PlanoId { get; set; }
		public Guid VacinacaoId { get; set; }
		public Consulta? Consulta { get; set; }
		public BanhoTosa? BanhoTosa { get; set; }
		public Plano? Plano { get; set; }
		public Vacinacao? Vacinacao { get; set;}
	}
}
