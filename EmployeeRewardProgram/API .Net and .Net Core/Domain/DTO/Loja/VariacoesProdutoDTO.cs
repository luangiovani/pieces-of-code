using System;
using System.Collections.Generic;

namespace Domain.DTO.Loja
{
    public class VariacoesProdutoDTO
    {
        public VariacoesProdutoDTO()
        {
            Valores = new List<VariacoesProdutoValoresDTO>();
        }

        public Guid id { get; set; }

        public string label { get; set; }

        public string descricao { get; set; }

        public int tipo { get; set; }

        public string tipo_descricao { get; set; }

        public IEnumerable<VariacoesProdutoValoresDTO> Valores { get; set; }
    }

    public class VariacoesProdutoValoresDTO
    {
        public string valor { get; set; }
    }
}
