//
//  CUIT.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2016 Hamekoz - www.hamekoz.com.ar
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace Hamekoz.Argentina
{
	public static class CUIT
	{
		/// <summary>
		/// Devuelvo la candena 
		/// </summary>
		/// <param name="cuit">Cuit.</param>
		public static string Formato (this string cuit)
		{
			return cuit.Validar () ? cuit.Limpiar ().Insert (2, "-").Insert (11, "-") : cuit;
		}

		/// <summary>
		/// Limpia un CUIT dejando solo los caracteres numericos
		/// </summary>
		/// <param name="cuit">Cuit</param>
		public static string Limpiar (this string cuit)
		{
			return cuit.Replace ("-", string.Empty);
		}

		/// <summary>
		/// Validar un cuit aplicando el algortimo del calculo del digito verificador.
		/// </summary>
		/// <param name="cuit">Cuit</param>
		/// <remarks>Sigue siendo necesario consultar el cuit en el padron de afip para garantizar la validez</remarks>
		public static bool Validar (this string cuit)
		{
			if (cuit == null)
				return false;
			if (cuit == string.Empty)
				return false;
			cuit = cuit.Limpiar ();
			if (cuit.Length != 11)
				return false;
			if (!cuit.All (char.IsDigit))
				return false;

			int calculado = CalcularDigitoVerificador (cuit);
			int digito = int.Parse (cuit.Substring (10));
			return calculado == digito;
		}

		static int CalcularDigitoVerificador (string cuit)
		{
			int[] mult = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
			char[] nums = cuit.ToCharArray ();
			int total = 0;
			for (int i = 0; i < mult.Length; i++) {
				total += int.Parse (nums [i].ToString ()) * mult [i];
			}
			var resto = total % 11;
			return resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;
		}

		public static void ObtenerConstancia (this string cuit)
		{
			if (!cuit.Validar ())
				throw new FormatException ("El CUIT no es válido.");
			const string urlBase = "https://soa.afip.gob.ar/sr-padron/v1/constancia/";
			string fileName = string.Format ("{0}/Constancia-de-CUIT-{1}-{2:yyyyMMdd}.pdf", Path.GetTempPath (), cuit.Limpiar (), DateTime.Now);
			string url = string.Format ("{0}{1}", urlBase, cuit.Limpiar ());
			using (var webClient = new WebClient ()) {
				webClient.DownloadFile (url, fileName);
			}
			Process.Start (fileName);
		}
	}
}

