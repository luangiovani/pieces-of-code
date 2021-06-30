using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Framework.Domain.Utils
{
    public class Util
    {
        public static string FormataCNPJ(string cnpj)
        {
            cnpj = cnpj.Trim();

            if (cnpj.Length == 14)
                return Convert.ToUInt64(cnpj.Replace(" ", "")).ToString(@"00\.000\.000\/0000\-00");

            return cnpj;
        }

        public static string FormataCEP(string cep)
        {
            cep = cep.Trim();

            if (cep.Length == 8)
                return Convert.ToUInt64(cep.Replace(" ", "")).ToString(@"00\.000\-000");

            return cep;
        }

        public static string GerarSenhaRandomica()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());

            return result;
        }

        #region RemoveNaoNumericos
        /// <summary>
        /// Remove caracteres não numéricos
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveNaoNumericos(string text)
        {
            var reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }
        #endregion

        #region ValidaCPF
        /// <summary>
        /// Valida se um cpf é válido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool ValidaCPF(string cpf)
        {
            //Remove formatação do número, ex: "123.456.789-01" vira: "12345678901"
            cpf = RemoveNaoNumericos(cpf);

            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                    return false;

            return true;
        }
        #endregion

        #region ValidaCNPJ
        public static bool ValidaCNPJ(string vrCNPJ)
        {
            string CNPJ = RemoveNaoNumericos(vrCNPJ);

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));

                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);

                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));
                }

                return (CNPJOk[0] && CNPJOk[1]);

            }
            catch
            {
                return false;
            }
        }
        #endregion

        public static string GetAreaIdDashByName(string name)
        {
            string retorno = String.Empty;
            switch (name)
            {
                case "Unassigned": retorno = "73726542-98ED-4067-A3BC-3FA12BF9A3B6"; break;
                case "Delay": retorno = "D8E01FE8-3341-4414-859E-774866E0678D"; break;
                case "Quoted": retorno = "7E97A69A-1110-4DF6-AC8C-7EEDE001C1A3"; break;
                case "Folow Up": retorno = "388CB45B-286E-4D23-B094-AD59B35BF4EC"; break;
                case "Wait Quoting": retorno = "E9E7AD45-3775-4C30-95F6-B110A161E749"; break;
                case "Go Ahead": retorno = "2B23988C-B65A-4E85-94D5-BC6998CE3965"; break;
                case "New": retorno = "35B3CB40-4F3C-4C50-A3FA-EC8A8537CA9D"; break;
                default: retorno = String.Empty; break;
            }

            return retorno;
        }
    }

    public enum WhoIsContact
    {
        Agent = 1,
        Customer = 2,
        ServiceOrder = 3,
        Supplier = 4
    }

    public enum TypeOfContactEnum
    {
        /// <summary>
        /// Contato principal
        /// </summary>
        Principal = 1,
        /// <summary>
        /// Contato Secundário
        /// </summary>
        Secondary = 2,
        /// <summary>
        /// Recados
        /// </summary>
        Message = 3
    }

    public enum TypeOfNoteEnum
    {
        /// <summary>
        /// Nota usada para inserir na TimeLine da Ordem de Serviço
        /// </summary>
        TimeLine = 1,
        /// <summary>
        /// Nota usada para LOG
        /// </summary>
        LOG = 2,
        /// <summary>
        /// Nota usada quando usuário insere alguma observação
        /// </summary>
        UserNote = 3,
        /// <summary>
        /// Nota usada para registrar as observações de Orçamento
        /// </summary>
        Quote = 4
    }

    public enum TypeOfMovementEnum
    {
        In = 1,
        Out = 2,
        Transit = 3,
        Late = 4,
        Other = 5
    }

    public enum ServiceOrderStatusEnum
    {
        //Status
        Closed = 1,
        Declined = 2,
        GoAhead = 3,
        Incoming = 4,
        Pending = 8,
        Quoted = 9,
        Recovered = 10,
        Unrecovered = 12,
        UnrecoveredDOA = 13,
        WaitingDestination = 14,
        WaitingforQuotation = 15,
        Transfer = 16
    }

    public enum PrincipalLocationsEnum
    {
        CBLSaoPaulo = 5,
        CBLCuritiba = 6,
        CBLRECIFE = 7,
        CBLRiodeJaneiro = 8,
        CBLPortoAlegre = 10,
        CBLBrasilia = 11,
        CBLBeloHorizonte = 12,
        CBLSalvador = 13,
        CBLManaus = 14,
        CBLCampinas = 15,
        Atlanta = 16,
        CBLSantaCatarinaParaSul = 17,
        CBLRiodeJaneiro2 = 18,
        CBLRiodeJaneiro3 = 19,
        CBLEspiritoSanto = 20
    }

    public class Conversor
    {
        // O método EscreverExtenso recebe um valor do tipo decimal
        public static string EscreverExtenso(decimal valor)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;
                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += Escrever_Valor_Extenso(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " Trilhão" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " Trilhões" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " Bilhão" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " Bilhões" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " Milhão" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " Milhões" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " Mil" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " e " : string.Empty);
                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "Bilhão" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "Milhão")
                                valor_por_extenso += " de";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "Bilhões" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "Milhões"
| valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "Trilhões")
                                    valor_por_extenso += " de";
                                else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "Trilhões")
                                        valor_por_extenso += " de";
                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " Real";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " Reais";
                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " e ";
                    }
                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " Centavo";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " Centavos";
                }
                return valor_por_extenso;
            }
        }
        static string Escrever_Valor_Extenso(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));
                if (a == 1) montagem += (b + c == 0) ? "Cem" : "Cento";
                else if (a == 2) montagem += "Duzentos";
                else if (a == 3) montagem += "Trezentos";
                else if (a == 4) montagem += "Quatrocentos";
                else if (a == 5) montagem += "Quinhentos";
                else if (a == 6) montagem += "Seiscentos";
                else if (a == 7) montagem += "Setecentos";
                else if (a == 8) montagem += "Oitocentos";
                else if (a == 9) montagem += "Novecentos";
                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " e " : string.Empty) + "Dez";
                    else if (c == 1) montagem += ((a > 0) ? " e " : string.Empty) + "Onze";
                    else if (c == 2) montagem += ((a > 0) ? " e " : string.Empty) + "Doze";
                    else if (c == 3) montagem += ((a > 0) ? " e " : string.Empty) + "Treze";
                    else if (c == 4) montagem += ((a > 0) ? " e " : string.Empty) + "Quatorze";
                    else if (c == 5) montagem += ((a > 0) ? " e " : string.Empty) + "Quinze";
                    else if (c == 6) montagem += ((a > 0) ? " e " : string.Empty) + "Dezesseis";
                    else if (c == 7) montagem += ((a > 0) ? " e " : string.Empty) + "Dezessete";
                    else if (c == 8) montagem += ((a > 0) ? " e " : string.Empty) + "Dezoito";
                    else if (c == 9) montagem += ((a > 0) ? " e " : string.Empty) + "Dezenove";
                }
                else if (b == 2) montagem += ((a > 0) ? " e " : string.Empty) + "Vinte";
                else if (b == 3) montagem += ((a > 0) ? " e " : string.Empty) + "Trinta";
                else if (b == 4) montagem += ((a > 0) ? " e " : string.Empty) + "Quarenta";
                else if (b == 5) montagem += ((a > 0) ? " e " : string.Empty) + "Cinquenta";
                else if (b == 6) montagem += ((a > 0) ? " e " : string.Empty) + "Sessenta";
                else if (b == 7) montagem += ((a > 0) ? " e " : string.Empty) + "Setenta";
                else if (b == 8) montagem += ((a > 0) ? " e " : string.Empty) + "Oitenta";
                else if (b == 9) montagem += ((a > 0) ? " e " : string.Empty) + "Noventa";
                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " e ";
                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "Um";
                    else if (c == 2) montagem += "Dois";
                    else if (c == 3) montagem += "Três";
                    else if (c == 4) montagem += "Quatro";
                    else if (c == 5) montagem += "Cinco";
                    else if (c == 6) montagem += "Seis";
                    else if (c == 7) montagem += "Sete";
                    else if (c == 8) montagem += "Oito";
                    else if (c == 9) montagem += "Nove";
                return montagem;
            }
        }
    }
}