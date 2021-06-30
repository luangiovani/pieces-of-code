using System;

namespace Domain.DTO
{
    public class BaseDTO
    {
        public Guid id { get; set; }

        public string nome { get; set; }
    }

    public class AtivarInativarDTO
    {
        public Guid? id { get; set; }

        public bool ativar { get; set; }
    }
}
