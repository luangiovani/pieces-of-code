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
    public class ServiceOrderAdditionalServicesConfiguration : EntityTypeConfiguration<ServiceOrderAdditionalServices>
    {
        public ServiceOrderAdditionalServicesConfiguration()
        {
            HasKey(m => m.serviceOrderAdditionalServices_id).Property((ac => ac.serviceOrderAdditionalServices_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property((ac => ac.additionalServices_id)).IsRequired();

            Property((ac => ac.serviceOrder_id)).IsRequired();

            Property(j => j.quantity).IsRequired();

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();

            Property(dt => dt.dateUpdate).IsRequired();

            Property(dt => dt.userUpdate_id).IsRequired(); 

            //HasRequired(soA => soA.additionalServices)
            //   .WithMany(a => a.serviceOrderAdditionalServices)
            //   .HasForeignKey(soA => soA.additionalServices_id);

            //HasRequired(so => so.serviceOrder)
            //   .WithMany(s => s.serviceOrderAdditionalServices)
            //   .HasForeignKey(soA => soA.serviceOrder_id);

            
        }
    }
}
