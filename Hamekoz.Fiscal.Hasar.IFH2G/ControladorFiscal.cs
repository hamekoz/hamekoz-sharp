//
//  ControladorFiscal.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2014 Hamekoz - www.hamekoz.com.ar
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
using System.Configuration;
using hfl.argentina;

namespace Hamekoz.Fiscal.Hasar.IFH2G
{
	public class ControladorFiscal : IFiscalHasar
	{
		public HasarImpresoraFiscalRG3561 fiscalHasar;

		public ControladorFiscal ()
		{
			fiscalHasar = new HasarImpresoraFiscalRG3561 ();
		}

		void DatosClientes (IResponsable cliente)
		{
			switch (cliente.Tipo) {
			case TipoDeResponsable.Sin_Dato:
			case TipoDeResponsable.Consumidor_Final:
				break;
			case TipoDeResponsable.Responsable_Monotributo:
				fiscalHasar.CargarDatosCliente (cliente.RazonSocial
												   , cliente.CUIT.Replace ("-", "")
												   , HasarImpresoraFiscalRG3561.TiposDeResponsabilidadesCliente.MONOTRIBUTO
												   , HasarImpresoraFiscalRG3561.TiposDeDocumentoCliente.TIPO_CUIT
												   , cliente.Domicilio
												   , string.Empty
												   , string.Empty
												   , string.Empty);
				break;
			case TipoDeResponsable.IVA_Responsable_Inscripto:
				fiscalHasar.CargarDatosCliente (cliente.RazonSocial
												   , cliente.CUIT.Replace ("-", "")
													, HasarImpresoraFiscalRG3561.TiposDeResponsabilidadesCliente.RESPONSABLE_INSCRIPTO
												   , HasarImpresoraFiscalRG3561.TiposDeDocumentoCliente.TIPO_CUIT
												   , cliente.Domicilio
												   , string.Empty
												   , string.Empty
												   , string.Empty);
				break;
			case TipoDeResponsable.IVA_Sujeto_Exento:
				fiscalHasar.CargarDatosCliente (cliente.RazonSocial
												   , cliente.CUIT.Replace ("-", "")
													, HasarImpresoraFiscalRG3561.TiposDeResponsabilidadesCliente.RESPONSABLE_EXENTO
												   , HasarImpresoraFiscalRG3561.TiposDeDocumentoCliente.TIPO_CUIT
												   , cliente.Domicilio
												   , string.Empty
												   , string.Empty
												   , string.Empty);
				break;
			}
		}

		#region IFiscalHasar Members

		public void Iniciar ()
		{
			string direccionIP = ConfigurationManager.AppSettings ["ImpresoraFiscal.DireccionIP"];
			Iniciar (direccionIP);

		}

		public void Iniciar (int puertoSerie)
		{
			throw new NotImplementedException ();
		}

		public void Iniciar (string direccionIP)
		{
			fiscalHasar.conectar (direccionIP);
			var respuesta = fiscalHasar.ConsultarVersion ();
			if (respuesta.getVersionProtocolo () > fiscalHasar.ObtenerVersionProtocolo ())
				throw new Exception ("Impresora NO soportada. El protocolo de comunicacion del controlador es mas nuevo que el soportado por el sistema.");
			try {
				fiscalHasar.Cancelar ();
			} catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}
		}

