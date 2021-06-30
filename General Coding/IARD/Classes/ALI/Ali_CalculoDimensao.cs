using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Classes
{
    public class Ali_CalculoDimensao // TB0622_CALC_DIMENSAO
    {
        #region Atributos
        private int numRepeticao;
        private Ali_Dimensao dimensao;
        private Ali_Projeto projeto;
        private Decimal vlrDimensao;
        private DateTime logDtaInclusao;
        private DateTime logDtaAlteracao;
        private Pessoa logCodPessoa;
        #endregion

        #region Propriedades
        public int NumRepeticao
        {
            get { return numRepeticao; }
            set { numRepeticao = value; }
        }
        public Ali_Dimensao Dimensao
        {
            get { return dimensao; }
            set { dimensao = value; }
        }
        public Ali_Projeto Projeto
        {
            get { return projeto; }
            set { projeto = value; }
        }
        public Decimal VlrDimensao
        {
            get { return vlrDimensao; }
            set { vlrDimensao = value; }
        }
        public DateTime LogDtaInclusao
        {
            get { return logDtaInclusao; }
            set { logDtaInclusao = value; }
        }
        public DateTime LogDtaAlteracao
        {
            get { return logDtaAlteracao; }
            set { logDtaAlteracao = value; }
        }
        public Pessoa LogCodPessoa
        {
            get { return logCodPessoa; }
            set { logCodPessoa = value; }
        }
        #endregion

        #region Construtores
        public Ali_CalculoDimensao()
            : this(-1)
        { }
        public Ali_CalculoDimensao(int codDimensao)
        {
            this.dimensao = new Ali_Dimensao(codDimensao);
            this.projeto = new Ali_Projeto();
            this.logCodPessoa = new Pessoa();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ALI_CALCDIMENSAO.CalcDimensaoInc";
        private const string SPUPDATE = "ALI_CALCDIMENSAO.CalcDimensaoAlt";
        private const string SPDELETE = "ALI_CALCDIMENSAO.CalcDimensaoDel";
        private const string SPSELECTID = "ALI_CALCDIMENSAO.CalcDimensaoSelId";
        private const string SPSELECTPAG = "ALI_CALCDIMENSAO.CalcDimensaoSelPag";
        private const string SPSELECTCALC = "ALI_CALCDIMENSAO.CalcDimensaoSelCalc";
        private const string SPSELECTCALCREL = "ALI_CALCDIMENSAO.CalcDimensaoSelRel";
        private const string SPSELECTREPETICAO = "ALI_CALCDIMENSAO.CalcDimensaoRepeticao";
        #endregion

        #region Parametros Oracle
        private const string PARMCURSOR = "curCalcDimensao";
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
                    /*0*/ new OracleParameter("codDimensao", OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()),
                    /*1*/ new OracleParameter("codProjeto", OracleType.Int32),
                    /*2*/ new OracleParameter("numRepeticao", OracleType.Int32),
                    /*3*/ new OracleParameter("vlrDimensao", OracleType.Double),
                    /*4*/ new OracleParameter("codPessoa", OracleType.Int32)
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
            parms[0].Value = this.dimensao.CodDimensao;
            parms[1].Value = this.projeto.CodProjeto;
            parms[2].Value = this.numRepeticao;
            parms[3].Value = this.vlrDimensao;
            parms[4].Value = this.logCodPessoa.CodPessoa;

            if (this.dimensao.CodDimensao < 0)
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
                        dimensao.CodDimensao = Convert.ToInt32(cmd.Parameters["codDimensao"].Value);
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
                dimensao.CodDimensao = Convert.ToInt32(cmd.Parameters["codDimensao"].Value);
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
        public static void Delete(int codDimensao, int codProjeto, int numRepeticao)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter("codDimensao", OracleType.Int32, 4),
                                                              new OracleParameter("codProjeto", OracleType.Int32, 4),
                                                              new OracleParameter("numRepeticao", OracleType.Int32, 4)};
            parms[0].Value = codDimensao;
            parms[1].Value = codProjeto;
            parms[2].Value = numRepeticao;
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
        public static void Delete(int codDimensao, int codProjeto, int numRepeticao, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter("codDimensao", OracleType.Int32, 4),
                                                              new OracleParameter("codProjeto", OracleType.Int32, 4),
                                                              new OracleParameter("numRepeticao", OracleType.Int32, 4)};
                parms[0].Value = codDimensao;
                parms[1].Value = codProjeto;
                parms[2].Value = numRepeticao;
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
        public static OracleDataReader LoadDataDr(int codDimensao, int codProjeto, int numRepeticao)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codDimensao", OracleType.Int32, 4), 
                    new OracleParameter("codProjeto", OracleType.Int32, 4), 
                    new OracleParameter("numRepeticao", OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codDimensao;
            param[1].Value = codProjeto;
            param[2].Value = numRepeticao;
            param[3].Direction = ParameterDirection.Output;

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
        public static OracleDataReader LoadDataDr(int codDimensao, int codProjeto, int numRepeticao, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter("codDimensao", OracleType.Int32, 4), 
                    new OracleParameter("codProjeto", OracleType.Int32, 4), 
                    new OracleParameter("numRepeticao", OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codDimensao;
            param[1].Value = codProjeto;
            param[2].Value = numRepeticao;
            param[3].Direction = ParameterDirection.Output;

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
        public static Ali_CalculoDimensao GetDataRow(int codDimensao, int codProjeto, int numRepeticao)
        {
            OracleDataReader dr = LoadDataDr(codDimensao, codProjeto, numRepeticao);
            Ali_CalculoDimensao calcDimensao = new Ali_CalculoDimensao();
            try
            {
                if (dr.Read())
                {
                    calcDimensao.dimensao = new Ali_Dimensao(Convert.ToInt32(dr["A0619_COD_DIMENSAO"]));
                    calcDimensao.projeto = new Ali_Projeto(Convert.ToInt32(dr["A0606_COD_PROJETO"]));
                    calcDimensao.numRepeticao = Convert.ToInt32(dr["A0622_NR_REPETICAO"]);
                    calcDimensao.vlrDimensao = Convert.ToDecimal(dr["A0622_VLR_DIMENSAO"]);
                    calcDimensao.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                calcDimensao = new Ali_CalculoDimensao();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return calcDimensao;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Usuario</returns>
        public static Ali_CalculoDimensao GetDataRow(int codDimensao, int codProjeto, int numRepeticao, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codDimensao, codProjeto, numRepeticao, trans);
            Ali_CalculoDimensao calcDimensao = new Ali_CalculoDimensao();
            try
            {
                if (dr.Read())
                {
                    calcDimensao.dimensao = new Ali_Dimensao(Convert.ToInt32(dr["A0619_COD_DIMENSAO"]));
                    calcDimensao.projeto = new Ali_Projeto(Convert.ToInt32(dr["A0606_COD_PROJETO"]));
                    calcDimensao.numRepeticao = Convert.ToInt32(dr["A0622_NR_REPETICAO"]);
                    calcDimensao.vlrDimensao = Convert.ToDecimal(dr["A0622_VLR_DIMENSAO"]);
                    calcDimensao.logCodPessoa = new Pessoa(Convert.ToInt32(dr["A572_CD_PES_ATZ"]));
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                calcDimensao = new Ali_CalculoDimensao();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return calcDimensao;
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

        #region LoadDataDrCalc

        public static OracleDataReader LoadDataDrCalc(int codProjeto, int numRepeticao)
        {
            OracleParameter[] param = new OracleParameter[] {
                    new OracleParameter("codProjeto", OracleType.Int32),
                    new OracleParameter("numRepeticao", OracleType.Int32),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codProjeto;
            param[1].Value = numRepeticao;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTCALC, param);
            return dr;
        }
        #endregion

        #region LoadDataDrCalcRel

        public static OracleDataReader LoadDataDrCalcRel(int codProjeto, int numRepeticao)
        {
            OracleParameter[] param = new OracleParameter[] {
                    new OracleParameter("codProjeto", OracleType.Int32),
                    new OracleParameter("numRepeticao", OracleType.Int32),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codProjeto;
            param[1].Value = numRepeticao;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTCALCREL, param);
            return dr;
        }
        #endregion

        #region LoadDataDrRepeticao
        public static OracleDataReader LoadDataDrRepeticao(int codProjeto)
        {
            OracleParameter[] param = new OracleParameter[] {
                    new OracleParameter("codProjeto", OracleType.Int32),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codProjeto;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTREPETICAO, param);
            return dr;
        }
        #endregion

        #endregion
    }
}
