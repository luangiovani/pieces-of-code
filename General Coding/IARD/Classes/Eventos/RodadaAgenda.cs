using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

//-- Classe Rodada Agenda
//-- Data : 24/10/2011
//-- Autor :  Denis Douglas Cavalheiro
namespace Classes
{
    public class RodadaAgenda
    {
        #region Atributos
        private int seqAgenda;
        private Rodada codRodada;
        private Cliente codCliAncora;
        private Cliente codCliEmpresa;
        private int numContatoEmpresa;
        private int numContatoAncora;
        private DateTime dtHrAgenda;
        #endregion

        #region Propriedades
        public int SeqAgenda
        {
            get { return seqAgenda; }
            set { seqAgenda = value; }
        }
        public Rodada CodRodada
        {
            get { return codRodada; }
            set { codRodada = value; }
        }
        public Cliente CodCliAncora
        {
            get { return codCliAncora; }
            set { codCliAncora = value; }
        }
        public Cliente CodCliEmpresa
        {
            get { return codCliEmpresa; }
            set { codCliEmpresa = value; }
        }
        public DateTime DtHrAgenda
        {
            get { return dtHrAgenda; }
            set { dtHrAgenda = value; }
        }

        public int NumContatoEmpresa
        {
            get { return numContatoEmpresa; }
            set { numContatoEmpresa = value; }
        }

        public int NumContatoAncora
        {
            get { return numContatoAncora; }
            set { numContatoAncora = value; }
        }
        #endregion

        #region Construtores
        public RodadaAgenda()
            : this(-1)
        { }
        public RodadaAgenda(int seqAgenda)
        {
            this.codRodada = new Rodada();
            this.seqAgenda = seqAgenda;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "Rodada_Agenda.Rodada_AgendaInc";
        private const string SPUPDATE = "Rodada_Agenda.Rodada_AgendaAlt";
        private const string SPDELETE = "Rodada_Agenda.Rodada_AgendaDel";
        private const string SPSELECTID = "Rodada_Agenda.Rodada_AgendaSelId";
        private const string SPSELECTPAG = "Rodada_Agenda.Rodada_AgendaSelPag";
        private const string SPSELECTEV = "Rodada_Agenda.Rodada_AgendaSelEv";
        private const string SPSELECTRODADA = "Rodada_Agenda.Rodada_AgendaSelRodada";
        private const string SPSELECTEMPRESAHORA = "Rodada_Agenda.Rodada_AgendaSelEmpresaHora";
        private const string SPDELETEEMPRESAHORA = "Rodada_Agenda.Rodada_AgendaDelEmpresaHora";
        #endregion

        #region Parametros Oracle
        private const string PARMseqRodadaAgenda    = "seqAgenda";
        private const string PARMcodRodada          = "codRodada";
        private const string PARMcodCliAncora       = "codCliAncora";
        private const string PARMcodCliEmpresa      = "codCliEmpresa";
        private const string PARMdtHrAgenda         = "dtHrAgenda";
        private const string PARMnumContatoEmpresa  = "numContatoEmpresa";
        private const string PARMnumContatoAncora   = "numContatoAncora"; 
        private const string PARMCURSOR = "curRODADA_AGENDA";
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
                    /*0*/ new OracleParameter(PARMseqRodadaAgenda, OracleType.Int32, 4, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter(PARMcodRodada, OracleType.Int32) ,
                    /*2*/ new OracleParameter(PARMcodCliAncora, OracleType.Int32) ,
                    /*3*/ new OracleParameter(PARMcodCliEmpresa, OracleType.Int32), 
                    /*4*/ new OracleParameter(PARMdtHrAgenda, OracleType.DateTime),
                    /*5*/ new OracleParameter(PARMnumContatoEmpresa, OracleType.Int32),
                    /*6*/ new OracleParameter(PARMnumContatoAncora, OracleType.Int32)
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
            parms[0].Value = this.seqAgenda;
            parms[1].Value = this.codRodada.CodRodada;
            parms[2].Value = this.codCliAncora.CodCLIENTE;
            parms[3].Value = this.codCliEmpresa.CodCLIENTE;
            parms[4].Value = this.dtHrAgenda;
            parms[5].Value = this.numContatoEmpresa;
            parms[6].Value = this.numContatoAncora;

            if (this.seqAgenda < 0)
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
                        seqAgenda = Convert.ToInt32(cmd.Parameters[PARMseqRodadaAgenda].Value);
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
                seqAgenda = Convert.ToInt32(cmd.Parameters[PARMseqRodadaAgenda].Value);
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
        public static void Delete(int codRodada, int seqRodadaAgenda)
        {
            OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodRodada, OracleType.Int32, 4),
                new OracleParameter(PARMseqRodadaAgenda, OracleType.Int32, 4)
            };
            parms[0].Value = codRodada;
            parms[1].Value = seqRodadaAgenda;
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
        public static void Delete(int codRodada, int seqRodadaAgenda, OracleTransaction trans)
        {
            try
            {
                OracleParameter[] parms = new OracleParameter[] { 
                new OracleParameter(PARMcodRodada, OracleType.Int32, 4)  ,
                new OracleParameter(PARMseqRodadaAgenda, OracleType.Int32, 4)
            };
                parms[0].Value = codRodada;
                parms[1].Value = 0;
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
        public static OracleDataReader LoadDataDr(int codRodada, int seqRodadaAgenda)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMseqRodadaAgenda, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRodada;
            param[1].Value = seqRodadaAgenda;
            param[2].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTID, param);
            return dr;
        }

        /// <summary>
        /// LoadDataDr para ser utilizando dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDr(int codRodada, int seqRodadaAgenda, OracleTransaction trans)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMseqRodadaAgenda, OracleType.Int32, 4),
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };
            
