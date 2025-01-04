using Microsoft.EntityFrameworkCore;
using RentalCar.Service.Core.Entities;

namespace RentalCar.Service.Infrastructure.Persistence;

public class ServiceContext : DbContext
{
    public ServiceContext(DbContextOptions<ServiceContext> options) : base(options) { }
    
    public DbSet<Services> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Services>(e => 
        {
            e.HasKey(c => c.Id);
            
            e.Property<string>(c => c.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);
            
            e.Property(s => s.Description)
                .HasMaxLength(200)
                .IsUnicode()
                .HasDefaultValue("NA")
                .IsRequired();
            
            e.HasIndex(c => c.Name).IsUnique();
        });

        base.OnModelCreating(builder);
    }
}