using CsvHelper.Configuration.Attributes;

namespace DataTest.Models
{
    public class OneHouseHoldDataOneWeek
    {
        [Name("id")]
        public int Id { get; set; }
        [Name("resource_id")]
        public int ResourceId { get; set; }
        [Name("at_date_time")]
        public DateTime AtDateTime { get; set; }
        [Name("active_energy_outlet")]
        public double ActiveEnergyOutlet { get; set; }
        [Name("active_effect_outlet")]
        public double ActiveEffectOutlet { get; set; }
        [Name("active_energy_input")]
        public double ActiveEnergyInput { get; set; }
        [Name("active_effect_input")]
        public double ActiveEffectInput { get; set; }
        [Name("phase_current1")]
        public double PhaseCurrent1 { get; set; }
        [Name("phase_current2")]
        public double PhaseCurrent2 { get; set; }
        [Name("phase_current3")]
        public double PhaseCurrent3 { get; set; }
    }
}
