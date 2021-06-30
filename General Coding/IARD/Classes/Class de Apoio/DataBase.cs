using System;
using System.Configuration;
using System.Data;
using System.Collections; 
using System.Data.OracleClient;

namespace Classes
{
    /// <summary>
    /// Summary description for DataBase.
    /// </summary>
    public abstract class DataBase
    {
        #region ConnectionString

        public static string GetConnStringFromConfigFile(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringsSection conStrings = config.ConnectionStrings as ConnectionStringsSection;

            if (config != null)
            {
                OracleConnectionStringBuilder s = new OracleConnectionStringBuilder(conStrings.ConnectionStrings[key].ConnectionString);
                return s.ConnectionString;
            }

            return string.Empty;
        }

        /// <summary>
        /// ConnectionString recuperada do arquivo de configura��o de aplica��es Web.config
        /// </summary>
        public static readonly string CONN_STRING = System.Configuration.ConfigurationSettings.AppSettings["ConnString"].ToString();
        //public static string CONN_STRING = GetConnStringFromConfigFile("ConnString");

        /// <summary>
        /// ConnectionString quebrada em duas partes
        /// a primeira isola o data source para loga com outro ID do banco por ex.
        /// a outra � o ID padrao do banco.
        /// </summary>
        public static readonly string CONN_STRING_DT_SOURCE = CONN_STRING.Split(';')[0] + ";";
        public static readonly string CONN_STRING_ID_PW = CONN_STRING.Split(';')[1] + ";" + CONN_STRING.Split(';')[2] + ";";
        //public static string CONN_STRING = GetConnStringFromConfigFile("ConnString");
        #endregion

        /// <summary>
        /// Hashtable para armazenamento e manipula��o dos par�metros das stored procedures da camada
        /// de neg�cio
        /// </summary>
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// M�todo respons�vel pela execu��o de opera��es sem retorno de resultados (INSERT, UPDATE e DELETE) em bancos de dados 
        /// </summary>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia � uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros da instru��o Oracle</param>
        /// <returns>N�mero de registros afetados pela instru��o Oracle</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {

            OracleCommand cmd = new OracleCommand();

            using (OracleConnection conn = new OracleConnection(CONN_STRING))
            {
                try
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
                catch (Exception ex)
                {
                    conn.Close();
                    conn.Dispose();
                    throw (ex);
                }
            }

        }

