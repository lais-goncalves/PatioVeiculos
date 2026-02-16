using Microsoft.AspNetCore.Mvc;
using PatioVeiculos.Application.DTOs;
using PatioVeiculos.Application.DTOs.Registro;
using PatioVeiculos.Application.Services.Interfaces;
using PatioVeiculos.Domain.Models;

namespace PatioVeiculos.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacoesController : ControllerBase
{
	private readonly IMovimentacoesServices _movimentacoesServices;

	public MovimentacoesController(IMovimentacoesServices movimentacoesServices)
	{
		_movimentacoesServices = movimentacoesServices;
	}
	
	[HttpPost("info-saida")]
	public async Task<IActionResult> BuscarInfoVeiculoSaida(MovimentacaoRegistroDTO movimentacaoDTO)
	{
		try
		{
			VeiculoSaidaDTO movimentacaoCadastrada = await _movimentacoesServices.BuscarInformacoesSaida(movimentacaoDTO);
			return Ok(movimentacaoCadastrada);
		}
    	
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	[HttpPost("cadastrar-entrada")]
	public async Task<IActionResult> Cadastrar([FromBody] MovimentacaoRegistroDTO movimentacaoDTO)
    {
    	try
	    {
		    MovimentacaoDTO movimentacaoCadastrada = await _movimentacoesServices.CadastrarEntradaAsync(movimentacaoDTO);
		    return Ok(movimentacaoCadastrada);
	    }
    	
    	catch (Exception e)
    	{
			return BadRequest(e.Message);
    	}
    }
	
	[HttpPost("cadastrar-saida")]
	public async Task<IActionResult> CadastrarSaidaAsync(MovimentacaoRegistroDTO movimentacaoDTO)
	{
		try
		{
			MovimentacaoDTO movimentacaoCadastrada = await _movimentacoesServices.CadastrarSaidaAsync(movimentacaoDTO);
			return Ok(movimentacaoCadastrada);
		}
    	
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
}