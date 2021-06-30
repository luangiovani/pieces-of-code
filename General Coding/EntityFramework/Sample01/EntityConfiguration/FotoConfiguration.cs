using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class FotoConfiguration : EntityTypeConfiguration<Foto>
    {
        public FotoConfiguration()
        {
            HasKey(o => o.id_foto);

            HasRequired(o => o.galeria)
                .WithMany(o => o.foto)
                .HasForeignKey(o => o.id_galeria);

            Property(o => o.url)
                .IsRequired()
                .HasMaxLength(100);

            Property(o => o.aprovado)
                .IsRequired();

            Property(o => o.dt_cadastro)
                .IsRequired();
        }
    }
}