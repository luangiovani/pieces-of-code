using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Domain.DTO.Loja
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Implementação dos Objetos para Abstração entre a Camada de Serviços e a Camada de Acesso a Dados
    /// </atividades>
    /// <summary>
    /// Objeto de Transferência da Entidade Produto
    /// </summary
    public class ProdutoDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

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

        public string b64_imagem { get; set; }

        /// <summary>
        /// Indica se está disponível ou não
        /// </summary>
        public bool disponibilidade { get; set; }

        public string data_disponibilidade { get; set; }

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

        public string cs_colaborador_criacao { get; set; }
    }

    public class ProdutosMaisTrocadosDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid id { get; set; }

        public int sequencial { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

        public decimal pontos { get; set; }

        public decimal valor { get; set; }
        
        public int trocas { get; set; }

        public string situacao { get; set; }
    }

    public class ProdutosColaboradorTrocaDTO
    {
        private string img64 { get; set; }

        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid id { get; set; }

        public int sequencial { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

        public decimal pontos { get; set; }

        public decimal valor { get; set; }

        public int trocas { get; set; }

        public string situacao { get; set; }

        /// <summary>
        /// ImagemProduto
        /// </summary>
        public string b64Imagem {
            get
            {
                return img64;
            }

            set
            {
                int leftI = value.IndexOf("base64,") + 7;
                string imgs = value.Substring(leftI, (value.Length-leftI));
                string left = value.Substring(0, leftI);
                byte[] bytes = Convert.FromBase64String(imgs);

                Image image;

                //Convert byte[] into image
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                int divisor = image.Width > 150 ? (image.Width/150) : 1;
                int width = 0;
                int heigth = 0;

                if (image.Width / divisor < 150 && divisor > 1)
                {
                    width = image.Width;
                    heigth = image.Height;
                }
                else
                {
                    width = image.Width / divisor;
                    heigth = image.Height / divisor;
                }


                // Resize the image
                Bitmap b = new Bitmap(width, heigth);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(image, 0, 0, 150, 150);
                g.Dispose();
                image = (Image)b;

                using (MemoryStream ms = new MemoryStream())
                {
                    // Convert Image to byte[]
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    img64 = left + base64String;
                }
            }
        }
    }

    public class ProdutoOpcoesDTO
    {
        public ProdutoOpcoesDTO()
        {
            OpcaoValores = new List<ProdutoOpcaoValoresDTO>();
        }

        public Guid? id { get; set; }

        public int sequencial { get; set; }

        public string produto_id { get; set; }

        public string opcao_id { get; set; }

        public bool ativo { get; set; }

        public DateTime data_hora_criacao { get; set; }

        public string cs_colaborador_criacao { get; set; }

        public DateTime? data_hora_alteracao { get; set; }

        public string cs_colaborador_alteracao { get; set; }

        public IEnumerable<ProdutoOpcaoValoresDTO> OpcaoValores { get; set; }
    }

    public class ProdutoOpcaoValoresDTO
    {
        public Guid? id { get; set; }

        public int sequencial { get; set; }

        public string produto_opcao_id { get; set; }

        public string valor { get; set; }

        public bool ativo { get; set; }

        public DateTime data_hora_criacao { get; set; }

        public string cs_colaborador_criacao { get; set; }

        public DateTime? data_hora_alteracao { get; set; }

        public string cs_colaborador_alteracao { get; set; }
    }

    /// <summary>
    /// Objeto de Transferência de informações de Opções Características para produtos
    /// Cor, Tamanho, Gênero e etc...
    /// </summary>
    public class OpcaoDTO
    {
        public OpcaoDTO()
        {
            Valores = new List<ValoresOpcoesDTO>();
        }

        public Guid? id { get; set; }

        public int sequencial { get; set; }

        public string nome { get; set; }

        public string observacao { get; set; }

        public string tipo_opcao_id { get; set; }

        public bool ativo { get; set; }

        public DateTime data_hora_criacao { get; set; }

        public string cs_colaborador_criacao { get; set; }

        public DateTime? data_hora_alteracao { get; set; }

        public string cs_colaborador_alteracao { get; set; }

        public List<ValoresOpcoesDTO> Valores { get; set; }
    }

    /// <summary>
    /// Objeto de Transferência de informações dos Valores de Opções Características para produtos
    /// Cor: Azul, Branco, Preto...
    /// Tamanho: P, PP, G, 36 ao 48
    /// Gênero: N/A, Unissex
    /// </summary>
    public class ValoresOpcoesDTO
    {
        public Guid? id { get; set; }

        public int sequencial { get; set; }

        public string opcao_id { get; set; }

        public string valor { get; set; }

        public bool ativo { get; set; }

        public DateTime data_hora_criacao { get; set; }

        public string cs_colaborador_criacao { get; set; }

        public DateTime? data_hora_alteracao { get; set; }

        public string cs_colaborador_alteracao { get; set; }
    }
}
