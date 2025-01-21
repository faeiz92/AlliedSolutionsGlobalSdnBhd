namespace AlliedSolutionsGlobalSdnBhd.Models
{
    public class PLCControlsDTO
    {
        public string DeviceName { get; set; }
        public string Gateway { get; set; }
        public string CommandTag { get; set; }
        public string StatusTag { get; set; }
        public string DataTag { get; set; }
        public bool? Download { get; set; }
        public string PLCType { get; set; }
        public string CPName { get; set; }
        public string Message { get; set; }
        public bool? TimeSync { get; set; }

    }
}
