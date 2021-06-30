using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 03/07/2007 
//-- Autor :  Honorato

namespace Classes
{
    public class OperadoraCartao  // T297_operadora_cartao
    {
        #region Atributos
        private int codOperadoraCartao;
        private string nomOperad;
        private string numDddOperad;
        private int numFoneOperad;
        private int indDispWeb;
        private decimal codTaxaOperadora;
        private int indCredito;
        private int indDebito;
        #endregion

        #region Propriedades
        public int IndDebito
        {
          get { return indDebito; }
          set { indDebito = value; }
        }
        public int IndCredito
        {
          get { return indCredito; }
          set { indCredito = value; }
        }
        public decimal CodTaxaOperadora
        {
          get { return codTaxaOperadora; }
          set { codTaxaOperadora = value; }
        }
        public int IndDispWeb
        {
          get { return indDispWeb; }
          set { indDispWeb = value; }
        }
        public int NumFoneOperad
        {
          get { return numFoneOperad; }
          set { numFoneOperad = value; }
        }        
        public string NumDddOperad
        {
          get { return numDddOperad; }
          set { numDddOperad = value; }
        }
        public string NomOperad
        {
          get { return nomOperad; }
          set { nomOperad = value; }
        }
        public int CodOperadoraCartao
        {
            get { return codOperadoraCartao; }
            set { codOperadoraCartao = value; }
        }        
        #endregion

        #region Construtores
        public OperadoraCartao()
            : this(-1)
        { }

        public OperadoraCartao(int codOperadoraCartao)
        {
            this.codOperadoraCartao = codOperadoraCartao;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "OperadoraCartao.OperadoraCartaoInc";
        private const string SPUPDATE = "OperadoraCartao.OperadoraCartaoAlt";
        private const string SPDELETE = "OperadoraCartao.OperadoraCartaoDel";
        private const string SPSELECTID = "OperadoraCartao.OperadoraCartaoSelId";
        private const string SPSELECTPAG = "OperadoraCartao.OperadoraCartaoSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codOperadoraCartao";
        private const string PARMCURSOR = "curOperadoraCartao";
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
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 2, ParameterDirection.InputOutput.ToString()) ,
                /*1*/ new OracleParameter( "nomOperad", OracleType.VarChar),		
                /*1*/ new OracleParameter( "numDddOperad", OracleType.VarChar),		
                /*1*/ new OracleParameter( "numFoneOperad", OracleType.Int32),		
                /*1*/ new OracleParameter( "indDispWeb", OracleType.Int32),		
                /*1*/ new OracleParameter( "codTaxaOperadora", OracleType.Float),		
                /*1*/ new OracleParameter( "indCredito", OracleType.Int32),		
                /*1*/ new OracleParameter( "indDebito", OracleType.Int32)
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
            parms[0].Value = this.codOperadoraCartao;
            parms[1].Value = this.nomOperad;
            parms[2].Value = this.numDddOperad;
            parms[3].Value = this.numFoneOperad;
            parms[4].Value = this.indDispWeb;
            parms[5].Value = this.codTaxaOperadora;
            parms[6].Value = this.indCredito;
            parms[7].Value = this.indDebito;
            if (this.codOperadoraCartao < 0)
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
        /// Insert com tratamento de transa��o
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
                        //Obtendo a chave de identifica��o do registro inserido.
                        codOperadoraCartao = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        /// Insert sem tratamento de transa��o
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        public void Insert(OracleTransaction trans)
        {
            OracleParameter[] parms = GetParameters();
            SetParameters(parms);
            try
            {
                OracleCommand cmd = DataBase.ExecuteNonQueryCmd(trans, CommandType.StoredProcedure, SPINSERT, parms);
                //Obtendo a chave de identifica��o do registro inserido.
                codOperadoraCartao = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
        /// Update com tratamento de transa��o
        /// </summary>
        public void Update()
        {
            // --------------------------------------------------------  
            // Obtendo e setando os par�metros 
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
        /// Update sem tratamento de transa��o
        /// </summary>
        /// <param name="trans">OracleTransaction</param>
        public void Update(OracleTransaction trans)
        {
            // -------------------------------------------------------- 
            // Obtendo e setando os par�metros 
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
        /// Delete com tratamento de transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
            parms[0].Value = codigo;
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
        /// Delete sem tratamento de transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2) };
                parms[0].Value = codigo;
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
        /// <param name="codigo">C�digo do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                new OracleParameter(PARMCODIGO, OracleType.Int32, 2), 
                new OracleParameter(PARMCURSOR, OracleType.Cursor)
            };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 2),
                                                              new OracleParameter(PARMCURSOR, OracleType.Cursor)
};
            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <returns>OperadoraCartao</returns>
        public static OperadoraCartao GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            OperadoraCartao OperadoraCartao = new OperadoraCartao();
            try
            {
                if (dr.Read())
                {
                    OperadoraCartao.codOperadoraCartao = Convert.ToInt32(dr["A297_cd_operad"]);
                    OperadoraCartao.nomOperad = Convert.ToString(dr["A297_nm_operad"]);
                    OperadoraCartao.numDddOperad = Convert.ToString(dr["A297_ddd_operad"]);
                    OperadoraCartao.numFoneOperad = Convert.ToInt32(dr["A297_fone_operad"]);
                    OperadoraCartao.indDispWeb = Convert.ToInt32(dr["A297_disponivel_web"]);
                    OperadoraCartao.codTaxaOperadora = Convert.ToInt32(dr["A297_taxa_operadora"]);
                    OperadoraCartao.indCredito = Convert.ToInt32(dr["A297_ind_credito"]);
                    OperadoraCartao.indDebito = Convert.ToInt32(dr["A297_ind_debito"]);                
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                OperadoraCartao = new OperadoraCartao();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return OperadoraCartao;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transa��o
        /// </summary>
        /// <param name="codigo">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>OperadoraCartao</returns>
        public static OperadoraCartao GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            OperadoraCartao OperadoraCartao = new OperadoraCartao();
            try
            {
                if (dr.Read())
                {
                    OperadoraCartao.codOperadoraCartao = Convert.ToInt32(dr["A297_cd_operad"]);
                    OperadoraCartao.nomOperad = Convert.ToString(dr["A297_nm_operad"]);
                    OperadoraCartao.numDddOperad = Convert.ToString(dr["A297_ddd_operad"]);
                    OperadoraCartao.numFoneOperad = Convert.ToInt32(dr["A297_fone_operad"]);
                    OperadoraCartao.indDispWeb = Convert.ToInt32(dr["A297_disponivel_web"]);
                    OperadoraCartao.codTaxaOperadora = Convert.ToInt32(dr["A297_taxa_operadora"]);
                    OperadoraCartao.indCredito = Convert.ToInt32(dr["A297_ind_credito"]);
                    OperadoraCartao.indDebito = Convert.ToInt32(dr["A297_ind_debito"]);                
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                OperadoraCartao = new OperadoraCartao();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return OperadoraCartao;
        }
        #endregion

        #region LoadDataPaginacao

        /// <summary>
        /// LoadDataPaginacao
        /// </summary>
        /// <param name="Where">Cl�usula where utilizada na consulta</param>
        /// <param name="PaginaCorrente">N�mero da p�gina que deseja selecionar</param>
        /// <param name="TamanhoPagina">Quantidade de registros em cada p�gina</param>
        /// <param name="ExpressaoOrdenacao">Express�o de ordena��o</param>
        /// <returns>Inst�ncia do objeto Pagina��o, contendo um DataReader e o total de registros</returns>
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
