using System.ComponentModel.DataAnnotations;

namespace PatioVeiculos.Domain.Models;

public class PrecoHoras
{
	[Key] public int Id { get; set; }

	public decimal PrimeiraHora { get; set; }
	public decimal DemaisHoras { get; set; }

	public DateTime AplicadoEm { get; set; }

	public List<Movimentacao> Movimentacoes { get; set; }
}