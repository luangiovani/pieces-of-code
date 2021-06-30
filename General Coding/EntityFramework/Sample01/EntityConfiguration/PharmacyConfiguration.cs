using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class PharmacyConfiguration : EntityTypeConfiguration<Pharmacy>
    {
        public PharmacyConfiguration()
        {
            HasKey(o => o.id_pharmacy);

            HasRequired(o => o.filial)
                .WithMany(o => o.pharmacy)
                .HasForeignKey(o => o.id_filial);

            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(o => o.descritivo)
                .IsMaxLength();

            Property(o => o.imagem)
                .HasMaxLength(200);

            Property(o => o.dt_cadastro)
                .IsRequired();
        }
    }
}