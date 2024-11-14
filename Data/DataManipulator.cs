using CsvHelper;
using DataTest.Services;
using System.Collections.Generic;
using System.Linq;

namespace DataTest.Data
{
    public class DataManipulator
    {
        private readonly MyCsvReader _csvReader;
        private readonly ApplianceCostCalculator _applianceCostCalculator;

        public DataManipulator(MyCsvReader csvReader, ApplianceCostCalculator applianceCostCalculator)
        {
            _csvReader = csvReader;
            _applianceCostCalculator = applianceCostCalculator;
        }

        public List<OneHouseHoldData24hrs> GetDataForOneHousehold24Hours()
        {
            var data24Hours = _csvReader.ReadDataFromOneHouseHoldTwentyFourHours();
            // Filter and sort as needed, for example:
            return data24Hours
                .Where(d => d.ActiveEnergyOutlet > 100)  // Filter if necessary
                .OrderBy(d => d.AtDateTime)
                .ToList();
        }

        public double GetTotalEnergyForOneHousehold24Hours()
        {
            var data24Hours = _csvReader.ReadDataFromOneHouseHoldTwentyFourHours();
            return data24Hours.Sum(d => d.ActiveEnergyOutlet);
        }

        //public List<(OneHouseHoldData24hrs Data, double Cost)> GetDataWithCostForOneHousehold24Hours()
        //{
        //    var data24Hours = _csvReader.ReadDataFromOneHouseHoldTwentyFourHours();

        //    // Calculate cost for each data row
        //    var dataWithCost = data24Hours
        //        .Select(d => (Data: d, Cost: _applianceCostCalculator.CalculateApplianceCost(d.AtDateTime, 1, d.ActiveEnergyOutlet)))
        //        .ToList();

        //    return dataWithCost;
        //}

        //public double GetTotalCostForOneHousehold24Hours()
        //{
        //    var dataWithCost = GetDataWithCostForOneHousehold24Hours();
        //    return dataWithCost.Sum(d => d.Cost);
        //}
    }
}
