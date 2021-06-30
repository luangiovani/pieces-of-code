using System;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using System.Data.SqlClient;

namespace Database.Context
{
  /// <summary>
  /// Classe de Abstração do Banco de Dados
  /// </summary>
  /// <autor>
  /// Luan Giovani Cassini Fernandes
  /// </autor>
  /// <data>
  /// 07/05/2018
  /// </data>
  /// <atividade>
  /// https://esfera.teamworkpm.net/#tasks/17108513
  /// </atividade>
  public abstract class DataBase
  {
    #region ConnectionString
    /// <summary>
    /// Método para Obter a Connection String do arquivo de configuração
    /// </summary>
    /// <param name="key">Nome do Atributo no arquivo de configuração</param>
    /// <returns>
    /// Connection String de Conexão
    /// </returns>
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
    /// ConnectionString recuperada do arquivo de configuração de aplicações Web.config
    /// </summary>
    public static readonly string CONN_STRING = System.Configuration.ConfigurationSettings.AppSettings["ConnString"].ToString();
    public static readonly string CONN_STRING_SIAC = System.Configuration.ConfigurationSettings.AppSettings["ConnStringSQL"].ToString();
    //public static string CONN_STRING = GetConnStringFromConfigFile("ConnString");

    /// <summary>
    /// ConnectionString quebrada em duas partes
    /// a primeira isola o data source para loga com outro ID do banco por ex.
    /// a outra é o ID padrao do banco.
    /// </summary>
    public static readonly string CONN_STRING_DT_SOURCE = CONN_STRING.Split(';')[0] + ";";
    public static readonly string CONN_STRING_ID_PW = CONN_STRING.Split(';')[1] + ";" + CONN_STRING.Split(';')[2] + ";";
    //public static string CONN_STRING = GetConnStringFromConfigFile("ConnString");
    #endregion

    /// <summary>
    /// Hashtable para armazenamento e manipulação dos parâmetros das stored procedures da camada
    /// de negócio
    /// </summary>
    private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

    /// <summary>
    /// Método responsável pela execução de operações sem retorno de resultados (INSERT, UPDATE e DELETE) em bancos de dados 
    /// </summary>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência à uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
    /// <returns>Número de registros afetados pela instrução Oracle</returns>
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
    /// Neste método as operações são executadas na forma de Transaction Oracle, ou seja, execução
    /// de mais de uma operação em bancos de dados
    /// </summary>
    /// <param name="trans">Instância do objeto OracleTransaction do .NET Framework</param>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência à uma
    /// stored procedure</param>
    /// <param name="cmd">OracleCommand contendo os parâmetros</param>
    /// <returns>OracleCommand utilizado na execução da instrução T-Oracle</returns>
    public static OracleCommand ExecuteNonQueryCmd(OracleTransaction trans, CommandType cmdType, string cmdText, OracleCommand cmd)
    {
      PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText);
      cmd.ExecuteNonQuery();
      return cmd;
    }

