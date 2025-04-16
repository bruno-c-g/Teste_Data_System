using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDataSystem.Domain.Entities;

namespace TesteDataSystem.Infrastructure.EntitiesConfiguration
{
    internal class DataBaseConfiguration : IEntityTypeConfiguration<DataBase>
    {
        public void Configure(EntityTypeBuilder<DataBase> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Titulo).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Descricao);
            builder.Property(x => x.DataCriacao).IsRequired();
            builder.Property(x => x.DataConclusao);
            builder.Property(x => x.Status);
        }
    }
}
