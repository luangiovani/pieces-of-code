using System;

namespace Database.Models.Loja
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Mapeamento de Modelo Lógico
    /// </atividades>
    /// <summary>
    /// Classe para Mapeamento da Tabela Produto, nesta tabela serão armazenadas os produtos das lojas
    /// </summary>
    public class Produto
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// Sequencial do registro na tabela (ordenação)
        /// </summary>
        public int sequencial { get; set; }

        /// <summary>
        /// Loja em que o Produto está disponível
        /// </summary>
        public string loja_id { get; set; }

        /// <summary>
        /// Nome do Produto
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Descrição do produto, uso, indicação....
        /// </summary>
        public string descricao { get; set; }

        /// <summary>
        /// Imagem do produto em Base64
        /// </summary>
        public string b64_imagem { get; set; }

        public string imagem
        {
            get
            {
                if (b64_imagem.Length > 0)
                {
                    int leftI = b64_imagem.IndexOf("base64,") + 7;
                    return b64_imagem.Substring(leftI, (b64_imagem.Length - leftI));
                }
                else
                    return String.Empty;
            }
        }

        public string tipo_imagem {
            get {
                if (b64_imagem.Length > 0)
                {
                    int leftI = b64_imagem.IndexOf("base64,") + 7;
                    string tipo = b64_imagem.Substring(0, leftI);
                    tipo = tipo.Replace("data:", "");
                    tipo = tipo.Replace(";base64,", "");

                    return tipo;
                }
                else
                    return String.Empty;
                
            }
        }

        public string nome_imagem {
            get
            {
                if (this.id != null)
                {
                    return this.id.ToString() + "." + tipo_imagem.Replace("image/", "");
                }
                else
                    return String.Empty;
            }
        }

        public string ext_imagem
        {
            get
            {
                if (tipo_imagem.Length > 0)
                {
                    return tipo_imagem.Replace("image/", ""); ;
                }
                else
                    return String.Empty;

            }
        }


        /// <summary>
        /// Indica se está disponível ou não
        /// </summary>
        public bool disponibilidade { get; set; }

        /// <summary>
        /// Indica a Data que o produto estará disponível
        /// </summary>
        public DateTime? data_disponibilidade { get; set; }

        /// <summary>
        /// Valor deste produto em TKU
        /// </summary>
        public decimal valor_pontos { get; set; }

        /// <summary>
        /// Valor deste produto em moeda
        /// </summary>
        public decimal valor_monetario { get; set; }

        /// <summary>
        /// Observações adicionais para o produto
        /// </summary>
        public string observacao { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }

        /// <summary>
        /// Data e Hora da Criação do registro
        /// </summary>
        public DateTime data_hora_criacao { get; set; }

        /// <summary>
        /// Usuário de Criação do registro
        /// </summary>
        public string cs_colaborador_criacao { get; set; }

        /// <summary>
        /// Data e Hora da Alteração do registro
        /// </summary>
        public DateTime? data_hora_alteracao { get; set; }

        /// <summary>
        /// Usuário de Alteração do registro
        /// </summary>
        public string cs_colaborador_alteracao { get; set; }
    }
}
