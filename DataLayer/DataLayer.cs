using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ClassLibraryDBtest;
using DataLayer.Interfaces;

namespace DataLayer
{
    public class DataLayer:IDataLayer
    {
         

        public exercise01Context Context { get; }

        public DataLayer(exercise01Context Context)
        {
            this.Context = Context;
        }

        //public long? WorkclassId { get; set; }
        //public long? EducationLevelId { get; set; }
        //public long? EducationNum { get; set; }
        //public long? MaritalStatusId { get; set; }
        //public long? OccupationId { get; set; }
        //public long? RelationshipId { get; set; }
        //public long? RaceId { get; set; }
        //public long? SexId { get; set; }
        //public long? CapitalGain { get; set; }
        //public long? CapitalLoss { get; set; }
        //public long? HoursWeek { get; set; }
        //public long? CountryId { get; set; }
        public IEnumerable<DesRecord> GetDesRecord(int skipRecords = 0,int takeRecords = 10)
        {
            return (from s in this.Context.Records.Skip(() => skipRecords)
                                .Take(() => takeRecords)
                               join p in this.Context.Workclasses on s.WorkclassId equals p.Id
                               join q in this.Context.EducationLevels on s.EducationLevelId equals q.Id
                               join h in this.Context.MaritalStatuses on s.MaritalStatusId equals h.Id
                               join w in this.Context.Occupations on s.OccupationId equals w.Id
                               join j in this.Context.Relationships on s.RelationshipId equals j.Id
                               join Race in this.Context.Races on s.MaritalStatusId equals Race.Id
                               join Sex in this.Context.Sexes on s.MaritalStatusId equals Sex.Id
                               join Country in this.Context.Countries on s.MaritalStatusId equals Country.Id
                               orderby s.Id
                               
                               select new DesRecord {
                                   Id =  s.Id,
                                   Over50k = s.Over50k == null ? null : s.Over50k,
                                   Age = s.Age == null ? null : s.Age,
                                   CapitalGain = s.CapitalGain == null ? null : s.CapitalGain,
                                   CapitalLoss = s.CapitalLoss == null ? null : s.CapitalLoss,
                                   EducationNum = s.EducationNum == null ? null : s.EducationNum,
                                   HoursWeek = s.HoursWeek == null ? null : s.HoursWeek,
                                   WorkclassName = p.Name == null ? null : p.Name,
                                   EducationLevelName = q.Name == null ? null : q.Name,
                                   EducationLevelsName = p.Name == null ? null : p.Name,
                                   MaritalStatusesName = h.Name == null ? null : h.Name,
                                   OccupationsName = w.Name == null ? null : w.Name,
                                   RelationshipsName = j.Name == null ? null : j.Name,
                                   RacesName = Race.Name == null ? null : Race.Name,
                                   SexesName = Sex.Name == null ? null : Sex.Name,
                                   CountriesName = Country.Name == null ? null : Country.Name
                                   
                               }).Distinct().ToList<DesRecord>();

        }


        public IEnumerable<ResultLine> GetRecord(string aggregationType , int aggregationFilter )
        {
            var enumeration=(from s in this.Context.Records
                    join p in this.Context.Workclasses on s.WorkclassId equals p.Id
                    join q in this.Context.EducationLevels on s.EducationLevelId equals q.Id
                    join h in this.Context.MaritalStatuses on s.MaritalStatusId equals h.Id
                    join w in this.Context.Occupations on s.OccupationId equals w.Id
                    join j in this.Context.Relationships on s.RelationshipId equals j.Id
                    join Race in this.Context.Races on s.MaritalStatusId equals Race.Id
                    join Sex in this.Context.Sexes on s.MaritalStatusId equals Sex.Id
                    join Country in this.Context.Countries on s.MaritalStatusId equals Country.Id
                    orderby s.Id

                    select new DesRecord {
                        Id = s.Id,
                        Over50k = s.Over50k == null ? null : s.Over50k,
                        Age = s.Age == null ? null : s.Age,
                        CapitalGain = s.CapitalGain == null ? null : s.CapitalGain,
                        CapitalLoss = s.CapitalLoss == null ? null : s.CapitalLoss,
                        EducationNum = s.EducationNum == null ? null : s.EducationNum,
                        HoursWeek = s.HoursWeek == null ? null : s.HoursWeek,
                        WorkclassName = p.Name == null ? null : p.Name,
                        EducationLevelName = q.Name == null ? null : q.Name,
                        EducationLevelsName = p.Name == null ? null : p.Name,
                        MaritalStatusesName = h.Name == null ? null : h.Name,
                        OccupationsName = w.Name == null ? null : w.Name,
                        RelationshipsName = j.Name == null ? null : j.Name,
                        RacesName = Race.Name == null ? null : Race.Name,
                        SexesName = Sex.Name == null ? null : Sex.Name,
                        CountriesName = Country.Name == null ? null : Country.Name

                    }).Distinct().ToList<DesRecord>();
                    var grouped = enumeration.GroupBy(x => GetPropertyValue(x, aggregationType)).Where(x => { return int.Parse(x.GetType().GetProperty(aggregationType).GetValue(x, null).ToString())== aggregationFilter; }).Select(cl => new ResultLine {
                        aggregationType= aggregationType,
                        aggregationFilter= aggregationFilter,
                        capital_gain_AVG = cl.Average(c => c.CapitalGain),
                        capital_gain_SUM = cl.Sum(c => c.CapitalGain),
                        capital_Loss_AVG = cl.Average(c => c.CapitalLoss),
                        capital_Loss_SUM = cl.Sum(c => c.CapitalLoss),
                        over_50k_count = cl.Count().ToString(),
                        Under_50k_count = cl.Count().ToString()

                        //over_50k_count = cl.Count(c => c.Over50k > 50000).ToString(),
                        //Under_50k_count = cl.Count(c.Over50k < 50000).ToString()
                    }).ToList();
            return grouped;
        }


        private static object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }

    }
}
