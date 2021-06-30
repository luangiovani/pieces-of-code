using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient; 
using System.Data;

/// Classe Contato
/// Autor: daniel
///Data: 11/05/2007
///Modificação: 10/09/2007

namespace Classes
{
    public class Contato  // T261_Contato 
    {
            #region Atributos
            private int codContato;
            private string nomContato;
            private string dddTelefone;
            private Int64? numTelefone;
            private Int64? numCelular;
            private int? codUsuario; 
            private int? codEstadoCivil;
            private string logusuario;
            private string nomLogin;
            private string nomSenha;
            private DateTime dataInclusao;
            private DateTime dataUltAlteracao;
            #endregion

            #region Propriedades
            public int CodContato
            {
                get { return codContato; }
                set { codContato = value; }
            }
            public string NomContato
            {
                get { return nomContato; }
                set { nomContato = value; }
            }
            public string DddTelefone
            {
                get { return dddTelefone; }
                set { dddTelefone = value; }
            }
            public Int64? NumTelefone
            {
                get { return numTelefone; }
                set { numTelefone = value; }
            }
            public Int64? NumCelular
            {
                get { return numCelular; }
                set { numCelular = value; }
            }
            public int? CodUsuario
            {
                get { return codUsuario; }
                set { codUsuario = value; }
            }
            public int? CodEstadoCivil
            {
                get { return codEstadoCivil; }
                set { codEstadoCivil = value; }
            }
            public string Logusuario
            {
                get { return logusuario; }
                set { logusuario = value; }
            }
            public string NomLogin
            {
                get { return nomLogin; }
                set { nomLogin = value; }
            }
            public string NomSenha
            {
                get { return nomSenha; }
                set { nomSenha = value; }
            }
            public DateTime DataInclusao
            {
                get { return dataInclusao; }
                set { dataInclusao = value; }
            }
            public DateTime DataUltAlteracao
            {
                get { return dataUltAlteracao; }
                set { dataUltAlteracao = value; }
            }
            #endregion

            #region Construtores
            public Contato()
                : this(-1)
            { }
            public Contato(int codContato)
            {
                this.codContato = codContato;
            }
            #endregion

            #region StoredProcedures
            private const string SPINSERT = "Contato261.ContatoInc";
            private const string SPUPDATE = "Contato261.ContatoAlt";
            private const string SPDELETE = "Contato261.ContatoDel";
            private const string SPSELECTID = "Contato261.ContatoSelId";
            private const string SPSELECTPAG = "Contato261.ContatoSelPag";
            #endregion

            #region Parametros Oracle
            private const string PARMCODIGO = "codContato";
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
                    /*0*/ new OracleParameter(PARMCODIGO, OracleType.Int32, 8, ParameterDirection.InputOutput.ToString()) ,
                    /*1*/ new OracleParameter( "nomContato", OracleType.VarChar),
                    /*2*/ new OracleParameter( "dddTelefone", OracleType.VarChar),
                    /*3*/ new OracleParameter( "numTelefone", OracleType.Int32),                    
                    /*4*/ new OracleParameter( "numCelular", OracleType.Int32),
                    /*5*/ new OracleParameter( "CodUsuario", OracleType.Int32),
                    /*6*/ new OracleParameter( "codEstadoCivil", OracleType.Int32),
                    /*7*/ new OracleParameter( "logUsuario", OracleType.VarChar),
                    /*8*/ new OracleParameter( "nomLogin", OracleType.VarChar),
                    /*9*/ new OracleParameter( "nomSenha", OracleType.VarChar)
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
                parms[0].Value = this.codContato;
                parms[1].Value = "";
                if (this.nomContato != null)
                { parms[1].Value = this.nomContato.ToUpper(); }

                parms[2].Value = "";
                if (this.dddTelefone != null)
                { parms[2].Value = this.dddTelefone; }

                parms[3].Value = DBNull.Value;
                if (this.numTelefone != null)
                { parms[3].Value = this.numTelefone; }

                parms[4].Value = DBNull.Value;
                if (this.numCelular != null)
                { parms[4].Value = this.numCelular; }

                parms[5].Value = DBNull.Value;
                if (this.codUsuario != null)
                { parms[5].Value = this.codUsuario; }

                parms[6].Value = DBNull.Value;
                if (this.codEstadoCivil != null)
                { parms[6].Value = this.codEstadoCivil; }

                parms[7].Value = "";
                if (this.logusuario != null)
                { parms[7].Value = this.logusuario.ToUpper(); }

                parms[8].Value = "";
                if (this.nomLogin != null)
                { parms[8].Value = this.nomLogin; }

                parms[9].Value = "";
                if (this.nomSenha!= null)
                { parms[9].Value = this.nomSenha; }

