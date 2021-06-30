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
    public class ServiceOrderPaymentsConfiguration : EntityTypeConfiguration<ServiceOrderPayments>
    {
        public ServiceOrderPaymentsConfiguration()
        {
            HasKey(s => s.serviceOrderPayment_id)
                .Property(s => s.serviceOrderPayment_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(s => s.serviceOrder_id).IsRequired().HasPrecision(18, 0);

            HasRequired(s => s.serviceOrder)
                .WithMany(s => s.serviceOrderPayments)
                .HasForeignKey(s => s.serviceOrder_id);

            HasRequired(s => s.paymentMethod)
                .WithMany(s => s.serviceOrderPayments)
                .HasForeignKey(s => s.paymentMethod_id);

            HasRequired(s => s.customer)
                .WithMany(s => s.serviceOrderPayments)
                .HasForeignKey(s => s.customer_id);

            HasMany(s => s.serviceOrderPaymentsItems)
                .WithRequired(sm => sm.serviceOrderPayment)
                .HasForeignKey(sm => sm.serviceOrderPayment_id);

            Property(s => s.paymentValue).IsRequired().HasPrecision(18,2);

            Property(s => s.paymentDate).IsOptional();

            Property(s => s.discountGiven).IsOptional().HasPrecision(18,2);

            Property(s => s.attachment).IsOptional();

            Property(s => s.dateRegistration).IsRequired();

            Property(s => s.userRegistrationId).IsRequired();
        }
    }
}
