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
    public class FollowUpMessagesBodyConfiguration : EntityTypeConfiguration<FollowUpMessagesBody>
    {
        public FollowUpMessagesBodyConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(f => f.followUpMessages_id)
                .Property((f => f.followUpMessages_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(f => f.name).HasMaxLength(500);

            Property(f => f.description).HasMaxLength(4000);

            Property(f => f.subject).HasMaxLength(250).IsRequired();

            Property(f => f.textBody).HasMaxLength(8000);

            Property(f => f.dateRegistration).IsRequired();

            Property(f => f.userRegistration_id).IsRequired();

            Property(f => f.dateUpdate).IsRequired();

            Property(f => f.userUpdate_id).IsRequired();
        }
    }
}
