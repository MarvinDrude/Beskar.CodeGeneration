using Beskar.CodeGeneration.ContentGenerator.Marker.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Templates;

public abstract class TranslationEntityConfiguration<TEntity, TValue>(string tableName)
   : IEntityTypeConfiguration<TEntity>
   where TEntity : TranslationEntity<TValue>
{
   private readonly string _tableName = tableName;
   
   public virtual void Configure(EntityTypeBuilder<TEntity> builder)
   {
      builder.ToTable(_tableName)
         .UseTphMappingStrategy();

      builder.HasKey(x => x.Id);

      builder.Property(x => x.TranslationKey)
         .IsRequired()
         .HasMaxLength(10);

      builder.Property(x => x.ParentId)
         .HasConversion<ContentTypeIdConverter>();
      
      builder.HasIndex(x => new { x.ParentId, x.TranslationKey })
         .IsUnique();
      builder.HasIndex(x => new { x.TranslationKey });
   }
}