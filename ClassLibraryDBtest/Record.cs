using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibraryDBtest
{
    public partial class Record
    {
        public long Id { get; set; }
        public long? Age { get; set; }
        public long? WorkclassId { get; set; }
        public long? EducationLevelId { get; set; }
        public long? EducationNum { get; set; }
        public long? MaritalStatusId { get; set; }
        public long? OccupationId { get; set; }
        public long? RelationshipId { get; set; }
        public long? RaceId { get; set; }
        public long? SexId { get; set; }
        public long? CapitalGain { get; set; }
        public long? CapitalLoss { get; set; }
        public long? HoursWeek { get; set; }
        public long? CountryId { get; set; }
        public byte[] Over50k { get; set; }
    }
}
