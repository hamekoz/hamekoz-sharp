//
//  Agip.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
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
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hamekoz.Argentina
{
	public class Agip
	{
		public Agip ()
		{
		}

		public static void Exportar(List<RegistroImportacionRetencionPercepcion> registros, string archivo)
		{
			StreamWriter sw = File.CreateText (archivo);
			foreach (var registro in registros) {
				sw.WriteLine (registro.ToFixedString ());
			}
			sw.Close ();
		}

		public static void Exportar(List<RegistroImportacionNotaDeCredito> registros, string archivo)
		{

			StreamWriter sw = File.CreateText (archivo);
			foreach (var registro in registros) {
				sw.WriteLine (registro.ToFixedString ());
			}
			sw.Close ();
		}

		public enum TipoDeOperacion
		{
			Percepcion = 1,
			Retencion = 2,
		}

		public enum TipoDeComprobante
		{
			Factura = 1,
			NotaDeDebito = 2,
			OrdenDePago = 3,
			BoletaDeDeposito = 4,
			LiquidacionDePago = 5,
			CertificadoDeObra = 6,
			Recibo = 7,
			ContDeLocacionDeServicio = 8,
			OtroComprobante = 9,
		}

		public enum Letra {
			A = 'A',
			B = 'B',
			C = 'C',
			M = 'M',
		}

		public enum TipoDeDocumento
		{
			CUIT = 3,
			CUIL = 2,
			CDI = 1,
		}

		public enum SituacionIngresosBrutos
		{
			Local = 1,
			ConvenioMultilateral = 2,
			NoInscripto = 4,
			RegimenSimplificado = 5,
		}

		public enum SituacionFrenteAlIVA
		{
			ResponsableInscripto = 1,
			Excento = 2,
			Monotributo = 4,
		}

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

			public Letra LetraDelComprobante {
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

			public string ToFixedString()
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
					throw new Exception (string.Format("La longitud del registro a exportar es incorrecta.\nNota de credito: {0}", this.NroNotaDeCredito));
				}
				return cadena;
			}
		}

		public class RegistroImportacionRetencionPercepcion
		{
			public TipoDeOperacion Operacion {
				get;
				set;
			}

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

			public Letra LetraDelComprobante {
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

			public string ToFixedString()
			{
				string cadena = string.Format ("{0:D1}{1:D3}{2:d}{3:D2}{4}{5:D16}{6:d}{7:0000000000000.00}{8}{9:D1}{10:D11}{11:D1}{12:D11}{13:D1}{14}{15:0000000000000.00}{16:0000000000000.00}{17:0000000000000.00}{18:00.00}{19:0000000000000.00}{20:0000000000000.00}"
					, (int)this.Operacion
					, this.CodigoDeNorma
					, this.FechaRetencionPercepcion
					, (int)this.TipoDeComprobanteOrigenDeLaRetencion
					, this.LetraDelComprobante.ToString()
					, this.NroDeComprobante
					, this.FechaDelComprobante
					, this.MontoDelComprobante
					, this.NroDeCertificadoPropio.PadRight(16).Substring(0, 16)
					, (int)this.TipoDeDocumentoDelRetenido
					, this.NroDeDocumentoDelRetenido
					, (int)this.SituacionIngresosBrutosDelRetenido
					, this.NroInscripcionIngresosBrutosDelRetenido
					, (int)this.SituacionFrenteAlIVADelRetenido
					, this.RazonSocialDelRetenido.PadRight(30).Substring(0, 30)
					, this.ImporteOtrosConceptos
					, this.ImporteIVA
					, this.MontoSujetoARetencionPercepcion
					, this.Alicuota
					, this.RetencionPercepcionPracticada
					, this.MontoTotalRetenidoPercibido
				);
				if (cadena.Length != 215) {
					throw new Exception (string.Format("La longitud del registro a exportar es incorrecta.\nRazon Social: {0}\nComprobante: {1}", this.RazonSocialDelRetenido, this.NroDeComprobante));
				}

				return cadena;
			}
		}
	}
}

