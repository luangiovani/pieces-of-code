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
    /// Mapeamento da Entidade State (Estados (UF)), para gravação na tabela State no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class StateViewModel
    {
        /// <summary>
        /// State ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name="State ID")]
        public int state_id { get; set; }

        /// <summary>
        /// Country ID
        /// Id interno do País que será utilizado nos relacionamentos
        /// </summary>
        [Required(ErrorMessage = "Country is Required")]
        [Display(Name = "Country")]
        public int country_id { get; set; }

        /// <summary>
        /// Initials
        /// Sigla do Estado
        /// </summary>
        [Required(ErrorMessage = "Initials is Required")]
        [Display(Name = "Initials")]
        [StringLength(6, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string initials { get; set; }

        /// <summary>
        /// Name
        /// Nome do Estado
        /// </summary>
        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// Entidade Country(País) carregada por lazy loading
        /// </summary>
        [Display(Name="Country")]
        public virtual CountryViewModel country { get; set; }

        /// <summary>
        /// Cities
        /// Cidades do Estado
        /// </summary>
        [Display(Name="Cities")]
        public virtual ICollection<CityViewModel> cities { get; set; }



        public static implicit operator StateViewModel(State obj)
        {

            return new StateViewModel
            {
                //cities = obj.cities,
                country = obj.country,
                country_id = obj.country_id,
                initials = obj.initials,
                name = obj.name,
                state_id = obj.state_id
                
            };
        }
        public static implicit operator State(StateViewModel obj)
        {

            return new State
            {
                //cities = obj.cities,
                country = obj.country,
                country_id = obj.country_id,
                initials = obj.initials,
                name = obj.name,
                state_id = obj.state_id

            };
        }
    }
}
