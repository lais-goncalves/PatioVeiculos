namespace PatioVeiculos.Application.DTOs.Relatorios;

public struct VeiculoPorTempoDTO
{
	public int VeiculoId { get; set; }
	public string Placa { get; set; }
	public long TempoTotalMinutos { get; set; }
}