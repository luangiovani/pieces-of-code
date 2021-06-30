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
    public class ReferredByConfiguration : EntityTypeConfiguration<ReferredBy>
    {
        public ReferredByConfiguration()
        {
            HasKey(r => r.referredBy_id)
                .Property((ac => ac.referredBy_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();

            ///Tamanho máximo de 100 caracteres para nome da Marca
            Property(r => r.name).HasMaxLength(250);

            ///Tamanho máximo de 500 caracteres para a descrição da Marca
            Property(r => r.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property(r => r.active).IsRequired();
        }
    }
}
