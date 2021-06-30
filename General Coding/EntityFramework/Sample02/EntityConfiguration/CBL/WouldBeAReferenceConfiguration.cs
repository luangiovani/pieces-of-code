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
    public class WouldBeAReferenceConfiguration : EntityTypeConfiguration<WouldBeAReference>
    {
        public WouldBeAReferenceConfiguration()
        {
            HasKey(w => w.wouldBeAReference_id)
                .Property(s => s.wouldBeAReference_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(w=> w.name).HasMaxLength(250);

            Property(w=> w.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property(w=> w.active).IsRequired();
        }
    }
}
