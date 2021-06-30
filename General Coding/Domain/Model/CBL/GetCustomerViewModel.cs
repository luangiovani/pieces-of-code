using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class GetCustomerViewModel
    {
        public int id { get; set; }
        public int customerId { get; set; }
        public int contact_id { get; set; }
        public string contact_name { get; set; }
        public string contact_email { get; set; }
        public string cpf { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string celular { get; set; }
        public string pais { get; set; }
        public string estado { get; set; }
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string endereco { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
    }
}
