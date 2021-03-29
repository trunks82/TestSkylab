namespace DataLayer
{
    public class ResultLine
    {
        public string aggregationType { get; set; }
        public int aggregationFilter { get; set; }
        public double? capital_gain_AVG { get; set; }
        public long? capital_gain_SUM { get; set; }
        public double? capital_Loss_AVG { get; set; }
        public long? capital_Loss_SUM { get; set; }
        public string over_50k_count { get; set; }
        public string Under_50k_count { get; set; }
    }
}