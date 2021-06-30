using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.EntityConfiguration.CBL
{
    public class RoleLocationsConfiguration : EntityTypeConfiguration<RoleLocations>
    {
        public RoleLocationsConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(rl => rl.roleLocations_id)
                .Property((rl => rl.roleLocations_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(rl => rl.dateRegistration).IsRequired();

            Property(rl => rl.userRegistration_id).IsRequired();

            HasRequired(rl => rl.location)
                .WithMany(l => l.roleLocations)
                .HasForeignKey(rl => rl.location_id);

            HasRequired(rl => rl.perfil)
                .WithMany(p => p.roleLocations)
                .HasForeignKey(rl => rl.id_perfil);
        }
    }
}
