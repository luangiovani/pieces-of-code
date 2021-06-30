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
    public class ProdutoVenda // T232_PRODUTO 
    {
        #region Atributos
        private int codProduto;
        private SegmentoProduto codSegmento;
        private ProdutoSebrae codProdSebrae;
        private Cliente codCliente;
        private int codSubGrupo;
        private string dscProduto;
        private string tipProduto;
        private int vlrCustoMedio;
        private string sitProduto;
        private string dscSetor;
        private string dscPeso;
        private int vlrCusto;
        private int indPedido;
        private int perComissao;
        private int indProdConsig;
        private int codProdutoAntigo;
        private int codMicrosiga;
        private Usuario codUsuario;
        private string dscImagem;
        private string logUsuario;
        #endregion

        #region Propriedades




        public SegmentoProduto CodSegmento
        {
            get { return codSegmento; }
            set { codSegmento = value; }
        }

        public int CodProduto
        {
            get { return codProduto; }
            set { codProduto = value; }
        }
        public ProdutoSebrae CodProdSebrae
        {
            get { return codProdSebrae; }
            set { codProdSebrae = value; }
        }
        public Cliente CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public int CodSubGrupo
        {
            get { return codSubGrupo; }
            set { codSubGrupo = value; }
        }
        public string DscProduto
        {
            get { return dscProduto; }
            set { dscProduto = value; }
        }
        public string TipProduto
        {
            get { return tipProduto; }
            set { tipProduto = value; }
        }
        public int VlrCustoMedio
        {
            get { return vlrCustoMedio; }
            set { vlrCustoMedio = value; }
        }
        public string SitProduto
        {
            get { return sitProduto; }
            set { sitProduto = value; }
        }
        public string DscSetor
        {
            get { return dscSetor; }
            set { dscSetor = value; }
        }
        public string DscPeso
        {
            get { return dscPeso; }
            set { dscPeso = value; }
        }
        public int VlrCusto
        {
            get { return vlrCusto; }
            set { vlrCusto = value; }
        }
        public int IndPedido
        {
            get { return indPedido; }
            set { indPedido = value; }
        }
        public int PerComissao
        {
            get { return perComissao; }
            set { perComissao = value; }
        }
        public int IndProdConsig
        {
            get { return indProdConsig; }
            set { indProdConsig = value; }
        }
        public int CodProdutoAntigo
        {
            get { return codProdutoAntigo; }
            set { codProdutoAntigo = value; }
        }
        public int CodMicrosiga
        {
            get { return codMicrosiga; }
            set { codMicrosiga = value; }
        }
        public Usuario CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        public string DscImagem
        {
            get { return dscImagem; }
            set { dscImagem = value; }
        }

        public string LogUsuario
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public ProdutoVenda()
            : this(-1)
        { }
        public ProdutoVenda(int codProduto)
        {
            this.codProduto = codProduto;
            this.codSegmento = new SegmentoProduto();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "ProdutoVenda.ProdutoVendaInc";
        private const string SPUPDATE = "ProdutoVenda.ProdutoVendaAlt";
        private const string SPDELETE = "ProdutoVenda.ProdutoVendaDel";
        private const string SPSELECTID = "ProdutoVenda.ProdutoVendaSelId";
        private const string SPSELECTPAG = "ProdutoVenda.ProdutoVendaSelPag";
        private const string SPSELECTSERVICOPAG = "ProdutoVenda.ProdutoVendaServicoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodProduto = "codProduto";
        private const string PARMcodSegmento = "codSegmento";
        private const string PARMCURSOR = "curProdutoVenda";
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
                    /*0*/ new OracleParameter(PARMcodProduto, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodSegmento, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter( "xxx", OracleType.VarChar),
                    /*3*/ new OracleParameter( "xxx", OracleType.VarChar),
                    /*4*/ new OracleParameter( "xxx", OracleType.VarChar),
                    /*5*/ new OracleParameter( "codCliente", OracleType.Int32),
                    /*5*/ new OracleParameter( "codProdSebrae", OracleType.Int32),
                    /*5*/ new OracleParameter( "codSubGrupo", OracleType.Int32),
                    /*5*/ new OracleParameter( "dscProduto", OracleType.VarChar),
                    /*5*/ new OracleParameter( "tipProduto", OracleType.Int32),
                    /*5*/ new OracleParameter( "vlrCustoMedio", OracleType.Int32),
                    /*5*/ new OracleParameter( "sitProduto", OracleType.Int32),
                    /*5*/ new OracleParameter( "dscSetor", OracleType.VarChar),
                    /*5*/ new OracleParameter( "dscPeso", OracleType.VarChar),
                    /*5*/ new OracleParameter( "vlrCusto", OracleType.Int32),
                    /*5*/ new OracleParameter( "indPedido", OracleType.Int32),
                    /*5*/ new OracleParameter( "perComissao", OracleType.Int32),
                    /*5*/ new OracleParameter( "indProdConsig", OracleType.Int32),
                    /*5*/ new OracleParameter( "codProdutoAntigo", OracleType.VarChar),
                    /*5*/ new OracleParameter( "codMicrosiga", OracleType.VarChar),
                    /*5*/ new OracleParameter( "codUsuario", OracleType.Int32),
                    /*5*/ new OracleParameter( "dscImagem ", OracleType.VarChar),
					
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
            parms[0].Value = this.codProduto;
            parms[1].Value = this.codSegmento;
            parms[2].Value = this.codCliente;
            parms[3].Value = this.codProdSebrae;
            parms[4].Value = this.codSubGrupo;
            parms[5].Value = this.dscProduto;
            parms[6].Value = this.tipProduto;
            parms[7].Value = this.vlrCustoMedio;
            parms[8].Value = this.sitProduto;
            parms[9].Value = this.dscSetor;
            parms[10].Value = this.dscPeso;
            parms[11].Value = this.vlrCusto;
            parms[12].Value = this.indPedido;
            parms[13].Value = this.perComissao;
            parms[14].Value = this.indProdConsig;
            parms[15].Value = this.codProdutoAntigo;
            parms[16].Value = this.codMicrosiga;
            parms[17].Value = this.codUsuario;
            parms[18].Value = this.dscImagem;
			
            parms[19].Value = this.logUsuario;
            if (this.codProduto < 0)
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
                        codProduto = Convert.ToInt32(cmd.Parameters[PARMcodProduto].Value);
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
                codProduto = Convert.ToInt32(cmd.Parameters[PARMcodProduto].Value);
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
        /// <param name="codProduto">Código do Registro</param>
        public static void Delete(int codProduto, int codSegmento)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodProduto, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodSegmento, OracleType.Int32, 4)
            };
            parms[0].Value = codProduto;
            parms[1].Value = codSegmento;
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
        /// <param name="codProduto">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codProduto, int codSegmento, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodProduto, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodSegmento, OracleType.Int32, 4)
            };
                parms[0].Value = codProduto;
                parms[1].Value = codSegmento;
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
        /// <param name="codProduto">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codProduto, int codSegmento)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodProduto, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodSegmento, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codProduto;
            param[1].Value = codSegmento;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codProduto">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codProduto, int codSegmento, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodProduto, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodSegmento, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codProduto;
            param[1].Value = codSegmento;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codProduto">Código do Registro</param>
        /// <returns>ProdutoVenda</returns>
        public static ProdutoVenda GetDataRow(int codProduto, int codSegmento)
        {
            OracleDataReader dr = LoadDataDr(codProduto, codSegmento);
            ProdutoVenda ProdutoVenda = new ProdutoVenda();
            try
            {
                if (dr.Read())
                {
                    ProdutoVenda.codProduto = Convert.ToInt32(dr["A232_cd_prod"]);
                    ProdutoVenda.codSegmento = new SegmentoProduto(Convert.ToInt32(dr["A796_cd_segmento"]));
                    ProdutoVenda.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    ProdutoVenda.codProdSebrae = new ProdutoSebrae(Convert.ToString(dr["A042_cd_prod_seb"]));
                    ProdutoVenda.codSubGrupo = Convert.ToInt32(dr["A231_cd_sub_grupo"]);
                    ProdutoVenda.dscProduto = Convert.ToString(dr["A232_desc_prod"]);
                    ProdutoVenda.tipProduto = Convert.ToString(dr["A232_tp_produto"]);
                    ProdutoVenda.vlrCustoMedio = Convert.ToInt32(dr["A232_vlr_custo_medio"]);
                    ProdutoVenda.sitProduto = Convert.ToString(dr["A232_sit_produto"]);
                    ProdutoVenda.dscSetor = Convert.ToString(dr["A232_setor"]);
                    ProdutoVenda.dscPeso = Convert.ToString(dr["A232_peso"]);
                    ProdutoVenda.vlrCusto = Convert.ToInt32(dr["A232_vlr_custo"]);
                    ProdutoVenda.indPedido = Convert.ToInt32(dr["A232_ind_pedido"]);
                    ProdutoVenda.perComissao = Convert.ToInt32(dr["A232_perc_comissao"]);
                    ProdutoVenda.indProdConsig = Convert.ToInt32(dr["A232_ind_prod_consig"]);
                    ProdutoVenda.codProdutoAntigo = Convert.ToInt32(dr["A232_cd_prod_antigo"]);
                    ProdutoVenda.codMicrosiga = Convert.ToInt32(dr["A232_cd_microsiga"]);
                    ProdutoVenda.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    ProdutoVenda.dscImagem  = Convert.ToString(dr["A232_imagem"]);

                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                    ProdutoVenda.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProdutoVenda = new ProdutoVenda();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProdutoVenda;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codProduto">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>ProdutoVenda</returns>
        public static ProdutoVenda GetDataRow(int codProduto, int codSegmento, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codProduto, codSegmento, trans);
            ProdutoVenda ProdutoVenda = new ProdutoVenda();
            try
            {
                if (dr.Read())
                {
                    ProdutoVenda.codProduto = Convert.ToInt32(dr["A232_cd_prod"]);
                    ProdutoVenda.codSegmento = new SegmentoProduto(Convert.ToInt32(dr["A796_cd_segmento"]));
                    ProdutoVenda.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    ProdutoVenda.codProdSebrae = new ProdutoSebrae(Convert.ToString(dr["A042_cd_prod_seb"]));
                    ProdutoVenda.codSubGrupo = Convert.ToInt32(dr["A231_cd_sub_grupo"]);
                    ProdutoVenda.dscProduto = Convert.ToString(dr["A232_desc_prod"]);
                    ProdutoVenda.tipProduto = Convert.ToString(dr["A232_tp_produto"]);
                    ProdutoVenda.vlrCustoMedio = Convert.ToInt32(dr["A232_vlr_custo_medio"]);
                    ProdutoVenda.sitProduto = Convert.ToString(dr["A232_sit_produto"]);
                    ProdutoVenda.dscSetor = Convert.ToString(dr["A232_setor"]);
                    ProdutoVenda.dscPeso = Convert.ToString(dr["A232_peso"]);
                    ProdutoVenda.vlrCusto = Convert.ToInt32(dr["A232_vlr_custo"]);
                    ProdutoVenda.indPedido = Convert.ToInt32(dr["A232_ind_pedido"]);
                    ProdutoVenda.perComissao = Convert.ToInt32(dr["A232_perc_comissao"]);
                    ProdutoVenda.indProdConsig = Convert.ToInt32(dr["A232_ind_prod_consig"]);
                    ProdutoVenda.codProdutoAntigo = Convert.ToInt32(dr["A232_cd_prod_antigo"]);
                    ProdutoVenda.codMicrosiga = Convert.ToInt32(dr["A232_cd_microsiga"]);
                    ProdutoVenda.codUsuario = new Usuario(Convert.ToInt32(dr["A052_cd_usuario"]));
                    ProdutoVenda.dscImagem = Convert.ToString(dr["A232_imagem"]);

                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        ProdutoVenda.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                ProdutoVenda = new ProdutoVenda();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return ProdutoVenda;
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

        #region LoadDataPaginacaoServico


        /// <summary>
        /// LoadDataPaginacao
        /// </summary>
        /// <param name="Where">Cláusula where utilizada na consulta</param>
        /// <param name="PaginaCorrente">Número da página que deseja selecionar</param>
        /// <param name="TamanhoPagina">Quantidade de registros em cada página</param>
        /// <param name="ExpressaoOrdenacao">Expressão de ordenação</param>
        /// <returns>Instância do objeto Paginação, contendo um DataReader e o total de registros</returns>
        /// 
        public static Paginacao LoadDataPaginacaoServico(string Where, int PaginaCorrente, int TamanhoPagina, string ExpressaoOrdenacao)
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


            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTSERVICOPAG, parms);


            paginacao.DataReader = dr;
            //  traz o total de registros de retorno direto dos parametros do ExecuteReader---
            paginacao.TotalRegistros = Convert.ToInt32(parms[5].Value);
            return paginacao;
        }
        #endregion


        #endregion

    }
}
