using Microsoft.EntityFrameworkCore;
using PatioVeiculos.Domain.Models;
using PatioVeiculos.Infrastructure.Commands.Interfaces;
using PatioVeiculos.Infrastructure.Contexts;

namespace PatioVeiculos.Infrastructure.Commands;

public class MovimentacoesCommands : IMovimentacoesCommands
{
	private readonly PatioVeiculosContext _context;
	private readonly IVeiculosCommands _veiculosCommands;

	public MovimentacoesCommands(PatioVeiculosContext context, IVeiculosCommands veiculosCommands)
	{
		_context = context;
		_veiculosCommands = veiculosCommands;
	}
	
	public async Task<Movimentacao?> BuscarMovimentacaoPorIdAsync(int id)
	{
		IQueryable<Movimentacao> query = _context
		                                 .Movimentacoes
		                                 .IgnoreAutoIncludes();
	
		query = query
				.Include(m => m.PrecoHoras);
	
		Movimentacao? movimentacaoBuscada = await query.FirstOrDefaultAsync(m => m.Id == id);
		return movimentacaoBuscada;
	}
	
	public async Task<Movimentacao> BuscarMovimentacaoAbertaPorVeiculoAsync(int veiculoId)
	{
		Movimentacao? movimentacao = await _context.Movimentacoes
		                                       .AsNoTracking()
		                                       .IgnoreAutoIncludes()
		                                       .Include(m => m.PrecoHoras)
		                                       .Where(m => 
			                                              m.VeiculoId == veiculoId && 
			                                              m.SessaoAberta == true
			                                   )
		                                       .FirstOrDefaultAsync();
		
		return movimentacao;
	}
	
	protected async Task<PrecoHoras> BuscarPrecoMaisRecenteAsync()
	{
		PrecoHoras? precoHoras = await _context.PrecosHoras
		                                        .AsNoTracking()
												.OrderByDescending(p => p.AplicadoEm)
												.FirstOrDefaultAsync();
		
		if (precoHoras == null)
		{
			throw new Exception("Não existe preço de horas cadastrado.");
		}
		
		return precoHoras;
	}

	public Decimal CalcularPrecoHoras(Movimentacao movimentacao)
	{
		if (movimentacao.DataHoraEntrada > movimentacao.DataHoraSaida)
		{
			throw new Exception("A data da saída deve ser maior do que a de entrada.");
		}
		
		TimeSpan? tempoEstacionado = movimentacao.DataHoraSaida - movimentacao.DataHoraEntrada;
		double horasEstacionado = tempoEstacionado?.TotalHours ?? 0;

		PrecoHoras precoMovimentacao = movimentacao.PrecoHoras;
		decimal precoFinal;
		double fracaoHora = 0.5;

		if (horasEstacionado <= 1)
		{
			precoFinal = precoMovimentacao.PrimeiraHora;
		}

		else
		{
			double demaisHoras = horasEstacionado - 1;
			
			double demaisHorasArredondadas = Math.Round(demaisHoras, MidpointRounding.AwayFromZero);
			decimal decDemaisHorasArredondadas = Convert.ToDecimal(demaisHorasArredondadas);
			
			decimal valorDemaisHoras = decDemaisHorasArredondadas * precoMovimentacao.DemaisHoras;
			precoFinal = precoMovimentacao.PrimeiraHora + valorDemaisHoras;
		}
		
		return precoFinal;
	}

	public async Task<Movimentacao> CadastrarEntradaAsync(Movimentacao movimentacao)
	{
		int veiculoId = movimentacao.VeiculoId;

		Veiculo? veiculoExiste = await _veiculosCommands.BuscarVeiculoPorIdAsync(veiculoId);
		if (veiculoExiste == null)
		{
			throw new Exception("A movimentação deve estar atrelada a um veículo cadastrado.");
		}
		

		bool veiculoJaEstaNoPatio = await _veiculosCommands.VerificarVeiculoNoPatioAsync(veiculoId);
		if (veiculoJaEstaNoPatio)
		{
			throw new Exception("Não é possível cadastrar entrada pois o veículo já está no pátio.");
		}

		PrecoHoras precoMaisRecente = await BuscarPrecoMaisRecenteAsync();
			
		var novaMovimentacao = new Movimentacao
		{
			PrecoHorasId = precoMaisRecente.Id,
			DataHoraEntrada = movimentacao.DataHoraEntrada,
			SessaoAberta = true,
			VeiculoId =  veiculoId
		};

		_context.Add(novaMovimentacao);

		int cadastrou = await _context.SaveChangesAsync();
		if (cadastrou <= 0)
		{
			throw new Exception("Ocorreu um erro inesperado ao cadastrar movimentação.");
		}
		
		Movimentacao? movimentacaoCadastrada = await BuscarMovimentacaoPorIdAsync(novaMovimentacao.Id);
		if (movimentacaoCadastrada == null)
		{
			throw new Exception("Ocorreu um erro inesperado ao cadastrar movimentação.");
		}
		
		return movimentacaoCadastrada;
	}

	public async Task<Movimentacao> CadastrarSaidaAsync(Movimentacao movimentacao)
	{
		int veiculoId = movimentacao.VeiculoId;

		Movimentacao? movimentacaoEmAberto = await BuscarMovimentacaoAbertaPorVeiculoAsync(veiculoId);
		if (movimentacaoEmAberto == null)
		{
			throw new Exception("Não é possível cadastrar saída pois o veículo não está no pátio.");
		}

		movimentacaoEmAberto.DataHoraSaida = movimentacao.DataHoraSaida ?? DateTime.UtcNow;
		decimal valorCobrado = CalcularPrecoHoras(movimentacaoEmAberto);

		Movimentacao movimentacaoEncerrada = new Movimentacao
		{
			Id = movimentacaoEmAberto.Id,
			SessaoAberta = false,
			DataHoraEntrada = movimentacaoEmAberto.DataHoraEntrada,
			DataHoraSaida = movimentacaoEmAberto.DataHoraSaida,
			PrecoHorasId = movimentacaoEmAberto.PrecoHorasId,
			VeiculoId = veiculoId,
			ValorCobrado = valorCobrado
		};

		_context.Update(movimentacaoEncerrada);
		
		var salvou = await _context.SaveChangesAsync();
		if (salvou <= 0)
		{
			throw new Exception("Houve um erro ao tentar cadastrar saída.");
		}

		Movimentacao? movimentacaoCadastrada = await BuscarMovimentacaoPorIdAsync(movimentacaoEncerrada.Id);
		if (movimentacaoCadastrada == null)
		{
			throw new Exception("Houve um erro ao tentar cadastrar saída.");
		}
		
		return movimentacaoCadastrada;
	}
}