using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
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
    public class LocationsViewModel
    {
        /// <summary>
        /// Location ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Location ID")]
        public int location_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do Escritório CBL
        /// </summary>
        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// EIN
        /// CNPJ no Brasil e Employer Identification Number nos Estados Unidos
        /// </summary>
        [Required(ErrorMessage = "CNPJ/EIN is Required")]
        [Display(Name = "CNPJ/EIN")]
        [StringLength(128, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string cnpj { get; set; }

        /// <summary>
        /// IE/SR
        /// Inscrição Estadual no Brasil e State Register nos Estados Unidos
        /// </summary>
        [Display(Name = "IE/SR")]
        [StringLength(128, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string ie { get; set; }

        /// <summary>
        /// Description
        /// Descrição da location CBL
        /// </summary>
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string description { get; set; }

        /// <summary>
        /// Company Name
        /// Nome da Empresa Location CBL
        /// </summary>
        [Display(Name = "Company Name")]
        [StringLength(400, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string companyName { get; set; }

        /// <summary>
        /// Address
        /// Endereço completo com Logradouro, numero, complemento
        /// </summary>
        [Display(Name = "Address")]
        [StringLength(400, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string address { get; set; }

        /// <summary>
        /// District
        /// Bairo do endereço da Location
        /// </summary>
        [Display(Name = "District")]
        [StringLength(150, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string district { get; set; }

        /// <summary>
        /// Postal Zip/Code
        /// CEP ou Código Postal do endereço
        /// </summary>
        [Display(Name = "Postal Zip/Code")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string postalZipCode { get; set; }

        /// <summary>
        /// City
        /// Cidade da Location
        /// </summary>
        [Display(Name = "City")]
        public int city_id { get; set; }

        /// <summary>
        /// Toll Free
        /// Número de telefone gratuito (0800)
        /// </summary>
        [Display(Name = "Toll Free")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string tollFree { get; set; }

        /// <summary>
        /// Telephone
        /// Número de telefone da Location
        /// </summary>
        [Display(Name = "Telephone")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string telephone { get; set; }

        /// <summary>
        /// Phone Extension
        /// Extensão do Telefone
        /// </summary>
        [Display(Name = "Extension")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string phoneExtension { get; set; }

        /// <summary>
        /// SAC Contact
        /// Nome da pessoa de Contato do SAC
        /// </summary>
        [Display(Name = "SAC Contact Name")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string sacContactName { get; set; }

        /// <summary>
        /// SAC Contact Email
        /// email da pessoa de Contato do SAC
        /// </summary>
        [Display(Name = "SAC Contact Email")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string sacContactEmail { get; set; }

        /// <summary>
        /// Line 1
        /// Número de telefone linha 1
        /// </summary>
        [Required(ErrorMessage="Line 1 is Required")]
        [Display(Name = "Line 1")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string line1 { get; set; }

        /// <summary>
        /// Line 2
        /// Número de telefone linha 2
        /// </summary>
        [Display(Name = "Line 2")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string line2 { get; set; }

        /// <summary>
        /// Line 3
        /// Número de telefone linha 3
        /// </summary>
        [Display(Name = "Line 3")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string line3 { get; set; }

        /// <summary>
        /// Line 4
        /// Número de telefone linha 4
        /// </summary>
        [Display(Name = "Line 4")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string line4 { get; set; }

        /// <summary>
        /// Line 5
        /// Número de telefone linha 5
        /// </summary>
        [Display(Name = "Line 5")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string line5 { get; set; }

        /// <summary>
        /// Fax 1
        /// Número de fax 1
        /// </summary>
        [Display(Name = "Fax 1")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string fax1 { get; set; }

        /// <summary>
        /// Fax 2
        /// Número de fax 2
        /// </summary>
        [Display(Name = "Fax 2")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string fax2 { get; set; }

        /// <summary>
        /// Web Site
        /// Endereço eletrônico (site) da Location CBL
        /// </summary>
        [Display(Name = "Web Site")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string website { get; set; }

        /// <summary>
        /// Gateway IP
        /// Endereço IP do Gateway
        /// </summary>
        [Display(Name = "Gateway IP")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string gatewayIp { get; set; }

        /// <summary>
        /// Range IP
        /// Range (intervalo) de Endereços IP
        /// </summary>
        [Display(Name = "Range IP")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string ipRange { get; set; }

        /// <summary>
        /// Primary DNS
        /// DNS primário
        /// </summary>
        [Display(Name = "Primary DNS")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string primaryDNS { get; set; }

        /// <summary>
        /// Secondary DNS
        /// DNS Secundário
        /// </summary>
        [Display(Name = "Secondary DNS")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string secondaryDNS { get; set; }
        
        /// <summary>
        /// Sub Net
        /// Máscara de Sub-Rede
        /// </summary>
        [Display(Name = "Sub Net")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string subNet { get; set; }

        /// <summary>
        /// International Gateway
        /// Gateway internacional
        /// </summary>
        [Display(Name = "Internal Gateway")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string internalGateway { get; set; }

        /// <summary>
        /// Internal Sub Net
        /// Máscara de Sub-Rede interna
        /// </summary>
        [Display(Name = "Internal Sub Net")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string internalSubNet { get; set; }

        /// <summary>
        /// Admin PC IP
        /// Endereço IP do computador administrador da rede
        /// </summary>
        [Display(Name = "Admin PC IP")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string adminPcIp { get; set; }

        /// <summary>
        /// Admin Machine IP
        /// Endereço IP da máquina administradora
        /// </summary>
        [Display(Name = "Admin Machine IP")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string adminMachineIp { get; set; }

        /// <summary>
        /// Admin Machine User
        /// Usuário administrador
        /// </summary>
        [Display(Name = "Admin Machine User")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string adminMachineUser { get; set; }

        /// <summary>
        /// Admin Machine Password
        /// Senha do Usuário administrador
        /// </summary>
        [Display(Name = "Admin Machine Password")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string adminMachinePwd { get; set; }

        /// <summary>
        /// Lab Machine IP
        /// Endereço IP da máquina da Location
        /// </summary>
        [Display(Name = "Lab Machine IP")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string labMachineIP { get; set; }

        /// <summary>
        /// Lab Machine User
        /// Usuário administrador
        /// </summary>
        [Display(Name = "lab Machine User")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string labMachineUser { get; set; }

        /// <summary>
        /// Lab Machine Pwd
        /// Senha do Usuário administrador
        /// </summary>
        [Display(Name = "Lab Machine Password")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string labMachinePwd { get; set; }

        /// <summary>
        /// QC Admin Machine IP
        /// Endereço IP da máquina QC
        /// </summary>
        [Display(Name = "QC Admin Machine IP")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string qcAdminMachineIp { get; set; }

        /// <summary>
        /// QC Admin Machine User
        /// Usuário administrador da máquina QC
        /// </summary>
        [Display(Name = "QC Admin Machine User")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string qcAdminMachineUser { get; set; }

        /// <summary>
        /// QC Admin Machine Pwd
        /// Senha do Usuário administrador da máquina QC
        /// </summary>
        [Display(Name = "QC Admin Machine Password")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string qcAdminMachinePwd { get; set; }

        /// <summary>
        /// VNC Admin Machine IP
        /// Endereço IP da máquina administradora do VNC
        /// </summary>
        [Display(Name = "VNC Admin Machine IP")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string vncAdminMachineIp { get; set; }

        /// <summary>
        /// VNC Admin Machine User
        /// Usuário da máquina administradora do VNC
        /// </summary>
        [Display(Name = "VNC Admin Machine User")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string vncAdminMachineUser { get; set; }

        /// <summary>
        /// VNC Admin Machine Pwd
        /// Senha do Usuário da máquina administradora do VNC
        /// </summary>
        [Display(Name = "VNC Admin Machine Password")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string vncAdminMachinePwd { get; set; }

        /// <summary>
        /// VNC Lab Machine IP
        /// Endereço IP da máquina Lab VNC
        /// </summary>
        [Display(Name = "VNC Lab Machine IP")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string vncLabMachineIP { get; set; }
        
        /// <summary>
        /// VNC Lab Machine User
        /// Usuário da máquina Lab VNC
        /// </summary>
        [Display(Name = "VNC Lab Machine User")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string vncLabMachineUser { get; set; }

        /// <summary>
        /// VNC Lab Machine Pwd
        /// Senha do Usuário da máquina Lab VNC
        /// </summary>
        [Display(Name = "VNC Lab Machine Password")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string vncLabMachinePwd { get; set; }

        /// <summary>
        /// FTP IP
        /// Endereço IP do FTP
        /// </summary>
        [Display(Name = "FTP IP")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string ftpIp { get; set; }

        /// <summary>
        /// FTP User
        /// Usuário de FTP
        /// </summary>
        [Display(Name = "FTP User")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string ftpUser { get; set; }

        /// <summary>
        /// FTP Pwd
        /// Senha do Usuário de FTP
        /// </summary>
        [Display(Name = "FTP Password")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string ftpPwd { get; set; }

        /// <summary>
        /// News Group IP
        /// Endereço IP de News Group
        /// </summary>
        [Display(Name = "News Group IP")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string newsGroupIp { get; set; }

        /// <summary>
        /// News Group User
        /// Usuário de News Group
        /// </summary>
        [Display(Name = "News Group User")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string newsGroupUser { get; set; }

        /// <summary>
        /// News Group Pwd
        /// Senha do Usuário de News Group
        /// </summary>
        [Display(Name = "News Group Password")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string newsGroupPwd { get; set; }

        /// <summary>
        /// Other Info
        /// Outras informações pertinentes a Location
        /// </summary>
        [Display(Name = "Other Informations")]
        [StringLength(2000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
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

        [Display(Name = "Bank")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string bank { get; set; }

        [Display(Name = "Agency #")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string accountAgency { get; set; }

        [Display(Name = "Account #")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string accountNumber { get; set; }

        [Display(Name = "Account Type")]
        [StringLength(50, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string accountType { get; set; }

        [Display(Name = "Max # Parcels")]
        [StringLength(4, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string maxParcels { get; set; }

        [Display(Name = "Series")]
        [StringLength(2, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string OS_Series { get; set; }

        public string foto { get; set; }

        /// <summary>
        /// City
        /// Cidade da Location 
        /// </summary>
        [Display(Name = "City")]
        [ScaffoldColumn(false)]
        public virtual CityViewModel city { get; set; }

        public virtual ICollection<UsuarioViewModel> users { get; set; }

        [Display(Name = "Locations")]
        public virtual ICollection<RoleLocationsViewModel> roleLocations { get; set; }

        /// <summary>
        /// emails
        /// Lista de emails 
        /// </summary>
        [Display(Name = "Emails")]
        public virtual ICollection<EmailServiceOrderViewModel> emails { get; set; }

        /// <summary>
        /// Medias
        /// Lista de equipamentos da Location
        /// </summary>
        //[Display(Name = "Medias")]
        //public virtual ICollection<MediaViewModel> medias { get; set; }

        /// <summary>
        /// Service Orders
        /// Lista de ordens de serviço da Location
        /// </summary>
        //[Display(Name = "Service Orders")]
        //public virtual ICollection<ServiceOrderViewModel> serviceOrders { get; set; }

        /// <summary>
        /// Service Orders Received
        /// Lista de ordens de serviço recebidas pela Location
        /// </summary>
        //[Display(Name = "Service Orders Received")]
        //public virtual ICollection<ServiceOrderViewModel> serviceOrdersReceived { get; set; }



        public static implicit operator LocationsViewModel(Locations obj)
        {
            if (obj != null)
            {
                return new LocationsViewModel
                {
                    accountAgency = obj.accountAgency,
                    accountNumber = obj.accountNumber,
                    accountType = obj.accountType,
                    address = obj.address,
                    adminMachineIp = obj.adminMachineIp,
                    adminMachinePwd = obj.adminMachinePwd,
                    adminMachineUser = obj.adminMachineUser,
                    adminPcIp = obj.adminPcIp,
                    bank = obj.bank,
                    city = obj.city,
                    city_id = obj.city_id,
                    cnpj = obj.cnpj,
                    companyName = obj.companyName,
                    dateRegistration = obj.dateRegistration,
                    description = obj.description,
                    district = obj.district,
                    fax1 = obj.fax1,
                    //emails = obj.emailsServiceOrder,
                    fax2 = obj.fax2,
                    ftpIp = obj.ftpIp,
                    ftpPwd = obj.ftpPwd,
                    ftpUser = obj.ftpUser,
                    gatewayIp = obj.gatewayIp,
                    ie = obj.ie,
                    internalGateway = obj.internalGateway,
                    internalSubNet = obj.internalSubNet,
                    ipRange = obj.ipRange,
                    labMachineIP = obj.labMachineIP,
                    labMachinePwd = obj.labMachinePwd,
                    labMachineUser = obj.labMachineUser,
                    line1 = obj.line1,
                    line2 = obj.line2,
                    line3 = obj.line3,
                    line4 = obj.line4,
                    line5 = obj.line5,
                    location_id = obj.location_id,
                    maxParcels = obj.maxParcels,
                    name = obj.name,
                    newsGroupIp = obj.newsGroupIp,
                    newsGroupPwd = obj.newsGroupPwd,
                    newsGroupUser = obj.newsGroupUser,
                    OS_Series = obj.OS_Series,
                    otherInformations = obj.otherInformations,
                    phoneExtension = obj.phoneExtension,
                    postalZipCode = obj.postalZipCode,
                    primaryDNS = obj.primaryDNS,
                    qcAdminMachineIp = obj.qcAdminMachineIp,
                    qcAdminMachinePwd = obj.qcAdminMachinePwd,
                    qcAdminMachineUser = obj.qcAdminMachineUser,
                    //roleLocations = obj.roleLocations,
                    sacContactEmail = obj.sacContactEmail,
                    sacContactName = obj.sacContactName,
                    secondaryDNS = obj.secondaryDNS,
                    subNet = obj.subNet,
                    telephone = obj.telephone,
                    tollFree = obj.tollFree,
                    userRegistration_id = obj.userRegistration_id,
                    //users = obj.users,
                    vncAdminMachineIp = obj.vncAdminMachineIp,
                    vncAdminMachinePwd = obj.vncAdminMachinePwd,
                    vncAdminMachineUser = obj.vncAdminMachineUser,
                    vncLabMachineIP = obj.vncLabMachineIP,
                    vncLabMachinePwd = obj.vncLabMachinePwd,
                    vncLabMachineUser = obj.vncLabMachineUser,
                    website = obj.website,
                    foto = obj.foto,
                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator Locations(LocationsViewModel obj)
        {
            if (obj != null)
            {
                return new Locations
                {

                    accountAgency = obj.accountAgency,
                    accountNumber = obj.accountNumber,
                    accountType = obj.accountType,
                    address = obj.address,
                    adminMachineIp = obj.adminMachineIp,
                    adminMachinePwd = obj.adminMachinePwd,
                    adminMachineUser = obj.adminMachineUser,
                    adminPcIp = obj.adminPcIp,
                    bank = obj.bank,
                    city = obj.city,
                    city_id = obj.city_id,
                    cnpj = obj.cnpj,
                    companyName = obj.companyName,
                    dateRegistration = obj.dateRegistration,
                    description = obj.description,
                    district = obj.district,
                    fax1 = obj.fax1,
                    //emails = obj.emailsServiceOrder,
                    fax2 = obj.fax2,
                    ftpIp = obj.ftpIp,
                    ftpPwd = obj.ftpPwd,
                    ftpUser = obj.ftpUser,
                    gatewayIp = obj.gatewayIp,
                    ie = obj.ie,
                    internalGateway = obj.internalGateway,
                    internalSubNet = obj.internalSubNet,
                    ipRange = obj.ipRange,
                    labMachineIP = obj.labMachineIP,
                    labMachinePwd = obj.labMachinePwd,
                    labMachineUser = obj.labMachineUser,
                    line1 = obj.line1,
                    line2 = obj.line2,
                    line3 = obj.line3,
                    line4 = obj.line4,
                    line5 = obj.line5,
                    location_id = obj.location_id,
                    maxParcels = obj.maxParcels,
                    name = obj.name,
                    newsGroupIp = obj.newsGroupIp,
                    newsGroupPwd = obj.newsGroupPwd,
                    newsGroupUser = obj.newsGroupUser,
                    OS_Series = obj.OS_Series,
                    otherInformations = obj.otherInformations,
                    phoneExtension = obj.phoneExtension,
                    postalZipCode = obj.postalZipCode,
                    primaryDNS = obj.primaryDNS,
                    qcAdminMachineIp = obj.qcAdminMachineIp,
                    qcAdminMachinePwd = obj.qcAdminMachinePwd,
                    qcAdminMachineUser = obj.qcAdminMachineUser,
                    //roleLocations = obj.roleLocations,
                    sacContactEmail = obj.sacContactEmail,
                    sacContactName = obj.sacContactName,
                    secondaryDNS = obj.secondaryDNS,
                    subNet = obj.subNet,
                    telephone = obj.telephone,
                    tollFree = obj.tollFree,
                    userRegistration_id = obj.userRegistration_id,
                    //users = obj.users,
                    vncAdminMachineIp = obj.vncAdminMachineIp,
                    vncAdminMachinePwd = obj.vncAdminMachinePwd,
                    vncAdminMachineUser = obj.vncAdminMachineUser,
                    vncLabMachineIP = obj.vncLabMachineIP,
                    vncLabMachinePwd = obj.vncLabMachinePwd,
                    vncLabMachineUser = obj.vncLabMachineUser,
                    website = obj.website,
                    foto = obj.foto,


                };
            }
            else
            {
                return null;
            }
        }


    }
}
