using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;
using System.Net.Mail;
using System.Net;
using System.IO;

//-- Classe Classes Sebrae
//-- Data : 14/05/2007 
//-- Autor :  Daniel

namespace Classes
{
    public class Usuario // T052_Usuario 
    {
        #region Atributos
        private int codUsuario;
        private int codEscr;
        private string nomeLogin;
        private int indAtivo;
        private string logusuario;
        private string tipoAcesso;
        private string loginAd;
        #endregion

        #region Propriedades
        public int CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public int CodEscr
        {
            get { return codEscr; }
            set { codEscr = value; }
        }
        public string NomeLogin
        {
            get { return nomeLogin; }
            set { nomeLogin = value; }
        }
        public int IndAtivo
        {
            get { return indAtivo; }
            set { indAtivo = value; }
        }        
        public string Logusuario
        {
            get { return logusuario; }
            set { logusuario = value; }
        }
        public string TipoAcesso
        {
            get { return tipoAcesso; }
            set { tipoAcesso = value; }
        }
        public string LoginAD
        {
            get { return loginAd; }
            set { loginAd = value; }
        }
        #endregion

        #region Construtores
        public Usuario()
            : this(-1)
        { }
        public Usuario(int codUsuario)
        {
            this.codUsuario = codUsuario;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Usuario.UsuarioInc";
        private const string SPUPDATE = "Usuario.UsuarioAlt";
        private const string SPDELETE = "Usuario.UsuarioDel";
        private const string SPSELECTID = "Usuario.UsuarioSelId";
        private const string SPSELECTLOGIN = "Usuario.UsuarioSelLogin";
        private const string SPSELECTPAG = "Usuario.UsuarioSelPag";
        private const string SPINSERTLOGINAD = "Usuario.UsuarioVinculoAd";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codUsuario";
        private const string PARMCURSOR = "curUsuario";
        #endregion

        #region Metodos

        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            parms = DataBase.GetCachedParameters(SPINSERT);
            //parms = OutputCacheParameters(SPINSERT);
            if (parms == null)
            {
                parms = new OracleParameter[]{ 
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter( "codEscr", OracleType.Int32),
                    /*2*/ new OracleParameter( "nomeLogin", OracleType.VarChar),
                    /*3*/ new OracleParameter( "indAtivo", OracleType.Int32),
                    /*4*/ new OracleParameter( "logUsuario", OracleType.VarChar)
                };

                // Criando cache dos parameters 
                DataBase.CacheParameters(SPINSERT, parms);
            }
            return (parms);
        }

        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[0].Value = this.codUsuario;
            parms[1].Value = this.codEscr;
            parms[2].Value = this.NomeLogin;
            parms[3].Value = this.indAtivo;
            parms[4].Value = this.logusuario;

            if (this.codUsuario < 0)
            {
                parms[0].Direction = ParameterDirection.Output;
            }
            else
            {
                parms[0].Direction = ParameterDirection.Input;
            }
        }
        #endregion

        #region VincularUsuarioAD
        
