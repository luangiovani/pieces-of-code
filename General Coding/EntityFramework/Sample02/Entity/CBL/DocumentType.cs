using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class DocumentType
    {
        /// <summary>
        /// Document Type ID
        /// Identificador do tipo de documento
        /// </summary>
        public int documentType_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do Tipo de Documento
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// URL File
        /// Url do Arquivo do Documento
        /// </summary>
        public string url_file { get; set; }

        /// <summary>
        /// Description
        /// Descrição do tipo de documento
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Active
        /// Indicador de status do tipo de documento
        /// </summary>
        //public bool active { get; set; }

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
        
        public int? id_old { get; set; }

    }
}
