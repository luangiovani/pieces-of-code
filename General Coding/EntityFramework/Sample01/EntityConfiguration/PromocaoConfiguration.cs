using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class PromocaoConfiguration : EntityTypeConfiguration<Promocao>
    {
        public PromocaoConfiguration()
        {
            HasKey(o => o.id_promocao);

            HasRequired(o => o.filial)
                .WithMany(o => o.promocao)
                .HasForeignKey(o => o.id_filial);

            Property(o => o.tipo)
                .IsRequired()
                .HasMaxLength(1);

            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(o => o.descritivo)
                .IsMaxLength();

            Property(o => o.imagem)
                .HasMaxLength(200);

            Property(o => o.dt_cadastro)
                .IsRequired();

            Property(o => o.tags);

            Property(o => o.pushEnviado);

            Property(o => o.dataHoraPush);
        }
    }
}