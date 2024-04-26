using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
	public class Plano 
	{
		public Guid PlanoId { get; set; }
		[Required(ErrorMessage = "O tipo do plano é obrigatório!")]
		[Display(Name = "Tipo do Plano")]
		public string TipoPlano { get; set;}
		[Required(ErrorMessage = "A mensalidade é obrigatória!")]
		[Display(Name = "Mensalidade do Plano")]
		public double Mensalidade { get; set; }


	}
}
