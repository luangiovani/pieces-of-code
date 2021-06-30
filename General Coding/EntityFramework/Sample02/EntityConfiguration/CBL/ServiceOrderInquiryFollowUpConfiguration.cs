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
    public class ServiceOrderInquiryFollowUpConfiguration : EntityTypeConfiguration<ServiceOrderInquiryFollowUp>
    {
        public ServiceOrderInquiryFollowUpConfiguration()
        {
            HasKey(s => s.serviceOrder_id)
                .Property(s => s.serviceOrder_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(s => s.causeNotSent).HasMaxLength(500);

            Property(s => s.sentSomewhereElseWhere).HasMaxLength(500);

            Property(s => s.comments).HasMaxLength(2000);

            Property(s => s.dateRegistration).IsRequired();

            Property(s => s.dateComplete).IsOptional();

        }
    }
}
