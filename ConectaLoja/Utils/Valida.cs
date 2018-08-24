using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConectaLoja.Utils
{
    public static class Valida
    {
        public static int LimpaString(string campo)
        {
            campo = campo.Replace("[", "").Replace("]", "").Replace("\"", "");
            return Convert.ToInt32(campo);
        }


        /// <summary>
        /// Valida um CPF de uma pessoa fisica
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public static bool CPF(string CPF)
        {
            // Considera cpf em branco como inválido
            if (CPF == "") return false;

            CPF = CPF.Trim();
            CPF = CPF.Replace(".", "").Replace("-", "").Replace("/", "");

            if (CPF.Length != 11) return false;

            string NumCpfCalc = CPF.Substring(0, 9);
            for (int i = 1; i <= 2; i++)
            {
                int Mod = 2;
                int Total = 0;
                for (int j = NumCpfCalc.Length - 1; j >= 0; j--)
                {
                    int SubTotal = int.Parse(NumCpfCalc.Substring(j, 1)) * Mod;
                    Total += SubTotal;
                    Mod++;
                }

                int DV = 11 - (Total % 11);
                if (DV > 9) DV = 0;
                NumCpfCalc += DV.ToString();
            }

            return (NumCpfCalc == CPF);
        }

        /// <summary>
        /// Valida um CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public static bool CNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        /// <summary>
        /// Valida um e-mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {

            // Expressão regular que vai validar os e-mails
            string emailRegex = @"^(([^<>()[\]\\.,;áàãâäéèêëíìîïóòõôöúùûüç:\s@\""]+"
            + @"(\.[^<>()[\]\\.,;áàãâäéèêëíìîïóòõôöúùûüç:\s@\""]+)*)|(\"".+\""))@"
            + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|"
            + @"(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

            // Instância da classe Regex, passando como 
            // argumento sua Expressão Regular 
            Regex rx = new Regex(emailRegex);

            // Método IsMatch da classe Regex que retorna
            // verdadeiro caso o e-mail passado estiver
            // dentro das regras da sua regex.
            return rx.IsMatch(email);
        }

        public static bool IsNomeMac(string nome)
        {
            // Expressão regular
            string valida = @"^([A-Z]{1})([A-Z0-9]{0,14})$";

            // Instância da classe Regex, passando como 
            // argumento sua Expressão Regular 
            Regex rx = new Regex(valida);

            return rx.IsMatch(nome);
        }

        public static bool IsBoleano(object value)
        {
            bool resposta = true;

            try
            {
                bool inteiro = Convert.ToBoolean(value);
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                resposta = false;
            }
            return resposta;
        }

        public static bool IsNumeric(object value)
        {
            bool resposta = true;

            try
            {
                int inteiro = Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                resposta = false;
            }
            return resposta;
        }

        public static bool IsNumeroEVirgula(string valor)
        {
            string expressao = "^([0-9]+(\\-[0-9]+)*,*)+$";
            Regex rx = new Regex(expressao);
            return rx.IsMatch(valor);
        }

        public static bool IsNumericLong(object value)
        {
            bool resposta = true;

            try
            {
                long inteiro = Convert.ToInt64(value);
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                resposta = false;
            }
            return resposta;
        }

        public static bool IsDouble(object value)
        {
            bool resposta = true;

            try
            {
                double inteiro = Convert.ToDouble(value);
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                resposta = false;
            }
            return resposta;
        }

        public static bool IsDecimal(object value)
        {
            bool resposta = true;

            try
            {
                decimal inteiro = Convert.ToDecimal(value);
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                resposta = false;
            }
            return resposta;
        }

        public static bool IsCep(string value)
        {
            int count = 0;
            value = value.Replace("-", "");
            if (value.Length != 8)
                return false;

            foreach (char c in value)
                if (Char.IsDigit(c))
                    count++;

            if (count != 8)
                return false;

            return true;
        }


        public static bool IsDate(object value)
        {
            bool resposta = true;
            if (value == null)
                return false;

            try
            {
                DateTime data = Convert.ToDateTime(value);
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                resposta = false;
            }
            return resposta;
        }

        public static bool IsString(object value)
        {
            bool resposta = true;
            try
            {
                string str = Convert.ToString(value);
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                resposta = false;
            }
            return resposta;
        }

    }
}
