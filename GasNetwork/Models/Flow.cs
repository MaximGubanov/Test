namespace GasNetwork.Models;

public class Flow
{
    public int FlowId { get; set; }
    public int FlowNumber { get; set; }
    public int CustId { get; set; }
    public int ParentId { get; set;}
}