using Microsoft.AspNetCore.Mvc;
using PatioVeiculos.Application.DTOs;
using PatioVeiculos.Application.DTOs.Registro;
using PatioVeiculos.Application.Services.Interfaces;
using PatioVeiculos.Domain.Models;

namespace PatioVeiculos.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VeiculosController : ControllerBase
{
	private readonly IVeiculosServices _veiculosServices;

	public VeiculosController(IVeiculosServices veiculosServices)
	{
		_veiculosServices = veiculosServices;
	}
	
	[HttpGet("")]
	public async Task<IActionResult> Todos()
	{
		try
		{
			VeiculoDTO[] veiculos = await _veiculosServices.BuscarTodosOsVeiculosAsync();
			return Ok(veiculos);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	[HttpGet("id/{id}")]
	public async Task<IActionResult> Id(int id)
	{
		try
		{
			VeiculoDTO? veiculo = await _veiculosServices.BuscarVeiculoPorIdAsync(id);
			return Ok(veiculo);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	[HttpGet("placa/{placa}")]
	public async Task<IActionResult> Placa(string placa)
	{
		try
		{
			VeiculoDTO? veiculo = await _veiculosServices.BuscarVeiculoPorPlacaAsync(placa);
			return Ok(veiculo);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	[HttpGet("no-patio")]
	public async Task<IActionResult> NoPatio()
	{
		try
		{
			VeiculoDTO[] veiculos = await _veiculosServices.BuscarVeiculosNoPatioAsync();
			return Ok(veiculos);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	[HttpGet("no-patio/{placa}")]
	public async Task<IActionResult> VerificarVeiculoNoPatio(string placa)
	{
		try
		{
			bool veiculoNoPatioResult = await _veiculosServices.VerificarVeiculoNoPatioAsync(placa);
        
			return Ok(new 
			{ 
				placa = placa,
				noPatio = veiculoNoPatioResult 
			});
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}
	
	[HttpGet("verificar-placa/{placa}")]
	public async Task<IActionResult> VerificarPlacaCadastrada(string placa)
	{
		try
		{
			bool placaCadastradaResultado = await _veiculosServices.VerificarPlacaCadastradaAsync(placa);
        
			return Ok(new 
			{ 
				placa = placa,
				estaCadastrada = placaCadastradaResultado 
			});
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}
	
	
	[HttpPost("cadastrar")]
	public async Task<IActionResult> Cadastrar([FromBody] VeiculoRegistroDTO veiculo)
    {
    	try
	    {
		    VeiculoDTO veiculoCadastrado = await _veiculosServices.CadastrarVeiculoAsync(veiculo);
		    return Ok(veiculoCadastrado);
	    }
    	
    	catch (Exception e)
    	{
			return BadRequest(e.Message);
    	}
    }
	
	
	[HttpPut("editar/{id}")]
	public async Task<IActionResult> Editar(int id, [FromBody] VeiculoRegistroDTO veiculoRegistroDto)
	{
		try
		{
			VeiculoDTO veiculo = await _veiculosServices.AtualizarVeiculoAsync(id, veiculoRegistroDto);
			return Ok(veiculo);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
}