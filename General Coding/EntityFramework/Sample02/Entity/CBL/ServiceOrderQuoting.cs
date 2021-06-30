using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ServiceOrderQuoting
    {
        /// <summary>
        /// Service Order Quoting ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public decimal serviceOrder_id { get; set; }

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

        /// <summary>
        /// Part Needed Id
        /// Parte necessária Id
        /// </summary>
        public int? partNeeded_id { get; set; }

        /// <summary>
        /// quoteEstimate
        /// Citação Estimate
        /// </summary>    
        public decimal quoteEstimate { get; set; }

        /// <summary>
        /// quotedAmount
        /// Citar, denominar montante
        /// </summary>   
        public decimal quotedAmount { get; set; }

        /// <summary>
        /// discountGivem
        /// Desconto dado
        /// </summary>    
        public decimal discountGivem { get; set; }

        /// <summary>
        /// nextDepotAmount
        /// Próxima Quantia Depot
        /// </summary>    
        public decimal nextDepotAmount { get; set; }

        /// <summary>
        /// Currency
        /// Moeda
        /// </summary>  
        public string currency { get; set; }

        /// <summary>
        /// timeNeeded
        /// Tempo Necessário
        /// </summary>  
        public string timeNeeded { get; set; }

        /// <summary>
        /// dueDate
        /// Data de Vencimento
        /// </summary>  
        public DateTime? dueDate { get; set; }

        /// <summary>
        /// destination
        /// Destino
        /// </summary> 
        public string destination { get; set; }

        /// <summary>
        /// quoteLines
        /// Linhas de cotação
        /// </summary>    
        public string quoteLines { get; set; }

        public int? id_old { get; set; }

        public bool? quotedFinished { get; set; }

        public string statusQuoting { get; set; }

        public int? quoteDays { get; set; }

        /// <summary>
        /// Date of Quoting
        /// Data do orçamento
        /// </summary>
        public DateTime? quoteDate { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>   
        public virtual ServiceOrder serviceOrder { get; set; }

        /// <summary>
        /// PartNeeded
        /// Parte Necessária
        /// </summary>   
        public virtual ICollection<PartNeeded> partsNeeded { get; set; }
    }
}
