using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
	public class Consulta 
	{
	public Guid ConsultaId { get; set; }
	[Required(ErrorMessage = "A data da vacinação é obrigatória!")]
	[Display(Name = "Data da Vacinação")]
	public DateTime DataConsulta { get; set; }
	[Display(Name = "Prontuário")]
	[StringLength(250, ErrorMessage = "O prontuário deve ter até 250 caracteres!")]
	public string Prontuario { get; set;}
	}
}
