namespace PatioVeiculos.Application.DTOs.Registro;

public struct MovimentacaoRegistroDTO
{
	public DateTime? DataHoraEntrada { get; set; }
	public DateTime? DataHoraSaida { get; set; }
	public string PlacaVeiculo { get; set; }
}