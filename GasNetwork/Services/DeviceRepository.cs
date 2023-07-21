using FirebirdSql.Data.FirebirdClient;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GasNetwork.Services
{
    public class DeviceRepository
    {
        public static async Task<Device> RetrievAsync(int id)
        {
            string? sql = 
                @"SELECT " +
                    $"s.id AS {nameof(Device.Id)}, " +
                    $"s.devtype AS {nameof(Device.DevType)}, " +
                    $"s.archbit AS {nameof(Device.ArchBit)}, " +
                    $"fl.id AS {nameof(Flow.FlowId)}, " +
                    $"fl.flownum AS {nameof(Flow.FlowNumber)}, " +
                    $"fl.custid AS {nameof(Flow.CustId)} " +
                "FROM sgs_device_view AS s " +
                "INNER JOIN flow AS fl " +
                "ON s.id = fl.deviceid " +
                "WHERE s.id = @id";

            await using (var conn = new FbConnection(DataProviderService.ConnectionString))
            {
                var deviceDictionary = new Dictionary<int, Device>();

                conn.Open();

                var result = conn.Query<Device, Flow, Device>(
                    sql,                   
                    (device, flow) =>
                    {
                        Device deviceEntry;

                        if (!deviceDictionary.TryGetValue(device.Id, out deviceEntry!))
                        {
                            deviceEntry = device;
                            deviceEntry.FlowList = new List<Flow>();
                            deviceEntry.ArchiveButtonList = DeviceService.MakeArchiveList(device);
                            deviceDictionary.Add(device.Id, deviceEntry);
                        }

                        flow.ParentId = deviceEntry.Id;
                        deviceEntry.FlowList?.Add(flow);

                        return deviceEntry;
                    },
                    new { id = $"{id}" },
                    splitOn: "FlowId")
                    .Distinct()
                    .ToList()[0];

                conn.Close();

                return result;
            }
        }

        public async static Task<List<DeviceParam>> RetrieveDeviceParameterAsync(Flow flow)
        {
            string? sql =
                $@"SELECT " +
                    $"SECTION as {nameof(DeviceParam.Section)}, " +
                    $"READDATE as {nameof(DeviceParam.ReadDate)}, " +
                    $"ADDRESS as {nameof(DeviceParam.Address)}, " +
                    $"LABLE as {nameof(DeviceParam.Lable)}, " +
                    $"VAL as {nameof(DeviceParam.ParamValue)}, " +
                    $"UNIT as {nameof(DeviceParam.Unit)}, " +
                    $"DESCRIPTION as {nameof(DeviceParam.Description)} " +
                $"FROM get_device_parameters(@id, @flowNumber)";

            await using (var conn = new FbConnection(DataProviderService.ConnectionString)) 
            {
                var deviceParam = new List<DeviceParam>();

                try
                {
                    conn.Open();
                    deviceParam = conn.Query<DeviceParam>(
                        sql, 
                        new { id = flow.ParentId, flowNumber = flow.FlowNumber })
                        .ToList();
                    conn.Close();

                    return deviceParam;
                } 
                catch { }

                return deviceParam;
            }
        }
    }
}