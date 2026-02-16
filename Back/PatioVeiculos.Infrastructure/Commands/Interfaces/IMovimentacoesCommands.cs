using PatioVeiculos.Domain.Models;

namespace PatioVeiculos.Infrastructure.Commands.Interfaces;

public interface IMovimentacoesCommands
{
	public Task<Movimentacao> BuscarMovimentacaoAbertaPorVeiculoAsync(int veiculoId);
	public Decimal CalcularPrecoHoras(Movimentacao movimentacao);
	public Task<Movimentacao> CadastrarEntradaAsync(Movimentacao movimentacao);
	public Task<Movimentacao> CadastrarSaidaAsync(Movimentacao movimentacao);
}