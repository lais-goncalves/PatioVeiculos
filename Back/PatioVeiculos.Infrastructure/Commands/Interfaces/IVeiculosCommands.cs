using PatioVeiculos.Domain.Models;

namespace PatioVeiculos.Infrastructure.Commands.Interfaces;

public interface IVeiculosCommands
{
	public Task<Veiculo?> BuscarVeiculoPorIdAsync(int id);
	public Task<Veiculo?> BuscarVeiculoPorPlacaAsync(string placa);
	public Task<Veiculo[]> BuscarTodosOsVeiculosAsync();
	public Task<Veiculo[]> BuscarVeiculosNoPatioAsync();
	public Task<bool> VerificarVeiculoNoPatioAsync(string placa);
	public Task<bool> VerificarVeiculoNoPatioAsync(int id);
	public Task<bool> VerificarPlacaCadastradaAsync(string placa);
	public Task<Veiculo> CadastrarVeiculoAsync(Veiculo veiculo);
	public Task<Veiculo> AtualizarVeiculoAsync(Veiculo veiculo);
}