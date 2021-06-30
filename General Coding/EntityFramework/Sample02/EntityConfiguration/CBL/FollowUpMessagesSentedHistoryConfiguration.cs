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
    public class FollowUpMessagesSentedHistoryConfiguration : EntityTypeConfiguration<FollowUpMessagesSentedHistory>
    {
        public FollowUpMessagesSentedHistoryConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(f => f.followUpMessagesSentedHistory_id)
                .Property((f => f.followUpMessagesSentedHistory_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(f => f.serviceOrder_id).HasPrecision(18,0);
            Property(f => f.dateToSend).IsRequired();
            Property(f => f.fromEmails).IsRequired().HasMaxLength(2000);
            Property(f => f.toEmails).IsRequired().HasMaxLength(2000);
            Property(f => f.bccEmails).IsOptional().HasMaxLength(2000);
            Property(f => f.ccEmails).IsOptional().HasMaxLength(2000);
            Property(f => f.subject).IsRequired().HasMaxLength(250);
            Property(f => f.textBody).IsRequired().HasMaxLength(8000);
            Property(f => f.dateUpdate).IsRequired();
            Property(f => f.userUpdate_id).IsRequired();
            Property(f => f.dateRegistration).IsRequired();
            Property(f => f.userRegistration_id).IsRequired();
            Property(f => f.dateSented).IsOptional();
            Property(f => f.userSented_id).IsOptional();
        }
    }
}

