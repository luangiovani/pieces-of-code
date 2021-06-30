using System.Text.RegularExpressions;

namespace Domain.DTO.APP
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
    /// Objeto de Transferência de dados para acesso e autenticação no sistema, não representa uma Entidade de banco de dados
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Login de acesso a aplicação, CS do colaborador
        /// </summary>
        public string login { get; set; }

        /// <summary>
        /// Senha de acesso a aplicação
        /// </summary>
        public string senha { get; set; }

        /// <summary>
        /// Id da Aplicação que está acessando
        /// </summary>
        public string aplicacao_id { get; set; }       

        /// <summary>
        /// Retorna o CS do login do usuário, somente os números
        /// </summary>
        public string cs {
            get
            {
                Regex regexObj = new Regex(@"[^\d]");
                return regexObj.Replace(login, "");
            }
        }
    }
}
