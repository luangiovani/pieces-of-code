using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Rodada Empresa
//-- Data : 24/10/2011
//-- Autor :  Denis Douglas Cavalheiro
namespace Classes
{
    public class RodadaEmpresa
    {
        #region Atributos
        private Rodada codRodada;
        private Cliente codCliente;
        private int? indAncora;
        #endregion

        #region Propriedades
        public Rodada CodRodada
        {
            get { return codRodada; }
            set { codRodada = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public int? IndAncora
        {
            get { return indAncora; }
            set { indAncora = value; }
        }
        #endregion

        #region Construtores
        public RodadaEmpresa()
            : this(-1)
        { }
        public RodadaEmpresa(int codRodada)
        {
            this.codRodada = new Rodada(codRodada);
            this.codCliente = new Cliente();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Rodada_Empresa.Rodada_EmpresaInc";
        private const string SPUPDATE = "Rodada_Empresa.Rodada_EmpresaAlt";
        private const string SPDELETE = "Rodada_Empresa.Rodada_EmpresaDel";
        private const string SPSELECTID = "Rodada_Empresa.Rodada_EmpresaSelId";
        private const string SPSELECTIDRODADA = "Rodada_Empresa.Rodada_EmpresaSelIdRodada";
        private const string SPSELECTPAG = "Rodada_Empresa.Rodada_EmpresaSelPag";
        private const string SPSELECTEV = "Rodada_Empresa.Rodada_EmpresaSelEv";
        private const string SPSELECTPARTICIPANTE = "Rodada_Empresa.Rodada_EmpresaSelParticipante";
        private const string SPSELECTTODOSPART = "Rodada_Empresa.Rodada_EmpresaSelTodosPart";
        #endregion

        #region Parametros Oracle
        private const string PARMcodRodada          = "codRodada";
        private const string PARMcodCliente         = "codCliente";
        private const string PARMindAncora          = "indAncora";
        private const string PARMCURSOR             = "curRODADA_EMPRESA";
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
                    /*0*/ new OracleParameter(PARMcodRodada, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodCliente, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMindAncora, OracleType.Int32)
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
            parms[0].Value = this.codRodada.CodRodada;
            parms[1].Value = this.codCliente.CodCLIENTE;
            parms[2].Value = this.indAncora;

            //if (this.seqAgenda < 0)
            //{
            //    parms[0].Direction = ParameterDirection.Output;
            //}
            //else
            //{
            //    parms[0].Direction = ParameterDirection.Input;
            //}
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
                        //seqAgenda = Convert.ToInt32(cmd.Parameters[PARMseqRodadaAgenda].Value);
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
                //seqAgenda = Convert.ToInt32(cmd.Parameters[PARMseqRodadaAgenda].Value);
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
        /// <param name="codEvento">Código do Registro</param>
        public static void Delete(int codRodada, int codCliente)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodRodada, OracleType.Int32, 4),
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
            parms[0].Value = codRodada;
            parms[1].Value = codCliente;
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
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codRodada, int codCliente, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodRodada, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)
            };
                parms[0].Value = codRodada;
                parms[1].Value = codCliente;
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
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codRodada)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRodada;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTIDRODADA, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codRodada, int codCliente, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };
            
            param[0].Value = codRodada;
            param[1].Value = codCliente;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>Sessao</returns>
        public static RodadaEmpresa GetDataRow(int codRodada)
        {
            OracleDataReader dr = LoadDataDr(codRodada);
            RodadaEmpresa rodadaEmpresa = new RodadaEmpresa();
            try
            {
                if (dr.Read())
                {
                    rodadaEmpresa.codRodada = new Rodada(Convert.ToInt32(dr["A930_cd_rodada"]));
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        rodadaEmpresa.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    if (dr["A931_ind_ancora"] != DBNull.Value)
                        rodadaEmpresa.indAncora = Convert.ToInt32(dr["A931_ind_ancora"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                rodadaEmpresa = new RodadaEmpresa();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return rodadaEmpresa;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Sessao</returns>
        public static RodadaEmpresa GetDataRow(int codRodada, int codCliente, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codRodada, codCliente, trans);
            RodadaEmpresa rodadaEmpresa = new RodadaEmpresa();
            try
            {
                if (dr.Read())
                {
                    rodadaEmpresa.codRodada = new Rodada(Convert.ToInt32(dr["A930_cd_rodada"]));
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        rodadaEmpresa.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    if (dr["A931_ind_ancora"] != DBNull.Value)
                        rodadaEmpresa.indAncora = Convert.ToInt32(dr["A931_ind_ancora"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                rodadaEmpresa = new RodadaEmpresa();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return rodadaEmpresa;
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

        #region LoadDataDrParticipante
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrParticipante(int codRodada, int indAncora)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMindAncora, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRodada;
            param[1].Value = indAncora;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTPARTICIPANTE, param);
            return dr;
        }
        #endregion

        #region LoadDataDrTodosParticipantes
        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrTodosParticipantes(int codRodada)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRodada;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTTODOSPART, param);
            return dr;
        }
        #endregion

        #endregion
    }
}
