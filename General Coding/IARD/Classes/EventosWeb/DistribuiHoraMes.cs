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
    public class DistribuiHoraMes // T355_DISTRIB_HORA_MES
    {
        #region Atributos
        private int codMesAno;
        private Eventos codEvento;
        private Instrutor codInstrutor;
        private int qtdHoras;
        private string logUsuario;
        #endregion

        #region Propriedades
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }

        public Instrutor CodInstrutor
        {
            get { return codInstrutor; }
            set { codInstrutor = value; }
        }
        public int QtdHoras
        {
            get { return qtdHoras; }
            set { qtdHoras = value; }
        }
        public int CodMesAno
        {
            get { return codMesAno; }
            set { codMesAno = value; }
        }
        public string LogUsuario 
        {
            get { return logUsuario; }
            set { logUsuario = value; }
        }
        #endregion

        #region Construtores
        public DistribuiHoraMes()
            : this(-1)
        { }
        public DistribuiHoraMes(int codMesAno)
        {
            this.codMesAno = codMesAno; 
            this.codEvento = new Eventos(); 
            this.codInstrutor = new Instrutor();
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "DistribuiHoraMes.DistribuiHoraMesInc";
        private const string SPUPDATE = "DistribuiHoraMes.DistribuiHoraMesAlt";
        private const string SPDELETE = "DistribuiHoraMes.DistribuiHoraMesDel";
        private const string SPSELECTID = "DistribuiHoraMes.DistribuiHoraMesSelId";
        private const string SPSELECTPAG = "DistribuiHoraMes.DistribuiHoraMesSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMcodMesAno = "codMesAno";
        private const string PARMcodEvento = "codEvento";
        private const string PARMcodInstrutor = "codInstrutor";
        private const string PARMCURSOR = "curDistribuiHoraMes";
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
                    /*0*/ new OracleParameter(PARMcodMesAno, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodEvento, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*2*/ new OracleParameter(PARMcodInstrutor, OracleType.Int32, 4, ParameterDirection.Input.ToString()) ,
                    /*3*/ new OracleParameter( "qtdHoras", OracleType.Int32),
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
            parms[0].Value = this.codMesAno;
            parms[1].Value = this.codEvento.CodEvento;
            parms[2].Value = this.codInstrutor.CodInstrutor;
            parms[3].Value = this.qtdHoras;
            parms[4].Value = this.logUsuario;
            if (this.codMesAno < 0)
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
                        codMesAno = Convert.ToInt32(cmd.Parameters[PARMcodMesAno].Value);
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
                codMesAno = Convert.ToInt32(cmd.Parameters[PARMcodMesAno].Value);
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
        /// <param name="codMesAno">C�digo do Registro</param>
        public static void Delete(int codMesAno, int codEvento, int codInstrutor)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodMesAno, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodInstrutor, OracleType.Int32, 4)
            };
            parms[0].Value = codMesAno;
            parms[1].Value = codEvento;
            parms[2].Value = codInstrutor;
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
        /// <param name="codMesAno">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codMesAno, int codEvento, int codInstrutor, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodMesAno, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodEvento, OracleType.Int32, 4)  ,
                new OracleParameter(PARMcodInstrutor, OracleType.Int32, 4) 
            };
                parms[0].Value = codMesAno;
                parms[1].Value = codEvento;
                parms[2].Value = codInstrutor;
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
        /// <param name="codMesAno">C�digo do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codMesAno, int codEvento, int codInstrutor)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodMesAno, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodInstrutor, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codMesAno;
            param[1].Value = codEvento;
            param[2].Value = codInstrutor;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transa��o
        /// </summary>
        /// <param name="codMesAno">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Ap�s a utiliza��o do LoadDataDr n�o esquecer de fechar a conex�o: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codMesAno, int codEvento, int codInstrutor, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodMesAno, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodEvento, OracleType.Int32, 4), 
                    new OracleParameter(PARMcodInstrutor, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codMesAno;
            param[1].Value = codEvento;
            param[2].Value = codInstrutor;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(trans, CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        #endregion

        #region GetDataRow

        /// <summary>
        /// GetDataRow
        /// </summary>
        /// <param name="codMesAno">C�digo do Registro</param>
        /// <returns>DistribuiHoraMes</returns>
        public static DistribuiHoraMes GetDataRow(int codMesAno, int codEvento, int codInstrutor)
        {
            OracleDataReader dr = LoadDataDr(codMesAno, codEvento, codInstrutor);
            DistribuiHoraMes DistribuiHoraMes = new DistribuiHoraMes();
            try
            {
                if (dr.Read())
                {
                    DistribuiHoraMes.codMesAno = Convert.ToInt32(dr["A355_mes_ano"]);
                    DistribuiHoraMes.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    DistribuiHoraMes.codInstrutor.CodInstrutor = new Pessoa(Convert.ToInt32(dr["A125_cd_instr"]));
                    if (dr["A355_qtd_horas"] != DBNull.Value && dr["A355_qtd_horas"] != DBNull.Value && dr["A355_qtd_horas"].ToString() != "")
                    DistribuiHoraMes.qtdHoras = Convert.ToInt32(dr["A355_qtd_horas"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        DistribuiHoraMes.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                DistribuiHoraMes = new DistribuiHoraMes();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return DistribuiHoraMes;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transa��o
        /// </summary>
        /// <param name="codMesAno">C�digo do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DistribuiHoraMes</returns>
        public static DistribuiHoraMes GetDataRow(int codMesAno, int codEvento, int codInstrutor, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codMesAno, codEvento, codInstrutor, trans);
            DistribuiHoraMes DistribuiHoraMes = new DistribuiHoraMes();
            try
            {
                if (dr.Read())
                {
                    DistribuiHoraMes.codMesAno = Convert.ToInt32(dr["A355_mes_ano"]);
                    DistribuiHoraMes.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    DistribuiHoraMes.codInstrutor.CodInstrutor.CodPessoa = Convert.ToInt32(dr["A125_cd_instr"]);
                    if (dr["A355_qtd_horas"] != DBNull.Value && dr["A355_qtd_horas"] != DBNull.Value && dr["A355_qtd_horas"].ToString() != "")
                        DistribuiHoraMes.qtdHoras = Convert.ToInt32(dr["A355_qtd_horas"]);
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        DistribuiHoraMes.logUsuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                DistribuiHoraMes = new DistribuiHoraMes();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return DistribuiHoraMes;
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