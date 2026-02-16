using AutoMapper;
using PatioVeiculos.Application.DTOs;
using PatioVeiculos.Application.DTOs.Registro;
using PatioVeiculos.Application.Services.Interfaces;
using PatioVeiculos.Domain.Models;
using PatioVeiculos.Infrastructure.Commands.Interfaces;

namespace PatioVeiculos.Application.Services;

public class MovimentacoesServices : IMovimentacoesServices
{
	private readonly IMapper _mapper;
	private readonly IMovimentacoesCommands _movimentacoesCommands;
	private readonly IVeiculosCommands _veiculosCommands;

	public MovimentacoesServices(
		IMovimentacoesCommands movimentacoesCommands,
		IVeiculosCommands veiculosCommands,
		IMapper mapper
	)
	{
		_movimentacoesCommands = movimentacoesCommands;
		_veiculosCommands = veiculosCommands;
		_mapper = mapper;
	}

	public async Task<VeiculoSaidaDTO> BuscarInformacoesSaida(MovimentacaoRegistroDTO movimentacaoDTO)
	{
		Movimentacao movimentacaoASerVerificada = _mapper.Map<Movimentacao>(movimentacaoDTO);
		
		Veiculo? veiculo = _veiculosCommands.BuscarVeiculoPorPlacaAsync(movimentacaoDTO.PlacaVeiculo).Result;
		if (veiculo == null)
		{
			throw new Exception("A movimentação deve estar atrelada a um veículo cadastrado.");
		}

		Movimentacao movimentacaoExistente =
			await _movimentacoesCommands.BuscarMovimentacaoAbertaPorVeiculoAsync(veiculo.Id);
		
		movimentacaoExistente.DataHoraSaida = movimentacaoASerVerificada.DataHoraSaida ?? DateTime.UtcNow;
		Decimal valorCobrado = _movimentacoesCommands.CalcularPrecoHoras(movimentacaoExistente);
		movimentacaoExistente.ValorCobrado = valorCobrado;

		VeiculoSaidaDTO veiculoSaidaDto = new VeiculoSaidaDTO();
		veiculoSaidaDto.Movimentacao = _mapper.Map<MovimentacaoDTO>(movimentacaoExistente);
		veiculoSaidaDto.Veiculo = _mapper.Map<VeiculoDTO>(veiculo);

		return veiculoSaidaDto;
	}
	
	public async Task<MovimentacaoDTO> CadastrarEntradaAsync(MovimentacaoRegistroDTO movimentacaoDTO)
	{
		try
		{
			Movimentacao movimentacaoASerAberta = _mapper.Map<Movimentacao>(movimentacaoDTO);
			Veiculo? veiculo = _veiculosCommands.BuscarVeiculoPorPlacaAsync(movimentacaoDTO.PlacaVeiculo).Result;
			if (veiculo == null)
			{
				throw new Exception("A movimentação deve estar atrelada a um veículo cadastrado.");
			}
			
			movimentacaoASerAberta.VeiculoId = veiculo.Id;
			
			Movimentacao movimentacaoAberta = _movimentacoesCommands.CadastrarEntradaAsync(movimentacaoASerAberta).Result;
			MovimentacaoDTO movimentacaoAbertaDTO = _mapper.Map<MovimentacaoDTO>(movimentacaoAberta);
			return movimentacaoAbertaDTO;
		}
		
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			throw new Exception("Não foi possível cadastrar entrada.");
		}
	}

	public async Task<MovimentacaoDTO> CadastrarSaidaAsync(MovimentacaoRegistroDTO movimentacaoDTO)
	{
		try
		{
			Movimentacao movimentacaoASerEncerrada = _mapper.Map<Movimentacao>(movimentacaoDTO);
			Veiculo? veiculo = _veiculosCommands.BuscarVeiculoPorPlacaAsync(movimentacaoDTO.PlacaVeiculo).Result;
			if (veiculo == null)
			{
				throw new Exception("A movimentação deve estar atrelada a um veículo cadastrado.");
			}
			
			movimentacaoASerEncerrada.VeiculoId = veiculo.Id;
			
			Movimentacao movimentacaoEncerrada = _movimentacoesCommands.CadastrarSaidaAsync(movimentacaoASerEncerrada).Result;
			MovimentacaoDTO movimentacaoEncerradaDTO = _mapper.Map<MovimentacaoDTO>(movimentacaoEncerrada);
			return movimentacaoEncerradaDTO;
		}
		
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			throw new Exception("Não foi possível cadastrar entrada.");
		}
	}
}