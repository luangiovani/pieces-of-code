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
    public class ServiceOrderSmsConfiguration : EntityTypeConfiguration<ServiceOrderSms>
    {
        public ServiceOrderSmsConfiguration()
        {
            HasKey(s => s.serviceOrderSms_id)
                .Property(s => s.serviceOrderSms_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(p => p.serviceOrder_id).HasPrecision(10, 0);

            Property(s => s.mensagem).IsRequired().HasMaxLength(120);
            Property(s => s.tipoSms).HasMaxLength(50);
            Property(s => s.nome).HasMaxLength(100);
            Property(s => s.dateRegistration).IsRequired();
            Property(s => s.userRegistration_id).IsRequired();

            
        }
    }
}
