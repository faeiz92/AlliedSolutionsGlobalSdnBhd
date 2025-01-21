namespace AlliedSolutionsGlobalSdnBhd.Models
{
    public class EqpReservationsDTO
    {
        public string BookSNo { get; set; }
        public string RecordSource { get; set; }
        public string Equipment { get; set; }
        public bool? ActState { get; set; }
        public DateTime ActTime { get; set; }
        public DateTime DeactTime { get; set; }
        public bool? CardEnable { get; set; }
        public bool? Init { get; set; }
    }
}