                if (this.codContato < 0)
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
                            codContato = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                    codContato = Convert.ToInt32(cmd.Parameters[PARMCODIGO].Value);
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
                    new OracleParameter("curContato", OracleType.Cursor)
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
                                                                  new OracleParameter("curContato", OracleType.Cursor)
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
            /// <returns>cont</returns>
            public static Contato GetDataRow(int codigo)
            {
                OracleDataReader dr = LoadDataDr(codigo);
                Contato cont = new Contato();
                try
                {
                    if (dr.Read())
                    {
                        cont.codContato      = Convert.ToInt32(dr["A261_cd_cont"]);
                        cont.nomContato      = Convert.ToString(dr["A261_nm_cont"]);
                        cont.dddTelefone = Convert.ToString(dr["A261_ddd_cont"]);
                        if (dr["A261_tel_cont"] != DBNull.Value && dr["A261_tel_cont"] != DBNull.Value && dr["A261_tel_cont"].ToString() != "")
                            cont.numTelefone = Convert.ToInt32(dr["A261_tel_cont"]);
                        if (dr["A261_cel_cont"] != DBNull.Value && dr["A261_cel_cont"] != DBNull.Value && dr["A261_cel_cont"].ToString() != "")
                            cont.numCelular = Convert.ToInt32(dr["A261_cel_cont"]);
                        if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                            cont.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                        if (dr["A598_cd_est_civ"] != DBNull.Value && dr["A598_cd_est_civ"] != DBNull.Value && dr["A598_cd_est_civ"].ToString() != "")
                            cont.codEstadoCivil = Convert.ToInt32(dr["A598_cd_est_civ"]);
                        if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                            cont.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                        if (dr["A261_login"] != DBNull.Value && dr["A261_login"] != DBNull.Value && dr["A261_login"].ToString() != "")
                            cont.nomLogin = Convert.ToString(dr["A261_login"]);
                        if (dr["A261_senha"] != DBNull.Value && dr["A261_senha"] != DBNull.Value && dr["A261_senha"].ToString() != "")
                            cont.nomSenha = Convert.ToString(dr["A261_senha"]);
                        if (dr["A261_dt_inc"] != DBNull.Value && dr["A261_dt_inc"] != DBNull.Value && dr["A261_dt_inc"].ToString() != "")
                            cont.dataInclusao = Convert.ToDateTime(dr["A261_dt_inc"]);
                        if (dr["dta_inc_alt"] != DBNull.Value && dr["dta_inc_alt"] != DBNull.Value && dr["dta_inc_alt"].ToString() != "")
                            cont.dataUltAlteracao = Convert.ToDateTime(dr["dta_inc_alt"]);
                    }
                }
                catch (Exception ex)
                {
                    if (!dr.IsClosed)
                        dr.Close();
                    cont = new Contato();
                    throw (ex);
                }
                finally
                {
                    if (!dr.IsClosed)
                        dr.Close();
                }
                return cont;
            }

            /// <summary>
            /// GetDataRow para ser utilizado dentro de alguma transação
            /// </summary>
            /// <param name="codigo">Código do Registro</param>
            /// <param name="trans">OracleTransaction</param>
            /// <returns>cont</returns>
            public static Contato GetDataRow(int codigo, OracleTransaction trans)
            {
                OracleDataReader dr = LoadDataDr(codigo, trans);
                Contato cont = new Contato();
                try
                {
                    if (dr.Read())
                    {
                        cont.codContato = Convert.ToInt32(dr["A261_cd_cont"]);
                        cont.nomContato = Convert.ToString(dr["A261_nm_cont"]);
                        cont.dddTelefone = Convert.ToString(dr["A261_ddd_cont"]);
                        if (dr["A261_tel_cont"] != DBNull.Value && dr["A261_tel_cont"] != DBNull.Value && dr["A261_tel_cont"].ToString() != "")
                            cont.numTelefone = Convert.ToInt32(dr["A261_tel_cont"]);
                        if (dr["A261_cel_cont"] != DBNull.Value && dr["A261_cel_cont"] != DBNull.Value && dr["A261_cel_cont"].ToString() != "")
                            cont.numCelular = Convert.ToInt32(dr["A261_cel_cont"]);
                        if (dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"] != DBNull.Value && dr["A052_cd_usuario"].ToString() != "")
                            cont.codUsuario = Convert.ToInt32(dr["A052_cd_usuario"]);
                        if (dr["A598_cd_est_civ"] != DBNull.Value && dr["A598_cd_est_civ"] != DBNull.Value && dr["A598_cd_est_civ"].ToString() != "")
                            cont.codEstadoCivil = Convert.ToInt32(dr["A598_cd_est_civ"]);
                        if (dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"] != DBNull.Value && dr["usu_inc_alt"].ToString() != "")
                            cont.logusuario = Convert.ToString(dr["usu_inc_alt"]);
                        if (dr["A261_login"] != DBNull.Value && dr["A261_login"] != DBNull.Value && dr["A261_login"].ToString() != "")
                            cont.nomLogin = Convert.ToString(dr["A261_login"]);
                        if (dr["A261_senha"] != DBNull.Value && dr["A261_senha"] != DBNull.Value && dr["A261_senha"].ToString() != "")
                            cont.nomSenha = Convert.ToString(dr["A261_senha"]);
                        if (dr["A261_dt_inc"] != DBNull.Value && dr["A261_dt_inc"] != DBNull.Value && dr["A261_dt_inc"].ToString() != "")
                            cont.dataInclusao = Convert.ToDateTime(dr["A261_dt_inc"]);
                        if (dr["dta_inc_alt"] != DBNull.Value && dr["dta_inc_alt"] != DBNull.Value && dr["dta_inc_alt"].ToString() != "")
                            cont.dataUltAlteracao = Convert.ToDateTime(dr["dta_inc_alt"]);
                    }
                }
                catch (Exception ex)
                {
                    if (!dr.IsClosed)
                        dr.Close();
                    cont = new Contato();
                    throw (ex);
                }
                finally
                {
                    if (!dr.IsClosed)
                        dr.Close();
                }
                return cont;
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
                new OracleParameter("curContato", OracleType.Cursor),
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
/* Modificação: 10/09/2007
 * Inclusão dos campos Lguin e senha para autenticação do usuario. 
 * Autor: Daniel
 * */

