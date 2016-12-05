//  RegistroPadron.cs
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
namespace Hamekoz.Argentina.Afip
{
	/// <summary>
	/// Registro padron de condicion impositiva.
	/// </summary>
	/// <see href="http://www.afip.gob.ar/genericos/cInscripcion/archivoCompleto.asp"/>
	public class RegistroPadron
	{
		public RegistroPadron ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Hamekoz.Argentina.Afip.RegistroPadron"/> class.
		/// </summary>
		/// <param name="linea">Linea del archivo</param>
		/// <param name="denominacion">Si es <c>true</c> carga la denominacion.</param>
		public RegistroPadron (string linea, bool denominacion)
		{
			int offset = 0;
			Denominacion = string.Empty;
			if (denominacion) {
				offset = 30;
				Denominacion = linea.Substring (11, 30);
			}
			CUIT = long.Parse (linea.Substring (0, 11));
			ImpuestoGanancias = linea.Substring (11 + offset, 2);
			ImpuestoIVA = linea.Substring (13 + offset, 2);
			Monotributo = linea.Substring (15 + offset, 2);
			IntegranteSociedad = linea.Substring (17 + offset, 1);
			Empleador = linea.Substring (18 + offset, 1);
			ActividadMonotributo = linea.Substring (19 + offset, 2);
		}

		public long CUIT {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the denominacion o razon social.
		/// </summary>
		/// <value>La denominacion o razon social.</value>
		/// <remarks>Longitud 11</remarks>
		public string Denominacion {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the impuesto ganancias.
		/// 'NI' , 'AC','EX', 'NC'
		/// Referencias:
		/// 'NI', 'N' = No Inscripto
		/// 'AC', 'S' = Activo
		///	'EX' = Exento
		///	'NA' = No alcanzado
		///	'XN' = Exento no alcanzado
		///	'AN' = Activo no alcanzado
		///	'NC' = No corresponde
		/// </summary>
		/// <value>The impuesto ganancias.</value>
		public string ImpuestoGanancias {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the impuesto IVA.
		/// 'NI' , 'AC','EX','NA','XN','AN'
		/// Referencias:
		/// 'NI', 'N' = No Inscripto
		/// 'AC', 'S' = Activo
		///	'EX' = Exento
		///	'NA' = No alcanzado
		///	'XN' = Exento no alcanzado
		///	'AN' = Activo no alcanzado
		///	'NC' = No corresponde
		/// </summary>
		/// <value>The impuesto IVA.</value>
		public string ImpuestoIVA {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the monotributo.
		/// 'NI' , "Codigo categoria tributaria"
		/// 'NI', 'N' = No Inscripto
		/// </summary>
		/// <value>The monotributo.</value>
		public string Monotributo {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the integrante sociedad.
		/// 'S', 'N'
		/// </summary>
		/// <value>The integrante sociedad.</value>
		public string IntegranteSociedad {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the empleador.
		/// 'S', 'N'
		/// </summary>
		/// <value>S o N</value>
		public string Empleador {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the actividad monotributo.
		/// 'S', 'N'
		/// </summary>
		/// <value>S o N</value>
		public string ActividadMonotributo {
			get;
			set;
		}
	}
}

