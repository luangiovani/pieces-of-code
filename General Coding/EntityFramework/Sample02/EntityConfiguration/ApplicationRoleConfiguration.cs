using System.Data.Entity.ModelConfiguration;
using Framework.Database.Entity;

namespace Framework.Database.EntityConfig
{
    public class ApplicationRoleConfiguration : EntityTypeConfiguration<ApplicationRole>
    {
        public ApplicationRoleConfiguration()
        {
            ToTable("Perfil");

            Property(o => o.Id)
                .HasMaxLength(128)
                .HasColumnName("id_perfil");

            Property(o => o.Name)
                .HasMaxLength(256)
                .HasColumnName("nome");

            //HasMany(l => l.roleLocations)
            //   .WithRequired(rl => rl.perfil)
            //   .HasForeignKey(rl => rl.id_perfil);
        } 
    }
}