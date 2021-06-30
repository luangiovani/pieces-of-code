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
    public class TypeRAIDConfiguration : EntityTypeConfiguration<TypeOfRAID>
    {
        public TypeRAIDConfiguration()
        {
            HasKey(t => t.typeOfRaid_id)
                .Property(s => s.typeOfRaid_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.name).HasMaxLength(250);

            Property(t => t.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property().IsRequired();
        }
    }
}
