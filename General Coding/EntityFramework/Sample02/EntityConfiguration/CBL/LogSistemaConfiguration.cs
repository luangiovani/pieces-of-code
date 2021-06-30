using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// 
namespace Framework.Database.EntityConfiguration.CBL
{
    public class LogSistemaConfiguration : EntityTypeConfiguration<LogSistema>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public LogSistemaConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(c => c.log_id).Property((ac => ac.log_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ///Tamanho máximo de 500 caracteres para o nome da moeda
            Property(c => c.description).HasMaxLength(250);

            Property(c => c.date).IsRequired();

          
        }
    }
}
