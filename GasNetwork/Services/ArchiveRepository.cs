using Dapper;
using FirebirdSql.Data.FirebirdClient;
using GasNetwork.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasNetwork.Services
{
    public class ArchiveRepository : IRepositoryArch
    {
        public static async Task<List<Archive>> RetrievAsync<T>(
            Archive archiveObject,
            DateTime startDatePicker,
            DateTime endDatePicker)
        {
            var sql = GetSql(archiveObject);

            await using (var conn = new FbConnection(DataProviderService.ConnectionString))
            {
                var result = new List<T>();

                try
                {
                    conn.Open();
                    result = conn.Query<T>(
                        sql,
                        new 
                        { 
                            id = archiveObject.Device?.Id, 
                            startDate = startDatePicker, 
                            endDate = endDatePicker
                        })
                        .ToList();
                    conn.Close();
                    // А в чем смыл оборочивать список в еще один сприсок
                    return new List<Archive>((IEnumerable<Archive>)result);
                }
                catch {
                // За подобное в моей команде пожизненный эцих с гвоздями
                // проглатывать исклчюения зло. Или обрабатывай их или если не можешь
                // паусть обрабатыаеет выше по стек то кто сможетэто сделать
                }

                return new List<Archive>((IEnumerable<Archive>)result) ?? throw new Exception();
            }
        }

        private static string GetSql(Archive archive)
        {
            switch (archive.TypeArchive)
            {
                case EArchType.Event:
                    // вообще не понятно зачем вы объявляте многострочные стоки с помощю @ а потом делаете конкатенацию?

                    return $@"SELECT " +
                               $"ed.eventclass AS {nameof(EventArchive.IconPath)}, " +
                               $"e.devdate AS {nameof(EventArchive.EventArchDate)}, " +
                               $"e.arcnum AS {nameof(EventArchive.ArchiveRecordNumber)}, " +
                               $"ed.sysevent AS {nameof(EventArchive.EventCode)}, " +
                               $"ed.description AS {nameof(EventArchive.EventArchDescription)} " +
                           //TODO: не хватает одного поля - "Граница события"
                           $"FROM eventarc AS e " +
                           $"INNER JOIN eventdict AS ed " +
                           $"ON e.eventid = ed.id " +
                           $"WHERE deviceid = @id " +
                               $"AND e.devdate BETWEEN @startDate AND @endDate";
                // Запросы ниже чем отлисча.тся друг от друга?
                //  Все жто только для того чтоюы поодержтвать бессмысленную иерархю 
                // И вто "плюсы" использования Dapper писать километровые запрос. В EF вам даже не пришлось писать слова SELECT
                case EArchType.Dayly:
                    return $@"SELECT " +
                                $"i.devdate AS {nameof(IntervalArchive.IntervalArchDate)}, " +
                                $"i.vstot AS {nameof(IntervalArchive.StandardTotalVol)}, " +
                                $"i.vsund AS {nameof(IntervalArchive.StandardUnperturbedVol)}, " +
                                $"i.vrtot AS {nameof(IntervalArchive.WorkingTotalVol)}, " +
                                $"i.vrund AS {nameof(IntervalArchive.WorkingUnperturbedVol)}, " +
                                $"i.p AS {nameof(IntervalArchive.GasPressure)}, " +
                                $"i.t AS {nameof(IntervalArchive.GasTemperature)}, " +
                                $"i.f1 AS {nameof(IntervalArchive.AccumPerHourVol)}, " +
                                $"i.f2 AS {nameof(IntervalArchive.AlarmTimer)}, " +
                                $"i.f3 AS {nameof(IntervalArchive.CountingTime)}, " +
                                $"i.f4 AS {nameof(IntervalArchive.AccumStandardVol)}, " +
                                $"i.f5 AS {nameof(IntervalArchive.AccumWorkingVol)} " +
                                $"FROM intarc AS i " +
                            $"WHERE deviceid = @id " +
                                $"AND i.devdate BETWEEN @startDate AND @endDate " +
                                $"AND i.inttype = {(int)EArchType.Dayly}";

                case EArchType.Monthly:
                    return $@"SELECT " +
                                $"i.devdate AS {nameof(IntervalArchive.IntervalArchDate)}, " +
                                $"i.vstot AS {nameof(IntervalArchive.StandardTotalVol)}, " +
                                $"i.vsund AS {nameof(IntervalArchive.StandardUnperturbedVol)}, " +
                                $"i.vrtot AS {nameof(IntervalArchive.WorkingTotalVol)}, " +
                                $"i.vrund AS {nameof(IntervalArchive.WorkingUnperturbedVol)}, " +
                                $"i.p AS {nameof(IntervalArchive.GasPressure)}, " +
                                $"i.t AS {nameof(IntervalArchive.GasTemperature)}, " +
                                $"i.f1 AS {nameof(IntervalArchive.AccumPerHourVol)}, " +
                                $"i.f2 AS {nameof(IntervalArchive.AlarmTimer)}, " +
                                $"i.f3 AS {nameof(IntervalArchive.CountingTime)}, " +
                                $"i.f4 AS {nameof(IntervalArchive.AccumStandardVol)}, " +
                                $"i.f5 AS {nameof(IntervalArchive.AccumWorkingVol)} " +
                                $"FROM intarc AS i " +
                            $"WHERE deviceid = @id " +
                                $"AND i.devdate BETWEEN @startDate AND @endDate " +
                                $"AND i.inttype = {(int)EArchType.Monthly}";

                case EArchType.Interval:
                    return $@"SELECT " +
                                $"i.devdate AS {nameof(IntervalArchive.IntervalArchDate)}, " +
                                $"i.vstot AS {nameof(IntervalArchive.StandardTotalVol)}, " +
                                $"i.vsund AS {nameof(IntervalArchive.StandardUnperturbedVol)}, " +
                                $"i.vrtot AS {nameof(IntervalArchive.WorkingTotalVol)}, " +
                                $"i.vrund AS {nameof(IntervalArchive.WorkingUnperturbedVol)}, " +
                                $"i.p AS {nameof(IntervalArchive.GasPressure)}, " +
                                $"i.t AS {nameof(IntervalArchive.GasTemperature)}, " +
                                $"i.f1 AS {nameof(IntervalArchive.AccumPerHourVol)}, " +
                                $"i.f2 AS {nameof(IntervalArchive.AlarmTimer)}, " +
                                $"i.f3 AS {nameof(IntervalArchive.CountingTime)}, " +
                                $"i.f4 AS {nameof(IntervalArchive.AccumStandardVol)}, " +
                                $"i.f5 AS {nameof(IntervalArchive.AccumWorkingVol)} " +
                                $"FROM intarc AS i " +
                            $"WHERE deviceid = @id " +
                                $"AND i.devdate BETWEEN @startDate AND @endDate " +
                                $"AND i.inttype = {(int)EArchType.Interval}";

                case EArchType.Change:
                    return $@"SELECT " +
                                $"ch.glnum AS {nameof(ChangesArchive.GlobalNumber)}, " +
                                $"ch.arcnum AS {nameof(ChangesArchive.ArchiveNumber)}, " +
                                $"ch.devdate AS {nameof(ChangesArchive.ChangeArchDate)}, " +
                                $"p.address AS {nameof(ChangesArchive.Address)}, " +
                                $"p.label AS {nameof(ChangesArchive.Lable)}, " +
                                $"p.description AS {nameof(ChangesArchive.ChangeArchDescription)}, " +
                                $"ch.oldval AS {nameof(ChangesArchive.OldValue)}, " +
                                $"ch.newval AS {nameof(ChangesArchive.NewValue)}, " +
                                $"ch.lock AS {nameof(ChangesArchive.StatusLock)} " +
                           $"FROM changearc AS ch " +
                           $"INNER JOIN paramdict AS p " +
                           $"ON ch.paramid = p.id " +
                           $"WHERE ch.deviceid = @id " +
                                $"AND ch.devdate BETWEEN @startDate AND @endDate";
            }
            return "";
        }
    }
}