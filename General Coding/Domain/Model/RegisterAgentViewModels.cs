using Framework.Domain.Model.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Domain.Model
{
    /// <summary>
    /// reverter as traduçoes das classes acima
    /// </summary>
    public class RegisterAgentViewModel
    {
        /// <summary>
        /// Agent ID
        /// Id interno do parceiro que será utilizado nos relacionamentos e controle único
        /// </summary>
        [Key]
        [Display(Name = "Agent ID")]
        public int agent_id { get; set; }

        /// <summary>
        /// City ID
        /// Id interno da cidade do endereço do parceiro
        /// </summary>
        [Required(ErrorMessage = "Cidade Obrigatório.")]
        [Display(Name = "Agent City Address")]
        public int city_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do parceiro
        /// </summary>
        [Required(ErrorMessage = "Nome Obrigatório.")]
        [Display(Name = "Agent Name")]
        [StringLength(500, ErrorMessage = "o {0} não pode exceder {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// Company Name
        /// Nome da empresa do parceiro
        /// </summary>
        [Display(Name = "Agent Company Name")]
        [StringLength(800, ErrorMessage = "o {0} não pode exceder {1} characters.")]
        public string companyName { get; set; }

        /// <summary>
        /// Address
        /// Endereço completo do parceiro (logradouro, número e complemento)
        /// </summary>
        [Display(Name = "Endereço")]
        [StringLength(400, ErrorMessage = "o {0} não pode exceder {1} characters.")]
        [Required(ErrorMessage = "Endereço Obrigatório")]
        public string address { get; set; }

        /// <summary>
        /// Postal / Zip
        /// CEP ou outro código postal do parceiro
        /// </summary>
        [Display(Name = "CEP")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Required(ErrorMessage = "CEP Obrigatório.")]
        public string postalZipCode { get; set; }

        /// <summary>
        /// Web Site
        /// Url do endereço eletrônico do parceiro
        /// </summary>
        [Display(Name = "Website")]
        [StringLength(250, ErrorMessage = "o {0} não pode exceder {1} characters.")]
        public string website { get; set; }

        /// <summary>
        /// User
        /// Usuario de acesso do parceiro
        /// </summary>
        [Display(Name = "Agent UserName")]
        [StringLength(150, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string userName { get; set; }

        /// <summary>
        /// Password
        /// Senha de acesso do parceiro
        /// </summary>
        [Display(Name = "Agent Password")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string password { get; set; }

        /// <summary>
        /// Forwarded To
        /// Encaminhamento do parceiro
        /// </summary>
        [Display(Name = "Forwarded To")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string forwardedTo { get; set; }

        /// <summary>
        /// Demarcation #
        /// Número de demarcação do parceiro
        /// </summary>
        [Display(Name = "Demarcation #")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string demarcation_no { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste parceiro
        /// </summary>
        [Display(Name = "Register Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dateRegistration { get; set; }

        /// <summary>
        /// Date Added
        /// Data de adição da Nota
        /// </summary>
        [Display(Name = "Date Added")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dateProgramInformation { get; set; }

        /// <summary>
        /// Program Information type
        /// Tipo do programa de parceria no cadastro da nota
        /// </summary>
        [Display(Name = "Program Information Type")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string programInformationType { get; set; }

        /// <summary>
        /// Program Information notes
        /// Notas do programa de parceria
        /// </summary>
        [Display(Name = "Program Information Notes")]
        [StringLength(2000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string programInformationNotes { get; set; }

        /// <summary>
        /// City
        /// Cidade do Endereço do Parceiro, retorna Estado e País, DDD e DDI
        /// </summary>
        [Display(Name = "City")]
        public virtual CityViewModel city { get; set; }

        [Required(ErrorMessage = "Nome do Contato Obrigatório")]//Name(Person) of Contact is required
        [Display(Name = "Name")]
        [StringLength(200, ErrorMessage = "o {0} não pode exceder {1} characters.")]
        public string nomeContato { get; set; }

        /// <summary>
        /// City ID
        /// Id interno da cidade que será utilizado nos relacionamentos
        /// </summary>
        [Display(Name = "City")]
        [Required(ErrorMessage = "Cidade Obrigatório.")]
        public int? cidadeContato { get; set; }

        /// <summary>
        /// Email
        /// E-mail de contato
        /// </summary>
        [Display(Name = "E-mail")]
        [StringLength(150, ErrorMessage = "o {0} não pode exceder {1} characters.")]
        [Required(ErrorMessage = "E-mail Obrigatório.")]
        public string emailContato { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha Obrigatório.")]
        public string senhaContato { get; set; }

        [Required(ErrorMessage = "CPF Obrigatório.")]
        public string cpf { get; set; }
        [Required(ErrorMessage = "Endereço Obrigatório")]
        public string enderecoContato { get; set; }
        //[Required(ErrorMessage = "Número Endereço Obrigatório")]
        public string numeroEnderecoContato { get; set; }
        public string complementoContato { get; set; }
        public string bairroContato { get; set; }
        
        [Required(ErrorMessage = "CEP Obrigatório")]
        public string cepContato { get; set; }
        public bool? indPrincipalContato { get; set; }

        [Required(ErrorMessage = "Celular Obrigatório.")]
        [Display(Name = "Celular")]
        [StringLength(20, ErrorMessage = "O {0} não pode exceder {1} characters.", MinimumLength = 10)]
        public string celular { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(20, ErrorMessage = "O {0} não pode exceder {1} characters.", MinimumLength = 10)]
        public string telefone { get; set; }
       


    }
    
}
