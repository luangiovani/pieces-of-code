using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;

namespace Classes
{
    public class CriterioFiltro
    {
        #region Atributos
        private DataTable tabelaDados;
        private bool whereVazia = false;
        #endregion

        #region Propriedades
        public DataTable TabelaDados
        {
            get { return this.tabelaDados; }
            set { this.tabelaDados = value; }
        }
        #endregion

        #region Construtores
        public CriterioFiltro()
        {
            tabelaDados = new DataTable();
            tabelaDados.Columns.Add("index"); // indice da datatable, necessário para remover uma linha
            tabelaDados.Columns.Add("nome"); // alias
            tabelaDados.Columns.Add("campo");// nome do campo no banco
            tabelaDados.Columns.Add("tipo");
            tabelaDados.Columns.Add("condicao");
            tabelaDados.Columns.Add("valor");
        }
        #endregion

        #region AdicionarLinha
        public void AdicionarLinha(string nome, string campo, string tipo, string condicao, string valor)
        {
            DataRow dr = this.tabelaDados.NewRow();
            if (this.tabelaDados.Rows.Count == 0)
            {
                dr["index"] = "0";
            }
            else
            {
                //recuperando o índice da última linha
                int index = Convert.ToInt32(this.tabelaDados.Rows[this.tabelaDados.Rows.Count - 1]["index"]);
                // incrementando o índice
                index++;
                // salvando o novo índice na datatable
                dr["index"] = index.ToString();
            }
            dr["nome"] = nome;
            dr["campo"] = campo;
            dr["tipo"] = tipo;
            dr["condicao"] = condicao;
            dr["valor"] = valor;
            this.tabelaDados.Rows.Add(dr);
        }
        #endregion

        #region AtualizarLinha
        public void AtualizarLinha(int index, string nome, string campo, string tipo, string condicao, string valor)
        {
            foreach (DataRow dr in this.tabelaDados.Rows)
            {
                if (dr["index"].ToString() == index.ToString())
                {
                    dr["nome"] = nome;
                    dr["campo"] = campo;
                    dr["tipo"] = tipo;
                    dr["condicao"] = condicao;
                    dr["valor"] = valor;
                }
            }
        }
        #endregion

        #region RemoverLinha
        public void RemoverLinha(int index)
        {
            if ((this.tabelaDados.Rows.Count > 0))
            {
                for (int i = 0; i <= this.tabelaDados.Rows.Count; i++)
                {
                    if (Convert.ToInt32(this.tabelaDados.Rows[i]["index"]) == index)
                    {
                        this.tabelaDados.Rows.RemoveAt(i);
                        break;
                    }
                }
            }
            if (tabelaDados.Rows.Count <= 0)
            {
                tabelaDados.Rows.Add(tabelaDados.NewRow());
                tabelaDados.Rows[0]["index"] = "0";
            }
        }
        #endregion

        #region RemoverTodasLinhas
        public void RemoverTodasLinhas()
        {
            this.tabelaDados.Rows.Clear();
            this.tabelaDados.Rows.Add(this.tabelaDados.NewRow());
            this.TabelaDados.Rows[0]["index"] = "0";
        }
        #endregion

