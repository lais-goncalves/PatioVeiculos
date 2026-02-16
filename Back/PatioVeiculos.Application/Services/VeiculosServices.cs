using AutoMapper;
using PatioVeiculos.Application.DTOs;
using PatioVeiculos.Application.DTOs.Registro;
using PatioVeiculos.Application.Services.Interfaces;
using PatioVeiculos.Domain.Models;
using PatioVeiculos.Infrastructure.Commands.Interfaces;

namespace PatioVeiculos.Application.Services;

public class VeiculosServices : IVeiculosServices
{
	private readonly IMapper _mapper;
	private readonly IVeiculosCommands _veiculosCommands;

	public VeiculosServices(
		IVeiculosCommands veiculosCommands,
		IMapper mapper
	)
	{
		_veiculosCommands = veiculosCommands;
		_mapper = mapper;
	}

	public async Task<VeiculoDTO?> BuscarVeiculoPorIdAsync(int id)
	{
		try {
			Veiculo? veiculo = await _veiculosCommands.BuscarVeiculoPorIdAsync(id);
			if (veiculo == null) return null;

			VeiculoDTO? veiculoDTO = _mapper.Map<VeiculoDTO>(veiculo);

			return veiculoDTO;
		}
		
		catch (Exception)
		{
			throw new Exception("Não foi possível buscar veículos.");
		}
	}

	public async Task<VeiculoDTO?> BuscarVeiculoPorPlacaAsync(string placa)
	{
		try {
			Veiculo? veiculo = await _veiculosCommands.BuscarVeiculoPorPlacaAsync(placa);
			if (veiculo == null) return null;

			VeiculoDTO? veiculoDTO = _mapper.Map<VeiculoDTO>(veiculo);

			return veiculoDTO;
		}
		
		catch (Exception)
		{
			throw new Exception("Não foi possível buscar veículos.");
		}
	}

	public async Task<VeiculoDTO[]> BuscarTodosOsVeiculosAsync()
	{
		try
		{
			Veiculo[] veiculos = await _veiculosCommands.BuscarTodosOsVeiculosAsync();
			if (veiculos.Length <= 0) return [];

			VeiculoDTO[] veiculosDTO = _mapper.Map<VeiculoDTO[]>(veiculos);
			return veiculosDTO;
		}

		catch (Exception)
		{
			throw new Exception("Não foi possível buscar veículos.");
		}
	}
	
	public async Task<VeiculoDTO[]> BuscarVeiculosNoPatioAsync()
	{
		try
		{
			Veiculo[] veiculos = await _veiculosCommands.BuscarVeiculosNoPatioAsync();
			if (veiculos.Length <= 0) return [];

			VeiculoDTO[] veiculosDTO = _mapper.Map<VeiculoDTO[]>(veiculos);

			return veiculosDTO;
		}

		catch (Exception)
		{
			throw new Exception("Não foi possível buscar veículos.");
		}
	}
	
	public async Task<bool> VerificarVeiculoNoPatioAsync(string placa)
	{
		try
		{
			bool veiculoNoPatio = await _veiculosCommands.VerificarVeiculoNoPatioAsync(placa);
			return veiculoNoPatio;
		}
		catch (Exception)
		{
			throw new Exception($"Não foi possível verificar se o veículo {placa} está no pátio.");
		}
	}
	
	public async Task<bool> VerificarPlacaCadastradaAsync(string placa)
	{
		try
		{
			bool placaCadastrada = await _veiculosCommands.VerificarPlacaCadastradaAsync(placa);
			return placaCadastrada;
		}
		catch (Exception)
		{
			throw new Exception($"Não foi possível verificar se a placa {placa} está cadastrada.");
		}
	}

	public async Task<VeiculoDTO> AtualizarVeiculoAsync(int id, VeiculoRegistroDTO veiculoDTO)
	{
		try
		{
			Veiculo? veiculoExistente = _veiculosCommands.BuscarVeiculoPorIdAsync(id).Result;
			if (veiculoExistente == null)
			{
				throw new Exception($"Veículo #{id} inexistente.");
			}
			
			string _placa = veiculoExistente.Placa;
			Veiculo veiculo = _mapper.Map(veiculoDTO, veiculoExistente);

			veiculo.Id = id;
			veiculo.Placa = _placa;
			
			Veiculo veiculoAtualizado = await _veiculosCommands.AtualizarVeiculoAsync(veiculo);
			VeiculoDTO veiculoAtualizadoDTO = _mapper.Map<VeiculoDTO>(veiculoAtualizado);

			return veiculoAtualizadoDTO;
		}
		
		catch (Exception)
		{
			throw new Exception("Não foi possível atualizar veículo.");
		}
	}

	public async Task<VeiculoDTO> CadastrarVeiculoAsync(VeiculoRegistroDTO veiculoDTO)
	{
		try
		{
			Veiculo veiculoASerCadastrado = _mapper.Map<Veiculo>(veiculoDTO);
			Veiculo veiculoCadastrado = _veiculosCommands.CadastrarVeiculoAsync(veiculoASerCadastrado).Result;
			if (veiculoCadastrado == null) throw new Exception();
			
			VeiculoDTO veiculoCadastradoDTO = _mapper.Map<VeiculoDTO>(veiculoCadastrado);
			return veiculoCadastradoDTO;
		}
		
		catch (Exception)
		{
			throw new Exception("Não foi possível cadastrar veículo.");
		}
	}
}