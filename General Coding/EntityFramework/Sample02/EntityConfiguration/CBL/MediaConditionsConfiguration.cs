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
    public class MediaConditionsConfiguration : EntityTypeConfiguration<MediaConditions>
    {
        public MediaConditionsConfiguration()
        {
            HasKey(m => m.mediaConditions_id)
                .Property((ac => ac.mediaConditions_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            Property(j => j.name).HasMaxLength(100);

            Property(j => j.description).HasMaxLength(500);

            //Property(j => j.active).IsRequired();

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();
        }
    }
}
