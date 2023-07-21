namespace GasNetwork.Models
{
    public class Consumer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ParentId { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Person { get; set; }
        public string? Duty { get; set; }
        public string? Kind { get; set; }
        public string? ContractNum { get; set; }
        public int SiteCode { get; set; }
        public int RowVersion { get; set; }
        public int INN { get; set; }
        public string? Email { get; set; }
    }
}