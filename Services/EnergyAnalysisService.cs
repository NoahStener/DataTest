using DataTest.Data;

namespace DataTest.Services
{
    public class EnergyAnalysisService
    {
        public List<OneHouseHoldData24hrs> GetMeterData(List<OneHouseHoldData24hrs> data, int meterResourceId)
        {
            return data.Where(d => d.ResourceId == meterResourceId).ToList();
        }

        // Beräkna total energiförbrukning för en viss elmätare
        public double CalculateTotalEnergyConsumption(List<OneHouseHoldData24hrs> meterData)
        {
            return meterData.Sum(d => d.ActiveEnergyOutlet);
        }
    }
}
