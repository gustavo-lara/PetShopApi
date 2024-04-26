using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
	public class Cliente 
	{
		public Guid ClienteId { get; set; }
		[Required(ErrorMessage =	"O nome do cliente é obrigatório!")]
		[Display(Name = "Nome do Cliente")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do cliente deve ter entre 3 e 100 caracteres!")]
		public string ClienteNome { get; set; }
		[Required(ErrorMessage = "O Email do cliente é obrigatório!")]
		[Display(Name = "Email do Cliente")]
		[EmailAddress(ErrorMessage = "O Email do cliente deve ter um formato válido! (Exemplo: nome@provedor.com).")]
		public string ClienteEmail { get; set; }
		[Required(ErrorMessage = "A Senha do cliente é obrigatória!")]
		[Display(Name = "Senha do Cliente")]
		[StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres!")]
		public string ClienteSenha { get; set; }
		[Required(ErrorMessage = "O CPF do cliente é obrigatório!")]
		[Display(Name = "CPF do Cliente")]
		[StringLength(11, MinimumLength = 11, ErrorMessage = "O nome do cliente deve ter 11 caracteres!")]
		public string ClienteCPF { get; set; }
		[Display(Name = "CEP do Cliente")]
		[StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP do cliente deve ter 8 caracteres!")]
		public string ClienteCEP { get; set; }
		[Display(Name = "Telefone do Cliente")]
		[StringLength(11, MinimumLength = 11, ErrorMessage = "O Telefone do cliente deve ter 11 caracteres!")]
		public string ClienteTelefone { get; set; }

	}
}
