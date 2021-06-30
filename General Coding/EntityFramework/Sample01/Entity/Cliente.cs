using System;

namespace TAJ.Database.Entity
{
    public class Cliente
    {
        public Guid id_cliente { get; set; }
        public int? id_bairro { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string cep { get; set; }
        public string endereco { get; set; }
        public string numero { get; set; }
        public string telefone { get; set; }
        public string celular { get; set; }
        public bool ativo { get; set; }
        public bool taj_pass { get; set; }
        public bool taj_pass_validado { get; set; }
        public DateTime? taj_pass_validade { get; set; }
        public string motivo_bloqueio { get; set; }
        public DateTime? dt_nascimento { get; set; }
        public DateTime dt_cadastro { get; set; }
        public string auth_token { get; set; }
        public string imagem { get; set; }
        public virtual Bairro bairro { get; set; }
        public bool vip { get; set; }
        public string tags { get; set; }
        public string deviceId { get; set; }
        public string deviceSystem { get; set; }

        public string nomeBairro { get; set; }
        public string nomeCidade { get; set; }
        public string nomeEstado { get; set; }

    }
}