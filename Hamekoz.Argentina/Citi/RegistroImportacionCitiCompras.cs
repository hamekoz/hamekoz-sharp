//
//  RegistroImportacionCitiCompras.cs
//
//  Author:
//       Mariano Ripa <ripamariano@gmail.com>
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

namespace Hamekoz.Argentina.Citi
{
	/// <summary>
	/// Registro importacion citi compras.
	/// </summary>
	/// <see href="http://www.afip.gob.ar/comprasyventas/"/>
	/// <see href="http://www.afip.gob.ar/comprasyventas/documentos/RegimendeInformaciondeComprasyVentasDisenosdeRegistros1.xls"/>
	public class RegistroImportacionCitiCompras
	{
		public DateTime FechaContable {
			get;
			set;
		}

		public string TipoComprobante {
			get;
			set;
		}

		public string PuntoVenta {
			get;
			set;
		}

		public string NroComprobante {
			get;
			set;
		}

		public string Despacho {
			get;
			set;
		}

		public string CodDocumento {
			get;
			set;
		}

		public string NroDocumento {
			get;
			set;
		}

		public string RazonSocial {
			get;
			set;
		}


		public string Total {
			get;
			set;
		}

		public string Neto {
			get;
			set;
		}

		public string NetoDif1 {
			get;
			set;
		}

		public string NetoDif2 {
			get;
			set;
		}

		public string Exento {
			get;
			set;
		}

		public string IVA {
			get;
			set;
		}

		public string IVADif1 {
			get;
			set;
		}

		public string IVADif2 {
			get;
			set;
		}

		public string PercepcionesACuentaIVA {
			get;
			set;
		}

		public string PercepcionesACuentaOtros {
			get;
			set;
		}

		public string PercepcionesIB {
			get;
			set;
		}

		public string PercepcionesMunicipales {
			get;
			set;
		}

		public string ImpuestosInternos {
			get;
			set;
		}

		public string CodigoMoneda {
			get;
			set;
		}


		public string TipoDeCambio {
			get;
			set;
		}

		/// <summary>
		/// Cantidad de alicuotas de IVA
		/// </summary>
		/// <value>0 a 9</value>
		public int Alicuotas {
			get;
			set;
		}

		public string CodigoOperacion {
			get;
			set;
		}

		public string CreditoFiscal {
			get;
			set;
		}

		public string OtrosTributos {
			get;
			set;
		}

		public string CUITEmisor {
			get;
			set;
		}

		public string DenominacionEmisor {
			get;
			set;
		}

		public string IVAComision {
			get;
			set;
		}

		public string IVAAlicuota {
			get;
			set;
		}

		public string NoInscripto {
			get;
			set;
		}

		public string ToFixedString ()
		{

			string cadena = string.Format ("{0:yyyyMMdd}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}"
				, FechaContable
				, TipoComprobante
				, PuntoVenta
				, NroComprobante
				, Despacho
				, CodDocumento
				, NroDocumento
				, RazonSocial
				, Total
				, NoInscripto
				, Exento
				, PercepcionesACuentaIVA
				, PercepcionesACuentaOtros
				, PercepcionesIB
				, PercepcionesMunicipales
				, ImpuestosInternos
				, CodigoMoneda
				, TipoDeCambio
				, Alicuotas
				, CodigoOperacion
				, CreditoFiscal
				, OtrosTributos
				, CUITEmisor
				, DenominacionEmisor
				, IVAComision

			                );
			if (cadena.Length != 325) {
				throw new Exception (string.Format ("La longitud del registro a exportar es incorrecta."));
			}

			return cadena;
		}


		public string ToFixedStringAlicuotas ()
		{
			string cadena = string.Format ("{0}{1}{2}{3}{4}{5}{6}{7}"				
				, TipoComprobante
				, PuntoVenta
				, NroComprobante
				, CodDocumento
				, NroDocumento
				, Neto
				, IVAAlicuota //puede ser 10.5 21 o 27%
				, IVA
			                );
			if (cadena.Length != 84) {
				throw new Exception (string.Format ("La longitud del registro a exportar es incorrecta."));
			}

			return cadena;
		}
	}
}

