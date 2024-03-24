using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace StadiumAnalytics
{

    public interface ISensorDbContext
    {
        DbSet<SensorData> SensorData { get; }
    }
    /// <summary>
    /// Sensor DB context form Entity Model creation
    /// </summary>
    public class SensorDbContext : DbContext, ISensorDbContext
    {
        public SensorDbContext(DbContextOptions<SensorDbContext> options) : base(options)
        {
        }
        public DbSet<SensorData> SensorData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the SensorData entity
            modelBuilder.Entity<SensorData>(entity =>
            {
                entity.ToTable("SensorData"); // Set the table name
                entity.HasKey(e => e.Id); // Set the primary key

                // Configure other properties
                entity.Property(e => e.GateName).IsRequired();
                entity.Property(e => e.TimeStamp).IsRequired();
                entity.Property(e => e.EventType).IsRequired();
                entity.Property(e => e.NumOfPeople).IsRequired();
            });
        }
    }
}
