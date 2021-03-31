namespace DataLayer
{
    internal class PreAggregate
    {
        public long Id { get; set; }
        public byte[] Over50k { get; set; }
        public long? age { get; set; }
        public long? CapitalGain { get; set; }
        public long? CapitalLoss { get; set; }
        public long? EducationNum { get; set; }
        public long education_level_id { get; set; }
        public long occupation_id { get; set; }
    }
}