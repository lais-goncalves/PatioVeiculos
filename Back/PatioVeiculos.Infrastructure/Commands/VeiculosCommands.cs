using Microsoft.EntityFrameworkCore;
using PatioVeiculos.Domain.Models;
using PatioVeiculos.Infrastructure.Commands.Interfaces;
using PatioVeiculos.Infrastructure.Contexts;

namespace PatioVeiculos.Infrastructure.Commands;

public class VeiculosCommands : IVeiculosCommands
{
	private readonly PatioVeiculosContext _context;

	public VeiculosCommands(PatioVeiculosContext context)
	{
		_context = context;
	}
	
	public async Task<Veiculo?> BuscarVeiculoPorIdAsync(int id)
	{
		IQueryable<Veiculo> query = _context
		                            .Veiculos
		                            .IgnoreAutoIncludes();

		query = query
		        .Include(v => v.Movimentacoes)
		        .ThenInclude(m => m.PrecoHoras);

		Veiculo? veiculoBuscado = await query.FirstOrDefaultAsync(v => v.Id == id);
		return veiculoBuscado;
	}

	public async Task<Veiculo?> BuscarVeiculoPorPlacaAsync(string placa)
	{
		IQueryable<Veiculo> query = _context
		                            .Veiculos
		                            .IgnoreAutoIncludes();

		Veiculo? veiculoBuscado = await query.FirstOrDefaultAsync(v => v.Placa == placa);
		
		return veiculoBuscado;
	}

	public async Task<Veiculo[]> BuscarVeiculosNoPatioAsync()
	{
		IQueryable<Veiculo> query = _context
		                            .Veiculos
		                            .IgnoreAutoIncludes();

		query = query.Include(v => v.Movimentacoes);

		query = query.Where(v =>
			                    v.Movimentacoes
			                     .FirstOrDefault(m => m.SessaoAberta) != null
		                   );

		Veiculo[] veiculosBuscados = await query.ToArrayAsync();
		return veiculosBuscados;
	}
	
	public async Task<Veiculo[]> BuscarTodosOsVeiculosAsync()
	{
		IQueryable<Veiculo> query = _context
		                            .Veiculos
		                            .IgnoreAutoIncludes();

		query = query.Include(v => v.Movimentacoes);
		query = query.OrderBy(v => v.Movimentacoes.Max(m => m.DataHoraEntrada));

		Veiculo[] veiculosBuscados = await query.ToArrayAsync();
		return veiculosBuscados;
	}
	
	public async Task<bool> VerificarVeiculoNoPatioAsync(string placa)
	{
		IQueryable<Veiculo> query = _context
		                            .Veiculos
		                            .IgnoreAutoIncludes()
		                            .Include(v => v.Movimentacoes);
		
		bool existeVeiculoNoPatio = await query.AnyAsync(v => 
										                       v.Placa == placa && 
										                       v.Movimentacoes.Any(m => m.SessaoAberta)
									                      );
    
		return existeVeiculoNoPatio;
	}
	
	public async Task<bool> VerificarVeiculoNoPatioAsync(int id)
	{
		IQueryable<Veiculo> query = _context
		                            .Veiculos
		                            .IgnoreAutoIncludes()
		                            .Include(v => v.Movimentacoes);
		
		bool existeVeiculoNoPatio = await query.AnyAsync(v => 
			                                                 v.Id == id && 
			                                                 v.Movimentacoes.Any(m => m.SessaoAberta)
		                                                );
    
		return existeVeiculoNoPatio;
	}
	
	public async Task<bool> VerificarPlacaCadastradaAsync(string placa)
	{
		bool placaCadastrada = await _context
		                             .Veiculos
		                             .AnyAsync(v => v.Placa == placa);
    
		return placaCadastrada;
	}

	public async Task<Veiculo> CadastrarVeiculoAsync(Veiculo veiculo)
	{
		Veiculo? veiculoComPlacaExistente = await BuscarVeiculoPorPlacaAsync(veiculo.Placa);
		if (veiculoComPlacaExistente != null)
			throw new Exception($"Já existe um veículo cadastrado com a placa '{veiculo.Placa}'.");

		await _context.AddAsync(veiculo);

		int resultado = await _context.SaveChangesAsync();
		if (resultado <= 0) throw new Exception("Ocorreu um erro inesperado ao cadastrar veículo.");

		Veiculo? veiculoCadastrado = await BuscarVeiculoPorIdAsync(veiculo.Id);
		if (veiculoCadastrado == null) throw new Exception("Ocorreu um erro inesperado ao cadastrar veículo.");

		return veiculoCadastrado;
	}

	public async Task<Veiculo> AtualizarVeiculoAsync(Veiculo veiculo)
	{
		_context.Update(veiculo);

		int resultado = await _context.SaveChangesAsync();
		if (resultado <= 0) throw new Exception("Ocorreu um erro inesperado ao atualizar veículo.");

		Veiculo? veiculoAtualizado = await BuscarVeiculoPorIdAsync(veiculo.Id);
		if (veiculoAtualizado == null) throw new Exception("Ocorreu um erro inesperado ao atualizar veículo.");

		return veiculoAtualizado;
	}
}