using AutoMapper;
using PatioVeiculos.Application.DTOs;
using PatioVeiculos.Application.DTOs.Registro;
using PatioVeiculos.Application.DTOs.Relatorios;
using PatioVeiculos.Domain.Models;
using PatioVeiculos.Domain.Models.Relatorios;

namespace PatioVeiculos.Application.Helpers;

public class PatioVeiculosProfile : Profile
{
	public PatioVeiculosProfile()
	{
		CreateMap<Veiculo, VeiculoDTO>().ReverseMap();
		CreateMap<Veiculo, VeiculoRegistroDTO>().ReverseMap();
		
		CreateMap<Movimentacao, MovimentacaoDTO>().ReverseMap();
		CreateMap<Movimentacao, MovimentacaoRegistroDTO>().ReverseMap();
		
		CreateMap<FaturamentoDia, FaturamentoDiaDTO>()
			.ForMember(dest => dest.TotalFaturado, opt => opt.MapFrom(src => src.Total));
		
		CreateMap<VeiculoTempo, VeiculoPorTempoDTO>()
			.ForMember(dest => dest.TempoTotalMinutos, 
			           opt => opt.MapFrom(src => (long)src.TempoTotalMinutos));
		
		CreateMap<OcupacaoHora, OcupacaoHoraDTO>()
			.ForMember(dest => dest.QuantidadeVeiculos, 
			           opt => opt.MapFrom(src => src.Quantidade));
	}
}