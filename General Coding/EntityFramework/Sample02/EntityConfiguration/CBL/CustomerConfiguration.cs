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
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public CustomerConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(c => c.customer_id)
                .Property((ac => ac.customer_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(c => c.contacts)
                .WithRequired(cc => cc.customer)
                .HasForeignKey(cc => cc.customer_id);

            ///Configuração de relacionamento das ordens de serviço que pertencem a este cliente
            HasMany(c => c.serviceOrders)
               .WithOptional(so => so.customer)
               .HasForeignKey(so => so.customer_id);

            HasMany(s => s.serviceOrderPayments)
                .WithRequired(sm => sm.customer)
                .HasForeignKey(sm => sm.customer_id);

            ///Tamanho máximo de 250 caracteres para o e-mail do cliente
            Property(c => c.email).HasMaxLength(250);

            ///Tamanho máximo de 250 caracteres para o nome do cliente
            Property(c => c.name).HasMaxLength(250);

            ///Tamanho máximo de 500 caracteres para o endereço do cliente
            Property(c => c.address).HasMaxLength(500);

            Property(c => c.addressNumber).HasMaxLength(20);

            Property(c => c.addressExtension).HasMaxLength(150);

            ///Tamanho máximo de 150 caracteres para o bairro do endereço do cliente
            Property(c => c.district).HasMaxLength(150);

            ///Tamanho máximo de 20 caracteres para o cep / código / endereço postal do cliente
            Property(c => c.postalZipCode).HasMaxLength(20);

            ///Tamanho máximo de 250 caracteres para o endereço eletrônico do cliente
            Property(c => c.website).HasMaxLength(250);

            ///Indica que o campo Data de Cadastro é obrigatório
            Property(c => c.dateRegistration).IsRequired();

            ///Tamanho máximo de 150 caracteres para a informação "chegou até nós como" do cliente
            Property(c => c.hearOfUs).HasMaxLength(150);

            ///Tamanho máximo de 150 caracteres para o meio de contato com o cliente
            Property(c => c.pointOfContact).HasMaxLength(150);

            ///Tamanho máximo de 200 caracteres para o termo de concessão de crédio para o cliente
            Property(c => c.creditTerms).HasMaxLength(200);

            ///Tamanho máximo de 20 caracteres para a senha do cliente
            Property(c => c.password).HasMaxLength(20);

            /// Indica que o campo percentual é opcional para o cliente
            Property(c => c.percentage).IsOptional();

            ///Tamanho máximo de 20 caracteres para o cpf/cnpj ou outro documento que venha a substituir do cliente
            Property(c => c.cpfCnpj).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o rg ou outro documento do cliente
            Property(c => c.rgIdState).HasMaxLength(20);

            ///Tamanho máximo de 500 caracteres para observações acerca do cliente
            Property(c => c.notes).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property().IsRequired();

            ///Tamanho máximo de 500 caracteres para tipo de negócio do cliente
            Property(c => c.business_type).HasMaxLength(500);

            Property(c => c.dateRegistration).IsRequired();

            Property(c => c.userRegistration_id).IsRequired();

            Property(c => c.tokenCode).HasMaxLength(255).IsOptional();

            Property(c => c.dateLastAccessPortal).IsOptional();

            Property(c => c.dateLastUpdateInformations).IsOptional();
        }
    }
}
