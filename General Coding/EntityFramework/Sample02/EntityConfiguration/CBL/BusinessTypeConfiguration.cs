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
    public class BusinessTypeConfiguration : EntityTypeConfiguration<BusinessType>
    {
        public BusinessTypeConfiguration ()
	    {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(b => b.business_type_id)
                .Property((b => b.business_type_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ///Tamanho máximo de 500 caracteres para a descrição
            Property(b => b.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property().IsRequired();

            Property(c => c.dateRegistration).IsRequired();

            Property(c => c.userRegistration_id).IsRequired();
	    }
    }
}
