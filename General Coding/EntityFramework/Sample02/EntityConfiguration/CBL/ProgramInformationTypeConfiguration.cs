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
    public class ProgramInformationTypeConfiguration : EntityTypeConfiguration<ProgramInformationType>
    {
        public ProgramInformationTypeConfiguration()
        {

            HasKey(p => p.programInfoType_id)
                .Property((ac => ac.programInfoType_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();

            ///Tamanho máximo de 100 caracteres para nome da Marca
            Property(p => p.name).HasMaxLength(100);

            ///Tamanho máximo de 500 caracteres para a descrição da Marca
            Property(p => p.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property(p => p.active).IsRequired();
        }
    }
}
