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
    public class SmsMenuConfiguration : EntityTypeConfiguration<SmsMenu>
    {
        public SmsMenuConfiguration()
        {
            HasKey(s => s.sms_id)
                .Property(s => s.sms_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(s => s.pergunta).IsRequired().HasMaxLength(100);
            Property(s => s.resposta).IsRequired().HasMaxLength(100);
            Property(s => s.dateRegistration).IsRequired();
            Property(s => s.userRegistration_id).IsRequired();

            
        }
    }
}
