using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class StartRequestViewModel
    {
        public string contact_name { get; set; }
        public string contact_rgie { get; set; }
        public string contact_cpfcnpj { get; set; }
        public string company_name { get; set; }
        public string contact_email { get; set; }
        public string password { get; set; }
        public string telephone { get; set; }
        public string hear_of_us { get; set; }
        public string cep { get; set; }
        public string address { get; set; }
        public string addressNumber { get; set; }
        public string addressExt { get; set; }
        public string city { get; set; }
        public string uf { get; set; }
        public string type_of_service { get; set; }
        public string type_of_media { get; set; }
        public string model { get; set; }
        public string serial_no { get; set; }
        public string failure { get; set; }
        public string most_important_files { get; set; }
        public string observation { get; set; }
    }
}
