using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatioVeiculos.Domain.Models;

namespace PatioVeiculos.Infrastructure.Contexts;

public class PatioVeiculosContext : DbContext
{
	public PatioVeiculosContext(DbContextOptions options) : base(options) { }
	public DbSet<Veiculo> Veiculos { get; set; }
	public DbSet<Movimentacao> Movimentacoes { get; set; }
	public DbSet<PrecoHoras> PrecosHoras { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder
			.Entity<Veiculo>()
			.HasMany(v => v.Movimentacoes)
			.WithOne(m => m.Veiculo)
			.HasForeignKey(m => m.VeiculoId);

		builder
			.Entity<PrecoHoras>()
			.HasMany(p => p.Movimentacoes)
			.WithOne(m => m.PrecoHoras)
			.HasForeignKey(m => m.PrecoHorasId);
	}
}