using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PatioVeiculos.Domain.Models;

public class Movimentacao
{
	[Key] public int Id { get; set; }

	public DateTime DataHoraEntrada { get; set; }
	public DateTime? DataHoraSaida { get; set; }
	public decimal? ValorCobrado { get; set; }

	[DefaultValue(false)] public bool SessaoAberta { get; set; }

	public int VeiculoId { get; set; }
	public Veiculo Veiculo { get; set; }

	public int PrecoHorasId { get; set; }
	public PrecoHoras PrecoHoras { get; set; }
}