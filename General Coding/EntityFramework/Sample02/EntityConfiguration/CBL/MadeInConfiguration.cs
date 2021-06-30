using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Database.Entity.CBL;

namespace Framework.Database.EntityConfiguration.CBL
{
    public class MadeInConfiguration : EntityTypeConfiguration<MadeIn>
    {
        public MadeInConfiguration()
        {
            HasKey(m => m.madeIn_id)
                .Property((m => m.madeIn_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(m => m.dateRegistration).IsRequired();

            Property(m => m.userRegistration_id).IsRequired();

            Property(m => m.name).HasMaxLength(250);

            Property(m => m.description).HasMaxLength(2000);
        }
    }
}
