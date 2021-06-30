using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class GaleriaConfiguration : EntityTypeConfiguration<Galeria>
    {
        public GaleriaConfiguration()
        {
            HasKey(o => o.id_galeria);

            HasRequired(o => o.filial)
               .WithMany(o => o.galeria)
               .HasForeignKey(o => o.id_filial);

            Property(o => o.titulo)
                .IsRequired()
                .HasMaxLength(100);

            Property(o => o.descritivo)
                .IsMaxLength();

            Property(o => o.data)
                .IsRequired();

            Property(o => o.dt_cadastro)
                .IsRequired();
        }
    }
}