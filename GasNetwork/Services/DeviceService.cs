using GasNetwork.Extensions;
using System.Collections.Generic;

namespace GasNetwork.Services
{
    public class DeviceService
    {
        public static List<Archive> MakeArchiveList(Device device)
        {
            List<Archive> archiveList = new();

            var bitString = device.ArchBit.ConvertToByteString();

            int index = 0;

            foreach (var bit in bitString)
            {
                if (int.Parse(bit.ToString()) == 1)
                {
                    switch (index)
                    {
                        // Очень полохая практика!  Для чего нужен ООП и всякие швблоны? Lля того чтобы минимизировать зацепления в коде
                        // и обеспечить быструю расширяемость
                        // а теперь прикиньте во скольких местах надо изменть ваш код чтоюы просто добавить новый архив с другим интрвалом
                        // допустим годовой
                        case 0: archiveList.Add(new MonthlyArchive { Device = device }); break;
                        case 1: archiveList.Add(new DaylyArchive { Device = device }); break;
                        case 2: archiveList.Add(new IntervalArchive { Device = device }); break;
                        case 3: break; //archiveList.Add(new ActualArchive { Device = device });
                        case 4: archiveList.Add(new ChangesArchive { Device = device }); break;
                        case 5: archiveList.Add(new EventArchive { Device = device }); break;
                    }
                }
                index++;
            }

            return archiveList;
        }
    }
}