using AutoMapper;
using PatioVeiculos.Application.DTOs.Relatorios;
using PatioVeiculos.Application.Services.Interfaces;
using PatioVeiculos.Domain.Models.Relatorios;
using PatioVeiculos.Infrastructure.Commands;
using PatioVeiculos.Infrastructure.Commands.Interfaces;

namespace PatioVeiculos.Application.Services;

public class RelatoriosServices : IRelatoriosServices
{
	private readonly IRelatoriosCommands _relatoriosCommands;
	private readonly IMapper _mapper;

	public RelatoriosServices(
		IRelatoriosCommands relatoriosCommands,
		IMapper mapper
	)
	{
		_relatoriosCommands = relatoriosCommands;
		_mapper = mapper;
	}
	
	public async Task<FaturamentoDiaDTO[]> BuscarFaturamentoPorDiaAsync(int dias)
	{
		try
		{
			if (dias != 7 && dias != 30)
			{
				throw new ArgumentException("Período deve ser 7 ou 30 dias.");
			}
			
			FaturamentoDia[] faturamento = await _relatoriosCommands.BuscarFaturamentoPorDiaAsync(dias);
			FaturamentoDiaDTO[] faturamentoDTO = _mapper.Map<FaturamentoDiaDTO[]>(faturamento);

			return faturamentoDTO;
		}
		catch (Exception)
		{
			throw new Exception($"Não foi possível buscar faturamento.");
		}
	}
	
	public async Task<VeiculoPorTempoDTO[]> BuscarTop10VeiculosPorTempoAsync(DateTime dataInicio, DateTime dataFim)
	{
		try
		{
			if (dataFim < dataInicio)
			{
				throw new ArgumentException("Data final deve ser maior que data inicial.");
			}

			VeiculoTempo[] topVeiculos = await _relatoriosCommands.BuscarTop10VeiculosPorTempoAsync(dataInicio, dataFim);
			VeiculoPorTempoDTO[] topVeiculosDTO = _mapper.Map<VeiculoPorTempoDTO[]>(topVeiculos);

			return topVeiculosDTO;
		}
		catch (Exception)
		{
			throw new Exception("Não foi possível buscar veículos.");
		}
	}
	
	public async Task<OcupacaoHoraDTO[]> BuscarOcupacaoPorHoraAsync(DateTime dataInicio, DateTime dataFim)
	{
		try
		{
			if (dataFim < dataInicio)
				throw new ArgumentException("Data final deve ser maior que data inicial.");

			if ((dataFim - dataInicio).TotalDays > 31)
			{
				throw new ArgumentException("Período não pode passar de 31 dias.");
			}

			OcupacaoHora[] ocupacao = await _relatoriosCommands.BuscarOcupacaoPorHoraAsync(dataInicio, dataFim);
			OcupacaoHoraDTO[] ocupacaoDTO = _mapper.Map<OcupacaoHoraDTO[]>(ocupacao);

			return ocupacaoDTO;
		}
		catch (Exception)
		{
			throw new Exception("Não foi possível buscar ocupação por hora.");
		}
	}
}