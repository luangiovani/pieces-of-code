using Framework.Domain.Utils;
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
    /// Mapeamento da Entidade Suppliers (Fornecedores), para gravação na tabela Suppliers no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class SuppliersViewModel
    {
        public SuppliersViewModel()
        {
            novoContato = new ContactViewModel();
            novoContato.whoIsContact = WhoIsContact.Supplier;
            contacts = new List<SuppliersContactViewModel>();
        }

        /// <summary>
        /// Supplier ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name="Supplier ID")]
        public int supplier_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do Fornecedor
        /// </summary>
        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

       
        /// <summary>
        /// Address
        /// Endereço completo do Fornecedor
        /// </summary>
        [Display(Name = "Address")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string address { get; set; }

        /// <summary>
        /// Postal Zip/Code
        /// CEP ou código postal do endereço do fornecedor
        /// </summary>
        [Display(Name = "Postal Zip/Code")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string postalZipCode { get; set; }

        /// <summary>
        /// City
        /// Cidade do Endereço do Fornecedor
        /// </summary>
        [Required(ErrorMessage = "City is Required")]
        [Display(Name = "City")]
        public int city_id { get; set; }

        /// <summary>
        /// Web Site
        /// Endereço eletrônico do fornecedor
        /// </summary>
        [Display(Name = "Web Site")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string website { get; set; }

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

        public virtual ContactViewModel novoContato { get; set; }

        /// <summary>
        /// City
        /// Cidade do Endereço do Fornecedor
        /// </summary>
        [Display(Name="City")]
        public virtual CityViewModel city { get; set; }

        /// <summary>
        /// Contacts
        /// Lista de Contatos do Fornecedor
        /// </summary>
        [Display(Name="Contacts")]
        public virtual ICollection<SuppliersContactViewModel> contacts { get; set; }

        /// <summary>
        /// Medias
        /// Lista de equipamentos do Fornecedor
        /// </summary>
        [Display(Name="Medias")]
        public virtual ICollection<MediaViewModel> medias { get; set; }
        public object controller { get; set; }
    }
}
