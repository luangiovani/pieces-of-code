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
    public class ServiceOrderQuotingConfiguration : EntityTypeConfiguration<ServiceOrderQuoting>
    {
        public ServiceOrderQuotingConfiguration()
        {
            HasKey(s => s.serviceOrder_id)
                .Property(s => s.serviceOrder_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(s => s.dateRegistration).IsRequired();

            Property(s => s.userRegistration_id).IsRequired();

            HasMany(s => s.partsNeeded)
                .WithRequired(p => p.serviceOrderQuoting)
                .HasForeignKey(p => p.serviceOrder_id);

            Property(s => s.quoteEstimate).HasPrecision(18, 2);
            
            Property(s => s.quotedAmount).HasPrecision(18, 2);

            Property(s => s.discountGivem).HasPrecision(18, 2);

            Property(s => s.nextDepotAmount).HasPrecision(18, 2);

            Property(s => s.currency).HasMaxLength(4);

            Property(s => s.timeNeeded).HasMaxLength(200);

            Property(s => s.dueDate).IsOptional();

            Property(s => s.destination).HasMaxLength(250);

            Property(s => s.quoteLines).HasMaxLength(4000);

            Property(s => s.quoteDate).IsOptional();

            Property(s => s.quotedFinished).IsOptional();

            Property(s => s.statusQuoting).HasMaxLength(50).IsOptional();

            Property(s => s.quoteDays).IsOptional();
        }
    }
}
