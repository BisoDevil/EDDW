using System;
using System.Collections.Generic;
using System.Text;
using EDDW.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EDDW.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Employee>()
                .Property(p => p.Title)
                .HasConversion<string>();
            builder.Entity<Employee>()
                .Property(p => p.Type)
                .HasConversion<string>();
            builder.Entity<Guest>()
               .Property(p => p.Title)
               .HasConversion<string>();
            builder.Entity<Speaker>()
               .Property(p => p.Title)
               .HasConversion<string>();
            builder.Entity<Company>()
                .HasMany<Employee>()
                .WithOne(m => m.Company)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Company>()
                .HasMany<PackageBook>()
                .WithOne(o => o.Company)
                .HasForeignKey(e => e.CompanyId);
            builder.Entity<Programme>()
                .HasOne<Video>()
                .WithOne(e => e.Programme);
            builder.Entity<Sponsor>()
                .HasMany<BoothBook>()
                .WithOne(o => o.Sponsor)
                .HasForeignKey(e => e.SponsorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Programme>()
                .Property(s => s.Status)
                .HasConversion(new EnumToStringConverter<ProgrammeStatus>());



        }
        public DbSet<EDDW.Models.Company> Company { get; set; }
        public DbSet<EDDW.Models.Employee> Employee { get; set; }
        public DbSet<EDDW.Models.Guest> Guest { get; set; }
        public DbSet<EDDW.Models.Speaker> Speaker { get; set; }
        public DbSet<EDDW.Models.Timeline> Timeline { get; set; }
        public DbSet<EDDW.Models.Room> Room { get; set; }
        public DbSet<EDDW.Models.Booth> Booth { get; set; }
        public DbSet<EDDW.Models.Package> Package { get; set; }
        public DbSet<EDDW.Models.Sponsor> Sponsor { get; set; }
        public DbSet<EDDW.Models.MetaAssist> MetaAssist { get; set; }
        public DbSet<EDDW.Models.Programme> Programme { get; set; }
        public DbSet<EDDW.Models.Attendance> Attendance { get; set; }
        public DbSet<EDDW.Models.Video> Video { get; set; }
        public DbSet<EDDW.Models.Questions> Questions { get; set; }
        public DbSet<EDDW.Models.PackageBook> PackageBook { get; set; }
        public DbSet<EDDW.Models.BoothBook> BoothBook { get; set; }
    }
}
