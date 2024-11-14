using DataTest.Data;
using DataTest.Models;

namespace DataTest.ViewModel
{
    public class HomeViewModel
    {
        public List<DailyEnergyConsumption> DailyConsumption { get; set; }
        public double CurrentElectricityPrice { get; set; }
    }
}
