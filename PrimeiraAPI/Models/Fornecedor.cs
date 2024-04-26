using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
	public class Fornecedor 
	{
		public Guid FornecedorId { get; set; }
		[Required(ErrorMessage = "O nome do fornecedor é obrigatório!")]
		[Display(Name = "Nome do Fornecedor")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do fornecedor deve ter entre 3 e 100 caracteres!")]
		public string FornecedorNome { get; set; }
		[Required(ErrorMessage = "O telefone do fornecedor é obrigatório!")]
		[Display(Name = "Telefone do Fornecedor")]
		[StringLength(11, MinimumLength = 11, ErrorMessage = "O Telefone do fornecedor deve ter 11 caracteres!")]
		public string FornecedorTelefone { get; set; }
	}
}
