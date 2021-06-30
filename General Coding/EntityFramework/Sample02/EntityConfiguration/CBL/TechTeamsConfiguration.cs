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
    public class TechTeamsConfiguration : EntityTypeConfiguration<TechTeams>
    {
        public TechTeamsConfiguration()
        {
            HasKey(t => t.techTeam_id)
                .Property(ss => ss.techTeam_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.name).HasMaxLength(250);

            Property(t => t.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property().IsRequired();

            Property(t => t.dateRegistration).IsRequired();

            Property(t => t.userRegistration_id).IsRequired();
        }
    }
}
