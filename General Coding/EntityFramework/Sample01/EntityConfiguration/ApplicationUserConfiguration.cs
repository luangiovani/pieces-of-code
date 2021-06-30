using System.Data.Entity.ModelConfiguration;
using TAJ.Database.Entity;

namespace TAJ.Database.EntityConfig
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            ToTable("Usuario");

            Property(o => o.Id)
                .HasMaxLength(128)
                .HasColumnName("id_usuario");

            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.telefone)
                .IsOptional()
                .HasMaxLength(15);

            Property(o => o.celular)
                .IsOptional()
                .HasMaxLength(15);

            Property(o => o.ativo)
                .IsRequired();

            Property(o => o.dt_cadastro)
                .IsRequired();
        }
    }
}