            param[0].Value = codRodada;
            param[1].Value = seqRodadaAgenda;
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
        public static RodadaAgenda GetDataRow(int codRodada, int seqRodadaAgenda)
        {
            OracleDataReader dr = LoadDataDr(codRodada, seqRodadaAgenda);
            RodadaAgenda rodadaAgenda = new RodadaAgenda();
            try
            {
                if (dr.Read())
                {
                    rodadaAgenda.codRodada = new Rodada(Convert.ToInt32(dr["A930_cd_rodada"]));
                    if (dr["A933_seq_agenda"] != DBNull.Value && dr["A933_seq_agenda"] != DBNull.Value && dr["A933_seq_agenda"].ToString() != "")
                        rodadaAgenda.seqAgenda = Convert.ToInt32(dr["A933_seq_agenda"]);
                    rodadaAgenda.codCliAncora = new Cliente(Convert.ToInt32(dr["A012_cd_cli_ancora"]));
                    rodadaAgenda.codCliEmpresa = new Cliente(Convert.ToInt32(dr["A012_cd_cli_empresa"]));
                    rodadaAgenda.dtHrAgenda = Convert.ToDateTime(dr["A933_dta_hr_agenda"]);
                    rodadaAgenda.numContatoEmpresa = Convert.ToInt32(dr["A014_num_cont_empresa"]);
                    rodadaAgenda.numContatoAncora = Convert.ToInt32(dr["A014_num_cont_ancora"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                rodadaAgenda = new RodadaAgenda();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return rodadaAgenda;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Sessao</returns>
        public static RodadaAgenda GetDataRow(int codRodada, int seqRodadaAgenda, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codRodada, seqRodadaAgenda, trans);
            RodadaAgenda rodadaAgenda = new RodadaAgenda();
            try
            {
                if (dr.Read())
                {
                    rodadaAgenda.codRodada = new Rodada(Convert.ToInt32(dr["A930_cd_rodada"]));
                    if (dr["A933_seq_agenda"] != DBNull.Value && dr["A933_seq_agenda"] != DBNull.Value && dr["A933_seq_agenda"].ToString() != "")
                        rodadaAgenda.seqAgenda = Convert.ToInt32(dr["A933_seq_agenda"]);
                    rodadaAgenda.codCliAncora = new Cliente(Convert.ToInt32(dr["A012_cd_cli_ancora"]));
                    rodadaAgenda.codCliEmpresa = new Cliente(Convert.ToInt32(dr["A012_cd_cli_empresa"]));
                    rodadaAgenda.dtHrAgenda = Convert.ToDateTime(dr["A933_dt_hr_agenda"]);
                    rodadaAgenda.numContatoEmpresa = Convert.ToInt32(dr["A014_num_cont_empresa"]);
                    rodadaAgenda.numContatoAncora = Convert.ToInt32(dr["A014_num_cont_ancora"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                rodadaAgenda = new RodadaAgenda();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return rodadaAgenda;
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

        #region LoadDataDrRodada

        /// <summary>
        /// LoadDataDr
        /// </summary>
        /// <param name="codEvento">Código do Registro</param>
        /// <returns>DataReader</returns>
        /// <remarks>Após a utilização do LoadDataDr não esquecer de fechar a conexão: dr.Close();</remarks>
        public static OracleDataReader LoadDataDrRodada(int codRodada)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodRodada, OracleType.Int32, 4), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codRodada;
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTRODADA, param);
            return dr;
        }
        #endregion

        #endregion

        #region Métodos Específicos

        #region LoadDataDrEmpresaHora
        public static OracleDataReader LoadDataDrEmpresaHora(int codEmpresaPartic, int numContato, string dta_hr_agenda)
        {
            OracleParameter[] param = new OracleParameter[] { 

                    new OracleParameter(PARMcodCliEmpresa, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumContatoEmpresa, OracleType.Int32, 4), 
                    new OracleParameter(PARMdtHrAgenda, OracleType.VarChar), 
                    new OracleParameter(PARMCURSOR, OracleType.Cursor)
                };

            param[0].Value = codEmpresaPartic;
            param[1].Value = numContato;
            param[2].Value = dta_hr_agenda;
            param[3].Direction = ParameterDirection.Output;

            OracleDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, SPSELECTEMPRESAHORA, param);
            return dr;
        }
        #endregion

        #region DeleteEmpresaHora
        public static void DeleteEmpresaHora(int codEmpresaPartic, int numContato, string dta_hr_agenda)
        {
            OracleParameter[] param = new OracleParameter[] { 
                    new OracleParameter(PARMcodCliEmpresa, OracleType.Int32, 4), 
                    new OracleParameter(PARMnumContatoEmpresa, OracleType.Int32, 4), 
                    new OracleParameter(PARMdtHrAgenda, OracleType.VarChar)
                };

            param[0].Value = codEmpresaPartic;
            param[1].Value = numContato;
            param[2].Value = dta_hr_agenda;

            using (OracleConnection conn = new OracleConnection(DataBase.CONN_STRING))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        DataBase.ExecuteNonQuery(trans, CommandType.StoredProcedure, SPDELETEEMPRESAHORA, param);
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
        #endregion

        #endregion
    }
}
