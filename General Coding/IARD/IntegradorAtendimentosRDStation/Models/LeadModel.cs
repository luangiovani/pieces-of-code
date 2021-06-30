using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegradorAtendimentosRDStation.Models
{
    public class Content
    {
        public string identificador { get; set; }
        public string nome { get; set; }
        public string CPF { get; set; }
        public string celular { get; set; }
        public string empresa { get; set; }
        public string CNPJ { get; set; }
        //public string __invalid_name__Código SIA (PF) { get; set; }
        public string A012_CD_CLI_PF { get; set; }
        //public string __invalid_name__Código SIA (PJ) { get; set; }
        public string A012_CD_CLI_PJ { get; set; }
        //public string __invalid_name__Código Contato (PJ) { get; set; }
        public string A261_CD_CONT { get; set; } 
        //public string __invalid_name__Porte da Empresa { get; set; }
        public string Porte { get; set; } 
        //public string __invalid_name__Ramo de atividade { get; set; }
        public string RamoAtividade { get; set; }
        public string Perfil { get; set; }
        public string estado { get; set; }
        public string cidade { get; set; }
        public string created_at { get; set; }
        public string email_lead { get; set; }
    }

    public class ConversionOrigin
    {
        public string source { get; set; }
        public string medium { get; set; }
        public object value { get; set; }
        public string campaign { get; set; }
        public string channel { get; set; }
    }

    public class FirstConversion
    {
        public Content content { get; set; }
        public DateTime created_at { get; set; }
        public string cumulative_sum { get; set; }
        public string source { get; set; }
        public ConversionOrigin conversion_origin { get; set; }
    }

    public class Content2
    {
        public string identificador { get; set; }
        public string nome { get; set; }
        public string CPF { get; set; }
        public string celular { get; set; }
        public string empresa { get; set; }
        public string CNPJ { get; set; }
        //public string __invalid_name__Código SIA (PF) { get; set; }
        public string A012_CD_CLI_PF { get; set; }
        //public string __invalid_name__Código SIA (PJ) { get; set; }
        public string A012_CD_CLI_PJ { get; set; }
        //public string __invalid_name__Código Contato (PJ) { get; set; }
        public string A261_CD_CONT { get; set; } 
        //public string __invalid_name__Porte da Empresa { get; set; }
        public string Porte { get; set; } 
        //public string __invalid_name__Ramo de atividade { get; set; }
        public string RamoAtividade { get; set; }
        public string Perfil { get; set; }
        public string estado { get; set; }
        public string cidade { get; set; }
        public string created_at { get; set; }
        public string email_lead { get; set; }
    }

    public class ConversionOrigin2
    {
        public string source { get; set; }
        public string medium { get; set; }
        public object value { get; set; }
        public string campaign { get; set; }
        public string channel { get; set; }
    }

    public class LastConversion
    {
        public Content2 content { get; set; }
        public DateTime created_at { get; set; }
        public string cumulative_sum { get; set; }
        public string source { get; set; }
        public ConversionOrigin2 conversion_origin { get; set; }
    }

    public class CustomFields
    {
        //public string __invalid_name__Código SIA (PF) { get; set; }
        public string A012_CD_CLI_PF { get; set; }
        //public string __invalid_name__Código SIA (PJ) { get; set; }
        public string A012_CD_CLI_PJ { get; set; }
        //public string __invalid_name__Código Contato (PJ) { get; set; }
        public string A261_CD_CONT { get; set; }
        //public string __invalid_name__Porte da Empresa { get; set; }
        public string Porte { get; set; }
        //public string __invalid_name__Ramo de atividade { get; set; }
        public string RamoAtividade { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
    }

    public class Lead
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public object job_title { get; set; }
        public object bio { get; set; }
        public string public_url { get; set; }
        public DateTime created_at { get; set; }
        public string opportunity { get; set; }
        public string number_conversions { get; set; }
        public object user { get; set; }
        public FirstConversion first_conversion { get; set; }
        public LastConversion last_conversion { get; set; }
        public CustomFields custom_fields { get; set; }
        public object website { get; set; }
        public object personal_phone { get; set; }
        public string mobile_phone { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public object tags { get; set; }
        public string lead_stage { get; set; }
        public object last_marked_opportunity_date { get; set; }
        public string uuid { get; set; }
        public string fit_score { get; set; }
        public int interest { get; set; }
    }

    public class RootObject
    {
        public List<Lead> leads { get; set; }
    }
}