using DataTest.Models;

namespace DataTest.ViewModel
{
    public class ApplianceCostViewModel
    {
        public List<Appliance> AvailableAppliances { get; set; }
        public string SelectedAppliance { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public double? TotalCost { get; set; }
        public List<Activity> Activities { get; set; } = new List<Activity>();
        public List<ElectricityPrice> ElectricityPrices { get; set; } = new List<ElectricityPrice>();

        public class Activity
        {
            public string ApplianceName { get; set; }
            public int Hours { get; set; }
            public int Minutes { get; set; }
            public DateTime StartTime { get; set; }
            public double Cost { get; set; }
        }
    }


}
