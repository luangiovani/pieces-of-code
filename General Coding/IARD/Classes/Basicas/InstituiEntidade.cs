using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
 
using System.Data;

//-- Classe Classes Sebrae
//-- Data : 16/05/2007 
//-- Autor :  Daniel

namespace Classes
{
    public class InstituiEntidade  // T028_INSTIT_ENTID
    {
        #region Atributos
        private int codCliente;
        private GrupoRP codGrupoRp;
        private SubgrupoRP codSubgrupoRp;
        private DetalheRP codDetalheRp;
        private string logusuario;
        #endregion

        #region Propriedades
        public int CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        public GrupoRP CodGrupoRp
        {
            get { return codGrupoRp; }
            set { codGrupoRp = value; }
        }
        public SubgrupoRP CodSubgrupoRp
        {
            get { return codSubgrupoRp; }
            set { codSubgrupoRp = value; }
        }
        public DetalheRP CodDetalheRp
        {
            get { return codDetalheRp; }
            set { codDetalheRp = value; }
        }
        public string Logusuario
        {
            get { return logusuario; }
            set { logusuario = value; }
        }
        #endregion

        #region Construtores
        public InstituiEntidade()
            : this(-1)
        { }
        public InstituiEntidade(int codCliente)
        {
            this.codCliente = codCliente;
        }
        #endregion

        #region StoredProcedures
        private const string SPINSERT = "InstituiEntidade.InstituiEntidadeInc";
        private const string SPUPDATE = "InstituiEntidade.InstituiEntidadeAlt";
        private const string SPDELETE = "InstituiEntidade.InstituiEntidadeDel";
        private const string SPSELECTID = "InstituiEntidade.InstituiEntidadeSelId";
        private const string SPSELECTPAG = "InstituiEntidade.InstituiEntidadeSelPag";
        #endregion

        #region Parametros Oracle
        private const string PARMCODIGO = "codCliente";
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
                /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32),
                /*1*/ new OracleParameter( "codGrupoRp", OracleType.Int32),
                /*2*/ new OracleParameter( "codSubgrupoRp", OracleType.Int32),
                /*3*/ new OracleParameter( "codDetalheRp", OracleType.Int32),
                /*4*/ new OracleParameter( "logusuario", OracleType.VarChar)
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
            parms[0].Value = this.codCliente;
            parms[1].Value = this.codGrupoRp.CodGrupoRP;
            parms[2].Value = this.codSubgrupoRp.CodSubgrupoRP;
            parms[3].Value = this.codDetalheRp.CodDetalheRP;
            parms[4].Value = this.logusuario; 

                parms[0].Direction = ParameterDirection.Input;
             
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
                        codCliente = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                codCliente = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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

        /// <summary>
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
                new OracleParameter("curinstituiEntidade", OracleType.Cursor)
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
                                                              new OracleParameter("curInstituiEntidade", OracleType.Cursor)
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
        /// <returns>InstituiEntidade</returns>
        public static InstituiEntidade GetDataRow(int codigo)
        {
            OracleDataReader dr = LoadDataDr(codigo);
            InstituiEntidade instituiEntidade = new InstituiEntidade();
            try
            {
                if (dr.Read())
                {
                    instituiEntidade.codCliente = Convert.ToInt32(dr["A012_cd_cli"]);
                    if (dr["A069_cd_gru_rp"] != DBNull.Value && dr["A069_cd_gru_rp"] != DBNull.Value && dr["A069_cd_gru_rp"].ToString() != "")
                        instituiEntidade.codGrupoRp = new GrupoRP(Convert.ToInt32(dr["A069_cd_gru_rp"]));
                    if (dr["A070_cd_sgr_rp"] != DBNull.Value && dr["A070_cd_sgr_rp"] != DBNull.Value && dr["A070_cd_sgr_rp"].ToString() != "")
                        instituiEntidade.codSubgrupoRp = new SubgrupoRP(Convert.ToInt32(dr["A070_cd_sgr_rp"]));
                    if (dr["A034_cd_det_rp"] != DBNull.Value && dr["A034_cd_det_rp"] != DBNull.Value && dr["A034_cd_det_rp"].ToString() != "")
                        instituiEntidade.codDetalheRp = new DetalheRP(Convert.ToInt32(dr["A034_cd_det_rp"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        instituiEntidade.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                instituiEntidade = new InstituiEntidade();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return instituiEntidade;
        }

        /// <summary>
        /// GetDataRow para ser utilizado dentro de alguma transação
        /// </summary>
        /// <param name="codigo">Código do Registro</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>InstituiEntidade</returns>
        public static InstituiEntidade GetDataRow(int codigo, OracleTransaction trans)
        {
            OracleDataReader dr = LoadDataDr(codigo, trans);
            InstituiEntidade instituiEntidade = new InstituiEntidade();
            try
            {
                if (dr.Read())
                {
                    instituiEntidade.codCliente = Convert.ToInt32(dr["A012_cd_cli"]);
                    if (dr["A069_cd_gru_rp"] != DBNull.Value && dr["A069_cd_gru_rp"] != DBNull.Value && dr["A069_cd_gru_rp"].ToString() != "")
                        instituiEntidade.codGrupoRp = new GrupoRP(Convert.ToInt32(dr["A069_cd_gru_rp"]));
                    if (dr["A070_cd_sgr_rp"] != DBNull.Value && dr["A070_cd_sgr_rp"] != DBNull.Value && dr["A070_cd_sgr_rp"].ToString() != "")
                        instituiEntidade.codSubgrupoRp = new SubgrupoRP(Convert.ToInt32(dr["A070_cd_sgr_rp"]));
                    if (dr["A034_cd_det_rp"] != DBNull.Value && dr["A034_cd_det_rp"] != DBNull.Value && dr["A034_cd_det_rp"].ToString() != "")
                        instituiEntidade.codDetalheRp = new DetalheRP(Convert.ToInt32(dr["A034_cd_det_rp"]));
                    if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                        instituiEntidade.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                }
            }
            catch (Exception ex)
            {
                if (!dr.IsClosed)
                    dr.Close();
                instituiEntidade = new InstituiEntidade();
                throw (ex);
            }
            finally
            {
                if (!dr.IsClosed)
                    dr.Close();
            }
            return instituiEntidade;
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
            new OracleParameter("curInstituiEntidade", OracleType.Cursor),
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