        #region RetornaWhere
        public string RetornaWhere()
        {
            IFormatProvider culture = new CultureInfo("pt-BR", true);
            StringBuilder where = new StringBuilder();

            // Criando uma view da tabela para poder ordernar pelo nome do campo
            DataView dw = this.tabelaDados.DefaultView;

            // TODO: Não esta ordenando
            // Ordenando a view pelo nome do campo ascendente
            dw.Sort = "campo ASC, condicao ASC";

            int ParentesesDoMesmoCampo = 0; //Indica quantos parenteses estão abertos para o campo
            int countCampo = 0; //Indica quantas vezes já passou pelo campo

            string operador = ""; //Armazena o operador selecionado
            string valor = ""; //Armazena o valor tratado para cada situação
            string conector = ""; //Armazena o conector da condição
            //percorrendo todas as linhas da view
            for (int i = 0; i < dw.Table.Rows.Count; i++)
            {
                if (dw.Table.Rows[i]["valor"].ToString() != "")//ignora a linha que o valor esta em branco
                {
                    #region Valor das Linhas Atual e Anterior
                    DataRow dr = dw.Table.Rows[i];
                    DataRow drAnterior;
                    if (i == 0)
                    {
                        drAnterior = dw.Table.NewRow();
                    }
                    else if (dw.Table.Rows[i - 1]["valor"].ToString() != "")
                    {
                        drAnterior = dw.Table.Rows[i - 1];
                    }
                    else
                    {
                        drAnterior = dw.Table.NewRow();
                    }
                    #endregion

                    #region Tratamento para agrupar os campos entre parenteses
                    //Fecha parenteses do campo anterior agrupado
                    if (dr["campo"].ToString() != drAnterior["campo"].ToString())
                    {
                        countCampo = 1;

                        if (i != 0)
                        {
                            //Fecha parenteses do mesmo campo que ficaram em aberto
                            for (int j = 0; j < ParentesesDoMesmoCampo; j++)
                            {
                                where.Append(")");
                            }
                        }
                        //Abre grupo do proximo campo
                        where.Append(" AND (");
                        ParentesesDoMesmoCampo = 1;
                    }
                    #endregion

                    #region Tratamento de Condicao
                    switch (dr["condicao"].ToString().ToLower())
                    {
                        case "igual":
                            operador = "=";
                            valor = dr["valor"].ToString();
                            break;

                        case "menor que":
                            operador = "<";
                            valor = dr["valor"].ToString();
                            break;

                        case "maior que":
                            operador = ">";
                            valor = dr["valor"].ToString();
                            break;

                        case "maior ou igual que":
                            operador = ">=";
                            valor = dr["valor"].ToString();
                            break;

                        case "menor ou igual que":
                            operador = "<=";
                            valor = dr["valor"].ToString();
                            break;

                        case "iniciado com":
                            operador = "LIKE";
                            valor = dr["valor"].ToString() + "%";
                            break;

                        case "terminado com":
                            operador = "LIKE";
                            valor = "%" + dr["valor"].ToString();
                            break;

                        case "que contenha":
                            operador = "LIKE";
                            valor = "%" + dr["valor"].ToString() + "%";
                            break;

                        case "diferente":
                            operador = "<>";
                            valor = dr["valor"].ToString();
                            break;

                        default: //Usa default como igual para nao ter problema
                            operador = "=";
                            valor = dr["valor"].ToString();
                            break;
                    }
                    #endregion

                    #region Valor do Campo Tratado

                    //Coloca o conector
                    conector = RetornaConector(dr["condicao"].ToString().ToLower(), countCampo);
                    where.Append(conector);

                    switch (dr["tipo"].ToString().ToLower())
                    {
                        case "t":
                            where.Append(dr["campo"].ToString() + " " + operador + " '" + valor + "'");
                            break;
                        case "n":
                            where.Append(dr["campo"].ToString() + " " + operador + " " + valor);
                            break;
                        case "d":
                            where.Append(dr["campo"].ToString() + " " + operador + " '" + DateTime.Parse(valor, culture).ToString("MM/dd/yyyy") + "'");//Com a Cultura "pt-BR" não havera erro de data
                            break;
                        default:
                            break;
                    }
                    #endregion

                    #region Fechar Parentes em Aberto
                    if (i == dw.Table.Rows.Count - 1)
                    {
                        //Fecha parenteses do mesmo campo que ficaram em aberto
                        for (int j = 0; j < ParentesesDoMesmoCampo; j++)
                        {
                            where.Append(")");
                        }
                    }
                    #endregion

                    countCampo = countCampo + 1;
                }
                else if (where.ToString() != "")
                {
                    where.Append(")");
                }
            }
            where.Append(" ");

            if (where.ToString() == " AND  ")//significa que é a primeira linha vazia
                where.Remove(0, where.Length - 1);

            if (!whereVazia)
                return where.ToString();
            else
                return " ";
        }

        #region RetornaConector
        private string RetornaConector(string condicao, int contadorCampo)
        {
            string conector = "";
            if (contadorCampo != 1)
            {
                if (condicao == "igual" || condicao == "iniciado com" || condicao == "terminado com" || condicao == "que contenha")
                    conector = " OR ";
                else
                    conector = " AND ";
            }
            return conector;
        }
        #endregion

        #endregion
    }
}
