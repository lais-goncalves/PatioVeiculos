using PatioVeiculos.Application.DTOs;
using PatioVeiculos.Application.DTOs.Registro;

namespace PatioVeiculos.Application.Services.Interfaces;

public interface IMovimentacoesServices
{
	public Task<VeiculoSaidaDTO> BuscarInformacoesSaida(MovimentacaoRegistroDTO movimentacaoDTO);
	public Task<MovimentacaoDTO> CadastrarEntradaAsync(MovimentacaoRegistroDTO movimentacaoDTO);
	public Task<MovimentacaoDTO> CadastrarSaidaAsync(MovimentacaoRegistroDTO movimentacaoDTO);
}