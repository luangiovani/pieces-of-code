using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class ProgramacaoConfiguration : EntityTypeConfiguration<Programacao>
    {
        public ProgramacaoConfiguration()
        {
            HasKey(o => o.id_programacao);

            HasRequired(o => o.filial)
                .WithMany(o => o.programacao)
                .HasForeignKey(o => o.id_filial);

            Property(o => o.dia_semana)
                .IsRequired()
                .HasMaxLength(15);

            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.descritivo)
                .IsMaxLength();

            Property(o => o.dt_cadastro)
                .IsRequired();
        }
    }
}