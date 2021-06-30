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
    public class TypeRAIDControlledConfiguration : EntityTypeConfiguration<TypeRAIDControlled>
    {
        public TypeRAIDControlledConfiguration()
        {
            HasKey(t => t.typeRAIDControlled_id)
                .Property(s => s.typeRAIDControlled_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.name).HasMaxLength(250);

            Property(t => t.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property().IsRequired();
        }
    }
}
