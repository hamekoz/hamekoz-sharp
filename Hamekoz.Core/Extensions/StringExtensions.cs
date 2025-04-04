//
//  StringExtensions.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <rodrigo@hamekoz.com.ar>
//		 Ezequiel Taranto <ezequiel89@gmail.com>
//       Juan Angel Dinamarca <juan.angel.dinamarca@gmail.com>
//
//  Copyright (c) 2014 Hamekoz -  www.hamekoz.com.ar
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Hamekoz.Extensions
{
    public static class StringExtensions
    {
        //TODO VERIFICAR QUE SEA EL METODO CORRECTO
        public static string ToBasicASCII(this string texto)
        {
            const string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ–";
            const string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC-";
            var textoSinAcentos = new StringBuilder(texto.Length);
            int indexConAcento;
            foreach (char caracter in texto)
            {
                indexConAcento = consignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos.Append(sinsignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos.Append(caracter);
            }
            return textoSinAcentos.ToString();
        }

        static readonly Regex emailRegex = new Regex(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.IgnoreCase);

        public static bool CheckEmailFormat(this string email)
        {
            return string.IsNullOrWhiteSpace(email) || emailRegex.IsMatch(email.Trim());
        }

        //UNDONE revisar logica y reemplazar por algo mas prolijo
        public static bool IsNumeric(this string theValue)
        {
            try
            {
                Convert.ToDouble(theValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene parte izquierda de una cadena
        /// </summary>
        /// <param name="param"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string param, int length)
        {
            string result = param;
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            if (param.Length > length)
                result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }

        /// <summary>
        /// obtiene parte derecha de una cadena
        /// </summary>
        /// <param name="param"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}