        /// <summary>
        /// Neste m�todo as opera��es s�o executadas na forma de Transaction Oracle, ou seja, execu��o
        /// de mais de uma opera��o em bancos de dados
        /// </summary>
        /// <param name="trans">Inst�ncia do objeto OracleTransaction do .NET Framework</param>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia � uma
        /// stored procedure</param>
        /// <param name="cmd">OracleCommand contendo os par�metros</param>
        /// <returns>OracleCommand utilizado na execu��o da instru��o T-Oracle</returns>
        public static OracleCommand ExecuteNonQueryCmd(OracleTransaction trans, CommandType cmdType, string cmdText, OracleCommand cmd)
        {
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText);
            cmd.ExecuteNonQuery();
            return cmd;
        }

        /// <summary>
        /// Neste OverLoad do m�todo as opera��es s�o executadas na forma de Transaction Oracle, ou seja, execu��o
        /// de mais de uma opera��o em bancos de dados
        /// </summary>
        /// <param name="trans">Inst�ncia do objeto OracleTransaction do .NET Framework</param>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia � uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros da instru��o Oracle</param>
        /// <returns>N�mero de registros afetados pela instru��o Oracle</returns>
        public static int ExecuteNonQuery(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Neste OverLoad do m�todo pode-se utilizar a mesma conex�o utilizada em outras consultas anteriores ou
        /// posteriores. AVISO: Altamente recomend�vel utilizar este overload com objetos
        /// OracleConnection inseridos no contexto de cl�usula "using" (respons�vel pela execu��o do Dispose das inst�ncias declaradas).
        /// Objetos OracleConnection utilizados neste contexto n�o permanecem abertos ao final da execu��o das instru��es.
        /// </summary>
        /// <param name="conn">Inst�ncia do objeto OracleConnection do .NET Framework</param>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia � uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros da instru��o Oracle</param>
        /// <returns>N�mero de registros afetados pela instru��o Oracle</returns>
        public static int ExecuteNonQuery(OracleConnection conn, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, OracleCommand cmd)
        {
            using (OracleConnection conn = new OracleConnection(CONN_STRING))
            {
                try
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }

        }

        /// <summary>
        /// Neste OverLoad do m�todo as opera��es s�o executadas na forma de Transaction Oracle, ou seja, execu��o
        /// de mais de uma opera��o em bancos de dados
        /// </summary>
        /// <param name="trans">Inst�ncia do objeto OracleTransaction do .NET Framework</param>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia � uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros da instru��o Oracle</param>
        /// <returns>OracleCommand utilizado na execu��o da instru��o T-Oracle</returns>
        public static OracleCommand ExecuteNonQueryCmd(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();

            return cmd;
        }

        /// <summary>
        /// M�todo respons�vel pela execu��o de opera��es com retorno de resultados (instru��es SELECT) atrav�s do 
        /// objeto OracleDataReader do ADO.NET 
        /// </summary>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia a uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros da instru��o Oracle</param>
        /// <returns>OracleDataReader armazenando os registros selecionados</returns>
        public static OracleDataReader ExecuteReader(CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(CONN_STRING);
            //-----------Tirando carcts especiais--------
            PreparaReader(cmdParms);
            //------------------------------------------
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                string _PrepararMensagem = "Comando: " + cmdText.ToString() + " Erro: " + ex.Message.ToString();
                _PrepararMensagem = _PrepararMensagem.Replace("'", "`");
                throw (ex);
            }
        }

        /// <summary>
        /// M�todo respons�vel pela execu��o de opera��es com retorno de resultados (instru��es SELECT) atrav�s do 
        /// objeto OracleDataReader do ADO.NET 
        /// </summary>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia a uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros da instru��o Oracle</param>
        /// <returns>OracleDataReader armazenando os registros selecionados</returns>
        public static OracleDataReader ExecuteReader(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            //-----------Tirando carcts especiais--------
            PreparaReader(cmdParms);
            //------------------------------------------
            try
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
                OracleDataReader rdr = cmd.ExecuteReader();
                //cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {

                throw (ex);
            }

        }

        /// <summary>
        /// M�todo respons�vel pela execu��o de opera��es com retorno de resultados (instru��es SELECT) atrav�s do 
        /// objeto OracleDataReader do ADO.NET 
        /// </summary>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia a uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros da instru��o Oracle</param>
        /// <returns>OracleDataReader armazenando os registros selecionados</returns>
        public static OracleDataReader ExecuteReader(CommandType cmdType, string cmdText, OracleCommand cmd)
        {
            OracleConnection conn = new OracleConnection(CONN_STRING);
            OracleDataReader rdr;
            if (cmd == null)
                cmd = new OracleCommand();

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw (ex);
            }

            return rdr;
        }

        /// <summary>
        /// M�todo respons�vel pela execu��o de opera��es com retorno de resultados (instru��es SELECT) atrav�s do 
        /// objeto OracleDataReader do ADO.NET. Este overload do m�todo pode ser executado diversas vezes utilizando-se
        /// uma mesma inst�ncia do objeto OracleConnection. AVISO: Altamente recomend�vel utilizar este overload com objetos
        /// OracleConnection inseridos no contexto de cl�usula "using" (respons�vel pela execu��o do Dispose das inst�ncias declaradas).
        /// Objetos OracleConnection utilizados neste contexto n�o permanecem abertos ao final da execu��o das instru��es.
        /// </summary>
        /// <param name="connection">Conex�o</param>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia a uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros da instru��o Oracle</param>
        /// <returns>OracleDataReader armazenando os registros selecionados</returns>
        public static OracleDataReader ExecuteReader(OracleConnection conn, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            //-----------Tirando carcts especiais--------
            PreparaReader(cmdParms);
            //------------------------------------------
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OracleDataReader rdr = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw (ex);
            }
        }

        public static OracleDataReader ExecuteReaderCmd(OracleCommand cmd, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleConnection conn = new OracleConnection(CONN_STRING);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw (ex);
            }
        }

        /// <summary>
        /// M�todo respons�vel pela execu��o de opera��es com retorno de resultados (instru��es SELECT)
        /// atrav�s do objeto DataSet do ADO.NET 
        /// </summary>
        /// <param name="cmdText">Comando Oracle a ser executado (string Oracle convencional)</param>
        /// <returns>DataSet armazenando os registros selecionados</returns>
        public static DataSet ExecuteReaderDs(string cmdText)
        {
            OracleConnection conn = new OracleConnection(CONN_STRING);
            OracleDataAdapter da = new OracleDataAdapter(cmdText, conn);

            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw (ex);
            }

            conn.Close();
            conn.Dispose();

            return ds;
        }

        /// <summary>
        /// Atrav�s deste OverLoad do m�todo pode-se preencher um DataSet (objeto do ADO.NET) atrav�s do processamento
        /// de instru��es Oracle convencionais ou procedimentos armazenados (stored procedures) existentes no SGDBR.
        /// </summary>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia a uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros da instru��o Oracle</param>
        /// <returns>DataSet armazenando os registros selecionados</returns>
        public static DataSet ExecuteReaderDs(CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(CONN_STRING);
            DataSet ds = new DataSet();
            //-----------Tirando carcts especiais--------
            PreparaReader(cmdParms);
            //------------------------------------------
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();

            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw (ex);
            }
            conn.Close();
            conn.Dispose();
            return ds;
        }

        //overload c/ transaction
        public static DataSet ExecuteReaderDs(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            //			OracleConnection conn = new OracleConnection(CONN_STRING);
            DataSet ds = new DataSet();
            //-----------Tirando carcts especiais--------
            PreparaReader(cmdParms);
            //------------------------------------------
            try
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();

            }
            catch (Exception ex)
            {
                //				conn.Close();
                //				conn.Dispose();
                throw (ex);
            }
            //			conn.Close();
            //			conn.Dispose();
            return ds;
        }

        /// <summary>
        /// Atrav�s deste m�todo pode-se preencher um DataSet (objeto do ADO.NET) atrav�s do processamento
        /// de instru��es Oracle convencionais ou procedimentos armazenados (stored procedures) existentes no SGDBR. Atrav�s
        /// do fornecimento do DataSet como par�metro pode-se preench�-lo com mais de uma DataTable.
        /// </summary>		
        /// <param name="dsCompleto">DataSet a ser preenchido (podendo ser preenchido com mais de uma tabela depois). Passado como refer�ncia.</param>
        /// <param name="nomeTabela">Nome da tabela a ser preenchida no DataSet</param>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia a uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros da instru��o Oracle</param>
        public static void ExecuteReaderDsRef(ref DataSet dsCompleto, string nomeTabela,
            CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(CONN_STRING);

            //-----------Tirando carcts especiais--------
            PreparaReader(cmdParms);
            //------------------------------------------
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dsCompleto, nomeTabela);
                cmd.Parameters.Clear();

            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw (ex);
            }
        }

        /// <summary>
        /// M�todo respons�vel pela recupera��o de valores individuais (�nicos) do banco de dados. Como retorno tem-se um objeto
        /// do tipo object que deve ser convertido em um momento posterior.
        /// </summary>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia � uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros da instru��o Oracle</param>
        /// <returns>Objeto (valor) �nico resultante da execu��o da consulta no banco de dados</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(CONN_STRING);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object obj = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return obj;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// M�todo respons�vel pela recupera��o de valores individuais (�nicos) do banco de dados. Como retorno tem-se um objeto
        /// do tipo object que deve ser convertido em um momento posterior. Este overload do m�todo deve ser utilizado para chamadas
        /// em contextos transacionais.
        /// </summary>
        /// <param name="trans">Inst�ncia do objeto OracleTransaction do .NET Framework</param>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado pelo m�todo</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia � uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros da instru��o Oracle</param>
        /// <returns>Objeto (valor) �nico resultante da execu��o da consulta no banco de dados</returns>
        public static object ExecuteScalar(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            object obj = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return obj;
        }

        /// <summary>
        /// M�todo respons�vel pela atribui��o de uma refer�ncia aos par�metros Oracle armazenados na hashtable parmCache
        /// </summary>
        /// <param name="cacheKey">Nome, refer�ncia ou chave dos valores armazenados na hashtable</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo par�metros de instru��es Oracle</param>
        public static void CacheParameters(string cacheKey, params OracleParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }

        /// <summary>
        /// M�todo respons�vel pela recupera��o do array de par�metros Oracle armazenado na hashtable parmCache
        /// </summary>
        /// <param name="cacheKey">Nome, refer�ncia ou chave dos valores armazenados na hashtable</param>
        /// <returns>Array de par�metros Oracle contendo par�metros de instru��es Oracle</returns>
        public static OracleParameter[] GetCachedParameters(string cacheKey)
        {
            OracleParameter[] cachedParms = (OracleParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            OracleParameter[] clonedParms = new OracleParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (OracleParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// M�todo respons�vel pela manipula��o de inst�ncias do objeto OracleCommand do ADO.NET
        /// para a execu��o de opera��es em bancos de dados
        /// </summary>
        /// <param name="cmd">Inst�ncia do objeto OracleCommand a ser ajustado</param>
        /// <param name="conn">Inst�ncia do objeto OracleConnection da conex�o adotada</param>
        /// <param name="trans">Inst�ncia do objeto OracleTransaction do .NET Framework</param>
        /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
        /// executado</param>
        /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a refer�ncia a uma
        /// stored procedure</param>
        /// <param name="cmdParms">Array de par�metros Oracle contendo eventuais par�metros de instru��es Oracle</param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    if (cmd.Parameters.Contains(parm))
                        cmd.Parameters[parm.ParameterName] = parm;
                    else
                        cmd.Parameters.Add(parm);
                }
            }
        }

        protected static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;
        }

        private static void PreparaReader(OracleParameter[] cmdParms)
        {
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    //----------------Retirando caracteres especiais-------
                    if ((parm.DbType.Equals(DbType.AnsiString) || parm.DbType.Equals(DbType.AnsiStringFixedLength)) && parm.Value != null)
                    {
                        if (parm.ParameterName != "sWhere" && parm.ParameterName != "sWhereEV" && parm.ParameterName != "sWhereAT") //n�o limpa as aspas caso o parametro seja a condi��o do filtro
                            parm.Value = RemoveQuote(parm.Value.ToString());

                        if (parm.Value.ToString() == "")
                            parm.Value = null;
                    }
                    //--------------------------------------------------
                }
            }
        }
        private static string RemoveQuote(string text)
        {
            int indice1 = text.IndexOf("'");
            int indice2 = text.IndexOf("`");
            int indice3 = text.IndexOf("\"");
            //string[] subStr;
            if (text.StartsWith("'") || text.StartsWith("\"") || text.StartsWith("`"))
                if (text.Length > 1)
                    RemoveQuote(text.Substring(1));
                else
                    return "";

            char[] separator1 = { '\'' };
            char[] separator2 = { '"' };
            char[] separator3 = { '`' };
            //Retirando '
            if (indice1 >= 0)
            {
                return text.Substring(0, indice1);
                //				if(text.Length-1>indice1 ) //se o ap�strofo N�O est� na �ltima posic
                //				{
                //					subStr=text.Split(separator1);
                //					text = subStr[0] + subStr[1];
                //				}
                //				else
                //				{
                //					text = Regex.Replace(text,"\'","");
                //				}
                //				RemoveQuote(text);
            }
            //Retirando `
            if (indice2 >= 0)
            {
                return text.Substring(0, indice2);
                //				if(text.Length-1>indice2 ) //se o ap�strofo N�O est� na �ltima posic
                //				{
                //					subStr=text.Split(separator2);
                //					text = subStr[0] + subStr[1];
                //				}
                //				else
                //				{
                //					text = Regex.Replace(text,"`","");
                //				}
                //				RemoveQuote(text);
            }
            //retirando "
            if (indice3 >= 0)
            {
                return text.Substring(0, indice3);
                //				if(text.Length-1>indice3 ) //se o ap�strofo N�O est� na �ltima posic
                //				{
                //					subStr=text.Split(separator3);
                //					text = subStr[0] + subStr[1];
                //				}
                //				else
                //				{
                //					text = Regex.Replace(text,"\"","");
                //				}
                //				RemoveQuote(text);

            }
            return text;
        }
    }
}