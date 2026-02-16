using PatioVeiculos.Domain.Models.Relatorios;

namespace PatioVeiculos.Infrastructure.Commands.Interfaces;

public interface IRelatoriosCommands
{
	public Task<FaturamentoDia[]> BuscarFaturamentoPorDiaAsync(int dias);
	public Task<VeiculoTempo[]> BuscarTop10VeiculosPorTempoAsync(DateTime dataInicio, DateTime dataFim);
	public Task<OcupacaoHora[]> BuscarOcupacaoPorHoraAsync(DateTime dataInicio, DateTime dataFim);
}