using System; 
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 11/07/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class ProdEvento // T187_PROD_EVENTO    And = codProduto And A125_cd_instr = codInstrutor

    {
        #region Atributos
        private int codSegmento;
        private TituloEvento codTituloEv;
        private ProdutoVenda codProduto;
        private Instrutor codInstrutor;
        private int qtdProd;
        private int qtdPartic;
        private int qtdEmpr;
        private int indPartic;
        private int indDev;
        private string logUsuario;
        #endregion

        #region Propriedades

        public int CodSegmento
        {
            get { return codSegmento; }
            set { codSegmento = value; }
        }
        public TituloEvento CodTituloEv
        {
            get { return codTituloEv; }
            set { codTituloEv = value; }
        }
        public ProdutoVenda CodProduto
        {
            get { return codProduto; }
            set { codProduto = value; }
        }
        public Instrutor CodInstrutor
        {
            get { return codInstrutor; }
            set { codInstrutor = value; }
        }
        public int QtdProd
        {
            get { return qtdProd; }
            set { qtdProd = value; }
        }
        public int QtdPartic
        {
            get { return qtdPartic; }
            set { qtdPartic = value; }
        }
        public int QtdEmpr
        {
            get { return qtdEmpr; }
            set { qtdEmpr = value; }
        }
        public int IndPartic
        {
            get { return indPartic; }
            set { indPartic = value; }
        }
        public int IndDev
        {
            get { return indDev; }
            set { indDev = value; }
        }

        public int IndDev1
        {
            get { return indDev; }
            set { indDev = value; }
        }
        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ProdEvento()
            : this(-1)
        { }
        public ProdEvento(int codSegmento)
        {
            this.codSegmento = codSegmento;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ProdEvento.ProdEventoInc";
        private const string SPUPDATE = "ProdEvento.ProdEventoAlt";
        private const string SPDELETE = "ProdEvento.ProdEventoDel";
        private const string SPSELECTID = "ProdEvento.ProdEventoSelId";
        private const string SPSELECTPAG = "ProdEvento.ProdEventoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodSegmento = "codSegmento";
        private const string PARMcodTituloEv = "codTituloEv";
        private const string PARMcodProduto = "codProduto";
        private const string PARMcodInstrutor = "codInstrutor";
        private const string PARMCURSOR = "curProdEvento";
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
                    /*0*/ new OracleParameter(PARMcodSegmento, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodProduto, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter(PARMcodInstrutor, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*4*/ new OracleParameter( "qtdProd", OracleType.Int32),
                    /*5*/ new OracleParameter( "qtdPartic", OracleType.Int32),
                    /*6*/ new OracleParameter( "qtdEmpr", OracleType.Int32),
                    /*6*/ new OracleParameter( "indPartic", OracleType.Int32),
                    /*6*/ new OracleParameter( "indDev", OracleType.Int32),
					
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
            parms[0].Value = this.codSegmento;
            parms[1].Value = this.codTituloEv;
            parms[2].Value = this.codProduto;
            parms[3].Value = this.codInstrutor;
            parms[4].Value = this.qtdProd;
            parms[5].Value = this.qtdPartic;
            parms[6].Value = this.qtdEmpr;
            parms[7].Value = this.indPartic;
            parms[8].Value = this.indDev;
			
            parms[9].Value = this.logUsuario;
            if (this.codSegmento < 0)
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
                        codSegmento = Convert.ToInt32(cmd.Parameters[PARMcodSegmento].Value);
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
                codSegmento = Convert.ToInt32(cmd.Parameters[PARMcodSegmento].Value);
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
        /// <param name="codSegmento">Código do Registro</param>
        public static void Delete(int codSegmento, int codTituloEv, int codProduto, int codInstrutor)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodSegmento, OracleType.Int32, 4),
                new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4),
                new OracleParameter(PARMcodProduto, OracleType.Int32, 4),
                new OracleParameter(PARMcodInstrutor, OracleType.Int32, 4)
            };
            parms[0].Value = codSegmento;
            parms[1].Value = codTituloEv;
            parms[2].Value = codProduto;
            parms[3].Value = codInstrutor;

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
        /// <param name="codSegmento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codSegmento, int codTituloEv, int codProduto, int codInstrutor, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodSegmento, OracleType.Int32, 4),
                new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4),
                new OracleParameter(PARMcodProduto, OracleType.Int32, 4),
                new OracleParameter(PARMcodInstrutor, OracleType.Int32, 4)
            };
                parms[0].Value = codSegmento;
                parms[1].Value = codTituloEv;
                parms[2].Value = codProduto;
                parms[3].Value = codInstrutor;

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
        /// <param name="codSegmento">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codSegmento, int codTituloEv, int codProduto, int codInstrutor)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodSegmento, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodProduto, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodInstrutor, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codSegmento;
            param[1].Value = codTituloEv;
            param[2].Value = codProduto;
            param[3].Value = codInstrutor;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codSegmento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codSegmento, int codTituloEv, int codProduto, int codInstrutor, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodSegmento, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodTituloEv, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodProduto, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodInstrutor, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codSegmento;
            param[1].Value = codTituloEv;
            param[2].Value = codProduto;
            param[3].Value = codInstrutor;
            param[4].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codSegmento">Código do Registro</param>
        /// <returns>ProdEvento</returns>
        public static ProdEvento GetDataRow(int codSegmento, int codTituloEv, int codProduto, int codInstrutor)
        {
            OracleDataReader dr = LoadDataDr(codSegmento, codTituloEv, codProduto, codInstrutor);
            ProdEvento ProdEvento = new ProdEvento();
            try
            {
                if (dr.Read())
                {
                    ProdEvento.codSegmento = Convert.ToInt32(dr["A796_cd_segmento"]);
                    ProdEvento.codTituloEv = new TituloEvento(Convert.ToInt32(dr["A008_cd_tit_ev"]));
                    ProdEvento.codProduto = new ProdutoVenda(Convert.ToInt32(dr["A232_cd_prod"]));
                    ProdEvento.codInstrutor.CodInstrutor.CodPessoa = Convert.ToInt32(dr["A125_cd_instr"]);
                    ProdEvento.qtdProd = Convert.ToInt32(dr["A187_qtde_prod"]);
                    ProdEvento.qtdPartic = Convert.ToInt32(dr["A187_qtde_partic"]);
                    ProdEvento.qtdEmpr = Convert.ToInt32(dr["A187_qtde_empr"]);
                    ProdEvento.indPartic = Convert.ToInt32(dr["A187_ind_partic"]);
                    ProdEvento.indDev = Convert.ToInt32(dr["A187_ind_dev"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProdEvento.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProdEvento = new ProdEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProdEvento;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codSegmento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ProdEvento</returns>
        public static ProdEvento GetDataRow(int codSegmento, int codTituloEv, int codProduto, int codInstrutor, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codSegmento, codTituloEv, codProduto, codInstrutor, trans);
            ProdEvento ProdEvento = new ProdEvento();
            try
            {
                if (dr.Read())
                {
                    ProdEvento.codSegmento = Convert.ToInt32(dr["A796_cd_segmento"]);
                    ProdEvento.codTituloEv = new TituloEvento(Convert.ToInt32(dr["A008_cd_tit_ev"]));
                    ProdEvento.codProduto = new ProdutoVenda(Convert.ToInt32(dr["A232_cd_prod"]));
                    ProdEvento.codInstrutor.CodInstrutor.CodPessoa = Convert.ToInt32(dr["A125_cd_instr"]);
                    ProdEvento.qtdProd = Convert.ToInt32(dr["A187_qtde_prod"]);
                    ProdEvento.qtdPartic = Convert.ToInt32(dr["A187_qtde_partic"]);
                    ProdEvento.qtdEmpr = Convert.ToInt32(dr["A187_qtde_empr"]);
                    ProdEvento.indPartic = Convert.ToInt32(dr["A187_ind_partic"]);
                    ProdEvento.indDev = Convert.ToInt32(dr["A187_ind_dev"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProdEvento.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProdEvento = new ProdEvento();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProdEvento;
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
