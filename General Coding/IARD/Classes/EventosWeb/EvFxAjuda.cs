using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

//-- Classe Classes Sebrae
//-- Data : 31/10/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class EvFxAjuda // T2224_EV_FX_AJUDA      A022_cd_ev= And A2224_ini_fx= And A2224_fim_fx= 
    {
        #region Atributos
        private int codIniFx;
        private int codFimFx;
        private Eventos codEvento;
        private decimal vlrAjuda;
        private int indAlterado;
        private int codPessoa;
        private string logUsuario;
        #endregion

        #region Propriedades
        public int CodFimFx
        {
            get { return codFimFx; }
            set { codFimFx = value; }
        }
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public decimal VlrAjuda
        {
            get { return vlrAjuda; }
            set { vlrAjuda = value; }
        }
        public int IndAlterado
        {
            get { return indAlterado; }
            set { indAlterado = value; }
        }

        public int CodPessoa
        {
            get { return codPessoa; }
            set { codPessoa = value; }
        }
        public int CodIniFx
        {
            get { return codIniFx; }
            set { codIniFx = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public EvFxAjuda()
            : this(-1)
        { }

        public EvFxAjuda(int codIniFx)
        {
            this.codIniFx = codIniFx;
            this.codFimFx = CodFimFx;
            this.codEvento = new Eventos();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "EvFxAjuda.EvFxAjudaInc";
        private const string SPUPDATE = "EvFxAjuda.EvFxAjudaAlt";
        private const string SPDELETE = "EvFxAjuda.EvFxAjudaDel";
        private const string SPSELECTID = "EvFxAjuda.EvFxAjudaSelId";
        private const string SPSELECTPAG = "EvFxAjuda.EvFxAjudaSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodIniFx = "codIniFx";
        private const string PARMcodFimFx = "codFimFx";
        private const string PARMcodEvento = "codEvento";
        private const string PARMCURSOR = "curEvFxAjuda";
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
                    /*0*/ new OracleParameter(PARMcodIniFx, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodFimFx, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodEvento, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "vlrAjuda", OracleType.Float),
                    /*6*/ new OracleParameter( "indAlterado", OracleType.Int32),
                    /*7*/ new OracleParameter( "codPessoa", OracleType.Int32),
                    /*11*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codIniFx;
            parms[1].Value = this.codFimFx;
            parms[2].Value = this.codEvento.CodEvento;
            parms[3].Value = this.vlrAjuda;
            parms[4].Value = this.indAlterado;
            parms[5].Value = this.codPessoa;
            parms[6].Value = this.logUsuario;
            if (this.codIniFx < 0)
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

            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                        //Obtendo a chave de identificação do registro inserido.
                        codIniFx = Convert.ToInt32(cmd.Parameters[PARMcodIniFx].Value);
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
                codIniFx = Convert.ToInt32(cmd.Parameters[PARMcodIniFx].Value);
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
        /// <param name="codIniFx">Código do Registro</param>
        public static void Delete(int codIniFx, int codFimFx, int codEvento)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodIniFx, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodFimFx, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4)
            };
            parms[0].Value = codIniFx;
            parms[1].Value = codFimFx;
            parms[2].Value = codEvento;
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
        /// <param name="codIniFx">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codIniFx, int codFimFx, int codEvento, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodIniFx, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodFimFx, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4) 
            };
                parms[0].Value = codIniFx;
                parms[1].Value = codFimFx;
                parms[2].Value = codEvento;
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
        /// <param name="codIniFx">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codIniFx, int codFimFx, int codEvento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodIniFx, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodFimFx, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codIniFx;
            param[1].Value = codFimFx;
            param[2].Value = codEvento;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codIniFx">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codIniFx, int codFimFx, int codEvento, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodIniFx, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodFimFx, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codIniFx;
            param[1].Value = codFimFx;
            param[2].Value = codEvento;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codIniFx">Código do Registro</param>
        /// <returns>EvFxAjuda</returns>
        public static EvFxAjuda GetDataRow(int codIniFx, int codFimFx, int codEvento)
        {
            OracleDataReader dr = LoadDataDr(codIniFx, codFimFx, codEvento);
            EvFxAjuda EvFxAjuda = new EvFxAjuda();
            try
            {
                if (dr.Read())
                {
                    EvFxAjuda.codIniFx = Convert.ToInt32(dr["A2224_ini_fx"]);
                    EvFxAjuda.codFimFx = Convert.ToInt32(dr["A2224_fim_fx"]);
                    EvFxAjuda.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A2224_vlr_ajuda"] != DBNull.Value && dr["A2224_vlr_ajuda"] != DBNull.Value && dr["A2224_vlr_ajuda"].ToString() != "")
                        EvFxAjuda.vlrAjuda = Convert.ToDecimal(dr["A2224_vlr_ajuda"]);
                    if (dr["A2224_ind_alterado"] != DBNull.Value && dr["A2224_ind_alterado"] != DBNull.Value && dr["A2224_ind_alterado"].ToString() != "")
                        EvFxAjuda.indAlterado = Convert.ToInt32(dr["A2224_ind_alterado"]);
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        EvFxAjuda.codPessoa = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        EvFxAjuda.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                EvFxAjuda = new EvFxAjuda();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return EvFxAjuda;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codIniFx">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>EvFxAjuda</returns>
        public static EvFxAjuda GetDataRow(int codIniFx, int codFimFx, int codEvento, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codIniFx, codFimFx, codEvento, trans);
            EvFxAjuda EvFxAjuda = new EvFxAjuda();
            try
            {
                if (dr.Read())
                {
                    EvFxAjuda.codIniFx = Convert.ToInt32(dr["A2224_ini_fx"]);
                    EvFxAjuda.codFimFx = Convert.ToInt32(dr["A2224_fim_fx"]);
                    EvFxAjuda.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A2224_vlr_ajuda"] != DBNull.Value && dr["A2224_vlr_ajuda"] != DBNull.Value && dr["A2224_vlr_ajuda"].ToString() != "")
                        EvFxAjuda.vlrAjuda = Convert.ToDecimal(dr["A2224_vlr_ajuda"]);
                    if (dr["A2224_ind_alterado"] != DBNull.Value && dr["A2224_ind_alterado"] != DBNull.Value && dr["A2224_ind_alterado"].ToString() != "")
                        EvFxAjuda.indAlterado = Convert.ToInt32(dr["A2224_ind_alterado"]);
                    if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                        EvFxAjuda.codPessoa = Convert.ToInt32(dr["A052_cd_usuario"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        EvFxAjuda.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                EvFxAjuda = new EvFxAjuda();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return EvFxAjuda;
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

        #region Métodos Específicos

        #region LoadDataDrCustoAjuda
        /// <summary>
        /// retorma dr com todos os custosAjuda de um evento-
        /// </summary>
        /// <param name="codEvento"></param>
        /// <returns>dr, deve ser fechado após uso</returns>
        public static OracleDataReader LoadDataDrCustoAjuda(int codEvento)
        {
            string where = " and a022_cd_ev = " + codEvento;
            Paginacao dr = EvFxAjuda.LoadDataPaginacao( where, 1, 1000, "a2224_ini_fx");

            return dr.DataReader;
        }
        #endregion

        #endregion

    }
}
