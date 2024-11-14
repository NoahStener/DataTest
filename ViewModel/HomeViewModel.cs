using DataTest.Data;
using DataTest.Models;

namespace DataTest.ViewModel
{
    public class HomeViewModel
    {
        public List<OneHouseHoldDataOneWeek> HouseHoldData { get; set; }
        public double TotalEnergyConsumption { get; set; }
    }
}
