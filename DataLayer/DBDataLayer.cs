using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using ClassLibraryDBtest;
using DataLayer.Interfaces;
using ExpressionPredicateBuilder;

namespace DataLayer
{
    public class DBDataLayer:IDataLayer
    {
         

        public exercise01Context Context { get; }

        public DBDataLayer(exercise01Context Context)
        {
            this.Context = Context;
        }

        public IEnumerable<DesRecord> GetDesRecord(int offset = 0,int count = 10)
        {
            return (from s in this.Context.Records.OrderBy(s =>s.Id).Skip(() => offset)
                                .Take(() => count)
                               join p in this.Context.Workclasses on s.WorkclassId equals p.Id
                               join q in this.Context.EducationLevels on s.EducationLevelId equals q.Id
                               join h in this.Context.MaritalStatuses on s.MaritalStatusId equals h.Id
                               join w in this.Context.Occupations on s.OccupationId equals w.Id
                               join j in this.Context.Relationships on s.RelationshipId equals j.Id
                               join Race in this.Context.Races on s.RaceId equals Race.Id
                               join Sex in this.Context.Sexes on s.SexId equals Sex.Id
                               join Country in this.Context.Countries on s.CountryId equals Country.Id
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
                                   MaritalStatusesName = h.Name == null ? null : h.Name,
                                   OccupationsName = w.Name == null ? null : w.Name,
                                   RelationshipsName = j.Name == null ? null : j.Name,
                                   RacesName = Race.Name == null ? null : Race.Name,
                                   SexesName = Sex.Name == null ? null : Sex.Name,
                                   CountriesName = Country.Name == null ? null : Country.Name
                                   
                               }).Distinct().ToList<DesRecord>();

        }


        public IEnumerable<ResultLine> GetRecord(string aggregationType , string aggregationFilter )
        {
            //var predicate = ExpressionBuilder.BuildPredicate<PreAggregate>(aggregationFilter, OperatorComparer.Equals,   aggregationType);

            var enumeration=(from s in this.Context.Records

                    join q in this.Context.EducationLevels on s.EducationLevelId equals q.Id
                    join w in this.Context.Occupations on s.OccupationId equals w.Id
                    
                    orderby s.Id

                    select new PreAggregate {
                        Id = s.Id,
                        Over50k = s.Over50k == null ? null : s.Over50k,
                        age = s.Age == null ? null : s.Age,
                        CapitalGain = s.CapitalGain == null ? null : s.CapitalGain,
                        CapitalLoss = s.CapitalLoss == null ? null : s.CapitalLoss,
                        EducationNum = s.EducationNum == null ? null : s.EducationNum,
                        education_level_id = q.Id ,
                        occupation_id = w.Id 

                    }).Distinct().ToList();

            if (aggregationType == "age")
            {
                long filt = Convert.ToInt64(aggregationFilter);
                enumeration = enumeration.Where(b => b.age == filt).ToList();
            }
            if (aggregationType == "education_level_id")
            {
                long filt = Convert.ToInt64(aggregationFilter);
                enumeration = enumeration.Where(b => b.education_level_id == filt).ToList();
            }
            if (aggregationType == "occupation_id")
            {
                long filt = Convert.ToInt64(aggregationFilter);
                enumeration = enumeration.Where(b => b.occupation_id == filt).ToList();
            }

            var grouped = enumeration.GroupBy(x => Utilities.GetPropertyValue(x, aggregationType)).Select(cl => new ResultLine {
                        aggregationType= aggregationType,
                        aggregationFilter= aggregationFilter,
                        capital_gain_AVG = cl.Average(c => c.CapitalGain),
                        capital_gain_SUM = cl.Sum(c => c.CapitalGain),
                        capital_Loss_AVG = cl.Average(c => c.CapitalLoss),
                        capital_Loss_SUM = cl.Sum(c => c.CapitalLoss),
                        over_50k_count = cl.Count().ToString(),
                        Under_50k_count = cl.Count().ToString()
                        //over_50k_count = cl.Count(c => c.Over50k).ToString(),
                        //Under_50k_count = cl.Count(c => !c.Over50k).ToString()

                    }).ToList();
            return grouped;
        }

        public IEnumerable<DesRecord> GetRecordAll()
        {
            List<DesRecord> lista = (from s in this.Context.Records                                
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
                        MaritalStatusesName = h.Name == null ? null : h.Name,
                        OccupationsName = w.Name == null ? null : w.Name,
                        RelationshipsName = j.Name == null ? null : j.Name,
                        RacesName = Race.Name == null ? null : Race.Name,
                        SexesName = Sex.Name == null ? null : Sex.Name,
                        CountriesName = Country.Name == null ? null : Country.Name

                    }).Distinct().ToList<DesRecord>();


            return lista;

            
        }

        

    }
}
