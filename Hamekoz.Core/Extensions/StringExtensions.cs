//
//  StringExtensions.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <rodrigo@hamekoz.com.ar>
//		 Ezequiel Taranto <ezequiel89@gmail.com>
//       Juan Angel Dinamarca <juan.angel.dinamarca@gmail.com>
//
//  Copyright (c) 2014 Hamekoz
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
using System.Text;
using System.Text.RegularExpressions;

namespace Hamekoz.Extensions
{
	public static class StringExtensions
	{
		//TODO VERIFICAR QUE SEA EL METODO CORRECTO
		public static string ToBasicASCII (this string texto)
		{
			const string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
			const string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";
			var textoSinAcentos = new StringBuilder (texto.Length);
			int indexConAcento;
			foreach (char caracter in texto) {
				indexConAcento = consignos.IndexOf (caracter);
				if (indexConAcento > -1)
					textoSinAcentos.Append (sinsignos.Substring (indexConAcento, 1));
				else
					textoSinAcentos.Append (caracter);
			}
			return textoSinAcentos.ToString ();
		}

		private static readonly Regex emailRegex = new Regex (@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.IgnoreCase);

		public static bool CheckEmailFormat (this string email)
		{
			if (!string.IsNullOrWhiteSpace (email)) {
				return emailRegex.IsMatch (email.Trim ());
			}
			return false;
		}
	
	}
}