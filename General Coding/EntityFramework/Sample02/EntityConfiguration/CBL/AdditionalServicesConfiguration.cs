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
    public class AdditionalServicesConfiguration : EntityTypeConfiguration<AdditionalServices>
    {
        public AdditionalServicesConfiguration()
        {
            HasKey(m => m.additionalServicesid).Property((ac => ac.additionalServicesid)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            Property(j => j.name).HasMaxLength(100);

            Property(j => j.description).HasMaxLength(500);

            Property(m => m.valuess).IsRequired();

            Property(m => m.extraInformations).HasMaxLength(2000);

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();

            Property(dt => dt.dateUpdate).IsRequired();

            Property(dt => dt.userUpdate_id).IsRequired();

            /*
            HasMany(c => c.serviceOrderAdditionalServices)
                .WithRequired(s => s.additionalServices)
                .HasForeignKey(c => c.additionalServices_id);

           */

            
        }
    }
}
