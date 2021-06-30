using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class EstadoConfiguration : EntityTypeConfiguration<Estado>
    {
        public EstadoConfiguration()
        {
            HasKey(o => o.id_estado);

            HasRequired(o => o.pais)
                .WithMany(o => o.estado)
                .HasForeignKey(o => o.id_pais);

            Property(o => o.estado)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.sigla)
                .HasMaxLength(2);
        }
    }
}