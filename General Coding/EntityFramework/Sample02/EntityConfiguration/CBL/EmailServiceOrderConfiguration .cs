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
    public class EmailServiceOrderConfiguration : EntityTypeConfiguration<EmailServiceOrder>
    {
        public EmailServiceOrderConfiguration()
        {
            HasKey(m => m.emailServiceOrder_id)
                .Property((ac => ac.emailServiceOrder_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            Property(j => j.name).HasMaxLength(100);

            Property(j => j.email).HasMaxLength(500);

            //Property(j => j.active).IsRequired();

            Property(m => m.location_id).IsRequired();

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();

            HasRequired(l => l.location)
               .WithMany(em => em.emailsServiceOrder)
               .HasForeignKey(l => l.location_id);

            
        }
    }
}
