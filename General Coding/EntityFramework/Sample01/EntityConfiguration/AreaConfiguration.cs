using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class AreaConfiguration : EntityTypeConfiguration<Area>
    {
        public AreaConfiguration()
        {
            HasKey(o => o.id_area);

            HasOptional(o => o.area_mae)
                .WithMany(o => o.area_filha)
                .HasForeignKey(o => o.id_area_mae);

            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.ordem)
                .IsRequired();

            Property(o => o.controller)
                .HasMaxLength(50);

            Property(o => o.action)
                .HasMaxLength(50);

            Property(o => o.help)
                .IsMaxLength();

            Property(o => o.ativo)
                .IsRequired();

            Property(o => o.dt_cadastro)
                .IsRequired();
        }
    }
}