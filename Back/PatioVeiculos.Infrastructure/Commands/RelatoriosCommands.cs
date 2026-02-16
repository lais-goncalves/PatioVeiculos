using Microsoft.EntityFrameworkCore;
using PatioVeiculos.Domain.Models.Relatorios;
using PatioVeiculos.Infrastructure.Commands.Interfaces;
using PatioVeiculos.Infrastructure.Contexts;

namespace PatioVeiculos.Infrastructure.Commands;

public class RelatoriosCommands : IRelatoriosCommands
{
	private readonly PatioVeiculosContext _context;

	public RelatoriosCommands(PatioVeiculosContext context)
	{
		_context = context;
	}
	
	public async Task<FaturamentoDia[]> BuscarFaturamentoPorDiaAsync(int dias)
	{
		DateTime dataInicio = DateTime.Now.Date.AddDays(-dias);
		DateTime dataFim = DateTime.Now.Date;

		var totalFaturado = await _context
		                          .Movimentacoes
		                          .Where(m => 
			                                 !m.SessaoAberta && 
			                                 m.DataHoraSaida.HasValue &&
			                                 m.DataHoraSaida.Value.Date >= dataInicio &&
			                                 m.DataHoraSaida.Value.Date <= dataFim
		                                )
		                          .SumAsync(m => m.ValorCobrado ?? 0m);

		var faturamento = new FaturamentoDia[]
		{
			new FaturamentoDia
			{
				Data = dataInicio,
				Total = totalFaturado
			}
		};
    
		return faturamento;
	}
	
	public async Task<VeiculoTempo[]> BuscarTop10VeiculosPorTempoAsync(DateTime dataInicio, DateTime dataFim)
	{
		var movimentacoes = await _context
		                          .Movimentacoes
		                          .Include(m => m.Veiculo)
		                          .Where(m => 
			                                 !m.SessaoAberta &&
			                                 m.DataHoraEntrada >= dataInicio &&
			                                 m.DataHoraSaida.HasValue &&
			                                 m.DataHoraSaida.Value <= dataFim
		                                )
		                          .ToListAsync();

		var topVeiculos = movimentacoes
		                  .GroupBy(m => new 
		                  { 
			                  m.VeiculoId,
			                  m.Veiculo.Placa
		                  })
		                  .Select(g => new VeiculoTempo
		                  {
			                  VeiculoId = g.Key.VeiculoId,
			                  Placa = g.Key.Placa,
			                  TempoTotalMinutos = g.Sum(m => 
				                                            (m.DataHoraSaida.Value - m.DataHoraEntrada).TotalMinutes
			                                           )
		                  })
		                  .OrderByDescending(x => x.TempoTotalMinutos)
		                  .Take(10)
		                  .ToArray();
    
		return topVeiculos;
	}
	
	public async Task<OcupacaoHora[]> BuscarOcupacaoPorHoraAsync(DateTime dataInicio, DateTime dataFim)
	{
		// Gera todas as horas do período
		var todasHoras = new List<DateTime>();
		for (DateTime data = dataInicio; data <= dataFim; data = data.AddHours(1))
		{
			todasHoras.Add(data);
		}

		var movimentacoes = await _context
		                          .Movimentacoes
		                          .Where(m => 
			                                 m.DataHoraEntrada <= dataFim &&
			                                 (!m.DataHoraSaida.HasValue || m.DataHoraSaida.Value >= dataInicio)
		                                )
		                          .Select(m => new
		                          {
			                          m.DataHoraEntrada,
			                          DataHoraSaida = m.DataHoraSaida ?? DateTime.MaxValue
		                          })
		                          .ToListAsync();

		var ocupacaoPorHora = todasHoras
		                      .Select(hora => new OcupacaoHora
		                      {
			                      DataHora = hora,
			                      Quantidade = movimentacoes.Count(m => 
				                                                       m.DataHoraEntrada <= hora && 
				                                                       m.DataHoraSaida >= hora
			                                                      )
		                      })
		                      .ToArray();

		return ocupacaoPorHora;
	}
}