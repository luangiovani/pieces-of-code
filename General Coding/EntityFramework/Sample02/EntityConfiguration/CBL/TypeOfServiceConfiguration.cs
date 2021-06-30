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
    public class TypeOfServiceConfiguration : EntityTypeConfiguration<TypeOfService>
    {
        public TypeOfServiceConfiguration()
        {
            HasKey(t => t.typeofservice_id)
                .Property(s => s.typeofservice_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.name).HasMaxLength(250);

            Property(t => t.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            ////Property().IsRequired();

            Property(t => t.order).IsOptional();
        }
    }
}
