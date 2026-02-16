namespace PatioVeiculos.Application.DTOs;

public struct VeiculoDTO
{
	public int Id { get; set; }
	public string Placa { get; set; }
	public string Modelo { get; set; }
	public string Cor { get; set; }
	public string Tipo { get; set; }

	public List<MovimentacaoDTO> Movimentacoes { get; set; }
}