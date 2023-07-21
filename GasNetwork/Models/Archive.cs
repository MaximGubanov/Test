using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GasNetwork.Models
{
    public abstract class Archive : IArchive
    {
        public int DeviceId { get; set; } // TODO: проверить, возможно нужно удалить
        public Device? Device { get; set; }
        public abstract EArchType TypeArchive { get; set; }
        public abstract string? Name { get; set; }

        public virtual Task<List<Archive>> GetDataAsync(DateTime start, DateTime end)
        {
            // а звчем это почему просто не сделать метод  GetDataAsync просто абстракным?
            throw new NotImplementedException();
        }
    }
    // Я не вижу разнинцы между классами MonthlyArchive, DaylyArchive, IntervalArchive они отличаются только значениями свойста
    // Nam и TypeArchive а их можно передвавать и в конструкторе класа
    // Также не понятно зачем обявлять одни и теже своства в каждом наследднике. Для этого же есть родитель, почему не определить общие свойства там?
    public class MonthlyArchive : Archive, IArchive
    {
        public override EArchType TypeArchive { get; set; } = EArchType.Monthly;
        public override string? Name { get; set; } = "Месячный архив";
        public DateTime IntervalArchDate { get; set; }
        public double StandardTotalVol { get; set; }
        public double StandardUnperturbedVol { get; set; }
        public double WorkingTotalVol { get; set; }
        public double WorkingUnperturbedVol { get; set; }
        public double GasPressure { get; set; }
        public double GasTemperature { get; set; }
        public double AccumPerHourVol { get; set; }
        public double AlarmTimer { get; set; }
        public double CountingTime { get; set; }
        public double AccumStandardVol { get; set; }
        public double AccumWorkingVol { get; set; }

        public override async Task<List<Archive>> GetDataAsync(DateTime start, DateTime end)
        {
            return await ArchiveRepository.RetrievAsync<MonthlyArchive>(this, start, end);
        }
    }

    public class DaylyArchive : Archive, IArchive
    {
        public override EArchType TypeArchive { get; set; } = EArchType.Dayly;
        public override string? Name { get; set; } = "Суточный архив";
        public DateTime IntervalArchDate { get; set; }
        public double StandardTotalVol { get; set; }
        public double StandardUnperturbedVol { get; set; }
        public double WorkingTotalVol { get; set; }
        public double WorkingUnperturbedVol { get; set; }
        public double GasPressure { get; set; }
        public double GasTemperature { get; set; }
        public double AccumPerHourVol { get; set; }
        public double AlarmTimer { get; set; }
        public double CountingTime { get; set; }
        public double AccumStandardVol { get; set; }
        public double AccumWorkingVol { get; set; }

        public async new Task<List<Archive>> GetDataAsync(DateTime start, DateTime end)
        {
            return await ArchiveRepository.RetrievAsync<MonthlyArchive>(this, start, end);
        }
    }

    public class IntervalArchive : Archive, IArchive
    {
        public override EArchType TypeArchive { get; set; } = EArchType.Interval;
        public override string? Name { get; set; } = "Интервальный архив";
        public DateTime IntervalArchDate { get; set; }
        public double StandardTotalVol { get; set; }
        public double StandardUnperturbedVol { get; set; }
        public double WorkingTotalVol { get; set; }
        public double WorkingUnperturbedVol { get; set; }
        public double GasPressure { get; set; }
        public double GasTemperature { get; set; }
        public double AccumPerHourVol { get; set; }
        public double AlarmTimer { get; set; }
        public double CountingTime { get; set; }
        public double AccumStandardVol { get; set; }
        public double AccumWorkingVol { get; set; }

        public override async Task<List<Archive>> GetDataAsync(DateTime startDate, DateTime endDate)
        {
            return await ArchiveRepository.RetrievAsync<IntervalArchive>(this, startDate, endDate);
        }
    }

    public class ChangesArchive : Archive, IArchive
    {
        public override EArchType TypeArchive { get; set; } = EArchType.Change;
        public override string? Name { get; set; } = "Архив изменений";
        public int GlobalNumber { get; set; }
        public int ArchiveNumber { get; set; }
        public DateTime ChangeArchDate { get; set; }
        public string? Address { get; set; }    
        public string? Lable { get; set; }
        public string? ChangeArchDescription { get; set; }
        public string? NewValue { get; set; }
        public string? OldValue { get; set; }
        public int StatusLock { get; set; }

        public override async Task<List<Archive>> GetDataAsync(DateTime start, DateTime end)
        {
            return await ArchiveRepository.RetrievAsync<ChangesArchive>(this, start, end);
        }
    }

    public class EventArchive : Archive, IArchive
    {
        public override EArchType TypeArchive { get; set; } = EArchType.Event;
        public override string? Name { get; set; } = "Архив событий";
        public string? IconPath { get; set; }
        public DateTime EventArchDate { get; set; }
        public int ArchiveRecordNumber { get; set; }
        public string? EventCode { get; set; }
        public string? EventArchDescription { get; set; }
        public string? BorderEvent { get; set; } = "?";

        public override async Task<List<Archive>> GetDataAsync(DateTime start, DateTime end)
        {
            return await ArchiveRepository.RetrievAsync<EventArchive>(this, start, end);
        }
    }

    public class ActualArchive : Archive, IArchive
    {
        public override EArchType TypeArchive { get; set; } = EArchType.Actual;
        public override string? Name { get; set; } = "Актуальный архив";

        public override async Task<List<Archive>> GetDataAsync(DateTime start, DateTime end)
        {
            return await ArchiveRepository.RetrievAsync<ActualArchive>(this, start, end);
        }
    }
}