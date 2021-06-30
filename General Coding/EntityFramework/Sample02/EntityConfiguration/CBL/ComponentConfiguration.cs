using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <task_url>https://esfera.teamworkpm.net/#tasks/7016888</task_url>
/// <autor>Fabricio Kikina</autor>
/// <date>23/11/2016</date>
/// <sumary>
/// Mapeamento da Entidade Component, para gravação na tabela Component no Banco de Dados
/// </sumary>
/// 
namespace Framework.Database.EntityConfiguration.CBL
{
    public class ComponentConfiguration : EntityTypeConfiguration<Component>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public ComponentConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(c => c.component_id)
                .Property((ac => ac.component_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ///Tamanho máximo de 500 caracteres para o nome da moeda
            Property(c => c.description).HasMaxLength(500);

            ///Tamanho máximo de 10 caracteres para o nome da moeda
            Property(c => c.color).HasMaxLength(10);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property().IsRequired();

            Property(c => c.dateRegistration).IsRequired();

            Property(c => c.userRegistration_id).IsRequired();

            Property(c => c.stockAddress).HasMaxLength(250).IsOptional();

            HasMany(c => c.stockMovements)
                .WithOptional(s => s.Component)
                .HasForeignKey(s => s.component_id)
                .WillCascadeOnDelete(true);
        }
    }
}