		public void Iniciar (string direccionIP, int puerto)
		{
			fiscalHasar.conectar (direccionIP, puerto);
			var respuesta = fiscalHasar.ConsultarVersion ();
			if (respuesta.getVersionProtocolo () > fiscalHasar.ObtenerVersionProtocolo ())
				throw new Exception ("Impresora NO soportada. El protocolo de comunicacion del controlador es mas nuevo que el soportado por el sistema.");
			try {
				fiscalHasar.Cancelar ();
			} catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}
		}

		public void ReporteX ()
		{
			fiscalHasar.CerrarJornadaFiscal (HasarImpresoraFiscalRG3561.TipoReporte.REPORTE_X);
		}

		public void ReporteZ ()
		{
			fiscalHasar.CerrarJornadaFiscal (HasarImpresoraFiscalRG3561.TipoReporte.REPORTE_Z);
		}

		public void ReporteZ (IZeta zeta)
		{
			//TODO mejorar IZeta para tener una version de 1G y otra 2G
			var respuesta = fiscalHasar.CerrarJornadaFiscal (HasarImpresoraFiscalRG3561.TipoReporte.REPORTE_Z);
			var z = respuesta.Z;

			zeta.NumeroReporte = z.getNumero ();
			zeta.CantidadDFCancelados = z.getDF_CantidadCancelados ();
			zeta.CantidadDNFHEmitidos = z.getDNFH_CantidadEmitidos ();
			zeta.CantidadDNFEmitidos = 0;
			zeta.CantidadDFEmitidos = z.getDF_CantidadEmitidos ();
			zeta.UltimoDocFiscalBC = 0;
			zeta.UltimoDocFiscalA = 0;
			zeta.MontoVentasDocFiscal = z.getDF_Total ();
			zeta.MontoIVADocFiscal = z.getDF_TotalIVA ();
			zeta.MontoImpInternosDocFiscal = z.getDF_TotalTributos ();
			zeta.MontoPercepcionesDocFiscal = 0;
			zeta.MontoIVANoInscriptoDocFiscal = z.getDF_TotalExento ();
			zeta.UltimaNotaCreditoBC = 0;
			zeta.UltimaNotaCreditoA = 0;
			zeta.MontoVentasNotaCredito = z.getNC_Total ();
			zeta.MontoIVANotaCredito = z.getNC_TotalIVA ();
			zeta.MontoImpInternosNotaCredito = z.getNC_TotalTributos ();
			zeta.MontoPercepcionesNotaCredito = 0;
			zeta.MontoIVANoInscriptoNotaCredito = 0;
			zeta.UltimoRemito = 0;
			zeta.CantidadNCCanceladas = z.getNC_CantidadCancelados ();
			zeta.CantidadDFBCEmitidos = z.getDF_CantidadEmitidos ();
			zeta.CantidadDFAEEmitidos = z.getDF_CantidadEmitidos ();
			zeta.CantidadNCBCEmitidos = z.getNC_CantidadEmitidos ();
			zeta.CantidadNCAEmitidos = z.getNC_CantidadEmitidos ();
		}

		public void ImprimirTicketFactura (IComprobante factura, IComprobante recibo, decimal vueltoefectivo)
		{
			try {
				switch (factura.Responsable.Tipo) {
				case TipoDeResponsable.Sin_Dato:
					throw new Exception ("No se puede facturar a un cliente que no tiene declarada situacion respecto al IVA");
				case TipoDeResponsable.Consumidor_Final:
					fiscalHasar.AbrirDocumento (HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_FACTURA_C);
					break;
				case TipoDeResponsable.Responsable_Monotributo:
					fiscalHasar.CargarDatosCliente (
						factura.Responsable.RazonSocial,
						factura.Responsable.CUIT.Replace ("-", ""),
						HasarImpresoraFiscalRG3561.TiposDeResponsabilidadesCliente.MONOTRIBUTO,
						HasarImpresoraFiscalRG3561.TiposDeDocumentoCliente.TIPO_CUIT,
						factura.Responsable.Domicilio,
						string.Empty,
						string.Empty,
						string.Empty
					);
					fiscalHasar.AbrirDocumento (HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_FACTURA_B);
					break;
				case TipoDeResponsable.IVA_Responsable_Inscripto:
					fiscalHasar.CargarDatosCliente (
						factura.Responsable.RazonSocial,
						factura.Responsable.CUIT.Replace ("-", ""),
						HasarImpresoraFiscalRG3561.TiposDeResponsabilidadesCliente.RESPONSABLE_INSCRIPTO,
						HasarImpresoraFiscalRG3561.TiposDeDocumentoCliente.TIPO_CUIT,
						factura.Responsable.Domicilio,
						string.Empty,
						string.Empty,
						string.Empty
					);
					fiscalHasar.AbrirDocumento (HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_FACTURA_A);
					break;
				case TipoDeResponsable.IVA_Sujeto_Exento:
					fiscalHasar.CargarDatosCliente (
						factura.Responsable.RazonSocial,
						factura.Responsable.CUIT.Replace ("-", ""),
						HasarImpresoraFiscalRG3561.TiposDeResponsabilidadesCliente.RESPONSABLE_EXENTO,
						HasarImpresoraFiscalRG3561.TiposDeDocumentoCliente.TIPO_CUIT,
						factura.Responsable.Domicilio,
						string.Empty,
						string.Empty,
						string.Empty
					);
					fiscalHasar.AbrirDocumento (HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_FACTURA_B);
					break;
				}

				//IMPRIMO RENGLONES
				foreach (IItem renglon in factura.Items) {
					fiscalHasar.ImprimirItem (
						renglon.DescripcionCorta,
						(double)renglon.Cantidad,
						(double)renglon.Precio,
						HasarImpresoraFiscalRG3561.CondicionesIVA.GRAVADO,
						(double)renglon.Iva.Alicuota (),
						HasarImpresoraFiscalRG3561.ModosDeMonto.MODO_SUMA_MONTO,
						HasarImpresoraFiscalRG3561.ModosDeImpuestosInternos.II_FIJO_MONTO,
						(double)renglon.Impuestos,
						HasarImpresoraFiscalRG3561.ModosDeDisplay.DISPLAY_SI,
						HasarImpresoraFiscalRG3561.ModosDePrecio.MODO_PRECIO_TOTAL,
						string.Empty
					);
				}

				double vuelto = 0;
				//IMPRIMO PAGOS
				if (recibo != null) {
					foreach (IItem ren in recibo.Items) {
						Console.WriteLine ((ren.Total + vueltoefectivo));
						var respuesta = fiscalHasar.ImprimirPago (ren.Descripcion, (double)(ren.Total + vueltoefectivo), HasarImpresoraFiscalRG3561.ModosDePago.PAGAR);
						vuelto = respuesta.getSaldo ();
					}
					vueltoefectivo = (decimal)vuelto;
				} else {
					fiscalHasar.ImprimirPago ("CUENTA CORRIENTE", (double)factura.Total, HasarImpresoraFiscalRG3561.ModosDePago.PAGAR);
				}

				//Cierro el documento
				var respuestaDocumento = fiscalHasar.CerrarDocumento ();
				var numero = respuestaDocumento.getNumeroComprobante ();
				//FIX aca deberia guardar el numero pelado, y tener una propiedad NumeroCompleto
				factura.Numero = string.Format ("{0}-{1}", factura.PuntoDeVenta.PadLeft (4, char.Parse ("0")), numero.ToString ().PadLeft (8, char.Parse ("0")));
				Console.WriteLine (numero);
			} catch (HasarException ex) {
				Console.WriteLine ("Hubo error al abrir comprobante. Se cancelará el comprobante. " + ex.getMessage ());
				fiscalHasar.Cancelar ();
				throw ex;
			}
		}

		public void ImprimirFacturaProveedor (IComprobante factura)
		{
			throw new NotImplementedException ();
		}

		public void ImprimirRecibo (IComprobante recibo)
		{
			ImprimirRecibo (recibo, 1);
		}

		public void ImprimirRecibo (IComprobante recibo, int copias)
		{
			throw new NotImplementedException ();
		}

		public void ImprimirRemitoCliente (IComprobante remito)
		{
			ImprimirRemitoCliente (remito, 1);
		}

		public void ImprimirRemitoCliente (IComprobante remito, int copias)
		{
			fiscalHasar.AbrirDocumento (HasarImpresoraFiscalRG3561.TiposComprobante.GENERICO);
			var atributos = new hfl.argentina.Hasar_Funcs.AtributosDeTexto ();
			fiscalHasar.ImprimirTextoGenerico (atributos, "Cliente:");
			fiscalHasar.ImprimirTextoGenerico (atributos, remito.Responsable.RazonSocial);
			fiscalHasar.ImprimirTextoGenerico (atributos, "Articulos:");
			foreach (IItem renglon in remito.Items) {
				fiscalHasar.ImprimirTextoGenerico (atributos, string.Format ("{0} x $ {1} => $ {2}", renglon.Cantidad, renglon.Precio, renglon.Total));
				fiscalHasar.ImprimirTextoGenerico (atributos, renglon.DescripcionCorta);
			}
			fiscalHasar.ImprimirTextoGenerico (atributos, string.Format ("Importe: $ {0}", remito.Total));
			fiscalHasar.CerrarDocumento ();
		}

		public void ImprimirRemitoProveedor (IComprobante remito)
		{
			ImprimirRemitoProveedor (remito, 1);
		}

		public void ImprimirRemitoProveedor (IComprobante remito, int copias)
		{
			fiscalHasar.AbrirDocumento (HasarImpresoraFiscalRG3561.TiposComprobante.GENERICO);
			var atributos = new hfl.argentina.Hasar_Funcs.AtributosDeTexto ();
			fiscalHasar.ImprimirTextoGenerico (atributos, "Proveedor:");
			fiscalHasar.ImprimirTextoGenerico (atributos, Limpiar (remito.Responsable.RazonSocial));
			fiscalHasar.ImprimirTextoGenerico (atributos, "Articulos:");
			foreach (IItem renglon in remito.Items) {
				fiscalHasar.ImprimirTextoGenerico (atributos, string.Format ("{0} x $ {1} => $ {2}", renglon.Cantidad, renglon.Precio, renglon.Total));
				fiscalHasar.ImprimirTextoGenerico (atributos, renglon.DescripcionCorta);
			}
			fiscalHasar.CerrarDocumento ();
		}

		public void ImprimirComprobanteCuentaCorriente (IComprobante factura)
		{
			fiscalHasar.AbrirDocumento (HasarImpresoraFiscalRG3561.TiposComprobante.GENERICO);
			var atributos = new hfl.argentina.Hasar_Funcs.AtributosDeTexto ();
			fiscalHasar.ImprimirTextoGenerico (atributos, "Comprobante de Cuenta Corriente:");
			fiscalHasar.ImprimirTextoGenerico (atributos, Limpiar (factura.Responsable.RazonSocial));
			fiscalHasar.ImprimirTextoGenerico (atributos, "Articulos:");
			foreach (IItem renglon in factura.Items) {
				fiscalHasar.ImprimirTextoGenerico (atributos, string.Format ("{0} x $ {1} => $ {2}", renglon.Cantidad, renglon.Precio, renglon.Total));
				fiscalHasar.ImprimirTextoGenerico (atributos, renglon.DescripcionCorta);
			}
			fiscalHasar.ImprimirTextoGenerico (atributos, string.Format ("Importe: $ {0}", factura.Total));
			fiscalHasar.ImprimirTextoGenerico (atributos, "Firma:");
			fiscalHasar.AvanzarPapelAmbasEstaciones (2);
			fiscalHasar.ImprimirTextoGenerico (atributos, "Aclaracion:");
			fiscalHasar.AvanzarPapelAmbasEstaciones (2);
			fiscalHasar.CerrarDocumento ();
		}

		public DateTime FechaHora {
			get {
				var respuesta = fiscalHasar.ConsultarFechaHora ();
				var fechaHora = respuesta.getFecha ();
				fechaHora.AddHours (respuesta.getHora ().Hours);
				fechaHora.AddMinutes (respuesta.getHora ().Minutes);
				fechaHora.AddSeconds (respuesta.getHora ().Seconds);
				return fechaHora;
			}
			set {
				var hora = new TimeSpan (value.Hour, value.Minute, value.Second);
				fiscalHasar.ConfigurarFechaHora (value, hora);
			}
		}

		#endregion

		static string Limpiar (string cadena)
		{
			//TODO considerar modelos que tiene hasta 50 caracteres
			int length = 40;
			length = cadena.Length > length ? length : cadena.Length;
			cadena = cadena.Substring (0, length);
			return cadena;
		}

		public string ReporteElectronico (DateTime desde, DateTime hasta)
		{
			string bloque = string.Empty;
			var primer_bloque = fiscalHasar.ObtenerPrimerBloqueReporteElectronico (desde, hasta, HasarImpresoraFiscalRG3561.TiposReporteAFIP.REPORTE_AFIP_COMPLETO);
			if (primer_bloque.getRegistro () == HasarImpresoraFiscalRG3561.IdentificadorBloque.BLOQUE_INFORMACION) {
				bloque += primer_bloque.getInformacion ();
				var respuesta = fiscalHasar.ObtenerSiguienteBloqueReporteElectronico ();
				while (respuesta.getRegistro () == HasarImpresoraFiscalRG3561.IdentificadorBloque.BLOQUE_INFORMACION) {
					bloque += respuesta.getInformacion ();
					respuesta = fiscalHasar.ObtenerSiguienteBloqueReporteElectronico ();
				}
			}
			return bloque;
		}
	}
}