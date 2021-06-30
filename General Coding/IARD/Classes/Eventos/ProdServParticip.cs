using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Rodada Produtos e Serviços / Particip
//-- Data : 24/10/2011
//-- Autor :  Denis Douglas Cavalheiro
namespace Classes
{
    public class ProdServParticip
    {
        #region Atributos
        private Rodada codRodada;
        private Cliente codCliente;
        private RodadaProdServ codProdServ;
        private int? indComprar;
        private int? indVender;
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
        public RodadaProdServ CodProdServ
        {
            get { return codProdServ; }
            set { codProdServ = value; }
        }
        public int? IndComprar
        {
            get { return indComprar; }
            set { indComprar = value; }
        }
        public int? IndVender
        {
            get { return indVender; }
            set { indVender = value; }
        }
        #endregion

        #region Construtores
        public ProdServParticip()
            : this(-1)
        { }
        public ProdServParticip(int codRodada)
        {
            this.codRodada = new Rodada(codRodada);
            this.codCliente = new Cliente();
            this.codProdServ = new RodadaProdServ(); 
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "PROD_SERV_PARTICIP.PROD_SERV_PARTICIPInc";
        private const string SPUPDATE = "PROD_SERV_PARTICIP.PROD_SERV_PARTICIPAlt";
        private const string SPDELETE = "PROD_SERV_PARTICIP.PROD_SERV_PARTICIPDel";
        private const string SPSELECTID = "PROD_SERV_PARTICIP.PROD_SERV_PARTICIPSelId";
        private const string SPSELECTPAG = "PROD_SERV_PARTICIP.PROD_SERV_PARTICIPSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodRodada = "codRodada";
        private const string PARMcodCliente = "codCliente";
        private const string PARMcodProdServ = "codProdServ";
        private const string PARMindComprar = "indComprar";
        private const string PARMindVender = "indVender";
        private const string PARMCURSOR = "curPROD_SERV_PARTICIP";
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
                    /*2*/ new OracleParameter(PARMcodProdServ, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( PARMindComprar, OracleType.Int32),
                    /*4*/ new OracleParameter( PARMindVender, OracleType.Int32)
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
            parms[2].Value = this.codProdServ.CodProdServ;
            parms[3].Value = this.indComprar;
            parms[4].Value = this.indVender;
            //if (this.codCliente < 0)
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
                        //codCliente = Convert.ToInt32(cmd.Parameters[PARMcodCliente].Value);
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
                //codCliente = Convert.ToInt32(cmd.Parameters[PARMcodCliente].Value);
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
        /// <param name="codCliente">Código do Registro</param>
        public static void Delete(int codRodada, int codCliente, int codProdServ)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodRodada, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodProdServ, OracleType.Int32, 4)
            };
            parms[0].Value = codRodada;
            parms[1].Value = codCliente;
            parms[2].Value = codProdServ;
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
        /// <param name="codCliente">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codRodada, int codCliente, int codProdServ, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodRodada, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodCliente, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodProdServ, OracleType.Int32, 4) 
            };
                parms[0].Value = codRodada;
                parms[1].Value = codCliente;
                parms[2].Value = codProdServ;
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
        /// <param name="codCliente">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codRodada, int codCliente, int codProdServ)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodProdServ, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRodada;
            param[1].Value = codCliente;
            param[2].Value = codProdServ;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codCliente">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codRodada, int codCliente, int codProdServ, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodCliente, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodProdServ, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRodada;
            param[1].Value = codCliente;
            param[2].Value = codProdServ;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        public static ProdServParticip GetDataRow(int codRodada, int codCliente, int codProdServ)
        {
            OracleDataReader dr = LoadDataDr(codRodada, codCliente, codProdServ);
            ProdServParticip prodServParticip = new ProdServParticip();
            try
            {
                if (dr.Read())
                {
                    prodServParticip.codRodada = new Rodada(Convert.ToInt32(dr["A930_cd_rodada"]));
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        prodServParticip.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    if (dr["A934_cd_prod_serv"] != DBNull.Value && dr["A934_cd_prod_serv"] != DBNull.Value && dr["A934_cd_prod_serv"].ToString() != "")
                        prodServParticip.codProdServ = new RodadaProdServ(Convert.ToInt32(dr["A934_cd_prod_serv"]));
                    if (dr["A932_ind_comprar"] != DBNull.Value)
                        prodServParticip.indComprar = Convert.ToInt32(dr["A932_ind_comprar"]);
                    if (dr["A932_ind_vender"] != DBNull.Value)
                        prodServParticip.indVender = Convert.ToInt32(dr["A932_ind_vender"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                prodServParticip = new ProdServParticip();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return prodServParticip;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        public static ProdServParticip GetDataRow(int codRodada, int codCliente, int codProdServ, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codRodada, codCliente, codProdServ, trans);
            ProdServParticip prodServParticip = new ProdServParticip();
            try
            {
                if (dr.Read())
                {
                    prodServParticip.codRodada = new Rodada(Convert.ToInt32(dr["A930_cd_rodada"]));
                    if (dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"] != DBNull.Value && dr["A012_cd_cli"].ToString() != "")
                        prodServParticip.codCliente = new Cliente(Convert.ToInt32(dr["A012_cd_cli"]));
                    if (dr["A934_cd_prod_serv"] != DBNull.Value && dr["A934_cd_prod_serv"] != DBNull.Value && dr["A934_cd_prod_serv"].ToString() != "")
                        prodServParticip.codProdServ = new RodadaProdServ(Convert.ToInt32(dr["A934_cd_prod_serv"]));
                    if (dr["A932_ind_comprar"] != DBNull.Value)
                        prodServParticip.indComprar = Convert.ToInt32(dr["A932_ind_comprar"]);
                    if (dr["A932_ind_vender"] != DBNull.Value)
                        prodServParticip.indVender = Convert.ToInt32(dr["A932_ind_vender"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                prodServParticip = new ProdServParticip();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return prodServParticip;
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
