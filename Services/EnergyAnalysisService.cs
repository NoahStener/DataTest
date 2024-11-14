using DataTest.Data;
using DataTest.Models;

namespace DataTest.Services
{
    public class EnergyAnalysisService
    {
        public List<OneHouseHoldDataOneWeek> GetMeterData(List<OneHouseHoldDataOneWeek> data, int meterResourceId)
        {
            return data.Where(d => d.ResourceId == meterResourceId).ToList();
        }

        // Beräkna total energiförbrukning för en viss elmätare
        public double CalculateTotalEnergyConsumption(List<OneHouseHoldDataOneWeek> meterData)
        {
            return meterData.Sum(d => d.ActiveEnergyOutlet);
        }
    }
}
