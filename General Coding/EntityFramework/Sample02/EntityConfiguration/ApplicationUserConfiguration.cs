using System.Data.Entity.ModelConfiguration;
using Framework.Database.Entity;

namespace Framework.Database.EntityConfig
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

            HasMany(u => u.serviceOrders)
               .WithRequired(s => s.user)
               .HasForeignKey(s => s.user_id);

            HasMany(u => u.serviceOrdersAssigned)
               .WithOptional(s => s.userAssigned)
               .HasForeignKey(s => s.userAssigned_id);

            Property(u => u.url_file)
                .IsOptional()
                .HasColumnType("varchar(max)")
                .HasMaxLength(int.MaxValue);

            Property(u => u.tokenCode).IsOptional().HasMaxLength(500);

            HasRequired(u => u.location)
               .WithMany(l => l.users)
               .HasForeignKey(u => u.location_id);
        }
    }
}