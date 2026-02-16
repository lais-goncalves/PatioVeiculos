using PatioVeiculos.Application.DTOs.Relatorios;

namespace PatioVeiculos.Application.Services.Interfaces;

public interface IRelatoriosServices
{
	public Task<FaturamentoDiaDTO[]> BuscarFaturamentoPorDiaAsync(int dias);
	public Task<VeiculoPorTempoDTO[]> BuscarTop10VeiculosPorTempoAsync(DateTime dataInicio, DateTime dataFim);
	public Task<OcupacaoHoraDTO[]> BuscarOcupacaoPorHoraAsync(DateTime dataInicio, DateTime dataFim);
}