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
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Configuracao da Entidade Currency (Moeda), para mapeamento na tabela Currency no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class CurrencyConfiguration : EntityTypeConfiguration<Currency>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public CurrencyConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(cy => cy.currency_id)
                .Property((ac => ac.currency_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ///Tamanho máximo de 4 caracteres para a sigla da Moeda
            Property(cy => cy.initials).HasMaxLength(4);


            ///Tamanho máximo de 3 caracteres para a sigla da Moeda
            Property(cy => cy.currency).HasMaxLength(3);

            ///Tamanho máximo de 50 caracteres para o nome da moeda
            Property(cy => cy.name).HasMaxLength(50);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property(cy => cy.active).IsRequired();

            Property(cy => cy.dateRegistration).IsRequired();

            Property(cy => cy.userRegistration_id).IsRequired();

        }
    }
}
