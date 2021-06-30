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
    /// Mapeamento da Entidade Interfaces (Interface de comunicação dos esquipamentos), para gravação na tabela Interfaces no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class InterfacesViewModel
    {
        /// <summary>
        /// Interface ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Interfaces ID")]
        public int interface_id { get; set; }

        /// <summary>
        /// Name
        /// Nome da Interface (USB, IDE, SATA...)
        /// </summary>
        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// Description
        /// Descrição da interface
        /// </summary>
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string description { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste parceiro
        /// </summary>
        [Display(Name = "Register Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateRegistration { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>

        public string userRegistration_id { get; set; }


        public static implicit operator Interfaces(InterfacesViewModel obj)
        {

            return new Interfaces
            {
                dateRegistration = obj.dateRegistration,
                description = obj.description,
                interface_id = obj.interface_id,
                name = obj.name,
                userRegistration_id = obj.userRegistration_id

            };
        }
        public static implicit operator InterfacesViewModel(Interfaces obj)
        {

            return new InterfacesViewModel
            {
                dateRegistration = obj.dateRegistration,
                description = obj.description,
                interface_id = obj.interface_id,
                name = obj.name,
                userRegistration_id = obj.userRegistration_id
            };
        }


    }
}
