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
    /// Configuracao da Entidade Locations (Escritórios CBL), para mapeamento na tabela Locations no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class LocationsConfiguration : EntityTypeConfiguration<Locations>
    {
        /// <summary>
        /// Constructor
        /// Construtor da classe, mapeando as propriedades com suas peculiaridades
        /// </summary>
        public LocationsConfiguration()
        {
            ///Primary Key
            ///Propriedade que mapeia a propriedade da entidade que será Chave primária na tabela do banco de dados
            HasKey(l => l.location_id)
                .Property((ac => ac.location_id)).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(dt => dt.dateRegistration).IsRequired();

            Property(dt => dt.userRegistration_id).IsRequired();


            ///Configuração de Relacionamento com a Tabela City (Cidades)
            HasRequired(l => l.city)
                .WithMany(l => l.locations)
                .HasForeignKey(l => l.city_id);

            ///Configuração de Relacionamento com a Tabela Media (equipamentos da location)
            HasMany(l => l.medias)
                .WithOptional(m => m.location)
                .HasForeignKey(m => m.location_id);

            HasMany(l => l.serviceOrders)
                .WithOptional(s => s.location)
                .HasForeignKey(s => s.location_id);

            HasMany(l => l.serviceOrdersReceived)
                .WithRequired(s => s.locationReceived)
                .HasForeignKey(s => s.locationReceived_id);

            HasMany(l => l.locationStocks)
                .WithOptional(s => s.Location)
                .HasForeignKey(s => s.location_id);

            ///Tamanho máximo de 200 caracteres para a nome da Location
            Property(l => l.name).HasMaxLength(200);

            Property(l => l.cnpj).IsOptional();
            Property(l => l.ie).IsOptional();

            ///Tamanho máximo de 500 caracteres para a descrição da Location
            Property(l => l.description).HasMaxLength(500);

            ///Indicador que o campo Active (ativo ou não) é obrigatório
            //Property(l => l.active).IsRequired();

            ///Tamanho máximo de 400 caracteres para o companyName da location
            Property(a => a.companyName).HasMaxLength(400);

            ///Tamanho máximo de 400 caracteres para o endereço da location
            Property(a => a.address).HasMaxLength(400);

            ///Tamanho máximo de 20 caracteres para o cep/código postal da location
            Property(a => a.postalZipCode).HasMaxLength(20);

            ///Tamanho máximo de 250 caracteres para o website da location
            Property(a => a.website).HasMaxLength(250);

            ///Tamanho máximo de 150 caracteres para o bairro do endereço da location
            Property(l => l.district).HasMaxLength(150);

            ///Tamanho máximo de 20 caracteres para o telefone grátis da Location (0800)
            Property(l => l.tollFree).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o telefone da Location
            Property(l => l.telephone).HasMaxLength(20);
            
            ///Tamanho máximo de 20 caracteres para o ramal do telefone da Location
            Property(l => l.phoneExtension).HasMaxLength(20);

            ///Tamanho máximo de 500 caracteres para o nome do contato do SAC da Location
            Property(l => l.sacContactName).HasMaxLength(500);

            ///Tamanho máximo de 500 caracteres para o email do contato do SAC da Location
            Property(l => l.sacContactEmail).HasMaxLength(500);

            ///Tamanho máximo de 20 caracteres para a linha1 da Location
            Property(l => l.line1).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a linha2 da Location
            Property(l => l.line2).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a linha3 da Location
            Property(l => l.line3).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a linha4 da Location
            Property(l => l.line4).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a linha5 da Location
            Property(l => l.line5).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a fax1 da Location
            Property(l => l.fax1).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a fax2 da Location
            Property(l => l.fax2).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o IP da Location
            Property(l => l.gatewayIp).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o intervalo de IP da Location
            Property(l => l.ipRange).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o DNS primario da Location
            Property(l => l.primaryDNS).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para DNS secundário da Location
            Property(l => l.secondaryDNS).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a máscara de sub-rede da Location
            Property(l => l.subNet).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o gateway interno da Location
            Property(l => l.internalGateway).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a sub-rede interna da Location
            Property(l => l.internalSubNet).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o IP do PC administrador da Location
            Property(l => l.adminPcIp).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o IP da Máquina do administrador da Location
            Property(l => l.adminMachineIp).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o usuário do PC administrador da Location
            Property(l => l.adminMachineUser).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a senha do usuário do PC administrador da Location
            Property(l => l.adminMachinePwd).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o IP da máquina do Lab da Location
            Property(l => l.labMachineIP).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o usuário da máquina do Lab da Location
            Property(l => l.labMachineUser).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a senha do usuário da máquina do Lab da Location
            Property(l => l.labMachinePwd).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o IP da máquina de QC da Location
            Property(l => l.qcAdminMachineIp).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o usuário da máquina de QC da Location
            Property(l => l.qcAdminMachineUser).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a senha  do usuário da máquina de QC da Location
            Property(l => l.qcAdminMachinePwd).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o IP da máquina admin do VNC da Location
            Property(l => l.vncAdminMachineIp).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o usuário da máquina admin do VNC da Location
            Property(l => l.vncAdminMachineUser).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a senha do usuário da máquina admin do VNC da Location
            Property(l => l.vncAdminMachinePwd).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o IP da máquina admin do VNC  do Lab da Location
            Property(l => l.vncLabMachineIP).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o usuário da máquina admin do VNC  do Lab da Location
            Property(l => l.vncLabMachineUser).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a senha do usuário da máquina admin do VNC  do Lab da Location
            Property(l => l.vncLabMachinePwd).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o IP do FTP da Location
            Property(l => l.ftpIp).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o usuário do FTP da Location
            Property(l => l.ftpUser).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a senha do usuário do FTP da Location
            Property(l => l.ftpPwd).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o IP do NewsGroup da Location
            Property(l => l.newsGroupIp).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para o usuário do NewsGroup da Location
            Property(l => l.newsGroupUser).HasMaxLength(20);

            ///Tamanho máximo de 20 caracteres para a senha do usuário do NewsGroup da Location
            Property(l => l.newsGroupPwd).HasMaxLength(20);

            ///Tamanho máximo de 2000 caracteres para outras informações da Location
            Property(l => l.otherInformations).HasMaxLength(2000);

            Property(l => l.bank).HasMaxLength(250).IsOptional();

            Property(l => l.accountAgency).HasMaxLength(20).IsOptional();

            Property(l => l.accountNumber).HasMaxLength(20).IsOptional();

            Property(l => l.accountType).HasMaxLength(50).IsOptional();

            Property(l => l.maxParcels).HasMaxLength(4).IsOptional();

            Property(l => l.OS_Series).HasMaxLength(2).IsOptional();

            //HasMany(l => l.users)
            //   .WithRequired(u => u.location)
            //   .HasForeignKey(u => u.location_id);

            HasMany(l => l.roleLocations)
               .WithRequired(rl => rl.location)
               .HasForeignKey(rl => rl.location_id);

            ///Configuração de relacionamento das Cidades que contém este país
            HasMany(c => c.emailsServiceOrder)
                .WithRequired(ci => ci.location)
                .HasForeignKey(ci => ci.location_id);

           
        }
    }
}
