using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe que Abstrai o Acesso a aplicações
    /// </summary>
    /// <autor>
    /// Luan Giovani Cassini Fernandes
    /// </autor>
    /// <data>
    /// 07/05/2018
    /// </data>
    /// <atividade>
    /// https://esfera.teamworkpm.net/#tasks/17108514
    /// </atividade>
    public class Autenticacao
    {
        #region Atributos
        private int codModulo;
        private int codUsuario;
        private string login;

        #endregion

        #region Propriedades

        public int CodModulo
        {
            get { return codModulo; }
        }

        public int CodigoUsuario
        {
            get { return codUsuario; }
        }

        public string LoginUsuario
        {
            get { return login; }
        }

        #endregion

        #region StoredProcedures

        private const string SPSELECT = "GerencUsuarioSel";

        #endregion

        #region Construtores

        public Autenticacao()
        { }

        #endregion

        #region Métodos
        
        #region ValidaAcesso
        public static Context.TipoAcesso ValidaAcesso(int codigoUsuario, string nomePagina)
        {
            //Implementar StoredeProcedure UsuarioValidaAcessoSel.sql
            //Implementar Classe


            //Apenas Teste
            return Context.TipoAcesso.Manutencao;
        }
        #endregion
        
        #region ValidaUsuario
        /// <summary>
        /// Método responsável em verificar se o usuário, à conectar-se no sistema, existe e se sua senha é válida.
        /// </summary>
        /// <param name="login">Login do Usuário</param>
        /// <param name="senha">Senha criptografada</param>
        /// <returns>Retorna o objeto do Usuário</returns>
        public static Autenticacao ValidaUsuario(string login, string senha)
        {
            ///Converte o login do usuario em caixa Alta maiusculo---
            login = login.ToUpper();

            //Usa o dataSorce da CONN_STRING remontando com usuario e senha do parametro para ver 
            //se o usuario tem acesso ao banco--
            string connS = Context.DataBase.CONN_STRING_DT_SOURCE + " User Id=" + login + "; Password=" + senha + ";";
            //string connS = Context.DataBase.CONN_STRING;
            //Instancia de 
            Autenticacao gerUsuario = new Autenticacao();

            using (OracleConnection conn = new OracleConnection(connS))
            {
                try
                {
                    conn.Open();
                    conn.Close();
                    conn.Dispose();

                    int codUsuario = Usuario.EncontraUsuario(login);

                    if (codUsuario <= 0)
                    {
                        return null;
                    }
                    else
                    {
                        var usuario = Usuario.GetDataRow(codUsuario);
                        if (usuario == null)
                        {
                            return null;    
                        }
                        gerUsuario.codUsuario = codUsuario;
                        return gerUsuario;
                    }
                }
                catch (Exception)
                {
                    gerUsuario = null;
                    conn.Close();
                    conn.Dispose();
                    return gerUsuario;
                    //throw (ex);
                }
            }            
        }
        #endregion

        #region LoadDataDR
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public static OracleDataReader LoadDataDR(string login, string senha)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter("@login", OracleType.VarChar),
                                                        new OracleParameter("@senha015", OracleType.VarChar), };

            param[0].Value = login;

            if (senha == string.Empty)
                param[1].Value = DBNull.Value;
            else
                param[1].Value = senha;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECT, param);

            return dr;
        }

        #endregion

        #region EnviarEmail
        /// <summary>
        /// Função para enviar email, ao usuário, com suas informações
        /// </summary>
        /// <param name="login">Login do usuário</param>
        public static void EnviarEmail(string login)
        {/*

            Autenticacao gerUsuario = Autenticacao.ValidaUsuario(login, string.Empty);

            if (gerUsuario != null)
            {

                Contato contato = Contato.GetDataRow(gerUsuario.CodigoContato);
                Usuario usuario = Usuario.GetDataRow(gerUsuario.CodigoUsuario);



                MailMessage mail = new MailMessage();

                mail.To.Add(new MailAddress(contato.Email013, contato.Nome013));

                mail.From = new MailAddress("suporte@esfera.com.br", "Esfera Informatica");

                // Assunto da mensagem
                mail.Subject = "Login e senha";

                // Mensagem, texto ou html
                mail.Body = "Login: " + usuario.Login015 + " e Senha: " + usuario.Senha015;

                // Prioridade da mensagem: Low, Normal ou High
                mail.Priority = MailPriority.Normal;

                // Definição do servidor SMTP a ser utilizado
                SmtpClient smtp = new SmtpClient("smtp.esfera.com.br");

                // Enviando o email
                smtp.Send(mail);

            }
*/
        }

        #endregion

        #endregion
    }
}


