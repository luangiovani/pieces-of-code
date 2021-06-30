using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T075_TEXTOS
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
    public class Textos
    {
        #region Atributos
        private int codTexto;
        private Usuario codUsuario;
        private TipoConsultaAtend codTipoConsAt;
        private string titTexto;
        private string dscTexto;
        private string dscUrl;
        private int indDesativado;
        private int indExpira;
        private DateTime dtaExpira;
        private string logUsuario;

        #endregion

        #region Propriedades
        public int CodTexto
        {
            get { return codTexto; }
            set { codTexto = value; }
        }
        public Usuario CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public TipoConsultaAtend CodTipoConsAt
        {
            get { return codTipoConsAt; }
            set { codTipoConsAt = value; }
        }
        public string TitTexto
        {
            get { return titTexto; }
            set { titTexto = value; }
        }
        public string DscTexto
        {
            get { return dscTexto; }
            set { dscTexto = value; }
        }
        public string DscUrl
        {
            get { return dscUrl; }
            set { dscUrl = value; }
        }
        public int IndDesativado
        {
            get { return indDesativado; }
            set { indDesativado = value; }
        }
        public int IndExpira
        {
            get { return indExpira; }
            set { indExpira = value; }
        }
        public DateTime DtaExpira
        {
            get { return dtaExpira; }
            set { dtaExpira = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }

        #endregion

        #region Construtores
        public Textos()
            : this(-1)
        { }
        public Textos(int codTexto)
        {
            this.codTexto = codTexto;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Textos.TextosInc";
        private const string SPUPDATE = "Textos.TextosAlt";
        private const string SPDELETE = "Textos.TextosDel";
        private const string SPSELECTID = "Textos.TextosSelId";
        private const string SPSELECTPAG = "Textos.TextosSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codTexto";
        private const string PARMCURSOR = "curTextos";
        #endregion

        #region Metodos

        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;

            // Tentando buscar os parameters do cache        
            parms = Context.DataBase.GetCachedParameters(SPINSERT);
            //parms = OutputCacheParameters(SPINSERT);
            if (parms == null)
            {
                parms = new OracleParameter[]{ 
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 2, ParameterDirection.InputOutput.ToString()) ,
                      new OracleParameter( "codUsuario", OracleType.Int32),
                      new OracleParameter( "codTipoConsAt", OracleType.Int32),
                      new OracleParameter( "titTexto", OracleType.VarChar),
                      new OracleParameter( "dscTexto", OracleType.VarChar),
                      new OracleParameter( "dscUrl", OracleType.VarChar),
                      new OracleParameter( "indDesativado", OracleType.Int32),
                      new OracleParameter( "indExpira", OracleType.Int32),
                      new OracleParameter( "dtaExpira", OracleType.DateTime),				
                /*9*/ new OracleParameter( "logUsuario", OracleType.VarChar)
            };

                // Criando cache dos parameters 
                Context.DataBase.CacheParameters(SPINSERT, parms);
            }
            return (parms);
        }

        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[0].Value = this.codTexto;
            parms[1].Value = this.codUsuario.CodUsuario;
            parms[2].Value = this.codTipoConsAt.CodTipoConsAt;
            parms[3].Value = this.titTexto.ToUpper();
            parms[4].Value = this.dscTexto.ToUpper();
            parms[5].Value = this.dscUrl.ToUpper();
            parms[6].Value = this.indDesativado;
            parms[7].Value = this.indExpira;
            parms[8].Value = this.dtaExpira;
            parms[9].Value = this.logUsuario.ToUpper();
            if (this.codTexto < 0)
            {
                parms[0].Direction = ParameterDirection.Output;
            }
            else
            {
                parms[0].Direction = ParameterDirection.Input;
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

            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        codTexto = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                OracleCommand cmd = Context.DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                //Obtendo a chave de identificação do registro inserido.
                codTexto = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
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
                Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPUPDATE, parms);
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
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
            parms[0].Value = codigo;
            using (OracleConnection conn = new OracleConnection(Context.DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
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
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
                parms[0].Value = codigo;
                Context.DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETE, parms);
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

                new OracleParameter(PARMCODIGO, OracleType.Int32, 2), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
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
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2),
                                                              new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <returns>Textos</returns>
        public static Textos GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            Textos Textos = new Textos();
            try
            {
                if (dr.Read())
                {
                    Textos.codTexto = Convert.ToInt32(dr["A075_cd_texto"]);
                    Textos.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    Textos.codTipoConsAt = new TipoConsultaAtend(Convert.ToInt32(dr["A005_cd_tp_cons"]));
                    Textos.titTexto = Convert.ToString(dr["A075_tit_texto"]);
                    Textos.dscTexto = Convert.ToString(dr["A075_dsc_texto"]);
                    if (dr["A075_url"] != DBNull.Value && dr["A075_url"].ToString() != "")
                        Textos.dscUrl = Convert.ToString(dr["A075_url"]);
                    if (dr["A075_ind_des"] != DBNull.Value && dr["A075_ind_des"].ToString() != "")
                        Textos.indDesativado = Convert.ToInt32(dr["A075_ind_des"]);
                    if (dr["A075_ind_expira"] != DBNull.Value && dr["A075_ind_expira"].ToString() != "")
                        Textos.indExpira = Convert.ToInt32(dr["A075_ind_expira"]);
                    if (dr["A075_DT_expira"] != DBNull.Value && dr["A075_DT_expira"].ToString() != "")
                        Textos.dtaExpira = Convert.ToDateTime(dr["A075_DT_expira"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Textos.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Textos = new Textos();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Textos;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Textos</returns>
        public static Textos GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            Textos Textos = new Textos();
            try
            {
                if (dr.Read())
                {
                    Textos.codTexto = Convert.ToInt32(dr["A075_cd_texto"]);
                    Textos.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    Textos.codTipoConsAt = new TipoConsultaAtend(Convert.ToInt32(dr["A005_cd_tp_cons"]));
                    Textos.titTexto = Convert.ToString(dr["A075_tit_texto"]);
                    Textos.dscTexto = Convert.ToString(dr["A075_dsc_texto"]);
                    if (dr["A075_url"] != DBNull.Value && dr["A075_url"].ToString() != "")
                        Textos.dscUrl = Convert.ToString(dr["A075_url"]);
                    if (dr["A075_ind_des"] != DBNull.Value && dr["A075_ind_des"].ToString() != "")
                        Textos.indDesativado = Convert.ToInt32(dr["A075_ind_des"]);
                    if (dr["A075_ind_expira"] != DBNull.Value && dr["A075_ind_expira"].ToString() != "")
                        Textos.indExpira = Convert.ToInt32(dr["A075_ind_expira"]);
                    if (dr["A075_DT_expira"] != DBNull.Value && dr["A075_DT_expira"].ToString() != "")
                        Textos.dtaExpira = Convert.ToDateTime(dr["A075_DT_expira"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        Textos.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                Textos = new Textos();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return Textos;
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
        public static Context.Paginacao LoadDataPaginacao(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
        {
            Context.Paginacao paginacao = new Context.Paginacao();

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


            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPAG, parms);

            // dr.Read();

            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion

        #region UltimoTexto
        public static OracleDataReader UltimoTexto()
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCURSOR, OracleType.Cursor) };
            param[0].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, "Textos.TextosSelUltimo", param);
            return dr;
        }
        #endregion

        #endregion

        public static string LoadDataTextoTratado(int CodTexto)
        {
            string text = "";
            string textTratado = "";
            if (CodTexto > 0)
            {
                Textos tx = Textos.GetDataRow(Convert.ToInt32(CodTexto));
                if (tx.DscTexto != null)
                {

                    text = "<align=\"center\"><font size=2px ><b>  " + tx.TitTexto + " </b></font></align><br><br>"
                        + tx.DscTexto.Replace("@1", "¬1").Replace("@2", "¬2").Replace("http://www.", "www.").Replace("www.", "http://www.");

                    text = text.Replace("href=\"http:", "TARGET=\"_blank\" href=\"http:");
                }
                else
                {
                    text = "Nenhum registro encontrado " +
                        "¬1<A HREF=\"sts_pesquisa.pes_textos?vcdtexto=0&^^\">Vazio</A>¬2<br>";
                }
            }

            //text = text;
            foreach (string t in text.Split('¬'))
            {
                if (t.Trim().Length > 0)
                {
                    if (t.ToCharArray(0, 1)[0] == '1')
                    {
                        //target=\"naveg\" 
                        textTratado += "<A HREF=\"" + "NavegaTextoMan.aspx" + t.Substring(33, t.Length - 33);
                    }
                    else if (t.ToCharArray(0, 1)[0] == '2')
                    {
                        textTratado += t.Substring(1, t.Length - 1);
                    }
                    else
                    {
                        textTratado += t;
                    }
                }
            }
            return textTratado;
        }
    }
}