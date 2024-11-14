using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DataTest.Models;
using Newtonsoft.Json;

public class ElectricityPriceService
{
    private readonly HttpClient _httpClient;

    public ElectricityPriceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ElectricityPrice>> GetHourlyElectricityPricesAsync(string region)
    {
        // Bygg URL baserat på aktuellt datum och region enligt API-specifikationen
        string year = DateTime.Now.ToString("yyyy");
        string month = DateTime.Now.ToString("MM");
        string day = DateTime.Now.ToString("dd");
        string url = $"https://www.elprisetjustnu.se/api/v1/prices/{year}/{month}-{day}_{region}.json";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var prices = JsonConvert.DeserializeObject<List<ApiEnergyPrice>>(jsonString);

                Console.WriteLine("API Response Data:");
                Console.WriteLine(jsonString);

                var pricelist = new List<ElectricityPrice>(); 

                foreach(var price in prices)
                {
                    pricelist.Add(new ElectricityPrice
                    {
                        Time = price.time_start,
                        Price = price.SEK_per_kWh
                    });
                }

                return pricelist ?? new List<ElectricityPrice>(); // Returnera en tom lista om deserialiseringen returnerar null
            }
            else
            {
                Console.WriteLine($"API response status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching electricity prices: {ex.Message}");
        }

        return new List<ElectricityPrice>();
    }
}
