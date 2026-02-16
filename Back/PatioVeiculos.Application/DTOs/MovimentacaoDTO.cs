namespace PatioVeiculos.Application.DTOs;

public struct MovimentacaoDTO
{
	public int Id { get; set; }
	public string DataHoraEntrada { get; set; }
	public string? DataHoraSaida { get; set; }
	public decimal? ValorCobrado { get; set; }
	public bool SessaoAberta { get; set; }

	public int VeiculoId { get; set; }
	// public VeiculoDTO Veiculo { get; set; }
}