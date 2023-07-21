using System;
using System.Collections.Generic;

namespace GasNetwork.ViewModels
{
    public class ConsumptionViewModel : ViewModelBase, IViewModel
    {
        public string Name { get; set; } = "Потребление";
        public List<IDataGridRepresenter>? DataFromConsumption { get; set; }

        public ConsumptionViewModel()
        {
            DataFromConsumption = new List<IDataGridRepresenter>()
            {
                new YearConsumption()
                {
                    WarningIcon = "/Assets/avalonia-logo.ico",
                    Year = new DateTime(2023, 1, 1),
                    VolumeStableOverall = 789658,
                    VolumeStablePerturbed = 1254,
                    VolumeStableUnperturbed = 789258,
                    TotalWorkingVolume = 123125.000,
                    Pressure = 12.2,
                    CorrectionFactor = 124,
                    Temperature = 9.9,
                    DataCompleteness = 100,
                },
                new YearConsumption()
                {
                    WarningIcon = "/Assets/avalonia-logo.ico",
                    Year = new DateTime(2023, 1, 1),
                    VolumeStableOverall = 789658,
                    VolumeStablePerturbed = 1254,
                    VolumeStableUnperturbed = 789258,
                    TotalWorkingVolume = 123125.000,
                    Pressure = 12.2,
                    CorrectionFactor = 124,
                    Temperature = 9.9,
                    DataCompleteness = 100,
                },
            };
        }
    }
}
