//
//  RegistroImportacionCitiVentas.cs
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
	public class RegistroImportacionCitiVentas
	{

		/*Fecha	Tipo	Letra	Comprobante	Nombre	TipoDoc	NroDoc	Neto	IVA 21%	Percepción	Percepción IIBB	No Grabados	Percepción Municipal	Exento	Total*/

		public DateTime Fecha {
			get;
			set;
		}

		public string Tipo {
			get;
			set;
		}

		public string Comprobante {
			get;
			set;
		}

		public string PuntoVenta {
			get;
			set;
		}

		public string RazonSocial {
			get;
			set;
		}

		public string TipoDocumento {
			get;
			set;
		}

		public string NroDocumento {
			get;
			set;
		}

		public string Neto {
			get;
			set;
		}

		public string IVA {
			get;
			set;
		}



		public string NoGrabados {
			get;
			set;
		}

		public string PercepcionMunicipal {
			get;
			set;
		}

		public string PercepcionesIB {
			get;
			set;
		}

		public string PercepcionesIVA {
			get;
			set;
		}

		public string Exento {
			get;
			set;
		}

		public string Total {
			get;
			set;
		}


		public string PercepcionNoCategorizados {
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

		public int CantidadAlicuotaIVA {
			get;
			set;
		}

		public string CodigoOperacion {
			get;
			set;
		}

		public string CodigoCiti {
			get;
			set;
		}

		public string OtrosTributos {
			get;
			set;
		}

		public string FechaVencimiento {
			get;
			set;
		}

		public string IVAAlicuota {
			get;
			set;
		}


		public string ToFixedString ()
		{


			string cadena = string.Format ("{0:yyyyMMdd}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}"
				, Fecha
				, CodigoCiti
				, PuntoVenta
				, Comprobante
				, Comprobante //Hasta
				, TipoDocumento
				, NroDocumento	
				, RazonSocial
				, Total
				, "000000000000000"
				, PercepcionNoCategorizados
				, Exento
				, PercepcionesIVA
				, PercepcionesIB
				, PercepcionMunicipal
				, ImpuestosInternos
				, CodigoMoneda
				, TipoDeCambio
				, CantidadAlicuotaIVA
				, CodigoOperacion
				, OtrosTributos
				, FechaVencimiento
			                );
			if (cadena.Length != 266) {
				throw new Exception (string.Format ("La longitud del registro a exportar es incorrecta."));
			}

			return cadena;
		}



		public string ToFixedStringAlicuotas ()
		{


			string cadena = string.Format ("{0}{1}{2}{3}{4}{5}"				
				, CodigoCiti
				, PuntoVenta
				, Comprobante
				, Neto
				, IVAAlicuota //simpre al 21% o Exento
				, IVA
			                );
			if (cadena.Length != 62) {
				throw new Exception (string.Format ("La longitud del registro a exportar es incorrecta."));
			}

			return cadena;
		}



	}
}

