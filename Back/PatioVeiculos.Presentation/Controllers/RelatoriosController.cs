using Microsoft.AspNetCore.Mvc;
using PatioVeiculos.Application.DTOs.Relatorios;
using PatioVeiculos.Application.Services.Interfaces;
using PatioVeiculos.Infrastructure.Commands.Interfaces;

namespace PatioVeiculos.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase
{
	private readonly IRelatoriosServices _relatoriosServices;

	public RelatoriosController(IRelatoriosServices relatoriosServices)
	{
		_relatoriosServices = relatoriosServices;
	}
	
	[HttpGet("faturamento/{dias}")]
	public async Task<IActionResult> BuscarFaturamentoPorDia(int dias)
	{
		try
		{
			FaturamentoDiaDTO[] faturamento = await _relatoriosServices.BuscarFaturamentoPorDiaAsync(dias);
			return Ok(faturamento);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}
	
	[HttpGet("top10-tempo")]
	public async Task<IActionResult> BuscarTop10VeiculosPorTempo(
		[FromQuery] DateTime dataInicio, 
		[FromQuery] DateTime dataFim)
	{
		try
		{
			VeiculoPorTempoDTO[] topVeiculos = await _relatoriosServices.BuscarTop10VeiculosPorTempoAsync(dataInicio, dataFim);
			return Ok(topVeiculos);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}
	
	[HttpGet("relatorio/ocupacao-hora")]
	public async Task<IActionResult> BuscarOcupacaoPorHora(
		[FromQuery] DateTime dataInicio, 
		[FromQuery] DateTime dataFim)
	{
		try
		{
			OcupacaoHoraDTO[] ocupacao = await _relatoriosServices.BuscarOcupacaoPorHoraAsync(dataInicio, dataFim);
			return Ok(ocupacao);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}
}