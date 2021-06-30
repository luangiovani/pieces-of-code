using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class FilialConfiguration : EntityTypeConfiguration<Filial>
    {
        public FilialConfiguration()
        {
            HasKey(o => o.id_filial);

            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.cep)
               .IsRequired()
               .HasMaxLength(9);

            Property(o => o.endereco)
                .IsRequired()
               .HasMaxLength(200);

            Property(o => o.numero)
                .IsRequired()
               .HasMaxLength(8);

            Property(o => o.descricao)
                .IsMaxLength();

            Property(o => o.dt_cadastro)
                .IsRequired();

            Property(o => o.id_facebook);


            Property(o => o.textoreserva)
                .IsMaxLength(); 
            Property(o => o.emailreserva)
                .IsMaxLength(); 
            Property(o => o.telefonereserva)
                .HasMaxLength(500);

            Property(o => o.url_galeria)
                .HasMaxLength(1000);

            HasRequired(o => o.bairro)
              .WithMany(o => o.filial)
              .HasForeignKey(o => o.id_bairro);
        }
    }
}