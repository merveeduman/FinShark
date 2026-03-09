using System;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Newtonsoft.Json;

namespace api.Service
{
    public class FMPService : IFMPService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public FMPService(HttpClient httpClient, IConfiguration config, IMapper mapper)
        {
            _httpClient = httpClient;
            _config = config;
            _mapper = mapper;
        }

        public async Task<Stock?> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var result = await _httpClient.GetAsync(
                    $"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_config["FMPKey"]}"
                );

                if (!result.IsSuccessStatusCode)
                    return null;

                var content = await result.Content.ReadAsStringAsync();
                var stocks = JsonConvert.DeserializeObject<FMPStock[]>(content);

                if (stocks == null || stocks.Length == 0)
                    return null;

                var fmpStock = stocks[0];
                if (fmpStock == null)
                    return null;

                return _mapper.Map<Stock>(fmpStock);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}