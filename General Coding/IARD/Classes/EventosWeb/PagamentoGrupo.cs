using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

//-- Classe Classes Sebrae
//-- Data : 10/11/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class PagamentoGrupo // T2206_CPGTO_GRUPO 
    {
        #region Atributos
        private Grupo2201 codGrupo2201;
        private CondicaoPagtoEv codCondPagto;
        private Usuario codUsuario;
        private string logUsuario;
        #endregion

        #region Propriedades

        public Usuario CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public CondicaoPagtoEv CodCondPagto
        {
            get { return codCondPagto; }
            set { codCondPagto = value; }
        }
        public Grupo2201 CodGrupo2201
        {
            get { return codGrupo2201; }
            set { codGrupo2201 = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public PagamentoGrupo()
            : this(-1)
        { }
        public PagamentoGrupo(int codGrupo2201)
        {
            this.codGrupo2201 = new Grupo2201();
            this.codCondPagto = new CondicaoPagtoEv();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "PagamentoGrupo.PagamentoGrupoInc";
        private const string SPUPDATE = "PagamentoGrupo.PagamentoGrupoAlt";
        private const string SPDELETE = "PagamentoGrupo.PagamentoGrupoDel";
        private const string SPSELECTID = "PagamentoGrupo.PagamentoGrupoSelId";
        private const string SPSELECTPAG = "PagamentoGrupo.PagamentoGrupoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodGrupo2201 = "codGrupo2201";
        private const string PARMcodCondPagto = "codCondPagto";
        private const string PARMCURSOR = "curPagamentoGrupo";
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
                    /*0*/ new OracleParameter(PARMcodGrupo2201, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodCondPagto, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*5*/ new OracleParameter( "logUsuario", OracleType.VarChar)
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
            parms[0].Value = this.codGrupo2201;
            parms[1].Value = this.codCondPagto;
            parms[2].Value = this.codUsuario;
            parms[3].Value = this.logUsuario;
            if (this.codGrupo2201.CodGrupo2201 < 0)
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
                        //codGrupo2201 = Convert.ToInt32(cmd.Parameters[PARMcodGrupo2201].Value);
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
                //codGrupo2201 = Convert.ToInt32(cmd.Parameters[PARMcodGrupo2201].Value);
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
        /// <param name="codGrupo2201">Código do Registro</param>
        public static void Delete(int codGrupo2201, int codCondPagto)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodGrupo2201, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCondPagto, OracleType.Int32, 4)
            };
            parms[0].Value = codGrupo2201;
            parms[1].Value = codCondPagto;
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
        /// <param name="codGrupo2201">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codGrupo2201, int codCondPagto, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodGrupo2201, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCondPagto, OracleType.Int32, 4)
            };
                parms[0].Value = codGrupo2201;
                parms[1].Value = codCondPagto;
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
        /// <param name="codGrupo2201">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codGrupo2201, int codCondPagto)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodGrupo2201, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCondPagto, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codGrupo2201;
            param[1].Value = codCondPagto;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codGrupo2201">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codGrupo2201, int codCondPagto, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodGrupo2201, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCondPagto, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codGrupo2201;
            param[1].Value = codCondPagto;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codGrupo2201">Código do Registro</param>
        /// <returns>PagamentoGrupo</returns>
        public static PagamentoGrupo GetDataRow(int codGrupo2201, int codCondPagto)
        {
            OracleDataReader dr = LoadDataDr(codGrupo2201, codCondPagto);
            PagamentoGrupo PagamentoGrupo = new PagamentoGrupo();
            try
            {
                if (dr.Read())
                {
                    PagamentoGrupo.codGrupo2201 = new Grupo2201(Convert.ToInt32(dr["A2201_cd_grupo"]));
                    PagamentoGrupo.codCondPagto = new CondicaoPagtoEv(Convert.ToInt32(dr["A2204_cd_cond_pgto"]));
                    PagamentoGrupo.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        PagamentoGrupo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                PagamentoGrupo = new PagamentoGrupo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return PagamentoGrupo;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codGrupo2201">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>PagamentoGrupo</returns>
        public static PagamentoGrupo GetDataRow(int codGrupo2201, int codCondPagto, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codGrupo2201, codCondPagto, trans);
            PagamentoGrupo PagamentoGrupo = new PagamentoGrupo();
            try
            {
                if (dr.Read())
                {
                    PagamentoGrupo.codGrupo2201 = new Grupo2201(Convert.ToInt32(dr["A2201_cd_grupo"]));
                    PagamentoGrupo.codCondPagto = new CondicaoPagtoEv(Convert.ToInt32(dr["A2204_cd_cond_pgto"]));
                    PagamentoGrupo.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        PagamentoGrupo.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                PagamentoGrupo = new PagamentoGrupo();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return PagamentoGrupo;
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

    }
}
