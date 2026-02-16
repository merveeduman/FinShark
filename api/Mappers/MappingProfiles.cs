using AutoMapper;
using api.Dtos.Stock;
using api.Models;
using api.Dtos;

namespace api.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Stock modelinden StockDto'ya dönüşüm yapabilirsin demek.
            // Property isimleri aynıysa otomatik eşleşir.
            CreateMap<Stock, StockDto>();
            CreateMap<StockDto, Stock>();
            CreateMap<StockDto, CreateStockRequestDto>();
            CreateMap<CreateStockRequestDto, Stock>();
            CreateMap<StockDto, UpdateStockRequestDto>();
            CreateMap<UpdateStockRequestDto, Stock>();
        }
    }
}