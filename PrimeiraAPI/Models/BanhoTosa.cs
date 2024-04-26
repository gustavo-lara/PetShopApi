using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
	public class BanhoTosa 
	{
		public Guid BanhoTosaId { get; set; }
		[Required(ErrorMessage = "A data do banho e tosa é obrigatória!")]
		[Display(Name = "Data do Banho e Tosa")]
		public DateTime DataBanhoTosa { get; set; }

	}
}
