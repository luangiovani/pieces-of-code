using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class BairroConfiguration : EntityTypeConfiguration<Bairro>
    {
        public BairroConfiguration()
        {
            HasKey(o => o.id_bairro);

            HasRequired(o => o.cidade)
                .WithMany(o => o.bairro)
                .HasForeignKey(o => o.id_cidade);

            Property(o => o.bairro)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}