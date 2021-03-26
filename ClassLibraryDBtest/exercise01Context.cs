using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ClassLibraryDBtest
{
    public partial class exercise01Context : DbContext
    {
        public exercise01Context()
        {
        }

        public exercise01Context(DbContextOptions<exercise01Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<Occupation> Occupations { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<Relationship> Relationships { get; set; }
        public virtual DbSet<Sex> Sexes { get; set; }
        public virtual DbSet<Workclass> Workclasses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=C:\\Users\\rta\\source\\repos\\WebApplicationTest\\ClassLibraryDBtest\\exercise01.sqlite");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<EducationLevel>(entity =>
            {
                entity.ToTable("education_levels");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.ToTable("marital_statuses");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Occupation>(entity =>
            {
                entity.ToTable("occupations");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.ToTable("races");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.ToTable("records");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.CapitalGain).HasColumnName("capital_gain");

                entity.Property(e => e.CapitalLoss).HasColumnName("capital_loss");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.EducationLevelId).HasColumnName("education_level_id");

                entity.Property(e => e.EducationNum).HasColumnName("education_num");

                entity.Property(e => e.HoursWeek).HasColumnName("hours_week");

                entity.Property(e => e.MaritalStatusId).HasColumnName("marital_status_id");

                entity.Property(e => e.OccupationId).HasColumnName("occupation_id");

                entity.Property(e => e.Over50k)
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("over_50k");

                entity.Property(e => e.RaceId).HasColumnName("race_id");

                entity.Property(e => e.RelationshipId).HasColumnName("relationship_id");

                entity.Property(e => e.SexId).HasColumnName("sex_id");

                entity.Property(e => e.WorkclassId).HasColumnName("workclass_id");
            });

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.ToTable("relationships");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Sex>(entity =>
            {
                entity.ToTable("sexes");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Workclass>(entity =>
            {
                entity.ToTable("workclasses");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
