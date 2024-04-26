using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Data
{
	public class MyContext : IdentityDbContext
	{
		public MyContext(DbContextOptions<MyContext> options) : base(options) { }
		public DbSet<Animal> Animais { get; set; }
		public DbSet<Cliente> Clientes { get; set; }
		public DbSet<Venda> Vendas { get; set; }
		public DbSet<Pagamento> Pagamentos { get; set; }
		public DbSet<ProdutoVenda> ProdutosVendas { get; set; }
		public DbSet<Produto> Produtos { get; set; }
		public DbSet<Fornecedor> Fornecedores { get; set; }
		public DbSet<Servico> Servicos { get; set; }
		public DbSet<Plano> Planos { get; set; }
		public DbSet<Vacinacao> Vacinacoes { get; set; }
		public DbSet<BanhoTosa> BanhosTosas { get; set; }
		public DbSet<Consulta> Consultas { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Cliente>().ToTable("Clientes");
			modelBuilder.Entity<Venda>().ToTable("Vendas");
			modelBuilder.Entity<Pagamento>().ToTable("Pagamentos");
			modelBuilder.Entity<ProdutoVenda>().ToTable("ProdutosVendas");
			modelBuilder.Entity<Animal>().ToTable("Animais");
			modelBuilder.Entity<Produto>().ToTable("Produtos");
			modelBuilder.Entity<Fornecedor>().ToTable("Fornecedores");
			modelBuilder.Entity<Servico>().ToTable("Servicos");
			modelBuilder.Entity<Plano>().ToTable("Planos");
			modelBuilder.Entity<Vacinacao>().ToTable("Vacinacoes");
			modelBuilder.Entity<BanhoTosa>().ToTable("BanhosTosas");
			modelBuilder.Entity<Consulta>().ToTable("Consultas");


		}
	}
}
