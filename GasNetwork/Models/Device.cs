using System.Collections.Generic;

namespace GasNetwork.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string? DevType { get; set; }
        public int ArchBit { get; set; }
        public List<Flow>? FlowList { get; set; }
        public List<DeviceParam>? DeviceParameterList { get; set; }
        public List<Archive>? ArchiveButtonList { get; set; }
    }
}