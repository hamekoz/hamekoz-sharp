//
//  RegistroImportacionRetencionPercepcion.cs
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

namespace Hamekoz.Argentina.Agip
{
	public class RegistroImportacionRetencionPercepcion
	{
		public TipoDeOperacion Operacion {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the codigo de norma.
		/// </summary>
		/// <value>The codigo de norma.</value>
		/// <see href="http://www.agip.gob.ar/web/agentes-recaudacion/ag-rec-arciba-codigo-de-normas.htm"/>
		public int CodigoDeNorma {
			get;
			set;
		}

		public DateTime FechaRetencionPercepcion {
			get;
			set;
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

		public DateTime FechaDelComprobante {
			get;
			set;
		}

		public decimal MontoDelComprobante {
			get;
			set;
		}

		string nroDeCertificadoPropio = string.Empty;

		public string NroDeCertificadoPropio {
			get {
				return nroDeCertificadoPropio;
			}
			set {
				nroDeCertificadoPropio = value;
			}
		}

		public TipoDeDocumento TipoDeDocumentoDelRetenido {
			get;
			set;
		}

		public long NroDeDocumentoDelRetenido {
			get;
			set;
		}

		public SituacionIngresosBrutos SituacionIngresosBrutosDelRetenido {
			get;
			set;
		}

		public long NroInscripcionIngresosBrutosDelRetenido {
			get;
			set;
		}

		public SituacionFrenteAlIVA SituacionFrenteAlIVADelRetenido {
			get;
			set;
		}

		string razonSocialDelRetenido = string.Empty;

		public string RazonSocialDelRetenido {
			get {
				return razonSocialDelRetenido;
			}
			set {
				razonSocialDelRetenido = value;
			}
		}

		public decimal ImporteOtrosConceptos {
			get;
			set;
		}

		public decimal ImporteIVA {
			get;
			set;
		}

		public decimal MontoSujetoARetencionPercepcion {
			get;
			set;
		}

		public float Alicuota {
			get;
			set;
		}

		public decimal RetencionPercepcionPracticada {
			get;
			set;
		}

		public decimal MontoTotalRetenidoPercibido {
			get;
			set;
		}

		string domicilio = string.Empty;

		/// <summary>
		/// Gets or sets the domicilio.
		/// Opcional para completar los datos del certificado de retencion
		/// </summary>
		/// <value>The domicilio.</value>
		public string Domicilio {
			get {
				return domicilio;
			}
			set {
				domicilio = value;
			}
		}

		/// <summary>
		/// Tos the fixed string.
		/// </summary>
		/// <returns>The fixed string.</returns>
		/// <see href="http://www.agip.gov.ar/web/files/DocTecnicoImpoOperacionesDise%F1odeRegistro.pdf"/>
		public string ToFixedString ()
		{
			if (MontoSujetoARetencionPercepcion != (MontoDelComprobante - ImporteIVA - ImporteOtrosConceptos)) {
				throw new Exception (string.Format ("El monto sujeto no es correcto.\nRazon Social: {0}\nComprobante: {1}", RazonSocialDelRetenido, NroDeComprobante));
			}

			string cadena = string.Format ("{0:D1}{1:D3}{2:d}{3:D2}{4}{5:D16}{6:d}{7:0000000000000.00}{8}{9:D1}{10:D11}{11:D1}{12:D11}{13:D1}{14}{15:0000000000000.00}{16:0000000000000.00}{17:0000000000000.00}{18:00.00}{19:0000000000000.00}{20:0000000000000.00}"
				, (int)Operacion
				, CodigoDeNorma
				, FechaRetencionPercepcion
				, (int)TipoDeComprobanteOrigenDeLaRetencion
				, LetraDelComprobante
				, NroDeComprobante
				, FechaDelComprobante
				, MontoDelComprobante
				, NroDeCertificadoPropio.PadRight (16).Substring (0, 16)
				, (int)TipoDeDocumentoDelRetenido
				, NroDeDocumentoDelRetenido
				, (int)SituacionIngresosBrutosDelRetenido
				, NroInscripcionIngresosBrutosDelRetenido
				, (int)SituacionFrenteAlIVADelRetenido
				, RazonSocialDelRetenido.PadRight (30).Substring (0, 30)
				, ImporteOtrosConceptos
				, ImporteIVA
				, MontoSujetoARetencionPercepcion
				, Alicuota
				, RetencionPercepcionPracticada
				, MontoTotalRetenidoPercibido
			                );
			if (cadena.Length != 215) {
				throw new Exception (string.Format ("La longitud del registro a exportar es incorrecta.\nRazon Social: {0}\nComprobante: {1}", RazonSocialDelRetenido, NroDeComprobante));
			}

			return cadena;
		}
	}
}