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
    public class ServiceOrderRecoveryFollowUpConfiguration : EntityTypeConfiguration<ServiceOrderRecoveryFollowUp>
    {
        public ServiceOrderRecoveryFollowUpConfiguration()
        {
            HasKey(s => s.serviceOrder_id)
                .Property(s => s.serviceOrder_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(s => s.dateRegistration).IsRequired();

            Property(s => s.userRegistration_id).IsRequired();

            Property(s => s.mediaStatus).HasMaxLength(100);

            Property(s => s.rateOurService).HasMaxLength(100);

            Property(s => s.wouldBeReference).HasMaxLength(100);

            Property(s => s.sendLetterReference).HasMaxLength(100);

            Property(s => s.dateComplete).IsOptional();

            Property(s => s.introFaxed).IsOptional();

            Property(s => s.emailSent).IsOptional();

            Property(s => s.comments).HasMaxLength(2000);
        }
    }
}
