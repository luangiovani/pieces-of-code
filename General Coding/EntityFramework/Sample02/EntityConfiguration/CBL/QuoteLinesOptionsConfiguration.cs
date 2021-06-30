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
    public class QuoteLinesOptionsConfiguration : EntityTypeConfiguration<QuoteLinesOptions>
    {       
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public QuoteLinesOptionsConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(ql => ql.quoteLineOption_id)
                .Property((ql => ql.quoteLineOption_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ///Tamanho máximo de 50 caracteres para o nome do tipo de erro encontrado
            Property(ql => ql.name).HasMaxLength(50);

            ///Tamanho máximo de 500 caracteres para a descrição do tipo de erro encontrado
            Property(ql => ql.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property(ql => ql.active).IsRequired();

            Property(ql => ql.dateRegistration).IsRequired();

            Property(ql => ql.userRegistration_id).IsRequired();

            Property(ql => ql.id_old).IsOptional();
        }
    }
}
