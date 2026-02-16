using PatioVeiculos.Application.DTOs;
using PatioVeiculos.Application.DTOs.Registro;
using PatioVeiculos.Domain.Models;

namespace PatioVeiculos.Application.Services.Interfaces;

public interface IVeiculosServices
{
	public Task<VeiculoDTO?> BuscarVeiculoPorIdAsync(int id);
	public Task<VeiculoDTO?> BuscarVeiculoPorPlacaAsync(string placa);
	public Task<VeiculoDTO[]> BuscarTodosOsVeiculosAsync();
	public Task<VeiculoDTO[]> BuscarVeiculosNoPatioAsync();
	public Task<bool> VerificarVeiculoNoPatioAsync(string placa);
	public Task<bool> VerificarPlacaCadastradaAsync(string placa);
	public Task<VeiculoDTO> AtualizarVeiculoAsync(int id, VeiculoRegistroDTO veiculoDTO);
	public Task<VeiculoDTO> CadastrarVeiculoAsync(VeiculoRegistroDTO veiculo);
}