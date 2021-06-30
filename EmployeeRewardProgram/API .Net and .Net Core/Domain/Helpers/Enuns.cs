namespace Domain.Helpers
{
    public static class AplicacaoEnum
    {
        public static string WEB => "A71C746F-EB2D-4CA9-898F-02C89B08F6AD";
        public static string Mobile => "DE6B6120-DBC5-4806-8EE3-EFC20B0843E9";
    }

    public static class SituacaoRecomendacaoEnum
    {
        public static string Aprovada => "5468FA72-FCA6-4A92-8BAC-BFC9E971BEB6";
        public static string Reprovada => "A045A8FE-3674-475C-8454-380AE3CC9FB4";
        public static string EmAnalise => "633B5B11-CF4B-4D44-A9F4-B39E38924BDE";
    }

    public static class SituacaoAvaliacaoEnum
    {
        public static string Pendente => "C402396C-4938-4FAB-9CDF-6B94BE5C5802";
        public static string Aprovada => "9FB43F1E-0F7C-4AB8-9FEF-E0AEB6C4265F";
        public static string Reprovada => "FE041110-BFD9-4F86-85CC-55CC31E9B68A";
    }

    public static class SituacaoCompraEnum
    {
        public static string SolicitacaoTroca => "6196698E-95D7-48CF-B07A-CEC3BDC15D18";
        public static string EmAnaliseDaLoja => "EBBC16C6-DC45-48DC-ACE7-1C31D7A8D909";
        public static string Efetivada => "72C108D6-E972-437E-82A7-FE83CD54DD0A";
        public static string Finalizada => "7D4E5C01-FF0D-433A-9A72-1BCD68612A5E";
        public static string Cancelada => "FDB7422B-521F-4970-9E24-FE5B4BC68C04";
        public static string Recusada => "AE254858-4843-493D-92B1-C198DAEFD088";
        public static string ProdutosRecebidos => "69EA87EC-1D85-4F31-AA98-F3AE33F204FB";

        public static string GetName(string value)
        {
            if (value.ToUpper() == SolicitacaoTroca)
                return "Solicitação de Troca";
            else if(value.ToUpper() == EmAnaliseDaLoja)
                return "Em Análise da Loja";
            else if (value.ToUpper() == Efetivada)
                return "Efetivada";
            else if (value.ToUpper() == Finalizada)
                return "Finalizada";
            else if (value.ToUpper() == Cancelada)
                return "Cancelada";
            else if (value.ToUpper() == Recusada)
                return "Recusada";
            else if (value.ToUpper() == ProdutosRecebidos)
                return "Produto(s) Recebido(s)!";
            else
                return "";
        }
    }

    public static class SituacaoTrocaEnum
    {
        public static string SolicitacaoTroca => "BECDF9E1-6D1C-42E7-AE25-4AF05C107845";
        public static string EmAnaliseDaLoja => "E16B94EE-70BB-4064-A4F3-29F44DF8FE66";
        public static string Efetivada => "96730B8D-7B59-4F8B-BCA1-FB877A203442";
        public static string Finalizada => "0F439A7F-F8CC-4A87-BA26-CD8E85BA9ABF";
        public static string Cancelada => "75B420FD-467E-41EC-A257-EF0A9599434F";
        public static string Recusada => "8978B475-AEA0-4C5F-8C5E-DB13341AFFFA";
    }

    public static class MeioDeCompraEnum
    {
        public static string PortalWeb => "5CA79155-AF62-4E03-BB7D-5DFD7E4B5CA9";
        public static string Aplicativo => "AAE66B43-75B6-42F9-A228-63F7292A86F9";
        public static string NaLoja => "2D2F7B07-9B3E-4E16-A097-BABBDD47551A";
    }

    public static class OpcaoEntregaEnum
    {
        public static string RetirarNaLojaMaisProxima => "81CEBE67-D92E-4058-BD14-0214ECF69647";
        public static string ReceberViaCorreio => "DBDA8B89-6A0C-46EE-A3C1-19CEA9BD02C7";
        public static string ReceberViaMalote => "A38D36B8-C48C-4145-984D-BB3D707DB485";
        public static string ReceberAtravesDoGestor => "2601ACF2-E36C-4A6A-8194-0BB9CDD4357A";
    }

    public static class LojaEnum
    {
        public static string LojaGrifeSede => "6A99C661-48E6-4E46-9B1F-168F4C6E513E";
    }

    public static class PerfilAcessoEnum
    {
        public static string AdminTecnico => "507BDB9F-B76A-40A7-A7CB-A299E79E0565";
        public static string Admin => "F7110B72-602F-43CF-9B23-E2F48A6E18F6";
        public static string AdminGestor => "A358766F-28E2-4790-87D9-CD1DDFA91493";
        public static string Gestor => "40A7DAF7-B890-4A2F-9EE6-51595F6E6B85";
        public static string Colaborador => "D9C7846B-6184-4F74-B03A-369F1DE62EF8";
        public static string Loja => "ADF3ECB0-AF51-41F2-9DEF-5C29D9EC4424";
    }
}
