using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 05/11/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class InstrutorHabilitado // T1156_INST_HABILITADO 
    {
        #region Atributos
        private int codUsuario;
        private AreaConhec codArea1144;
        private ProdutoCredenciado codProdCredenc;
        private EscritorioSebrae codEscrSebrae;
        private TituloEvento codTituloEv;
        private string tipInstCons;
        private string logUsuario;
        #endregion

        #region Propriedades
        public AreaConhec CodArea1144
        {
          get { return codArea1144; }
          set { codArea1144 = value; }
        }
        public ProdutoCredenciado CodProdCredenc
        {
          get { return codProdCredenc; }
          set { codProdCredenc = value; }
        }
        public EscritorioSebrae CodEscrSebrae
        {
          get { return codEscrSebrae; }
          set { codEscrSebrae = value; }
        }
        public TituloEvento CodTituloEv
        {
          get { return codTituloEv; }
          set { codTituloEv = value; }
        }
        public string TipInstCons
        {
          get { return tipInstCons; }
          set { tipInstCons = value; }
        }
        public int CodUsuario
        {
          get { return codUsuario; }
          set { codUsuario = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public InstrutorHabilitado()
            : this(-1)
        { }
        public InstrutorHabilitado(int codUsuario)
        {
            this.codUsuario = codUsuario;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "InstrutorHabilitado.InstrutorHabilitadoInc";
        private const string SPUPDATE = "InstrutorHabilitado.InstrutorHabilitadoAlt";
        private const string SPDELETE = "InstrutorHabilitado.InstrutorHabilitadoDel";
        private const string SPSELECTID = "InstrutorHabilitado.InstrutorHabilitadoSelId";
        private const string SPSELECTPAG = "InstrutorHabilitado.InstrutorHabilitadoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodUsuario = "codUsuario";
        private const string PARMcodArea1144 = "codArea1144";
        private const string PARMcodProdCredenc = "codProdCredenc";
        private const string PARMcodEscrSebrae = "codEscrSebrae";
        private const string PARMcodTituloEv = "codTituloEv";
        private const string PARMCURSOR = "curInstrutorHabilitado";

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
                    /*0*/ new OracleParameter(PARMcodUsuario, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodArea1144, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodProdCredenc, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*4*/ new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*5*/ new OracleParameter( "tipInstCons", OracleType.Int32),
                    /*6*/ new OracleParameter( "logUsuario", OracleType.VarChar)
					
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
            parms[1].Value = this.codArea1144;
            parms[2].Value = this.codProdCredenc;
            parms[3].Value = this.codEscrSebrae;
            parms[4].Value = this.codTituloEv;
            parms[5].Value = this.tipInstCons;
            parms[6].Value = this.logUsuario;
			
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
                        codUsuario = Convert.ToInt32(cmd.Parameters[PARMcodUsuario].Value);
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
                codUsuario = Convert.ToInt32(cmd.Parameters[PARMcodUsuario].Value);
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
        /// <param name="codUsuario">Código do Registro</param>
        public static void Delete(int codUsuario, int codArea1144, int codProdCredenc, int codEscrSebrae, int codTituloEv)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4),
                new OracleParameter(PARMcodArea1144, OracleType.Int32, 4),
                new OracleParameter(PARMcodProdCredenc, OracleType.Int32, 4),
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4),
		        new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4)
            };
            parms[0].Value = codUsuario;
            parms[1].Value = codArea1144;
            parms[2].Value = codProdCredenc;
            parms[3].Value = codEscrSebrae;
            parms[4].Value = codTituloEv;

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
        /// <param name="codUsuario">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codUsuario, int codArea1144, int codProdCredenc, int codEscrSebrae,int codTituloEv, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodUsuario, OracleType.Int32, 4),
                new OracleParameter(PARMcodArea1144, OracleType.Int32, 4),
                new OracleParameter(PARMcodProdCredenc, OracleType.Int32, 4),
                new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4),
		        new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4)
            };
                parms[0].Value = codUsuario;
                parms[1].Value = codArea1144;
                parms[2].Value = codProdCredenc;
                parms[3].Value = codEscrSebrae;
      		parms[4].Value = codTituloEv;


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
        /// <param name="codUsuario">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codUsuario, int codArea1144, int codProdCredenc, int codEscrSebrae, int codTituloEv)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodArea1144, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodProdCredenc, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codUsuario;
            param[1].Value = codArea1144;
            param[2].Value = codProdCredenc;
            param[3].Value = codEscrSebrae;
param[4].Value = codTituloEv;

            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codUsuario">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codUsuario, int codArea1144, int codProdCredenc, int codEscrSebrae, int codTituloEv, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodUsuario, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodArea1144, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodProdCredenc, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEscrSebrae, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codUsuario;
            param[1].Value = codArea1144;
            param[2].Value = codProdCredenc;
            param[3].Value = codEscrSebrae;

            param[4].Value = codTituloEv;

            param[5].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codUsuario">Código do Registro</param>
        /// <returns>InstrutorHabilitado</returns>
        public static InstrutorHabilitado GetDataRow(int codUsuario, int codArea1144, int codProdCredenc, int codEscrSebrae, int codTituloEv)
        {
            OracleDataReader dr = LoadDataDr(codUsuario, codArea1144, codProdCredenc, codEscrSebrae, codTituloEv);
            InstrutorHabilitado InstrutorHabilitado = new InstrutorHabilitado();
            try
            {
                if (dr.Read())
                {
                    InstrutorHabilitado.codUsuario = Convert.ToInt32(dr["A572_cd_pes"]);
                    InstrutorHabilitado.codArea1144 = new AreaConhec(Convert.ToInt32(dr["A1144_cd_area"]));
                    InstrutorHabilitado.codProdCredenc = new ProdutoCredenciado(Convert.ToInt32(dr["A1157_cd_prod_credenc"]));
                    InstrutorHabilitado.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    InstrutorHabilitado.codTituloEv = new TituloEvento(Convert.ToInt32(dr["A008_cd_tit_ev"]));
                    InstrutorHabilitado.tipInstCons = Convert.ToString(dr["A1156_tp_inst_cons"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        InstrutorHabilitado.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                InstrutorHabilitado = new InstrutorHabilitado();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return InstrutorHabilitado;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codUsuario">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>InstrutorHabilitado</returns>
        public static InstrutorHabilitado GetDataRow(int codUsuario, int codArea1144, int codProdCredenc, int codEscrSebrae, int codTituloEv, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codUsuario, codArea1144, codProdCredenc, codEscrSebrae, codTituloEv, trans);
            InstrutorHabilitado InstrutorHabilitado = new InstrutorHabilitado();
            try
            {
                if (dr.Read())
                {
                    InstrutorHabilitado.codUsuario = Convert.ToInt32(dr["A572_cd_pes"]);
                    InstrutorHabilitado.codArea1144 = new AreaConhec(Convert.ToInt32(dr["A1144_cd_area"]));
                    InstrutorHabilitado.codProdCredenc = new ProdutoCredenciado(Convert.ToInt32(dr["A1157_cd_prod_credenc"]));
                    InstrutorHabilitado.codEscrSebrae = new EscritorioSebrae(Convert.ToInt32(dr["A004_cd_escr"]));
                    InstrutorHabilitado.codTituloEv = new TituloEvento(Convert.ToInt32(dr["A008_cd_tit_ev"]));
                    InstrutorHabilitado.tipInstCons = Convert.ToString(dr["A1156_tp_inst_cons"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        InstrutorHabilitado.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                InstrutorHabilitado = new InstrutorHabilitado();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return InstrutorHabilitado;
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