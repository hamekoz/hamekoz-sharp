//
//  RegistroPadronUnificado.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2015 Hamekoz
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

namespace Hamekoz.Argentina.Arba
{
	/// <summary>
	/// Registro de padrón unificado.
	/// </summary>
	/// <see cref="http://www.arba.gov.ar/archivos/Publicaciones/regimen%20de%20rec%20x%20sujeto_nuevo%20dise%C3%B1o%20padr%C3%B3n.pdf"/>
	/// <seealso cref="http://www.arba.gov.ar/Informacion/IBrutos/LinksIIBB/RegimenSujeto.asp"/>
	public class RegistroPadronUnificado
	{
		private readonly char separador = char.Parse(";");

		/// <summary>
		/// Gets or sets the regimen.
		/// R o P según se trate del régimen de Retención o Percepción
		/// </summary>
		/// <value>The regimen.</value>
		public char Regimen {
			get;
			set;
		}

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
		/// "C" Convenio Multilateral
		/// "D" Directo Pcia de Bs. As
		/// </summary>
		/// <value>El tipo de contribuyente inscripto.</value>
		public char TipoDeContribuyenteInscripto {
			get;
			set;
		}

		/// <summary>
		/// Obtiene la marca alta baja sujeto.
		/// S o N
		/// "S" indica que el sujeto se incorpora al padrón
		/// "B" indica Baja.
		/// </summary>
		/// <value>Marca alta sujeto.</value>
		public char MarcaAltaBajaSujeto {
			get;
			set;
		}

		/// <summary>
		/// Gets the marca cambio alicuota.
		/// S o N indica si hubo o no cambio de alícuota
		/// con respecto al padrón anterior
		/// </summary>
		/// <value>The marca cambio alicuota.</value>
		public char MarcaCambioAlicuota {
			get;
			private set;
		}

		public double Alicuota {
			get;
			set;
		}

		public int NumeroGrupo {
			get;
			set;
		}

		public RegistroPadronUnificado (string linea)
		{
			if (linea.Length != 55 ) {
				throw new Exception ("Longitud de linea incorrecta. Verificar");
			}
			string[] split = linea.Split (separador);
			Regimen = char.Parse (split [0]);
			FechaDePublicacion = DateTime.ParseExact(split [1], "ddMMyyyy", CultureInfo.InvariantCulture);
			FechaVigenciaDesde = DateTime.ParseExact(split [2], "ddMMyyyy", CultureInfo.InvariantCulture);
			FechaVigenciaHasta = DateTime.ParseExact(split [3], "ddMMyyyy", CultureInfo.InvariantCulture);
			CUIT = long.Parse (split [4]);
			TipoDeContribuyenteInscripto = char.Parse (split [5]);
			MarcaAltaBajaSujeto = char.Parse (split [6]);
			MarcaCambioAlicuota = char.Parse (split [7]);
			Alicuota = double.Parse (split [8]);
			NumeroGrupo =  int.Parse (split [9]);
		}
	}
}

