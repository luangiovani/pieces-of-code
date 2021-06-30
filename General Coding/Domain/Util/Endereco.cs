using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Data;

namespace Framework.Domain.Utils
{
    public class Endereco
    {
        #region Atributos
        private bool status;
        private string descricao;
        private string logradouro;
        private string numero;
        private string bairro;
        private string cidade;
        private string uf;
        private string estado;
        private string pais;
        private string cep;
        private string latitude;
        private string longitude;
        private string tipologradouro;
        private string estados { get; set; }
        private string cidades { get; set; }
        #endregion

        #region Propriedades
        public bool Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        public string Descricao
        {
            get { return this.descricao; }
            set { this.descricao = value; }
        }
        public string Logradouro
        {
            get { return this.logradouro; }
            set { this.logradouro = value; }
        }
        public string Numero
        {
            get { return this.numero; }
            set { this.numero = value; }
        }
        public string Bairro
        {
            get { return this.bairro; }
            set { this.bairro = value; }
        }
        public string Cidade
        {
            get { return this.cidade; }
            set { this.cidade = value; }
        }
        public string Uf
        {
            get { return this.uf; }
            set { this.uf = value; }
        }
        public string Estado
        {
            get { return this.estado; }
            set { this.estado = value; }
        }
        public string Pais
        {
            get { return this.pais; }
            set { this.pais = value; }
        }
        public string Cep
        {
            get { return this.cep; }
            set { this.cep = value; }
        }
        public string Latitude
        {
            get { return this.latitude; }
            set { this.latitude = value; }
        }
        public string Longitude
        {
            get { return this.longitude; }
            set { this.longitude = value; }
        }

        public string Tipologradouro
        {
            get { return this.tipologradouro; }
            set { this.tipologradouro = value; }
        }

        public string Estados
        {
            get { return this.estados; }
            set { this.estados = value; }
        }

        public string Cidades
        {
            get { return this.cidades; }
            set { this.cidades = value; }
        }
        #endregion

        #region Configuracao
        private const int timeout = 2000; //em milesegundos.
        #endregion

        #region ConsultarCep
        public static Endereco ConsultarCep(string cep)
        {
            Endereco endereco = new Endereco();

            //Tentativa 0
            viacep(endereco, cep);
            if (endereco.status)
                return endereco;

            //Tentativa 0
            republicavirtual(endereco, cep);
            if (endereco.status)
                return endereco;

            //Tentativa 1
            Correios(endereco, cep);
            if (endereco.status)
                return endereco;
            
            //Tentativa 2
            GoogleApi(endereco, cep + " Brasil");
            if (endereco.status)
                return endereco;

            //Retorno Vazio
            return endereco;
        }
        #endregion

        #region ConsultarEndereco
        public static Endereco ConsultarEndereco(string logradouro)
        {
            Endereco endereco = new Endereco();

            GoogleApi(endereco, logradouro);
            if (endereco.status)
                return endereco;

            //Retorno Vazio
            return endereco;
        }
        #endregion

