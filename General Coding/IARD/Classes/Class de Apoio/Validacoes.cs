using System;
using System.Collections.Generic;
using System.Text;
using Classes;

namespace Classes
{
    public class Validacoes
    {
        #region Validar CPF/CNPJ
        public static bool Validar(string var)
        {
            string valor = var.Replace(".", "");

            valor = valor.Replace("/", "");

            valor = valor.Replace("-", "");

            valor = valor.Replace(",", "");

            long result;
            bool success;
            success = long.TryParse(valor, out result);
            if (success)
            {

                switch (valor.Length)
                {
                    case 11: return ValidaCPF(valor); break;
                    case 14: return ValidaCNPJ(valor); break;
                    default: return false;
                }
            }
            else
            { 
                return false; 
            }
        }

        public static bool ValidaCPF(string vrCPF)
        {
            string valor = vrCPF;
            bool igual = true;

            valor = valor.Replace(".", "");

            valor = valor.Replace("/", "");

            valor = valor.Replace("-", "");

            valor = valor.Replace(",", "");

            long result;
            bool success;
            success = long.TryParse(valor, out result);
            if (success)
            {

                for (int i = 1; i < 11 && igual; i++)

                    if (valor[i] != valor[0])

                        igual = false;



                if (igual || valor == "12345678909")

                    return false;



                int[] numeros = new int[11];



                for (int i = 0; i < 11; i++)

                    numeros[i] = int.Parse(

                      valor[i].ToString());



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
            else
                return false;
        }

        public static bool ValidaCNPJ(string vrCNPJ)
        {

            string CNPJ = vrCNPJ;

            string valor = CNPJ.Replace(".", "");

            valor = valor.Replace("/", "");

            valor = valor.Replace("-", "");

            valor = valor.Replace(",", "");

            long result;
            bool success;
            success = long.TryParse(valor, out result);
            if (success)
            {


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
            else
                return false;
        }

        public static bool ValidaCPR(string vrCPR)
        {
            // 5/8/13 - Peguei no site Sintegra http://www.sintegra.gov.br/Cad_Estados/cad_SC.html 

            string sCPR = vrCPR;
            string valor = sCPR.Replace(".", "");
            valor = valor.Replace("/", "");
            valor = valor.Replace("-", "");
            valor = valor.Replace(",", "");
            valor = "000000000" + valor;
            valor = valor.Substring(valor.Length-9, 9);

            long result;
            bool success;
            success = long.TryParse(valor, out result);
            if (success)
            {
                int[] digitos, soma, digitoscalculado;
                int nrDig;
                string ftmt;
                ftmt = "987654320";
                digitos = new int[9];
                digitoscalculado = new int[9];
                soma = new int[2];
                soma[0] = 0;
                soma[1] = 0;
                try
                {
                    for (nrDig = 0; nrDig < 9; nrDig++)
                    {
                        digitos[nrDig] = int.Parse(valor.Substring(nrDig, 1));
                        digitoscalculado[nrDig] = int.Parse(ftmt.Substring(nrDig, 1));
                        if (nrDig!=8)
                            soma[0] += ( digitos[nrDig] * digitoscalculado[nrDig] );
                    }
                    //
                    soma[1] = soma[0] % 11;
                    //soma[1] = 11 - soma[1];
                    //Resto se 9 digito 2, 1 é zero
                    if (soma[1] == 9)
                        soma[1] = 2;
                    else if (soma[1] == 1)
                        soma[1] = 0;
                    else if (soma[1] == 0)
                        soma[1] = 0;
                    else
                        soma[1] = 11 - soma[1];
                    //
                    if (soma[1] == digitos[8])
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }

        #endregion

        #region FormataComZero
        /// <summary>
        /// Acrecenta zeros a esquerda do numero 
        /// mantendo o tamanho length da string zeros
        /// </summary>
        /// <param name="zeros">qtde de length desejado</param>
        /// <param name="cod">codigo com lentgt inferiror ao do zeros</param>
        /// <returns></returns>
        public static string FormataComZero(string zeros, string cod)
        {
            string valor = cod;
            //adiciona o codigo no titulo da pagina
            //string zero = "000000";
            //string cod = CodigoChave.ToString();
            string codzero = zeros + cod;
            try
            {
                valor = codzero.Substring(cod.Length, zeros.Length);
            }
            catch (Exception) { }
            return valor;
        }
        #endregion
    }
}
