using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace IntegradorAtendimentosRDStation.Models
{
    public class FiltroLeadsParticipacoesEventosModel
    {
        public FiltroLeadsParticipacoesEventosModel()
        {
            LeadsParticipantes = new List<LeadsParticipacoesEventosModel>();
        }

        [DisplayName("Enviados De")]
        public string dataDe { get; set; }
        [DisplayName("Até")]
        public string dataAte { get; set; }
        [DisplayName("Título")]
        public string codTituloEvento { get; set; }
        [DisplayName("Email do Participante")]
        public string emailParticipante { get; set; }
        [DisplayName("Cod. PJ SIA")]
        public int A012_CD_CLI { get; set; }
        [DisplayName("Cod. Contato SIA")]
        public int A261_CD_CONT { get; set; }
        [DisplayName("Evento SIA")]
        public string eventoSIA { get; set; }
        public List<LeadsParticipacoesEventosModel> LeadsParticipantes { get; set; }

        public void Load(FiltroLeadsParticipacoesEventosModel filtro)
        {
            var listaRetorno = new List<LeadsParticipacoesEventosModel>();

            if (filtro == null)
                this.LeadsParticipantes = listaRetorno;
            else
            {

                try
                {
                    #region Monta sql

                    #region consultabase
                    string sSql = @"SELECT CV.A022_CD_EV,
                                       CV.A012_CD_CLI,
                                       CV.A261_CD_CONT,
                                       CV.A014_NUM_CONT,
                                       T022.A008_CD_TIT_EV,
                                       T008.A008_TIT_EV,
                                       T262.A262_EMAIL,
                                       CV.SJSON_ENVIADO,
                                       to_char(CV.DT_ENVIO, 'DD/MM/YYYY HH24:MI:SS' ) DT_ENVIO
                                  FROM CONVERSOES_RDSTATION CV
                                  JOIN T022_EVENTOS T022 ON T022.A022_CD_EV = CV.A022_CD_EV
                                  JOIN T008_TIT_EVENTO T008 ON T008.A008_CD_TIT_EV = T022.A008_CD_TIT_EV
                                  JOIN T014_CONTATO_CLIENTE T014 ON T014.A012_CD_CLI = CV.A012_CD_CLI
                                   AND T014.A261_CD_CONT = CV.A261_CD_CONT
                                   AND T014.A014_NUM_CONT = CV.A014_NUM_CONT
                                  JOIN T262_DAD_AD_CONT T262 ON T262.A261_CD_CONT = T014.A261_CD_CONT
                                 WHERE 1=1";
                    #endregion

                    #region filtros

                    if (filtro.A012_CD_CLI > 0)
                    {
                        sSql += @" AND CV.A012_CD_CLI = " + A012_CD_CLI;
                    }

                    if (filtro.A261_CD_CONT > 0)
                    {
                        sSql += @" AND CV.A261_CD_CONT = " + A261_CD_CONT;
                    }

                    if (!String.IsNullOrEmpty(filtro.eventoSIA))
                    {
                        sSql += @" AND (CV.A022_CD_EV =" + filtro.eventoSIA + @" OR CV.A022_CD_EV IN(SELECT T022R.A022_CD_EV_REP FROM T022_EVENTOS_REPLICADO T022R WHERE T022R.A022_CD_EV_ORI = " + filtro.eventoSIA + @"))";
                    }

                    if (!String.IsNullOrEmpty(filtro.codTituloEvento))
                    {
                        sSql += @" AND CV.SJSON_ENVIADO LIKE '%titulo_" + filtro.codTituloEvento+@"%'";
                    }

                    if (!String.IsNullOrEmpty(filtro.emailParticipante))
                    {
                        sSql += @" AND CV.SJSON_ENVIADO LIKE '%" + filtro.emailParticipante + @"%'";
                    }

                    if (!String.IsNullOrEmpty(filtro.dataDe))
                    {
                        sSql += @" AND CAST(CV.DT_ENVIO AS DATE) >= TO_DATE('" + filtro.dataDe + @"','dd/MM/yy')";
                    }

                    if (!String.IsNullOrEmpty(filtro.dataAte))
                    {
                        sSql += @" AND CAST(CV.DT_ENVIO AS DATE) <= TO_DATE('" + filtro.dataAte + @"','dd/MM/yy')";
                    }
                    
                    #endregion

                    #endregion

                    #region Excuta Consulta se filtros foram preenchidos

                    if (filtro.A012_CD_CLI > 0 ||
                        filtro.A261_CD_CONT > 0 ||
                        !String.IsNullOrEmpty(filtro.eventoSIA) ||
                        !String.IsNullOrEmpty(filtro.codTituloEvento) ||
                        !String.IsNullOrEmpty(filtro.emailParticipante) ||
                        !String.IsNullOrEmpty(filtro.dataDe) || 
                        !String.IsNullOrEmpty(filtro.dataAte))
                    {
                        using (OracleDataReader dr = Classes.DataBase.ExecuteReader(System.Data.CommandType.Text, sSql))
                        {
                            #region Percorre Leitura

                            while (dr.Read())
                            {
                                var LeadsParticipacoes = new LeadsParticipacoesEventosModel()
                                {
                                    A022_CD_EV = Convert.ToInt32(dr["A022_CD_EV"].ToString()),
                                    A012_CD_CLI = Convert.ToInt32(dr["A012_CD_CLI"].ToString()),
                                    A261_CD_CONT = Convert.ToInt32(dr["A261_CD_CONT"].ToString()),
                                    A014_NUM_CONT = Convert.ToInt32(dr["A014_NUM_CONT"].ToString()),
                                    codTituloEvento = dr["A008_CD_TIT_EV"].ToString(),
                                    nomeTituloEvento = dr["A008_TIT_EV"].ToString(),
                                    email = dr["A262_EMAIL"].ToString(),
                                    sJsonEnviado = dr["SJSON_ENVIADO"].ToString(),
                                    DataEnvio = DateTime.ParseExact(dr["DT_ENVIO"].ToString(), "dd/MM/yyyy HH:mm:ss", null)
                                };

                                var leadP = LeadsParticipacoes.LeadParticipante;

                                LeadsParticipacoes.codTituloEventoEnviado = leadP.titEnviado;
                                LeadsParticipacoes.nomeTituloEventoEnviado = leadP.nomeTituloEventoEnviado;
                                LeadsParticipacoes.emailEnviado = leadP.email;

                                listaRetorno.Add(LeadsParticipacoes);
                            }
                            dr.Close();

                            #endregion
                        }
                    }

                    #endregion
                }
                catch
                {
                    this.LeadsParticipantes = listaRetorno;
                }

                this.LeadsParticipantes = listaRetorno;
            }
        }
    }

    public class LeadsParticipacoesEventosModel
    {
        public int A022_CD_EV { get; set; }
        public int A012_CD_CLI { get; set; }
        public int A261_CD_CONT { get; set; }
        public int A014_NUM_CONT { get; set; }
        public string codTituloEvento { get; set; }
        public string nomeTituloEvento { get; set; }
        public string email { get; set; }
        public string sJsonEnviado { get; set; }
        public DateTime DataEnvio { get; set; }
        public string codTituloEventoEnviado { get; set; }
        public string nomeTituloEventoEnviado { get; set; }
        public string emailEnviado { get; set; }
        

        public LeadParticipanteModel LeadParticipante 
        { 
            get 
            {
                try
                {
                    LeadParticipanteModel lead = JsonConvert.DeserializeObject<LeadParticipanteModel>(this.sJsonEnviado);
                    return lead;
                }
                catch
                {
                    return new LeadParticipanteModel();  
                }
            } 
        }
    }

    public class LeadParticipanteModel
    {
        public LeadParticipanteModel()
        {
        }

        public string token_rdstation { get; set; }
        public string identificador { get; set; }
        public string email { get; set; }
        public string titEnviado
        {
            get
            {
                return identificador.Replace("titulo_", "");
            }
        }
        public string nomeTituloEventoEnviado
        {
            get
            {
                string dscTitEnviado = "";
                try
                {
                    if (!String.IsNullOrEmpty(this.titEnviado))
                    {
                        using (var dr = Classes.DataBase.ExecuteReader(System.Data.CommandType.Text, "SELECT A008_TIT_EV FROM T008_TIT_EVENTO WHERE A008_CD_TIT_EV = " + titEnviado))
                        {
                            if (dr.Read())
                            {
                                dscTitEnviado = dr["A008_TIT_EV"].ToString();
                            }
                            dr.Close();
                        }   
                    }
                }
                catch
                {
                    dscTitEnviado = "";
                }

                return dscTitEnviado;
            }
        }
    }
}