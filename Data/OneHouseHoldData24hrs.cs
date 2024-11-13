using CsvHelper.Configuration.Attributes;

namespace DataTest.Data
{
    public class OneHouseHoldData24hrs
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

        [Name("active_effect_input")]
        public double ActiveEffectInput { get; set; }

        [Name("active_effect_input1")]
        public double ActiveEffectInput1 { get; set; }

        [Name("active_effect_input2")]
        public double ActiveEffectInput2 { get; set; }

        [Name("active_effect_input3")]
        public double ActiveEffectInput3 { get; set; }

        [Name("active_effect_outlet1")]
        public double ActiveEffectOutlet1 { get; set; }

        [Name("active_effect_outlet2")]
        public double ActiveEffectOutlet2 { get; set; }

        [Name("active_effect_outlet3")]
        public double ActiveEffectOutlet3 { get; set; }

        [Name("active_energy_input")]
        public double ActiveEnergyInput { get; set; }

        [Name("phase_current1")]
        public double PhaseCurrent1 { get; set; }

        [Name("phase_current2")]
        public double PhaseCurrent2 { get; set; }

        [Name("phase_current3")]
        public double PhaseCurrent3 { get; set; }

        [Name("phase_voltage1")]
        public double PhaseVoltage1 { get; set; }

        [Name("phase_voltage2")]
        public double PhaseVoltage2 { get; set; }

        [Name("phase_voltage3")]
        public double PhaseVoltage3 { get; set; }
    }

}
