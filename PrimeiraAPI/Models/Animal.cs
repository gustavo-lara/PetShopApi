using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
	public class Animal 
	{
		public Guid AnimalId { get; set; }

		[Display(Name = "Raça do Animal")]
		public string AnimalRaca { get; set; }
		[Required(ErrorMessage = "O nome do animal é obrigatório!")]
		[Display(Name = "Nome do Animal")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "O nome do cliente deve ter entre 2 e 100 caracteres!")]
		public string AnimalNome { get; set; }
		[Display(Name = "Idade do Animal")]	
		public double AnimalIdade { get; set; }
		[Display(Name = "Descrição do Animal")]
		[StringLength(250, ErrorMessage = "A descrição deve ter até 250 caracteres!")]
		public string AnimalDescricao{ get; set; }
		public Guid ClienteId { get; set; }	
		public Cliente? Cliente { get; set; }
	}
}
