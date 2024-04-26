using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
	public class Vacinacao
	{ 
		public Guid VacinacaoId { get; set; }
		[Required(ErrorMessage = "A data da vacinação é obrigatória!")]
		[Display(Name = "Data da Vacinação")]
		public DateTime DataVacinacao { get; set; }
		[Required(ErrorMessage = "O tipo da vacina é obrigatória!")]
		[Display(Name = "Tipo da Vacina")]
		public string TipoVacina { get; set; }
	}
}
