//
//  RegistroPadronUnificado.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2015 Hamekoz - www.hamekoz.com.ar
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
using System.Globalization;

namespace Hamekoz.Argentina.Agip
{
	/// <summary>
	/// Registro de padrón unificado.
	/// Validos para:
	/// - Padrón de Riesgo Fiscal
	/// - Padrón de contribuyentes exentos, de actividades promovidas, de nuevos emprendimientos y con alícuotas diferenciales.
	/// </summary>
	/// <see href="http://www.agip.gov.ar/web/files/DISENOODEREGISTROPADRONUNIFICADO.pdf"/>
	/// <seealso href="http://www.agip.gov.ar/web/banners-comunicacion/alto_riesgo_fiscal.htm"/>
	/// <seealso href="http://www.agip.gov.ar/web/agentes-recaudacion/padron-.html"/>
	public class RegistroPadronUnificado
	{
		readonly char separador = char.Parse (";");

		public DateTime FechaDePublicacion {
			get;
			set;
		}

		public DateTime FechaVigenciaDesde {
			get;
			set;
		}

		public DateTime FechaVigenciaHasta {
			get;
			set;
		}

		public long CUIT {
			get;
			set;
		}

		/// <summary>
		/// Obtiene el tipo de contribuyente inscripto.
		/// D -> Directo C.A.B.A
		/// C -> Convenio
		/// </summary>
		/// <value>El tipo de contribuyente inscripto.</value>
		public char TipoDeContribuyenteInscripto {
			get;
			set;
		}

		/// <summary>
		/// Obtiene la marca alta sujeto.
		/// S -> el sujeto se incorporo al padrón
		/// N ->
		/// B ->
		/// </summary>
		/// <value>Marca alta sujeto.</value>
		public char MarcaAltaSujeto {
			get;
			set;
		}

		public char MarcaAlicuota {
			get;
			set;
		}

		public double AlicuotaPercepcion {
			get;
			set;
		}

		public double AlicuotaRetencion {
			get;
			set;
		}

		public int NumeroGrupoPercepcion {
			get;
			set;
		}

		public int NumeroGrupoRetencion {
			get;
			set;
		}

		public string RazonSocial {
			get;
			set;
		}

		public RegistroPadronUnificado ()
		{
			
		}

		public RegistroPadronUnificado (string linea)
		{
			if (linea.Length < 61)
				throw new Exception ("Longitud de linea es menor a lo esperado, no se insertara. Verificar");
			if (linea.Length > 121)
				Console.WriteLine ("Longitud de linea es superior a lo esperado. Se insertara igual. Verificar");

			string[] split = linea.Split (separador);
			FechaDePublicacion = DateTime.ParseExact (split [0], "ddMMyyyy", CultureInfo.InvariantCulture);
			FechaVigenciaDesde = DateTime.ParseExact (split [1], "ddMMyyyy", CultureInfo.InvariantCulture);
			FechaVigenciaHasta = DateTime.ParseExact (split [2], "ddMMyyyy", CultureInfo.InvariantCulture);
			CUIT = long.Parse (split [3]);
			TipoDeContribuyenteInscripto = char.Parse (split [4]);
			MarcaAltaSujeto = char.Parse (split [5]);
			MarcaAlicuota = char.Parse (split [6]);
			AlicuotaPercepcion = double.Parse (split [7]);
			AlicuotaRetencion = double.Parse (split [8]);
			NumeroGrupoPercepcion = int.Parse (split [9]);
			NumeroGrupoPercepcion = int.Parse (split [10]);
			RazonSocial = split [11];
		}
	}
}

