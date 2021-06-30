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
    public class ServiceOrderBillingConfiguration : EntityTypeConfiguration<ServiceOrderBilling>
    {
        public ServiceOrderBillingConfiguration()
        {
            HasKey(s => s.serviceOrder_id);
            Property(s => s.serviceOrder_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasOptional(s => s.city)
                .WithMany(c => c.serviceOrdersBilling);

            Property(s => s.dateRegistration).IsRequired();

            Property(s => s.userRegistration_id).IsRequired();

            Property(s => s.billingAddress).HasMaxLength(500);

            Property(s => s.billingCompany).HasMaxLength(500);

            Property(s => s.billingCity_id).IsRequired();

            Property(s => s.billingPostalZipCode).HasMaxLength(50);

            Property(s => s.billingDistrict).HasMaxLength(500);

            Property(s => s.paymentMethod).HasMaxLength(100);

            Property(s => s.freight).HasMaxLength(100);

            Property(s => s.creditCardNumber).HasMaxLength(20);

            Property(s => s.nameCreditCard).HasMaxLength(200);

            Property(s => s.expireCreditCard).HasMaxLength(7);

            Property(s => s.originalQuotedAmount).HasPrecision(18, 2);

            Property(s => s.discountCost).HasPrecision(18, 2);

            Property(s => s.amountToBeBilled).HasPrecision(18, 2);

            Property(s => s.originalQuotedAmount).HasPrecision(18, 2);

            Property(s => s.invoiceNumber).HasMaxLength(100);

            Property(s => s.invoicedAmount).HasPrecision(18, 2);

            Property(s => s.datePaid).IsOptional();

            Property(s => s.referredBy).HasMaxLength(200);

            Property(s => s.commissionAmount).HasPrecision(18, 2);

            Property(s => s.comissionDate).IsOptional();

            Property(s => s.partsNeeded).IsRequired();

            Property(s => s.partsAmount).HasPrecision(18,2);
        }
    }
}
