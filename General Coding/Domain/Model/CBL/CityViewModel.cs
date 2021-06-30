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
    /// Mapeamento da Entidade City (Cidades utilizadas em endereços, contatos), para gravação na tabela City no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class CityViewModel
    {
        /// <summary>
        /// City ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name="City ID")]
        public int city_id { get; set; }

        /// <summary>
        /// State ID
        /// Id interno do Estado que será utilizado nos relacionamentos
        /// </summary>
        [Required(ErrorMessage="State is required")]
        [Display(Name = "State")]
        public int state_id { get; set; }

        /// <summary>
        /// Country ID
        /// Id interno do País que será utilizado nos relacionamentos
        /// </summary>
        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public int country_id { get; set; }

        /// <summary>
        /// Name
        /// Nome da Cidade
        /// </summary>
        [Required(ErrorMessage = "Name of City is required")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Display(Name = "Name")]
        public string name { get; set; }

        /// <summary>
        /// DDD
        /// DDD da cidade
        /// </summary>
        [Display(Name="DDD")]
        public int ddd { get; set; }
        
        /// <summary>
        /// State
        /// Estado (UF) da Cidade
        /// </summary>
        [Display(Name = "State")]
        public virtual StateViewModel state { get; set; }

        /// <summary>
        /// Country
        /// País da Cidade e Estado
        /// </summary>
        [Display(Name = "Country")]
        public virtual CountryViewModel country { get; set; }

        /*/// <summary>
        /// Agents
        /// Parceiros CBL localizados na cidade
        /// </summary>
        [Display(Name = "Agents")]
        public virtual ICollection<AgentViewModel> agents { get; set; }

        /// <summary>
        /// Locations
        /// Escritórios CBL localizados na cidade
        /// </summary>
        [Display(Name = "Locations")]
        public virtual ICollection<LocationsViewModel> locations { get; set; }

        /// <summary>
        /// Contacts
        /// Contatos localizados na cidade
        /// </summary>
        [Display(Name = "Contacts")]
        public virtual ICollection<ContactViewModel> contacts { get; set; }

        /// <summary>
        /// Suppliers
        /// Fornecedores localizados na cidade
        /// </summary>
        [Display(Name = "Suppliers")]
        public virtual ICollection<SuppliersViewModel> suppliers { get; set; }

        [Display(Name = "Service Orders Billing")]
        public virtual ICollection<ServiceOrderBillingViewModel> serviceOrdersBilling { get; set; }

        [Display(Name = "Service Orders Shipping")]
        public virtual ICollection<ServiceOrderShippingViewModel> serviceOrdersShip { get; set; }
         * */

        public static implicit operator CityViewModel(City obj)
        {
            if (obj != null)
            {
                return new CityViewModel
                {
                    city_id = obj.city_id,
                    country = obj.country,
                    country_id = obj.country_id,
                    ddd = obj.ddd,
                    name = obj.name,
                    state = obj.state,
                    state_id = obj.state_id
                

                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator City(CityViewModel obj)
        {
            if (obj != null)
            {
                return new City
                {
                    city_id = obj.city_id,
                    country = obj.country,
                    country_id = obj.country_id,
                    ddd = obj.ddd,
                    name = obj.name,
                    state = obj.state,
                    state_id = obj.state_id
                

                };
            }
            else
            {
                return null;
            }
        }

    }
}
