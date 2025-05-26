using CabTap.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CabTap.Data.EntityConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> category)
    {
        category.HasKey(c => c.Id);

        category.HasIndex(c => c.Name)
            .IsUnique();
            
        category.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        category.Property(c => c.Rate)
            .HasPrecision(18, 2)
            .IsRequired();
        
        category.Property(c => c.Image)
            .HasDefaultValue("/images/categories/default.png")
            .HasMaxLength(100);
        
        category.HasMany(c => c.Taxis)
            .WithOne(t => t.Category)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}