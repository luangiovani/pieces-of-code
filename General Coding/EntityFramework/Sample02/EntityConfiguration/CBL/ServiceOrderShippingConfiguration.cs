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
    public class ServiceOrderShippingConfiguration : EntityTypeConfiguration<ServiceOrderShipping>
    {
        public ServiceOrderShippingConfiguration()
        {
            HasKey(s => s.serviceOrder_id)
                .Property(s => s.serviceOrder_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasOptional(s => s.city)
                .WithMany(s => s.serviceOrdersShip);

            Property(s => s.dateRegistration).IsRequired();

            Property(s => s.userRegistration_id).IsRequired();

            Property(s => s.shipCompany).HasMaxLength(500);

            Property(s => s.shipAddress).HasMaxLength(500);

            Property(s => s.shipPostalZipCode).HasMaxLength(20);

            Property(s => s.shipPostalZipCode).HasMaxLength(200);

            Property(s => s.shipContact).HasMaxLength(250);

            Property(s => s.shipTelephone).HasMaxLength(20);

            Property(s => s.shipEmail).HasMaxLength(250);

            Property(s => s.shipMethod).HasMaxLength(250);

            Property(s => s.shipAccountNumber).HasMaxLength(150);

            Property(s => s.shipTrackingNumber).HasMaxLength(150);
            
            Property(s => s.shipMediaStatus).HasMaxLength(150);

            Property(s => s.shipMediaDate).IsOptional();

            Property(s => s.shipDataShipped).IsOptional();

            Property(s => s.shipInstructions).HasMaxLength(2000);

            Property(s => s.shipContents).HasMaxLength(2000);

            Property(s => s.shipPreRecoveryInfo).HasMaxLength(2000);
        }
    }
}
