using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class NovidadeConfiguration : EntityTypeConfiguration<Novidade>
    {
        public NovidadeConfiguration()
        {
            HasKey(o => o.id_novidade);

            HasRequired(o => o.filial)
               .WithMany(o => o.novidade)
               .HasForeignKey(o => o.id_filial);

            Property(o => o.titulo)
                .IsRequired()
                .HasMaxLength(100);

            Property(o => o.descritivo)
                .IsMaxLength();

            Property(o => o.imagem)
                .HasMaxLength(500);

            Property(o => o.dt_cadastro)
                .IsRequired();

            Property(o => o.ver_no_mobile)
                .IsRequired();

            Property(o => o.id_post_facebook);

        }    
    }
}