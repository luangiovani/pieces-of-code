using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;

using System.Data;

namespace Classes
{
   public class EventosOnline/// t942_eventos_online
    {
        #region Atributos
        private Eventos codEvento;
        private string imgLogo;
        private string imgBanner;
        private string dscLink;
        private int intervaloDe;
        private int a188_cd_quest;
        private int intervaloAte;
        private string orientacao;
        private DateTime dtaIncAlt;
        private string  usuIncAlt;
     
        #endregion
        
        #region Propriedades
        public Eventos CodEvento
        {
            get { return codEvento; }
            set { codEvento = value; }
        }
        public string ImgLogo
        {
            get { return imgLogo; }
            set { imgLogo = value; }
        }
        public string ImgBanner
        {
            get { return imgBanner; }
            set { imgBanner = value; }
        }
        public string DscLink
        {
            get { return dscLink; }
            set { dscLink = value; }
        }
        public int IntervaloDe
        {
            get { return intervaloDe; }
            set { intervaloDe = value; }
        }
       public int A188_cd_quest
       {
           get { return a188_cd_quest; }
           set { a188_cd_quest = value; }
       }

       public int IntervaloAte
        {
            get { return intervaloAte; }
            set { intervaloAte = value; }
        }
        public string Orientacao
        {
            get { return orientacao; }
            set { orientacao = value; }
        }
        public DateTime DtaIncAlt
        {
            get { return dtaIncAlt; }
            set { dtaIncAlt = value; }
        }
        public string UsuIncAlt
        {
            get { return usuIncAlt; }
            set { usuIncAlt = value; }
        }
        #endregion

        #region Construtores
        public EventosOnline()
            : this(-1)
        { }
       public EventosOnline(int codEvento)
        {
            this.codEvento = new Eventos();
        }
        #endregion

        #region StoredProcedures
           private const string SPINSERT = "EventosOnline.EventosOnlineInc";
           private const string SPUPDATE = "EventosOnline.EventosOnlineAlt";
           private const string SPSELECTID = "EventosOnline.EventosOnlineSelId";
           private const string SPSELECTPAG = "EventosOnline.EventosOnlineSelPag";
           private const string SPDELETE = "EventosOnline.EventosOnlineDel";
           private const string SPINSERTQUEST = "EventosOnline.QuestionarioInc";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codEvento";
        private const string PARMCURSOR = "curEventosOnline";
        #endregion

        #region Metodos

        #region GetParameters
        public static OracleParameter[] GetParameters()
        {
            OracleParameter[] parms;
                parms = new OracleParameter[]{ 
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter( "imgLogo", OracleType.VarChar),
                    /*2*/ new OracleParameter( "imgBanner", OracleType.VarChar),
                    /*3*/ new OracleParameter( "dscLink", OracleType.VarChar),
                    /*4*/ new OracleParameter( "intervaloDe", OracleType.Int32),
                    /*5*/ new OracleParameter( "intervaloAte", OracleType.Int32),
                    /*6*/ new OracleParameter( "orientacao", OracleType.VarChar),
                    /*7*/ new OracleParameter( "usuIncAlt", OracleType.VarChar)
                };

                // Criando cache dos parameters 
                DataBase.CacheParameters(SPINSERT, parms);
            //}
            return (parms);
        }

        #endregion

        #region SetParameters
        public void SetParameters(OracleParameter[] parms)
        {
            parms[0].Value = this.codEvento.CodEvento;
            parms[1].Value = this.imgLogo;
            parms[2].Value = this.imgBanner;
            parms[3].Value = this.dscLink;
            parms[4].Value = this.intervaloDe;
            parms[5].Value = this.intervaloAte;
            parms[6].Value = this.orientacao;
            parms[7].Value = this.usuIncAlt;


            //if (this.codEvento.CodEvento < 0)
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
                        codEvento.CodEvento = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codEvento.CodEvento = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void InsertQuestionario(int codQuest,int codEvento)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter("codQuest", OracleType.Int32, 4) ,
                new OracleParameter("codEvento", OracleType.Int32, 4)
            };
            parms[0].Value = codQuest;
            parms[1].Value = codEvento;

            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPINSERTQUEST, parms);
                        trans.Commit();
                    }
                    catch (OracleException ex)
                    {
                        trans.Rollback();
                        throw (ex);
                    }
                }
            }
        } // end 

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
        public static void Delete(int codigo)
        {
            OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
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

     
        /// Delete sem tratamento de transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        public static void Delete(int codigo, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4) };
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
        /// <param name="codigo">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codigo)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMCODIGO, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codigo;
            param[1].Direction = ParameterDirection.Output;

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
        public static OracleDataReader LoadDataDr(int codigo, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { new OracleParameter(PARMCODIGO, OracleType.Int32, 4),
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
        /// <param name="codigo">Código do Registro</param>
        /// <returns>Eventos</returns>
        public static EventosOnline GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            EventosOnline evOn = new EventosOnline();
            try
            {
                if (dr.Read())
                {
                    evOn.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A942_IMG_LOGO"].ToString() != "")
                        evOn.imgLogo = Convert.ToString(dr["A942_IMG_LOGO"]);
                    if (dr["A942_IMG_BANNER"].ToString() != "")
                        evOn.imgBanner = Convert.ToString(dr["A942_IMG_BANNER"]);
                    if (dr["A942_DSC_LINK"].ToString() != "")
                        evOn.dscLink = Convert.ToString(dr["A942_DSC_LINK"]);
                    evOn.intervaloDe =Convert.ToInt32(dr["A942_INTERVALODE"]);
                    evOn.intervaloAte = Convert.ToInt32(dr["A942_INTERVALOATE"]);
                    evOn.orientacao = Convert.ToString(dr["A942_orientacao"]);
                    evOn.usuIncAlt = Convert.ToString(dr["USU_INC_ALT"]);
                    if (dr["A188_cd_quest"].ToString() != "")
                        evOn.A188_cd_quest = Convert.ToInt32(dr["A188_cd_quest"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                evOn = new EventosOnline();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return evOn;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Eventos</returns>
        public static EventosOnline GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            EventosOnline evOn = new EventosOnline();
            try
            {
                if (dr.Read())
                {
                    evOn.codEvento = new Eventos(Convert.ToInt32(dr["A022_cd_ev"]));
                    if (dr["A942_IMG_LOGO"].ToString() != "")
                        evOn.imgLogo = Convert.ToString(dr["A942_IMG_LOGO"]);
                    if (dr["A942_IMG_BANNER"].ToString() != "")
                        evOn.imgBanner = Convert.ToString(dr["A942_IMG_BANNER"]);
                    if (dr["A942_DSC_LINK"].ToString() != "")
                        evOn.dscLink = Convert.ToString(dr["A942_DSC_LINK"]);
                    evOn.intervaloDe = Convert.ToInt32(dr["A942_INTERVALODE"]);
                    evOn.intervaloAte = Convert.ToInt32(dr["A942_INTERVALOATE"]);
                    evOn.usuIncAlt = Convert.ToString(dr["USU_INC_ALT"]);
                    if (dr["A188_cd_quest"].ToString() != "")
                        evOn.A188_cd_quest = Convert.ToInt32(dr["A188_cd_quest"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                evOn = new EventosOnline();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return evOn;
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
