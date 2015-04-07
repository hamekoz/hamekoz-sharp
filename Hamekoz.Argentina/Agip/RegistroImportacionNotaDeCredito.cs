//
//  RegistroImportacionNotaDeCredito.cs
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
using Hamekoz.Interfaces;
using System.Collections.Generic;

namespace Hamekoz.Argentina.Agip
{
	public class RegistroImportacionNotaDeCredito
	{
		public TipoDeOperacion Operacion {
			get;
			set;
		}

		public long NroNotaDeCredito {
			get;
			set;
		}

		public DateTime FechaNotaDeCredito {
			get;
			set;
		}

		public decimal MontoNotaDeCredito {
			get;
			set;
		}

		string nroCertificadoPropio = string.Empty;

		public string NroCertificadoPropio {
			get {
				return nroCertificadoPropio;
			}
			set {
				nroCertificadoPropio = value;
			}
		}

		public TipoDeComprobante TipoDeComprobanteOrigenDeLaRetencion {
			get;
			set;
		}

		public string LetraDelComprobante {
			get;
			set;
		}

		public long NroDeComprobante {
			get;
			set;
		}

		public long NroDeDocumento {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the codigo de norma.
		/// </summary>
		/// <value>The codigo de norma.</value>
		/// <see cref="http://www.agip.gob.ar/web/agentes-recaudacion/ag-rec-arciba-codigo-de-normas.htm"/>
		public int CodigoDeNorma {
			get;
			set;
		}

		public DateTime FechaDeRetencionPercepcion {
			get;
			set;
		}

		public decimal RetecionPercepcionADeducir {
			get;
			set;
		}

		public float Alicuota {
			get;
			set;
		}

		/// <summary>
		/// Tos the fixed string.
		/// </summary>
		/// <returns>The fixed string.</returns>
		/// <see cref="http://www.agip.gov.ar/web/files/DocTecnicoImpoOperacionesDise%F1odeRegistro.pdf"/>
		public string ToFixedString ()
		{
			string cadena = string.Format ("{0:D1}{1:D12}{2:d}{3:0000000000000.00}{4}{5:D2}{6}{7:D16}{8:D11}{9:D3}{10:d}{11:0000000000000.00}{12:00.00}"
				, (int)this.Operacion
				, this.NroNotaDeCredito
				, this.FechaNotaDeCredito
				, this.MontoNotaDeCredito
				, this.NroCertificadoPropio.PadRight (16).Substring (0, 16)
				, (int)this.TipoDeComprobanteOrigenDeLaRetencion
				, this.LetraDelComprobante
				, this.NroDeComprobante
				, this.NroDeDocumento
				, this.CodigoDeNorma
				, this.FechaDeRetencionPercepcion
				, this.RetecionPercepcionADeducir
				, this.Alicuota
			                );
			if (cadena.Length != 119) {
				throw new Exception (string.Format ("La longitud del registro a exportar es incorrecta.\nNota de credito: {0}", this.NroNotaDeCredito));
			}
			return cadena;
		}
	}
}

