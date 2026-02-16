using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PatioVeiculos.Domain.Models;

[Index(nameof(Placa), IsUnique = true)]
public class Veiculo
{
	[Key] public int Id { get; set; }

	[StringLength(9, MinimumLength = 7)]
	public string Placa { get; set; }

	[StringLength(20, MinimumLength = 2)]
	public string Modelo { get; set; }

	[StringLength(20, MinimumLength = 3)]
	public string Cor { get; set; }

	[StringLength(20, MinimumLength = 2)]
	public string Tipo { get; set; }

	public List<Movimentacao> Movimentacoes { get; set; }
}