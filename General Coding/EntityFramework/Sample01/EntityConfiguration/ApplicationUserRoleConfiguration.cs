using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TAJ.Database.EntityConfig
{
    public class ApplicationUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {
        public ApplicationUserRoleConfiguration()
        {
            ToTable("Usuario_Perfil");

            Property(o => o.UserId)
                .HasMaxLength(128)
                .HasColumnName("id_usuario");

            Property(o => o.RoleId)
                .HasMaxLength(128)
                .HasColumnName("id_perfil");
        }
    }
}