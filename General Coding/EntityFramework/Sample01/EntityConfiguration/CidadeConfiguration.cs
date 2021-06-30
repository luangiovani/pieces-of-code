using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class CidadeConfiguration : EntityTypeConfiguration<Cidade>
    {
        public CidadeConfiguration()
        {
            HasKey(o => o.id_cidade);

            HasRequired(o => o.estado)
                .WithMany(o => o.cidade)
                .HasForeignKey(o => o.id_estado);

            Property(o => o.cidade)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}