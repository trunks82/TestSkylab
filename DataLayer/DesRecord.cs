namespace DataLayer
{
    public class DesRecord
    {
        public long Id { get; set; }
        public byte[] Over50k { get; set; }
        public long? Age { get; set; }
        public long? CapitalGain { get; set; }
        public long? CapitalLoss { get; set; }
        public long? EducationNum { get; set; }
        public long? HoursWeek { get; set; }
        public string WorkclassName { get; set; }
        public string EducationLevelName { get; set; }
        public string MaritalStatusesName { get; set; }
        public string OccupationsName { get; set; }
        public string RelationshipsName { get; set; }
        public string RacesName { get; set; }
        public string SexesName { get; set; }
        public string CountriesName { get; set; }
    }
}