        #region Correios
        private static void Correios(Endereco end, string cep)
        {
            string url = "http://m.correios.com.br/movel/buscaCepConfirma.do";

            string postData = "&cepEntrada=" + cep + "&tipoCep=&cepTemp=&metodo=buscarCep";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Timeout = timeout;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            try
            {
                //Adiciona os parâmetros ao request.
                Stream stream = request.GetRequestStream();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Close();

                //Página retorno.
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);

                string linha = "";
                bool ler = false;
                string strXml = "";

                while (!reader.EndOfStream)
                {
                    linha = reader.ReadLine();

                    if (linha.Trim().Contains("caixacampobranco"))
                        ler = true;

                    if (ler)
                        strXml += linha.Replace("<br/>", "").Trim();

                    if (linha.Trim() == "</div>")
                        ler = false;
                }
                reader.Close();

                if (linha != "")
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(strXml);

                    foreach (XmlNode node in xml["div"].ChildNodes)
                    {
                        if (node.InnerText.Replace(":", "").Trim() == "Logradouro")
                            end.Logradouro = node.NextSibling.InnerText.Split('-')[0].Trim();

                        if (node.InnerText.Replace(":", "").Trim() == "Bairro")
                            end.Bairro = node.NextSibling.InnerText;

                        if (node.InnerText.Replace(":", "").Trim() == "Localidade / UF")
                        {
                            end.Cidade = node.NextSibling.InnerText.Split('/')[0].Trim();
                            end.Uf = node.NextSibling.InnerText.Split('/')[1].Trim();
                        }

                        if (node.InnerText.Replace(":", "").Trim() == "CEP")
                            end.Cep = node.NextSibling.InnerText;
                    }

                    end.Status = true;
                    end.Descricao = "OK";
                }
                else
                {
                    end.Status = false;
                    end.Descricao = "Não encontrado!";
                }
            }
            catch
            {
                end.Status = false;
                end.Descricao = "ERRO";
            }
        }
        #endregion

        #region GoogleApi
        private static void GoogleApi(Endereco end, string busca)
        {
            string url = "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false";
            string requestUrl = string.Format(url, busca);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
            request.Timeout = timeout;

            try
            {
                WebResponse response = request.GetResponse();
                XmlDocument xml = new XmlDocument();
                xml.Load(response.GetResponseStream());

                XmlNode raiz = xml.DocumentElement;
                end.descricao = raiz["status"].InnerText;

                //OK
                //ZERO_RESULTS
                //OVER_QUERY_LIMIT
                //REQUEST_DENIED
                //INVALID_REQUEST
                if (end.descricao == "OK")
                {
                    end.status = true;
                    bool postal_code = false;

                    foreach (XmlNode node in raiz["result"].ChildNodes)
                    {
                        //if (node.InnerXml == "postal_code" && !postal_code)
                        //    postal_code = true;
                        //else if (node.InnerXml != "postal_code" && !postal_code)
                        //    break;

                        if (node.Name == "address_component")
                        {
                            switch (node["type"].InnerXml)
                            {
                                case "locality":
                                    end.cidade = node["short_name"].InnerXml;
                                    break;
                                case "sublocality":
                                case "neighborhood":
                                    end.bairro = node["short_name"].InnerXml;
                                    break;
                                case "postal_code":
                                    end.cep = node["short_name"].InnerXml;
                                    break;
                                case "administrative_area_level_1":
                                    end.uf = node["short_name"].InnerXml;
                                    end.estado = node["long_name"].InnerXml;
                                    break;
                                case "country":
                                    end.pais = node["short_name"].InnerXml;
                                    break;
                            }
                        }

                        if (node.Name == "geometry")
                        {
                            end.latitude = node["location"]["lat"].InnerXml;
                            end.longitude = node["location"]["lng"].InnerXml;
                        }
                    }
                }
                else
                    end.status = false;
            }
            catch
            {
                end.status = false;
            }
        }
        #endregion

        #region republicavirtual
        private static void republicavirtual(Endereco end, string cep) 
        {
            DataSet ds = new DataSet();
            string _resultado;
            try
            {
                ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" + cep.Replace("-", "").Trim() + "&formato=xml");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        _resultado = ds.Tables[0].Rows[0]["resultado"].ToString();
                        switch (_resultado)
                        {
                            case "1":
                                end.Uf = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                                end.Cidade = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                                end.Bairro = ds.Tables[0].Rows[0]["bairro"].ToString().Trim();
                                end.Tipologradouro = ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim();
                                end.Logradouro = ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
                                end.Descricao = "CEP completo";
                                end.Status = true;
                                break;
                            case "2":
                                end.Uf = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                                end.Cidade = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                                end.Bairro = "";
                                end.Tipologradouro = "";
                                end.Logradouro = "";
                                end.Descricao = "CEP único";
                                end.Status = false;
                                break;
                            default:
                                end.Uf = "";
                                end.Cidade = "";
                                end.Bairro = "";
                                end.Tipologradouro = "";
                                end.Logradouro = "";
                                end.Descricao = "CEP não  encontrado";
                                end.Status = false;
                                break;
                        }
                    }
                }
            }
            catch
            {
                end.Status = false;
                end.Descricao = "ERRO";
            }
        }
        #endregion

        #region http://viacep.com.br/
        private static void viacep(Endereco end, string cep)
        {
            DataSet ds = new DataSet();
            string _resultado;
            try
            {
                var url = "http://viacep.com.br/ws/" + cep.Replace("-", "").Trim() + "/xml/";
                ds.ReadXml(url);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {       
                        end.Uf = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                        end.Cidade = ds.Tables[0].Rows[0]["localidade"].ToString().Trim();
                        end.Bairro = ds.Tables[0].Rows[0]["bairro"].ToString().Trim();
                        end.Tipologradouro = "";
                        end.Logradouro = ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
                        end.Descricao = "CEP completo";
                        end.Status = true;
                    }
                }
            }
            catch
            {
                end.Status = false;
                end.Descricao = "ERRO";
            }
        }
        #endregion
    }

    public class EstadoEndereco
    {
        public int UFID { get; set; }
        public string UF { get; set; }
    }

    public class CidadeEndereco
    {
        public int CidadeID { get; set; }
        public string Nome { get; set; }
    }
}
