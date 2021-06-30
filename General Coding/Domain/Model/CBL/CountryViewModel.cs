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
    /// Mapeamento da Entidade Country (Países das cidades e estados utilizadas em endereços, contatos), para gravação na tabela Country no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class CountryViewModel
    {
        /// <summary>
        /// Country ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Country ID")]
        public int country_id { get; set; }

        /// <summary>
        /// Initials
        /// Sigla do País
        /// </summary>
        [Required(ErrorMessage = "Initials of Country is required")]
        [Display(Name = "Initials")]
        [StringLength(3, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string initials { get; set; }

        /// <summary>
        /// Name
        /// Nome do País
        /// </summary>
        [Required(ErrorMessage = "Name of Country is required")]
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// IDD
        /// DDI do País
        /// </summary>
        [Display(Name = "IDD")]
        public int idd { get; set; }

        /// <summary>
        /// Cities
        /// Lista de Cidades do País
        /// </summary>
        [Display(Name="Cities")]
        public virtual ICollection<CityViewModel> cities { get; set; }

        /// <summary>
        /// States
        /// Lista de Estados do País
        /// </summary>
        [Display(Name="States")]
        public virtual ICollection<StateViewModel> states { get; set; }


        public static implicit operator CountryViewModel(Country obj)
        {

            return new CountryViewModel
            {
                //cities = obj.cities,
                country_id = obj.country_id,
                idd = obj.idd,
                initials = obj.initials,
                name = obj.name,
                //states = obj.states 
            };
        }
        public static implicit operator Country(CountryViewModel obj)
        {

            return new Country
            {
                //cities = obj.cities,
                country_id = obj.country_id,
                idd = obj.idd,
                initials = obj.initials,
                name = obj.name,
                //states = obj.states
            };
        }

    }
}
