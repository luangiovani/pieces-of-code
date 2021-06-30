using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class PaisConfiguration : EntityTypeConfiguration<Pais>
    {
        public PaisConfiguration()
        {
            HasKey(o => o.id_pais);

            Property(o => o.pais)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.sigla)
                .HasMaxLength(3);
        }
    }
}