        public void VincularUsuarioAD()
        {
            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleParameter[] parmsVinculo = new OracleParameter[]{ 
                            /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                            /*1*/ new OracleParameter( "tipoAcesso", OracleType.VarChar),
                            /*2*/ new OracleParameter( "loginAd", OracleType.VarChar)
                        };

                        parmsVinculo[0].Value = this.codUsuario;
                        parmsVinculo[1].Value = this.tipoAcesso;
                        parmsVinculo[2].Value = this.loginAd;
                        parmsVinculo[0].Direction = ParameterDirection.Input;

                        OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERTLOGINAD, parmsVinculo);
                        cmd.Parameters.Clear();

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        }
        
        #endregion

        #region Insert

        /// <summary>
        /// Insert com tratamento de transação
        /// </summary>
        public void Insert()
        {
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);

            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        codUsuario = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
                        cmd.Parameters.Clear();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        }

        /// <summary>
        /// Insert sem tratamento de transação
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        public void Insert(OracleTransaction trans)
        {
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);
            try
            {
                OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                //Obtendo a chave de identificação do registro inserido.
                codUsuario = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Update

        /// <summary>
        /// Update com tratamento de transação
        /// </summary>
        public void Update()
        {
            // --------------------------------------------------------  
            // Obtendo e setando os parâmetros 
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);
            // -------------------------------------------------------- 
            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        } // END UPDATE

        /// <summary>
        /// Update sem tratamento de transação
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        public void Update(OracleTransaction trans)
        {
            // -------------------------------------------------------- 
            // Obtendo e setando os parâmetros 
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);
            // -------------------------------------------------------- 
            try
            {
                DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } // END UPDATE 
        #endregion

        #region Delete

        /// <summary>
        /// Delete com tratamento de transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
            parms[0].Value = codigo;
            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
                        trans.Commit();
                    }
                    catch (OracleException ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        } // end DELETE

        /// <summary>
        /// Delete sem tratamento de transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
                parms[0].Value = codigo;
                DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region LoadDataDr


        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
                                                                  new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }


        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>Usuario</returns>
        public static Usuario GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Usuario usuario = new Usuario();
            try
            {
                if (dr.Read())
                {
                    usuario.codUsuario = Convert.ToInt32(dr["A052_cd_Usuario"]);
                    usuario.codEscr = Convert.ToInt32(dr["A004_cd_escr"]);
                    usuario.NomeLogin = Convert.ToString(dr["A052_nm_login"]);
                    if ( dr["A052_ind_ativo"] != DBNull.Value && dr["A052_ind_ativo"].ToString() != "")
                    usuario.indAtivo = Convert.ToInt32(dr["A052_ind_ativo"]);
                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    usuario.logusuario = Convert.ToString(dr["Usu_Inc_Alt"]);
                    usuario.tipoAcesso = dr["A052_TIPO_ACESSO"] != DBNull.Value ? dr["A052_TIPO_ACESSO"].ToString() : "";
                    usuario.loginAd = dr["A052_LOGIN_AD"] != DBNull.Value ? dr["A052_LOGIN_AD"].ToString() : "";
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                usuario = new Usuario();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return usuario;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Usuario</returns>
        public static Usuario GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Usuario usuario = new Usuario();
            try
            {
                if (dr.Read())
                {
                    usuario.codUsuario = Convert.ToInt32(dr["A052_cd_Usuario"]);
                    usuario.codEscr = Convert.ToInt32(dr["A004_cd_escr"]);
                    usuario.NomeLogin = Convert.ToString(dr["A052_nm_login"]);
                    if ( dr["A052_ind_ativo"] != DBNull.Value && dr["A052_ind_ativo"].ToString() != "")
                        usuario.indAtivo = Convert.ToInt32(dr["A052_ind_ativo"]);
                    if ( dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        usuario.logusuario = Convert.ToString(dr["Usu_Inc_Alt"]);
                    usuario.tipoAcesso = dr["A052_TIPO_ACESSO"] != DBNull.Value ? dr["A052_TIPO_ACESSO"].ToString() : "";
                    usuario.loginAd = dr["A052_LOGIN_AD"] != DBNull.Value ? dr["A052_LOGIN_AD"].ToString() : "";
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                usuario = new Usuario();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return usuario;
        }
        #endregion

        #region LoadDataPaginacao


        /// <summary>
        /// LoadDataPaginacao
        /// </summary>
        /// <param name="Where">Cláusula where utilizada na consulta</param>
        /// <param name="PaginaCorrente">Número da página que deseja selecionar</param>
        /// <param name="TamanhoPagina">Quantidade de registros em cada página</param>
        /// <param name="ExpressaoOrdenacao">Expressão de ordenação</param>
        /// <returns>Instância do objeto Paginação, contendo um DataReader e o total de registros</returns>
        /// 
        public static Paginacao LoadDataPaginacao(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Paginacao paginacao = new Paginacao();

            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("sWhere", OracleType.VarChar,5000),
			    new OracleParameter("CurrentPage", OracleType.Int32),
			    new OracleParameter("PageSize", OracleType.Int32),
			    new OracleParameter("SortExpression", OracleType.VarChar,50),
                new OracleParameter(PARMCURSOR, OracleType.Cursor),
                new OracleParameter("nRegistro", OracleType.Int32)
              };

            parms[0].Value = Where;
            parms[1].Value = PaginaCorrente;
            parms[2].Value = TamanhoPagina;
            parms[3].Value = ExpressaoOrdenacao;
            parms[4].Direction = ParameterDirection.Output;
            parms[5].Direction = ParameterDirection.Output;
            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAG, parms);


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion

        #endregion

        #region Metodos Específicos

        #region EnviarEmail
        /// <summary>
        /// Função para enviar email, ao usuário, com suas informações
        /// mas somente para emails que nao exigem autenticação
        /// </summary>
        /// <param name="login">Login do usuário</param>
        public static void EnviarEmail(string EmailDestino, string EmailOrigem, string SMTP, string EmailTitulo, string EmailCorpo, bool FormatoHtml)
        { 
            //Destinatario
            MailAddress to = new MailAddress(EmailDestino);
            //Remetente
            MailAddress from = new MailAddress(EmailOrigem);
            MailMessage mensagem = new MailMessage(from, to);

            // Assunto da mensagem
            mensagem.Subject = EmailTitulo;
            // Mensagem, texto ou html
            mensagem.IsBodyHtml = FormatoHtml;
            mensagem.Body = EmailCorpo;

            //Servidor
            SmtpClient cliente = new SmtpClient(SMTP); //SMTP DA SIGMAONE PARA ENVIO DE EMAIL
 
            // envia a mensagem
            cliente.Send(mensagem);
        }

        /// <summary>
        /// Função para enviar email, ao usuário, com suas informações
        /// mas somente para emails que exigem autenticação com o servidor
        /// </summary>
        /// <param name="login">Login do usuário</param>
        public static void EnviarEmail(string EmailDestino, string EmailOrigem, string EmailAssunto, string EmailCorpo, string SMTP, bool FormatoHtml, string Login, string Senha)
        {  
            //Destinatario
            MailAddress to = new MailAddress(EmailDestino);
            //Remetente
            MailAddress from = new MailAddress(EmailOrigem);
            MailMessage mensagem = new MailMessage(from, to);

            // Assunto da mensagem
            mensagem.Subject = EmailAssunto;
            // Mensagem, texto ou html
            mensagem.IsBodyHtml = FormatoHtml;
            mensagem.Body = EmailCorpo;

            //Servidor
            SmtpClient cliente = new SmtpClient(SMTP); //SMTP DA SIGMAONE PARA ENVIO DE EMAIL

            // dados para autenticação
            //Nem todo servidor SMTP requer autenticação: nesse caso, as linhas 286 e 287 devem ser omitidas.
            cliente.Credentials = new NetworkCredential(Login, Senha);             

            // envia a mensagem
            cliente.Send(mensagem);
        }

        /// <summary>
        /// Função para enviar email, sem login e sem smtp 
        /// </summary>
        /// <param name="EmailDestino"></param>
        /// <param name="EmailOrigem"></param>
        /// <param name="EmailAssunto"></param>
        /// <param name="EmailCorpo"></param>
        public static void EnviarEmail(string EmailDestino, string EmailOrigem, string EmailAssunto, string EmailCorpo)//, string Copia, string CopiaOculta)
        {
            string EmailFrom = System.Configuration.ConfigurationSettings.AppSettings["EmailFrom"].ToString();
            string EmailHost = System.Configuration.ConfigurationSettings.AppSettings["EmailHost"].ToString();
            string EmailCredentialsN = System.Configuration.ConfigurationSettings.AppSettings["EmailCredentialsN"].ToString();
            string EmailCredentialsS = System.Configuration.ConfigurationSettings.AppSettings["EmailCredentialsS"].ToString();

            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();
            msg.From = new MailAddress(EmailFrom);//new MailAddress(EmailOrigem);
            msg.ReplyTo = new MailAddress(EmailOrigem);
            string[] destinatarios = EmailDestino.Split(';');
            foreach (string destinatario in destinatarios)
                msg.To.Add(destinatario);

            msg.Subject = EmailAssunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = EmailCorpo;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            //client.Port= 587; // 995 587 598
            client.Host = EmailHost;
            client.Credentials = new NetworkCredential(EmailCredentialsN, EmailCredentialsS);
            client.Timeout = 30001;
            try
            {                client.Send(msg);            }
            catch (Exception ex)
            {                throw ex;            }
        }

        /// <summary>
        /// Função para enviar email, sem login e sem smtp 
        /// mas com copia e copia oculta
        /// </summary>
        /// <param name="EmailDestino"></param>
        /// <param name="EmailOrigem"></param>
        /// <param name="EmailAssunto"></param>
        /// <param name="EmailCorpo"></param>
        public static void EnviarEmail(string EmailDestino, string EmailOrigem, string EmailAssunto, string EmailCorpo, string Copia, string CopiaOculta)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();

            msg.From = new MailAddress(EmailOrigem);
            msg.ReplyTo = new MailAddress(EmailOrigem);
            string[] destinatarios = EmailDestino.Split(';');
            foreach (string destinatario in destinatarios)
                msg.To.Add(destinatario);
            if (CopiaOculta != string.Empty)
           {
                string[] ccos = CopiaOculta.Split(';');
                foreach (string cco in ccos)
                    msg.Bcc.Add(cco);
            }
            if (Copia != string.Empty)
            {
                string[] ccs = Copia.Split(';');
                foreach (string cc in ccs)
                   msg.CC.Add(cc);
            }
            msg.Subject = EmailAssunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = EmailCorpo;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            client.Host = "localhost";
            client.Timeout = 30002;
            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Função para enviar email, sem login e sem smtp 
        /// </summary>
        /// <param name="EmailDestino"></param>
        /// <param name="EmailOrigem"></param>
        /// <param name="EmailAssunto"></param>
        /// <param name="EmailCorpo"></param>
        public static void EnviarEmail(string EmailDestino, string EmailOrigem, string EmailAssunto, string EmailCorpo, Dictionary<string, Stream> listaAnexo)
        {
            if (EmailOrigem.Trim() == "")
                EmailOrigem = System.Configuration.ConfigurationSettings.AppSettings["EmailFrom"].ToString();

            string EmailFrom = EmailOrigem;
            string EmailHost = System.Configuration.ConfigurationSettings.AppSettings["EmailHost"].ToString();
            string EmailCredentialsN = System.Configuration.ConfigurationSettings.AppSettings["EmailCredentialsN"].ToString();
            string EmailCredentialsS = System.Configuration.ConfigurationSettings.AppSettings["EmailCredentialsS"].ToString();

            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();
            msg.From = new MailAddress(EmailFrom);
            msg.ReplyTo = new MailAddress(EmailOrigem);

            string[] destinatarios = EmailDestino.Split(';');
            foreach (string destinatario in destinatarios)
                msg.To.Add(destinatario);
            
            msg.Subject = EmailAssunto;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = EmailCorpo;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            foreach (KeyValuePair<string, Stream> anexo in listaAnexo)
            {
                msg.Attachments.Add(new System.Net.Mail.Attachment(anexo.Value, anexo.Key));
            }

            client.Host = EmailHost;
            client.Credentials = new NetworkCredential(EmailCredentialsN, EmailCredentialsS);
            client.Timeout = 30001;
            try
            { client.Send(msg); }
            catch (Exception ex)
            { throw ex; }
        }

        #endregion

        #region EncontraUsuario
        /// <summary>
        /// Encontra o codigo do usuario atraves do login dele--
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static Int32 EncontraUsuario(string login)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("nomeLogin", OracleType.VarChar), 
                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4)
                };

            param[0].Value = login;
            param[1].Direction = ParameterDirection.Output;

            Int32 dr = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, SPSELECTLOGIN, param);
            //Obtendo a chave de identificação do registro inserido.
            Int32 codUsuario = Convert.ToInt32(param[1].Value);
            return codUsuario;
        }

        /// <summary>
        /// Encontra o codigo do usuario atraves do login dele--
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static Int32 EncontraUsuario(string login, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("nomeLogin", OracleType.VarChar), 
                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4)
                };

            param[0].Value = login;
            param[1].Direction = ParameterDirection.Output;

            Int32 dr = DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPSELECTLOGIN, param);
            //Obtendo a chave de identificação do registro inserido.
            Int32 codUsuario = Convert.ToInt32(param[1].Value);
            return codUsuario;
        }
        #endregion

        #endregion

    }
}
