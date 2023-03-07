using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FinalProject.Areas.Identity.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Areas.Identity.Data;

public class FinalProjectIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public virtual DbSet<Chore> Chores { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public FinalProjectIdentityDbContext(DbContextOptions<FinalProjectIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        builder.Entity<Chore>()
            .HasOne(c => c.User)
            .WithOne()
            .IsRequired(true)
            .HasForeignKey("Chore", "UserId");
    }
}

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}