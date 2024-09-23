using DriveApp.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DriveApp.Models.Data
{
    public class AppDbContext : IdentityDbContext<UserApplication,RoleApplication,string>
    {
        private readonly IConfiguration config;
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<TripDetail> TripDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Trip>().ToTable("Trips").HasKey(t=>t.Id);
            builder.Entity<TripDetail>().ToTable("TripDetails").HasKey(t =>t.Id);
            builder.Entity<UserApplication>().UseTptMappingStrategy();
            builder.Entity<Vehicle>().HasKey(v => v.Id);
            builder.Entity<Vehicle>().HasDiscriminator<string>("Type")
                .HasValue<Car>("Car")
                .HasValue<Motocycle>("Motocycle");
        }
        public AppDbContext(DbContextOptions options,IConfiguration confing) : base(options) 
        {
            this.config = confing;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(config["ConnectionStrings:cs"]);
        }
    }
}
