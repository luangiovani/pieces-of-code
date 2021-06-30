using System;
using System.Data.OracleClient;
using System.Data;

namespace Database.Entities
{
    /// <summary>
    /// Classe de Mapeamento com a Entidade de Banco de Dados - T196_GrupoReserva
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
    public class GrupoReserva
    {
        #region Atributos
        private int codGrupoReserva;
        private Eventos codEvento;
        private DateTime? dataRes;
        private decimal? valorPago;
        private EscritorioSebrae codEscr;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodGrupoReserva
        {
            get { return codGrupoReserva; }
            set { codGrupoReserva = value; }
        }
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public DateTime? DataRes
        {
            get { return dataRes; }
            set { dataRes = value; }
        }
        public decimal? ValorPago
        {
            get { return valorPago; }
            set { valorPago = value; }
        }
        public EscritorioSebrae CodEscr
        {
            get { return codEscr; }
            set { codEscr = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public GrupoReserva()
            : this(-1)
        { }
        public GrupoReserva(int codGrupoReserva)
        {
            this.codGrupoReserva = codGrupoReserva;
            this.codEvento = new Eventos();
            this.codEscr = new EscritorioSebrae();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "GrupoReserva.GrupoReservaInc";
        private const string SPUPDATE = "GrupoReserva.GrupoReservaAlt";
        private const string SPDELETE = "GrupoReserva.GrupoReservaDel";
        private const string SPSELECTID = "GrupoReserva.GrupoReservaSelId";
        private const string SPSELECTPAG = "GrupoReserva.GrupoReservaSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODGRUPORESERVA = "codGrupoReserva";
        private const string PARMCODEVENTO = "codEvento";
        private const string PARMCURSOR = "curGrupoReserva";
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
                    /*0*/ new OracleParameter(PARMCODGRUPORESERVA, OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMCODEVENTO, OracleType.Int32, 8, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "dataRes", OracleType.DateTime),
                    /*3*/ new OracleParameter( "valorPago", OracleType.Float),
                    /*4*/ new OracleParameter( "CodEscr", OracleType.Int32),
                    /*5*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codGrupoReserva;
            parms[1].Value = this.codEvento.CodEvento;
            parms[2].Value = this.DataRes;
            if (this.valorPago != null)
            { parms[3].Value = this.ValorPago; }           
            parms[4].Value = this.codEscr.CodEscrSebrae;
            parms[5].Value = this.logUsuario;
            if (this.codGrupoReserva < 0)
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
                        codGrupoReserva = Convert.ToInt32(cmd.Parameters[PARMCODGRUPORESERVA].Value);
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
                codGrupoReserva = Convert.ToInt32(cmd.Parameters[PARMCODGRUPORESERVA].Value);
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
        /// <param name="codGrupoReserva">Código do Registro</param>
        public static void Delete(int codGrupoReserva, int codEvento)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODGRUPORESERVA, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4)
            };
            parms[0].Value = codGrupoReserva;
            parms[1].Value = codEvento;
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
        /// <param name="codGrupoReserva">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codGrupoReserva, int codEvento, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMCODGRUPORESERVA, OracleType.Int32, 4)  ,
                new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4)
            };
                parms[0].Value = codGrupoReserva;
                parms[1].Value = codEvento;
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
        /// <param name="codGrupoReserva">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codGrupoReserva, int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODGRUPORESERVA, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codGrupoReserva;
            param[1].Value = codEvento;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codGrupoReserva">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codGrupoReserva, int codEvento, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODGRUPORESERVA, OracleType.Int32, 4), 
                    new OracleParameter(PARMCODEVENTO, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codGrupoReserva;
            param[1].Value = codEvento;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = Context.DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codGrupoReserva">Código do Registro</param>
        /// <returns>GrupoReserva</returns>
        public static GrupoReserva GetDataRow(int codGrupoReserva, int codEvento)
        {
            OracleDataReader dr = LoadDataDr(codGrupoReserva, codEvento);
            GrupoReserva gr = new GrupoReserva();
            try
            {
                if (dr.Read())
                {
                    gr.codGrupoReserva = Convert.ToInt32(dr["a196_cd_grupo_res"]);
                    gr.codEvento = new Eventos(Convert.ToInt32(dr["a022_cd_ev"]));
                    if (dr["a196_dt_res"] != DBNull.Value)
                    { gr.dataRes = Convert.ToDateTime(dr["a196_dt_res"]); }
                    if (dr["a196_vlr_pagto"] != DBNull.Value)
                        gr.valorPago = Convert.ToDecimal(dr["a196_vlr_pagto"]);
                    gr.codEscr = new EscritorioSebrae(Convert.ToInt32(dr["a004_cd_escr"]));
                    gr.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                gr = new GrupoReserva();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return gr;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codGrupoReserva">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>GrupoReserva</returns>
        public static GrupoReserva GetDataRow(int codGrupoReserva, int codEvento, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codGrupoReserva, codEvento, trans);
            GrupoReserva gr = new GrupoReserva();
            try
            {
                if (dr.Read())
                {
                    gr.codGrupoReserva = Convert.ToInt32(dr["a196_cd_grupo_res"]);
                    gr.codEvento = new Eventos(Convert.ToInt32(dr["a022_cd_ev"]));
                    if (dr["a196_dt_res"] != DBNull.Value)
                    { gr.dataRes = Convert.ToDateTime(dr["a196_dt_res"]); }
                    if (dr["a196_vlr_pagto"] != DBNull.Value)
                        gr.valorPago = Convert.ToDecimal(dr["a196_vlr_pagto"]);
                    gr.codEscr = new EscritorioSebrae(Convert.ToInt32(dr["a004_cd_escr"]));
                    gr.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                gr = new GrupoReserva();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return gr;
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


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion


        #endregion

    }
}
