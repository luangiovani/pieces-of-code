using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace IntegradorAtendimentosRDStation.Models
{
    public class IntegradorModel
    {
        [DisplayName("Sequencial")]
        public int SEQUENCIAL { get; set; }

        [DisplayName("API")]
        [Required(ErrorMessage = "Informe um Nome")]
        public string API { get; set; }

        [DisplayName("Descrição Atendimento")]
        [Required(ErrorMessage = "Informe uma Descrição")]
        public string A001_OBS { get; set; }

        [DisplayName("CODSOL")]
        public string CODSOL { get; set; }

        [DisplayName("CODOBJ")]
        public string CODOBJ { get; set; }

        [DisplayName("Projeto")]
        [Required(ErrorMessage = "Selecione um Projeto")]
        public string COD_PROJETO_SGE { get; set; }

        public string PROJETO { get; set; }

        [DisplayName("Ação")]
        [Required(ErrorMessage = "Selecione uma Ação")]
        public string COD_ACAO_SEQ { get; set; }

        public string ACAO { get; set; }

        [DisplayName("Data de Inclusão")]
        public DateTime DT_INC { get; set; }

        [DisplayName("Usuário de Inclusão")]
        public string USUARIO_INC { get; set; }

        [DisplayName("Data de Alteração")]
        public DateTime DT_ALT { get; set; }

        [DisplayName("Usuário de Alteração")]
        public string USUARIO_ALT { get; set; }

        [DisplayName("Ativo")]
        [Required(ErrorMessage = "Informe SIM ou NÃO")]
        public int ATIVO { get; set; }

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "Informe um tipo")]
        public int TIPOATENDIMENTO { get; set; }

        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Informe uma categoria")]
        public int CATEGORIAATENDIMENTO { get; set; }

        [DisplayName("Conversões")]
        public int CONVERSOES { get; set; }

        public void Gravar()
        {
            OracleDataReader rdr;
            string sSql = "";
            sSql = "SELECT * FROM INTEGRADOR_RDSTATION WHERE API = '" + API + "'";
            using (var drVER = Classes.DataBase.ExecuteReader(System.Data.CommandType.Text, sSql))
            {
                if (drVER.Read())
                {
                    SEQUENCIAL = Convert.ToInt32(drVER["SEQUENCIAL"].ToString());
                }
                drVER.Close();
            }
            if (SEQUENCIAL == 0)
            {

                #region Obtem Sequencial
                string seq = "SELECT MAX(SEQUENCIAL) + 1 SEQ FROM INTEGRADOR_RDSTATION";
                var rdrSQ = Classes.DataBase.ExecuteReader(System.Data.CommandType.Text, seq);
                if (rdrSQ.Read() && !String.IsNullOrEmpty(rdrSQ["SEQ"].ToString()))
	                SEQUENCIAL = Convert.ToInt32(rdrSQ["SEQ"].ToString());
                else
                    SEQUENCIAL = 1;
                rdrSQ.Close();
                #endregion

                #region Insere

                sSql = "INSERT INTO INTEGRADOR_RDSTATION(SEQUENCIAL, API, A001_OBS, CODSOL, CODOBJ, COD_PROJETO_SGE, COD_ACAO_SEQ, DT_INC, USUARIO_INC, DT_ALT, USUARIO_ALT, ATIVO,TIPOATENDIMENTO,CATEGORIAATENDIMENTO) VALUES";
                sSql += "(" + SEQUENCIAL + ",";
                sSql += "'" + API + "',";
                sSql += "'" + A001_OBS + "',";
                sSql += "'" + CODSOL + "',";
                sSql += "'" + CODOBJ + "',";
                sSql += "'" + COD_PROJETO_SGE + "',";
                sSql += COD_ACAO_SEQ + ",";
                sSql += "systimestamp,";
                sSql += "'" + USUARIO_INC + "',";
                sSql += "systimestamp,";
                sSql += "'" + USUARIO_ALT + "',";
                sSql += ATIVO + ",";
                sSql += TIPOATENDIMENTO + ",";
                sSql += CATEGORIAATENDIMENTO + ")";

                #endregion
            }
            else
            {
                #region Atualiza
                sSql = "UPDATE INTEGRADOR_RDSTATION";
                sSql += " SET A001_OBS = '" + A001_OBS + "',";
                sSql += " CODSOL = '" + CODSOL + "',";
                sSql += " CODOBJ = '" + CODOBJ + "',";
                sSql += " COD_PROJETO_SGE = '" + COD_PROJETO_SGE + "',";
                sSql += " COD_ACAO_SEQ = "+COD_ACAO_SEQ + ",";
                sSql += " DT_ALT = systimestamp,";
                sSql += " USUARIO_ALT = '" + USUARIO_ALT + "',";
                sSql += " ATIVO = " + ATIVO + ",";
                sSql += " TIPOATENDIMENTO = " + TIPOATENDIMENTO + ",";
                sSql += " CATEGORIAATENDIMENTO = " + CATEGORIAATENDIMENTO ;
                sSql += " WHERE SEQUENCIAL = " + SEQUENCIAL;

                #endregion
            }

            rdr = Classes.DataBase.ExecuteReader(System.Data.CommandType.Text, sSql);
            rdr.Close();
        }
    }
}