    /// <summary>
    /// Neste OverLoad do método as operações são executadas na forma de Transaction Oracle, ou seja, execução
    /// de mais de uma operação em bancos de dados
    /// </summary>
    /// <param name="trans">Instância do objeto OracleTransaction do .NET Framework</param>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência à uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
    /// <returns>Número de registros afetados pela instrução Oracle</returns>
    public static int ExecuteNonQuery(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
    {
      OracleCommand cmd = new OracleCommand();
      PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
      int val = cmd.ExecuteNonQuery();
      cmd.Parameters.Clear();
      return val;
    }

    /// <summary>
    /// Neste OverLoad do método pode-se utilizar a mesma conexão utilizada em outras consultas anteriores ou
    /// posteriores. AVISO: Altamente recomendável utilizar este overload com objetos
    /// OracleConnection inseridos no contexto de cláusula "using" (responsável pela execução do Dispose das instâncias declaradas).
    /// Objetos OracleConnection utilizados neste contexto não permanecem abertos ao final da execução das instruções.
    /// </summary>
    /// <param name="conn">Instância do objeto OracleConnection do .NET Framework</param>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência à uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
    /// <returns>Número de registros afetados pela instrução Oracle</returns>
    public static int ExecuteNonQuery(OracleConnection conn, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
    {
      OracleCommand cmd = new OracleCommand();
      PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
      int val = cmd.ExecuteNonQuery();
      cmd.Parameters.Clear();
      return val;
    }

    /// <summary>
    /// Neste OverLoad do método as operações são executadas diretamente como uma operação em bancos de dados
    /// </summary>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência à uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
    /// <returns>Número de registros afetados pela instrução Oracle</returns>
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
    /// Neste OverLoad do método as operações são executadas na forma de Transaction Oracle, ou seja, execução
    /// de mais de uma operação em bancos de dados
    /// </summary>
    /// <param name="trans">Instância do objeto OracleTransaction do .NET Framework</param>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência à uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
    /// <returns>OracleCommand utilizado na execução da instrução T-Oracle</returns>
    public static OracleCommand ExecuteNonQueryCmd(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
    {
      OracleCommand cmd = new OracleCommand();
      PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
      int val = cmd.ExecuteNonQuery();

      return cmd;
    }

    /// <summary>
    /// Método responsável pela execução de operações com retorno de resultados (instruções SELECT) através do 
    /// objeto OracleDataReader do ADO.NET 
    /// </summary>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência a uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
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
    /// Método responsável pela execução de operações com retorno de resultados (instruções SELECT) através do 
    /// objeto OracleDataReader do ADO.NET 
    /// </summary>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência a uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
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
    /// Método responsável pela execução de operações com retorno de resultados (instruções SELECT) através do 
    /// objeto OracleDataReader do ADO.NET 
    /// </summary>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência a uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
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
    /// Método responsável pela execução de operações com retorno de resultados (instruções SELECT) através do 
    /// objeto OracleDataReader do ADO.NET. Este overload do método pode ser executado diversas vezes utilizando-se
    /// uma mesma instância do objeto OracleConnection. AVISO: Altamente recomendável utilizar este overload com objetos
    /// OracleConnection inseridos no contexto de cláusula "using" (responsável pela execução do Dispose das instâncias declaradas).
    /// Objetos OracleConnection utilizados neste contexto não permanecem abertos ao final da execução das instruções.
    /// </summary>
    /// <param name="connection">Conexão</param>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência a uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
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

    /// <summary>
    /// Método responsável pela execução de operações com retorno de resultados (instruções SELECT) através do 
    /// objeto OracleDataReader do ADO.NET. Este overload do método pode ser executado diversas vezes utilizando-se
    /// uma mesma instância do objeto OracleConnection. AVISO: Altamente recomendável utilizar este overload com objetos
    /// OracleConnection inseridos no contexto de cláusula "using" (responsável pela execução do Dispose das instâncias declaradas).
    /// Objetos OracleConnection utilizados neste contexto não permanecem abertos ao final da execução das instruções.
    /// </summary>
    /// <param name="cmd">Instância do objeto OracleCommand a ser ajustado</param>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência a uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
    /// <returns>OracleDataReader armazenando os registros selecionados</returns>
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
    /// Método responsável pela execução de operações com retorno de resultados (instruções SELECT)
    /// através do objeto DataSet do ADO.NET 
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
    /// Através deste OverLoad do método pode-se preencher um DataSet (objeto do ADO.NET) através do processamento
    /// de instruções Oracle convencionais ou procedimentos armazenados (stored procedures) existentes no SGDBR.
    /// </summary>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência a uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
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

    /// <summary>
    /// Através deste OverLoad do método pode-se preencher um DataSet (objeto do ADO.NET) através do processamento
    /// de instruções Oracle convencionais ou procedimentos armazenados (stored procedures) existentes no SGDBR.
    /// </summary>
    /// <param name="trans">Instância do objeto OracleTransaction do .NET Framework</param>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência a uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
    /// <returns>DataSet armazenando os registros selecionados</returns>
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
    /// Através deste método pode-se preencher um DataSet (objeto do ADO.NET) através do processamento
    /// de instruções Oracle convencionais ou procedimentos armazenados (stored procedures) existentes no SGDBR. Através
    /// do fornecimento do DataSet como parâmetro pode-se preenchê-lo com mais de uma DataTable.
    /// </summary>		
    /// <param name="dsCompleto">DataSet a ser preenchido (podendo ser preenchido com mais de uma tabela depois). Passado como referência.</param>
    /// <param name="nomeTabela">Nome da tabela a ser preenchida no DataSet</param>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência a uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
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
    /// 
    /// </summary>
    /// <param name="cmdType"></param>
    /// <param name="cmdText"></param>
    /// <param name="cmdParms"></param>
    /// <returns></returns>
    public static SqlDataReader ExecuteReaderSemFechar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
    {
      SqlCommand cmd = new SqlCommand();
      SqlConnection conn = new SqlConnection(CONN_STRING_SIAC);
      //-----------Tirando carcts especiais--------
      PreparaReader(cmdParms);
      //------------------------------------------
      try
      {
        PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
        SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return rdr;

      }
      catch (Exception ex)
      {
        if (conn != null && conn.State == ConnectionState.Open)
        {
          conn.Close();
          conn.Dispose();
        }
        throw (ex);
      }
    }

    /// <summary>
    /// Método responsável pela recuperação de valores individuais (únicos) do banco de dados. Como retorno tem-se um objeto
    /// do tipo object que deve ser convertido em um momento posterior.
    /// </summary>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência à uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
    /// <returns>Objeto (valor) único resultante da execução da consulta no banco de dados</returns>
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
    /// Método responsável pela recuperação de valores individuais (únicos) do banco de dados. Como retorno tem-se um objeto
    /// do tipo object que deve ser convertido em um momento posterior. Este overload do método deve ser utilizado para chamadas
    /// em contextos transacionais.
    /// </summary>
    /// <param name="trans">Instância do objeto OracleTransaction do .NET Framework</param>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado pelo método</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência à uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros da instrução Oracle</param>
    /// <returns>Objeto (valor) único resultante da execução da consulta no banco de dados</returns>
    public static object ExecuteScalar(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
    {
      OracleCommand cmd = new OracleCommand();
      PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
      object obj = cmd.ExecuteScalar();
      cmd.Parameters.Clear();
      return obj;
    }

    /// <summary>
    /// Método responsável pela atribuição de uma referência aos parâmetros Oracle armazenados na hashtable parmCache
    /// </summary>
    /// <param name="cacheKey">Nome, referência ou chave dos valores armazenados na hashtable</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo parâmetros de instruções Oracle</param>
    public static void CacheParameters(string cacheKey, params OracleParameter[] cmdParms)
    {
      parmCache[cacheKey] = cmdParms;
    }

    /// <summary>
    /// Método responsável pela recuperação do array de parâmetros Oracle armazenado na hashtable parmCache
    /// </summary>
    /// <param name="cacheKey">Nome, referência ou chave dos valores armazenados na hashtable</param>
    /// <returns>Array de parâmetros Oracle contendo parâmetros de instruções Oracle</returns>
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
    /// Método responsável pela manipulação de instâncias do objeto OracleCommand do ADO.NET
    /// para a execução de operações em bancos de dados
    /// </summary>
    /// <param name="cmd">Instância do objeto OracleCommand a ser ajustado</param>
    /// <param name="conn">Instância do objeto OracleConnection da conexão adotada</param>
    /// <param name="trans">Instância do objeto OracleTransaction do .NET Framework</param>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência a uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Oracle contendo eventuais parâmetros de instruções Oracle</param>
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
    /// <summary>
    /// Método responsável pela manipulação de instâncias do objeto SqlCommand do ADO.NET
    /// para a execução de operações em bancos de dados
    /// </summary>
    /// <param name="cmd">Instância do objeto SqlCommand a ser ajustado</param>
    /// <param name="conn">Instância do objeto SqlConnection da conexão adotada</param>
    /// <param name="trans">Instância do objeto SqlTransaction do .NET Framework</param>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Sql convencional ou a referência a uma
    /// stored procedure</param>
    /// <param name="cmdParms">Array de parâmetros Sql contendo eventuais parâmetros de instruções Sql</param>
    private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
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
        foreach (SqlParameter parm in cmdParms)
        {
          if (cmd.Parameters.Contains(parm))
            cmd.Parameters[parm.ParameterName] = parm;
          else
            cmd.Parameters.Add(parm);
        }
      }
    }

    /// <summary>
    /// Método responsável pela manipulação de instâncias do objeto OracleCommand do ADO.NET
    /// para a execução de operações em bancos de dados
    /// </summary>
    /// <param name="cmd">Instância do objeto OracleCommand a ser ajustado</param>
    /// <param name="conn">Instância do objeto OracleConnection da conexão adotada</param>
    /// <param name="trans">Instância do objeto OracleTransaction do .NET Framework</param>
    /// <param name="cmdType">Tipo de comando (CommandType.StoredProcedure, CommandType.TableDirect, CommandType.Text) a ser
    /// executado</param>
    /// <param name="cmdText">Comando Oracle a ser executado, podendo ser uma string Oracle convencional ou a referência a uma
    /// stored procedure</param>
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

    /// <summary>
    /// Método responsável por preparar o DataReader a ser utilizado nas operações de banco de dados com Oracle
    /// </summary>
    /// <param name="cmdParms">Parâmetros para Reader</param>
    private static void PreparaReader(OracleParameter[] cmdParms)
    {
      if (cmdParms != null)
      {
        foreach (OracleParameter parm in cmdParms)
        {
          //----------------Retirando caracteres especiais-------
          if ((parm.DbType.Equals(DbType.AnsiString) || parm.DbType.Equals(DbType.AnsiStringFixedLength)) && parm.Value != null)
          {
            if (parm.ParameterName != "sWhere" && parm.ParameterName != "sWhereEV" && parm.ParameterName != "sWhereAT") //não limpa as aspas caso o parametro seja a condição do filtro
              parm.Value = RemoveQuote(parm.Value.ToString());

            if (parm.Value.ToString() == "")
              parm.Value = null;
          }
          //--------------------------------------------------
        }
      }
    }

    private static void PreparaReader(SqlParameter[] cmdParms)
    {
      if (cmdParms != null)
      {
        foreach (SqlParameter parm in cmdParms)
        {
          //----------------Retirando caracteres especiais-------
          if ((parm.DbType.Equals(DbType.AnsiString) || parm.DbType.Equals(DbType.AnsiStringFixedLength)) && parm.Value != null)
          {
            if (parm.ParameterName != "@Where") //não limpa as aspas caso o parametro seja a condição do filtro
              parm.Value = RemoveQuote(parm.Value.ToString());

            if (parm.Value.ToString() == "")
              parm.Value = null;
          }
          //--------------------------------------------------
        }
      }
    }

    /// <summary>
    /// Remove aspa simples de texto para o banco de dados
    /// </summary>
    /// <param name="text">
    /// Texto a ser manipulado para execução no banco de dados.
    /// </param>
    /// <returns></returns>
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
        //				if(text.Length-1>indice1 ) //se o apóstrofo NÃO está na última posic
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
        //				if(text.Length-1>indice2 ) //se o apóstrofo NÃO está na última posic
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
        //				if(text.Length-1>indice3 ) //se o apóstrofo NÃO está na última posic
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