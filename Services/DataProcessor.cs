using DataTest.Data;
using DataTest.Models;

namespace DataTest.Services
{
    public class DataProcessor
    {
        private readonly MyCsvReader _csvReader;

        public DataProcessor(MyCsvReader csvReader)
        {
            _csvReader = csvReader;
        }

        public List<DailyEnergyConsumption> GetDailyEnergyConsumption()
        {
            var data = _csvReader.ReadDataFromOneHouseHoldOneWeek();

            var groupedData = data
                .GroupBy(d => d.AtDateTime.Date)
                .Select(g => new DailyEnergyConsumption
                {
                    Date = g.Key,
                    TotalEnergyOutlet = g.Sum(d => d.ActiveEnergyOutlet)
                })
                .ToList();

            return groupedData;
        }
    }

   
}

