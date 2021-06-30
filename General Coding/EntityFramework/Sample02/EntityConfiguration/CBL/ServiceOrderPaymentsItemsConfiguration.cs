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
    public class ServiceOrderPaymentsItemsConfiguration : EntityTypeConfiguration<ServiceOrderPaymentsItems>
    {
        public ServiceOrderPaymentsItemsConfiguration()
        {
            HasKey(s => s.serviceOrderPaymentItems_id)
               .Property(s => s.serviceOrderPaymentItems_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(s => s.serviceOrderPayment)
                .WithMany(s => s.serviceOrderPaymentsItems)
                .HasForeignKey(s => s.serviceOrderPayment_id);

            Property(s => s.additionalService_id).IsOptional();
            Property(s => s.description).IsRequired().HasMaxLength(2000);
            Property(s => s.itemValue).IsRequired().HasPrecision(18,2);
            Property(s => s.dateRegistration).IsRequired();
            Property(s => s.userRegistrationId).IsRequired();
        }
    }
}
