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
    /// Configuracao da Entidade AgentCommissions (Comissões para Parceiros da CBL), para mapeamento na tabela AgentCommisions no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class AgentCommissionsConfiguration : EntityTypeConfiguration<AgentCommissions>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public AgentCommissionsConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(ac => ac.agentCommissions_id)
                .Property((ac => ac.agentCommissions_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ///Configuração de Relacionamento com a Tabela Agents (Parceiros CBL)
            HasRequired(ac => ac.agent)
                .WithMany(a => a.agentCommissions)
                .HasForeignKey(ac => ac.agent_id);

            ///Configuração de Relacionamento com a Tabela de Ordem de Serviço (ServiceOrder)
            HasRequired(ac => ac.serviceOrder)
                .WithMany(so => so.agentComissions)
                .HasForeignKey(ac => ac.serviceOrder_id);

            ///Tamanho máximo de 5 caracteres para a modeda utilizada (campo initials da entidade Currency)
            Property(ac => ac.currency).HasMaxLength(5);

            ///Tamanho máximo de 50 caracteres para a Nota Fiscal
            Property(ac => ac.projectInvoice).HasMaxLength(50);

            ///Determina a escala de precisão
            Property(ac => ac.quotedAmount).HasPrecision(18, 2);

            ///Determina a escala de precisão
            Property(ac => ac.discountGiven).HasPrecision(18, 2);

            ///Determina a escala de precisão
            Property(ac => ac.nextDepotAmount).HasPrecision(18, 2);

            ///Determina a escala de precisão
            Property(ac => ac.amountComm).HasPrecision(18, 2);

            ///Determina a escala de precisão
            Property(ac => ac.commisionPaid).HasPrecision(18, 2);

            ///Determina a escala de precisão
            Property(ac => ac.commisionGiven).HasPrecision(18, 2);

            ///Tamanho máximo de 5 caracteres para indicar a quantidade de peças adquiridas para execução do serviço
            Property(ac => ac.partsPurchased).HasMaxLength(250);

            Property(ac => ac.timeNeeded).HasMaxLength(50);

            ///Data de recebimento é opcional
            Property(ac => ac.received).IsOptional();

            ///Data de orçamento é opcional
            Property(ac => ac.quoted).IsOptional();

            ///Data de mudança para o status Go Ahead é opcional
            Property(ac => ac.goAhead).IsOptional();

            ///Data de pagamento do cliente é opcional
            Property(ac => ac.customerPaid).IsOptional();

            ///Indicador de status da Comissão do Agente é obrigatória
            //Property(ac => ac.active).IsRequired();

            //Data de Cadastro é Obrigatória
            Property(ac => ac.dateRegistration).IsRequired();

            //Usuário de Cadastro é obrigatório
            Property(ac => ac.userRegistration_id).IsRequired();
        }
    }
}
