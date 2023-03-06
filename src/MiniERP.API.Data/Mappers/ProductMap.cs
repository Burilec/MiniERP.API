using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniERP.API.Models;

namespace MiniERP.API.Data.Mappers
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.ApiId)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .IsRequired();
            builder.Property(x => x.Description)
                   .IsRequired();

            // Table & Column Mappings
            builder.ToTable($"{nameof(Product)}s");

            // Relationship

            // Ignores

            // Indexes
            builder.HasIndex(_ => _.ApiId);
        }
    }
}