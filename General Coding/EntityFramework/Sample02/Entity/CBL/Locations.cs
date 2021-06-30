using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Mapeamento da Entidade Locations (Locais(Escritórios) da CBL), para gravação na tabela Locations no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class Locations
    {
        /// <summary>
        /// Location ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int location_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do Escritório CBL
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// EIN
        /// CNPJ no Brasil e Employer Identification Number nos Estados Unidos
        /// </summary>
        public string cnpj { get; set; }

        /// <summary>
        /// IE/SR
        /// Inscrição Estadual no Brasil e State Register nos Estados Unidos
        /// </summary>
        public string ie { get; set; }

        /// <summary>
        /// Description
        /// Descrição da location CBL
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Active
        /// Indicador de status da Location CBL
        /// </summary>
        //public bool active { get; set; }

        /// <summary>
        /// Company Name
        /// Nome da Empresa Location CBL
        /// </summary>
        public string companyName { get; set; }

        /// <summary>
        /// Address
        /// Endereço completo com Logradouro, numero, complemento
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// District
        /// Bairo do endereço da Location
        /// </summary>
        public string district { get; set; }

        /// <summary>
        /// Postal Zip/Code
        /// CEP ou Código Postal do endereço
        /// </summary>
        public string postalZipCode { get; set; }

        /// <summary>
        /// City
        /// Cidade da Location
        /// </summary>
        public int city_id { get; set; }

        /// <summary>
        /// Toll Free
        /// Número de telefone gratuito (0800)
        /// </summary>
        public string tollFree { get; set; }

        /// <summary>
        /// Telephone
        /// Número de telefone da Location
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// Phone Extension
        /// Extensão do Telefone
        /// </summary>
        public string phoneExtension { get; set; }

        /// <summary>
        /// SAC Contact
        /// Nome da pessoa de Contato do SAC
        /// </summary>
        public string sacContactName { get; set; }

        /// <summary>
        /// SAC Contact Email
        /// email da pessoa de Contato do SAC
        /// </summary>
        public string sacContactEmail { get; set; }

        /// <summary>
        /// Line 1
        /// Número de telefone linha 1
        /// </summary>
        public string line1 { get; set; }

        /// <summary>
        /// Line 2
        /// Número de telefone linha 2
        /// </summary>
        public string line2 { get; set; }

        /// <summary>
        /// Line 3
        /// Número de telefone linha 3
        /// </summary>
        public string line3 { get; set; }

        /// <summary>
        /// Line 4
        /// Número de telefone linha 4
        /// </summary>
        public string line4 { get; set; }

        /// <summary>
        /// Line 5
        /// Número de telefone linha 5
        /// </summary>
        public string line5 { get; set; }

        /// <summary>
        /// Fax 1
        /// Número de fax 1
        /// </summary>
        public string fax1 { get; set; }

        /// <summary>
        /// Fax 2
        /// Número de fax 2
        /// </summary>
        public string fax2 { get; set; }

        /// <summary>
        /// Web Site
        /// Endereço eletrônico (site) da Location CBL
        /// </summary>
        public string website { get; set; }

        /// <summary>
        /// Gateway IP
        /// Endereço IP do Gateway
        /// </summary>
        public string gatewayIp { get; set; }

        /// <summary>
        /// Range IP
        /// Range (intervalo) de Endereços IP
        /// </summary>
        public string ipRange { get; set; }

        /// <summary>
        /// Primary DNS
        /// DNS primário
        /// </summary>
        public string primaryDNS { get; set; }

        /// <summary>
        /// Secondary DNS
        /// DNS Secundário
        /// </summary>
        public string secondaryDNS { get; set; }
        
        /// <summary>
        /// Sub Net
        /// Máscara de Sub-Rede
        /// </summary>
        public string subNet { get; set; }

        /// <summary>
        /// International Gateway
        /// Gateway internacional
        /// </summary>
        public string internalGateway { get; set; }

        /// <summary>
        /// Internal Sub Net
        /// Máscara de Sub-Rede interna
        /// </summary>
        public string internalSubNet { get; set; }

        /// <summary>
        /// Admin PC IP
        /// Endereço IP do computador administrador da rede
        /// </summary>
        public string adminPcIp { get; set; }

        /// <summary>
        /// Admin Machine IP
        /// Endereço IP da máquina administradora
        /// </summary>
        public string adminMachineIp { get; set; }

        /// <summary>
        /// Admin Machine User
        /// Usuário administrador
        /// </summary>
        public string adminMachineUser { get; set; }

        /// <summary>
        /// Admin Machine Password
        /// Senha do Usuário administrador
        /// </summary>
        public string adminMachinePwd { get; set; }

        /// <summary>
        /// Lab Machine IP
        /// Endereço IP da máquina da Location
        /// </summary>
        public string labMachineIP { get; set; }

        /// <summary>
        /// Lab Machine User
        /// Usuário administrador
        /// </summary>
        public string labMachineUser { get; set; }

        /// <summary>
        /// Lab Machine Pwd
        /// Senha do Usuário administrador
        /// </summary>
        public string labMachinePwd { get; set; }

        /// <summary>
        /// QC Admin Machine IP
        /// Endereço IP da máquina QC
        /// </summary>
        public string qcAdminMachineIp { get; set; }

        /// <summary>
        /// QC Admin Machine User
        /// Usuário administrador da máquina QC
        /// </summary>
        public string qcAdminMachineUser { get; set; }

        /// <summary>
        /// QC Admin Machine Pwd
        /// Senha do Usuário administrador da máquina QC
        /// </summary>
        public string qcAdminMachinePwd { get; set; }

        /// <summary>
        /// VNC Admin Machine IP
        /// Endereço IP da máquina administradora do VNC
        /// </summary>
        public string vncAdminMachineIp { get; set; }

        /// <summary>
        /// VNC Admin Machine User
        /// Usuário da máquina administradora do VNC
        /// </summary>
        public string vncAdminMachineUser { get; set; }

        /// <summary>
        /// VNC Admin Machine Pwd
        /// Senha do Usuário da máquina administradora do VNC
        /// </summary>
        public string vncAdminMachinePwd { get; set; }

        /// <summary>
        /// VNC Lab Machine IP
        /// Endereço IP da máquina Lab VNC
        /// </summary>
        public string vncLabMachineIP { get; set; }
        
        /// <summary>
        /// VNC Lab Machine User
        /// Usuário da máquina Lab VNC
        /// </summary>
        public string vncLabMachineUser { get; set; }

        /// <summary>
        /// VNC Lab Machine Pwd
        /// Senha do Usuário da máquina Lab VNC
        /// </summary>
        public string vncLabMachinePwd { get; set; }

        /// <summary>
        /// FTP IP
        /// Endereço IP do FTP
        /// </summary>
        public string ftpIp { get; set; }

        /// <summary>
        /// FTP User
        /// Usuário de FTP
        /// </summary>
        public string ftpUser { get; set; }

        /// <summary>
        /// FTP Pwd
        /// Senha do Usuário de FTP
        /// </summary>
        public string ftpPwd { get; set; }

        /// <summary>
        /// News Group IP
        /// Endereço IP de News Group
        /// </summary>
        public string newsGroupIp { get; set; }

        /// <summary>
        /// News Group User
        /// Usuário de News Group
        /// </summary>
        public string newsGroupUser { get; set; }

        /// <summary>
        /// News Group Pwd
        /// Senha do Usuário de News Group
        /// </summary>
        public string newsGroupPwd { get; set; }

        /// <summary>
        /// Other Info
        /// Outras informações pertinentes a Location
        /// </summary>
        public string otherInformations { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste registro
        /// </summary>
        public DateTime dateRegistration { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>
        public string userRegistration_id { get; set; }

        public string bank { get; set; }

        public string accountAgency { get; set; }

        public string accountNumber { get; set; }

        public string accountType { get; set; }

        public string maxParcels { get; set; }
        
        public int? id_old { get; set; }

        public string OS_Series { get; set; }

        public string foto { get; set; }

        /// <summary>
        /// City
        /// Cidade da Location 
        /// </summary>
        public virtual City city { get; set; }       

        /// <summary>
        /// Service Orders
        /// Ordens de Serviço da Location
        /// </summary>
        public virtual ICollection<ServiceOrder> serviceOrders { get; set; }

        /// <summary>
        /// Service Orders
        /// Ordens de Serviço Recebidas pela Location
        /// </summary>
        public virtual ICollection<ServiceOrder> serviceOrdersReceived { get; set; }

        /// <summary>
        /// Medias
        /// Lista de equipamentos da Location
        /// </summary>
        public virtual ICollection<Media> medias { get; set; }

        public virtual ICollection<ApplicationUser> users { get; set; }

        public virtual ICollection<Stock> locationStocks { get; set; }

        public virtual ICollection<RoleLocations> roleLocations { get; set; }

        public virtual ICollection<EmailServiceOrder> emailsServiceOrder { get; set; }
    }
}
