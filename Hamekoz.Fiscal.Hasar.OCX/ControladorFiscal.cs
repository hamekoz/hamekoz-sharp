//
//  ControladorFiscal.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//		 Hernan Ignacio Vivani <hernan@vivani.com.ar>
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
using System.Configuration;
using FiscalPrinterLib;

namespace Hamekoz.Fiscal.Hasar.OCX
{
	public class ControladorFiscal : IFiscalHasar
	{
		public FiscalPrinterLib.HASAR fiscalHasar;
		private int idErrorFiscal;
		private string descripcionErrorFiscal;
		private int idErrorImpresora;
		private string descripcionErrorImpresora;
		private Boolean abortar;

		public ControladorFiscal ()
		{
			fiscalHasar = new FiscalPrinterLib.HASAR ();
			fiscalHasar.ImpresoraNoResponde += new FiscalPrinterLib._FiscalEvents_ImpresoraNoRespondeEventHandler (fiscalHasar_ImpresoraNoResponde);
			fiscalHasar.ErrorFiscal += new FiscalPrinterLib._FiscalEvents_ErrorFiscalEventHandler (fiscalHasar_ErrorFiscal);
			fiscalHasar.ErrorImpresora += new FiscalPrinterLib._FiscalEvents_ErrorImpresoraEventHandler (fiscalHasar_ErrorImpresora);
		}

		void fiscalHasar_ErrorImpresora (int Flags)
		{
			Console.WriteLine ("Error Impresora: " + Flags);
			idErrorImpresora = Flags;
			descripcionErrorImpresora = fiscalHasar.DescripcionStatusImpresor (Flags).ToString ();
		}

		void fiscalHasar_ErrorFiscal (int Flags)
		{
			Console.WriteLine ("Error Fiscal: " + Flags);
			idErrorFiscal = Flags;
			descripcionErrorFiscal = fiscalHasar.DescripcionStatusFiscal (Flags).ToString ();
		}


		void fiscalHasar_ImpresoraNoResponde (int CantidadReintentos)
		{
			if (fiscalHasar.ReintentoConstante && CantidadReintentos == 3) {
				fiscalHasar.Abortar ();
			}
		}

		private void DatosClientes (IResponsable cliente)
		{
			switch (cliente.CondicionDeIVA) {
			case SituacionIVA.SIN_DATO:
				break;
			case SituacionIVA.CONSUMIDOR_FINAL:
				break;
			case SituacionIVA.MONOTRIBUTO:
				fiscalHasar.DatosCliente (cliente.RazonSocial
                        , cliente.CUIT.Replace ("-", "")
                        , FiscalPrinterLib.TiposDeDocumento.TIPO_CUIT
                        , FiscalPrinterLib.TiposDeResponsabilidades.MONOTRIBUTO
                        , 1);
				break;
			case SituacionIVA.RESPONSABLE_INSCRIPTO:
				fiscalHasar.DatosCliente (cliente.RazonSocial
                        , cliente.CUIT.Replace ("-", "")
                        , FiscalPrinterLib.TiposDeDocumento.TIPO_CUIT
                        , FiscalPrinterLib.TiposDeResponsabilidades.RESPONSABLE_INSCRIPTO
                        , 1);
				break;
			case SituacionIVA.EXENTO:
				fiscalHasar.DatosCliente (cliente.RazonSocial
                        , cliente.CUIT.Replace ("-", "")
                        , FiscalPrinterLib.TiposDeDocumento.TIPO_CUIT
                        , FiscalPrinterLib.TiposDeResponsabilidades.RESPONSABLE_EXENTO
                        , 1);
				break;
			default:
				break;
			}
		}

		#region IFiscalHasar Members

		public void Iniciar ()
		{
			int puertoSerie = int.Parse (ConfigurationManager.AppSettings ["ImpresoraFiscal.PuertoSerie"]);
			Iniciar (puertoSerie);
		}

		public void Iniciar (int puertoSerie)
		{
			fiscalHasar.Puerto = 1;
			fiscalHasar.Transporte = FiscalPrinterLib.TiposDeTransporte.PUERTO_SERIE;
			fiscalHasar.AutodetectarControlador (puertoSerie);
			fiscalHasar.AutodetectarModelo ();
			fiscalHasar.ReintentoConstante = true;
			fiscalHasar.TratarDeCancelarTodo ();
		}

		public void ReporteX ()
		{
			object NumeroReporte;
			object CantidadDFCancelados;
			object CantidadDNFHEmitidos;
			object CantidadDNFEmitidos;
			object CantidadDFEmitidos;
			object UltimoDocFiscalBC;
			object UltimoDocFiscalA;
			object MontoVentasDocFiscal;
			object MontoIVADocFiscal;
			object MontoImpInternosDocFiscal;
			object MontoPercepcionesDocFiscal;
			object MontoIVANoInscriptoDocFiscal;
			object UltimaNotaCreditoBC;
			object UltimaNotaCreditoA;
			object MontoVentasNotaCredito;
			object MontoIVANotaCredito;
			object MontoImpInternosNotaCredito;
			object MontoPercepcionesNotaCredito;
			object MontoIVANoInscriptoNotaCredito;
			object UltimoRemito;
			object CantidadNCCanceladas;
			object CantidadDFBCEmitidos;
			object CantidadDFAEEmitidos;
			object CantidadNCBCEmitidos;
			object CantidadNCAEmitidos;
			fiscalHasar.ReporteX (out NumeroReporte, out CantidadDFCancelados, out CantidadDNFHEmitidos, out CantidadDNFEmitidos, out CantidadDFEmitidos, out UltimoDocFiscalBC, out UltimoDocFiscalA, out MontoVentasDocFiscal, out MontoIVADocFiscal, out MontoImpInternosDocFiscal, out MontoPercepcionesDocFiscal, out MontoIVANoInscriptoDocFiscal, out UltimaNotaCreditoBC, out UltimaNotaCreditoA, out MontoVentasNotaCredito, out MontoIVANotaCredito, out MontoImpInternosNotaCredito, out MontoPercepcionesNotaCredito, out MontoIVANoInscriptoNotaCredito, out UltimoRemito, out CantidadNCCanceladas, out CantidadDFBCEmitidos, out CantidadDFAEEmitidos, out CantidadNCBCEmitidos, out CantidadNCAEmitidos);
		}

		public void ReporteZ ()
		{
			object NumeroReporte;
			object CantidadDFCancelados;
			object CantidadDNFHEmitidos;
			object CantidadDNFEmitidos;
			object CantidadDFEmitidos;
			object UltimoDocFiscalBC;
			object UltimoDocFiscalA;
			object MontoVentasDocFiscal;
			object MontoIVADocFiscal;
			object MontoImpInternosDocFiscal;
			object MontoPercepcionesDocFiscal;
			object MontoIVANoInscriptoDocFiscal;
			object UltimaNotaCreditoBC;
			object UltimaNotaCreditoA;
			object MontoVentasNotaCredito;
			object MontoIVANotaCredito;
			object MontoImpInternosNotaCredito;
			object MontoPercepcionesNotaCredito;
			object MontoIVANoInscriptoNotaCredito;
			object UltimoRemito;
			object CantidadNCCanceladas;
			object CantidadDFBCEmitidos;
			object CantidadDFAEEmitidos;
			object CantidadNCBCEmitidos;
			object CantidadNCAEmitidos;
			fiscalHasar.ReporteZ (out NumeroReporte, out CantidadDFCancelados, out CantidadDNFHEmitidos, out CantidadDNFEmitidos, out CantidadDFEmitidos, out UltimoDocFiscalBC, out UltimoDocFiscalA, out MontoVentasDocFiscal, out MontoIVADocFiscal, out MontoImpInternosDocFiscal, out MontoPercepcionesDocFiscal, out MontoIVANoInscriptoDocFiscal, out UltimaNotaCreditoBC, out UltimaNotaCreditoA, out MontoVentasNotaCredito, out MontoIVANotaCredito, out MontoImpInternosNotaCredito, out MontoPercepcionesNotaCredito, out MontoIVANoInscriptoNotaCredito, out UltimoRemito, out CantidadNCCanceladas, out CantidadDFBCEmitidos, out CantidadDFAEEmitidos, out CantidadNCBCEmitidos, out CantidadNCAEmitidos);
		}

		public void ReporteZ (out object NumeroReporte, out object CantidadDFCancelados, out object CantidadDNFHEmitidos, out object CantidadDNFEmitidos, out object CantidadDFEmitidos, out object UltimoDocFiscalBC, out object UltimoDocFiscalA, out object MontoVentasDocFiscal, out object MontoIVADocFiscal, out object MontoImpInternosDocFiscal, out object MontoPercepcionesDocFiscal, out object MontoIVANoInscriptoDocFiscal, out object UltimaNotaCreditoBC, out object UltimaNotaCreditoA, out object MontoVentasNotaCredito, out object MontoIVANotaCredito, out object MontoImpInternosNotaCredito, out object MontoPercepcionesNotaCredito, out object MontoIVANoInscriptoNotaCredito, out object UltimoRemito, out object CantidadNCCanceladas, out object CantidadDFBCEmitidos, out object CantidadDFAEEmitidos, out object CantidadNCBCEmitidos, out object CantidadNCAEmitidos)
		{
			fiscalHasar.ReporteZ (out NumeroReporte, out CantidadDFCancelados, out CantidadDNFHEmitidos, out CantidadDNFEmitidos, out CantidadDFEmitidos, out UltimoDocFiscalBC, out UltimoDocFiscalA, out MontoVentasDocFiscal, out MontoIVADocFiscal, out MontoImpInternosDocFiscal, out MontoPercepcionesDocFiscal, out MontoIVANoInscriptoDocFiscal, out UltimaNotaCreditoBC, out UltimaNotaCreditoA, out MontoVentasNotaCredito, out MontoIVANotaCredito, out MontoImpInternosNotaCredito, out MontoPercepcionesNotaCredito, out MontoIVANoInscriptoNotaCredito, out UltimoRemito, out CantidadNCCanceladas, out CantidadDFBCEmitidos, out CantidadDFAEEmitidos, out CantidadNCBCEmitidos, out CantidadNCAEmitidos);
		}

        public void ReporteZ(IZeta  zeta)
        {
            object NumeroReporte;
            object CantidadDFCancelados;
            object CantidadDNFHEmitidos;
            object CantidadDNFEmitidos;
            object CantidadDFEmitidos;
            object UltimoDocFiscalBC;
            object UltimoDocFiscalA;
            object MontoVentasDocFiscal;
            object MontoIVADocFiscal;
            object MontoImpInternosDocFiscal;
            object MontoPercepcionesDocFiscal;
            object MontoIVANoInscriptoDocFiscal;
            object UltimaNotaCreditoBC;
            object UltimaNotaCreditoA;
            object MontoVentasNotaCredito;
            object MontoIVANotaCredito;
            object MontoImpInternosNotaCredito;
            object MontoPercepcionesNotaCredito;
            object MontoIVANoInscriptoNotaCredito;
            object UltimoRemito;
            object CantidadNCCanceladas;
            object CantidadDFBCEmitidos;
            object CantidadDFAEEmitidos;
            object CantidadNCBCEmitidos;
            object CantidadNCAEmitidos;
            
            fiscalHasar.ReporteZ(out NumeroReporte, out CantidadDFCancelados, out CantidadDNFHEmitidos, out CantidadDNFEmitidos, out CantidadDFEmitidos, out UltimoDocFiscalBC, out UltimoDocFiscalA, out MontoVentasDocFiscal, out MontoIVADocFiscal, out MontoImpInternosDocFiscal, out MontoPercepcionesDocFiscal, out MontoIVANoInscriptoDocFiscal, out UltimaNotaCreditoBC, out UltimaNotaCreditoA, out MontoVentasNotaCredito, out MontoIVANotaCredito, out MontoImpInternosNotaCredito, out MontoPercepcionesNotaCredito, out MontoIVANoInscriptoNotaCredito, out UltimoRemito, out CantidadNCCanceladas, out CantidadDFBCEmitidos, out CantidadDFAEEmitidos, out CantidadNCBCEmitidos, out CantidadNCAEmitidos);
            
            zeta.NumeroReporte = (int)NumeroReporte;
            zeta.CantidadDFCancelados = (int)CantidadDFCancelados;
            zeta.CantidadDNFHEmitidos = (int)CantidadDNFHEmitidos;
            zeta.CantidadDNFEmitidos = (int)CantidadDNFEmitidos;
            zeta.CantidadDFEmitidos = (int)CantidadDFEmitidos;
            zeta.UltimoDocFiscalBC = (int)UltimoDocFiscalBC;
            zeta.UltimoDocFiscalA = (int)UltimoDocFiscalA;
            zeta.MontoVentasDocFiscal = (double)MontoVentasDocFiscal;
            zeta.MontoIVADocFiscal = (double)MontoIVADocFiscal;
            zeta.MontoImpInternosDocFiscal = (double)MontoImpInternosDocFiscal;
            zeta.MontoPercepcionesDocFiscal = (double)MontoPercepcionesDocFiscal;
            zeta.MontoIVANoInscriptoDocFiscal = (double)MontoIVANoInscriptoDocFiscal;
            zeta.UltimaNotaCreditoBC = (int)UltimaNotaCreditoBC;
            zeta.UltimaNotaCreditoA = (int)UltimaNotaCreditoA;
            zeta.MontoVentasNotaCredito = (double)MontoVentasNotaCredito;
            zeta.MontoIVANotaCredito = (double)MontoIVANotaCredito;
            zeta.MontoImpInternosNotaCredito = (double)MontoImpInternosNotaCredito;
            zeta.MontoPercepcionesNotaCredito = (double)MontoPercepcionesNotaCredito;
            zeta.MontoIVANoInscriptoNotaCredito = (double)MontoIVANoInscriptoNotaCredito;
            zeta.UltimoRemito = (int)UltimoRemito;
            zeta.CantidadNCCanceladas = (int)CantidadNCCanceladas;
            zeta.CantidadDFBCEmitidos = (int)CantidadDFBCEmitidos;
            zeta.CantidadDFAEEmitidos = (int)CantidadDFAEEmitidos;
            zeta.CantidadNCBCEmitidos = (int)CantidadNCBCEmitidos;
            zeta.CantidadNCAEmitidos = (int)CantidadNCAEmitidos;
        }

		/*public void ImprimirTicket()
        {
            //fiscalHasar.DatosCliente(cliente.RazonSocial, "", FiscalPrinterLib.TiposDeDocumento.TIPO_NINGUNO , FiscalPrinterLib.TiposDeResponsabilidades.CONSUMIDOR_FINAL, 1);
            fiscalHasar.AbrirComprobanteFiscal(FiscalPrinterLib.DocumentosFiscales.TICKET_C);
            fiscalHasar.ImprimirTextoFiscal("texto fiscal 1");
            fiscalHasar.ImprimirItem("item 1", 1, 10, 0.21, 0);
            object vuelto;
            object numero;
            fiscalHasar.ImprimirPago("Efectivo", 10, "",out vuelto);
            fiscalHasar.CerrarComprobanteFiscal(1,out numero);
        }

        public void ImprimirTicketFactura(ClienteEntity cliente)
        {
            fiscalHasar.DatosCliente(cliente.RazonSocial, "", FiscalPrinterLib.TiposDeDocumento.TIPO_NINGUNO, FiscalPrinterLib.TiposDeResponsabilidades.CONSUMIDOR_FINAL, 1);
            if (cliente.CondicionDeIVA.Descripcion == "MONOTRIBUTO" || cliente.CondicionDeIVA.Descripcion == "EXENTO")
                fiscalHasar.AbrirComprobanteFiscal(FiscalPrinterLib.DocumentosFiscales.TICKET_FACTURA_B);
            if (cliente.CondicionDeIVA.Descripcion=="RESPONSABLE INSCRIPTO")
                fiscalHasar.AbrirComprobanteFiscal(FiscalPrinterLib.DocumentosFiscales.TICKET_FACTURA_A);
            fiscalHasar.ImprimirTextoFiscal("texto fiscal 1");
            fiscalHasar.ImprimirItem("item 1", 1, 10, 0.21, 0);
            object vuelto;
            object numero;
            fiscalHasar.ImprimirPago("Efectivo", 10, "", out vuelto);
            fiscalHasar.CerrarComprobanteFiscal(1, out numero);
        }*/

		public void ImprimirTicketFactura (IComprobante factura, IComprobante recibo, double vueltoefectivo)
		{
			abortar = false;

			switch (factura.Responsable.CondicionDeIVA) {
			case SituacionIVA.SIN_DATO:
				throw new Exception ("No se puede facturar a un cliente que no tiene declarada situacion respecto al IVA");
			case SituacionIVA.CONSUMIDOR_FINAL:
				fiscalHasar.AbrirComprobanteFiscal (FiscalPrinterLib.DocumentosFiscales.TICKET_C);
				if (fiscalHasar.HuboErrorFiscal) {
					Console.WriteLine ("Hubo error al abrir comprobante. Se cancelará el comprobante. " + descripcionErrorFiscal);
					fiscalHasar.CancelarComprobante ();
					abortar = true;
				}
				break;
			case SituacionIVA.MONOTRIBUTO:
				fiscalHasar.DatosCliente (factura.Responsable.RazonSocial, factura.Responsable.CUIT.Replace ("-", ""), FiscalPrinterLib.TiposDeDocumento.TIPO_CUIT, FiscalPrinterLib.TiposDeResponsabilidades.MONOTRIBUTO, 1);
				if (fiscalHasar.HuboErrorFiscal)
					Console.WriteLine ("hubo error al cargar datos cliente:" + descripcionErrorFiscal);
				fiscalHasar.AbrirComprobanteFiscal (FiscalPrinterLib.DocumentosFiscales.TICKET_FACTURA_B);
				if (fiscalHasar.HuboErrorFiscal) {
					Console.WriteLine ("Hubo error al abrir comprobante. Se cancelará el comprobante. " + descripcionErrorFiscal);
					fiscalHasar.CancelarComprobante ();
					abortar = true;
				}
				break;
			case SituacionIVA.RESPONSABLE_INSCRIPTO:
				fiscalHasar.DatosCliente (factura.Responsable.RazonSocial, factura.Responsable.CUIT.Replace ("-", ""), FiscalPrinterLib.TiposDeDocumento.TIPO_CUIT, FiscalPrinterLib.TiposDeResponsabilidades.RESPONSABLE_INSCRIPTO, 1);
				if (fiscalHasar.HuboErrorFiscal)
					Console.WriteLine ("hubo error al cargar datos cliente:" + descripcionErrorFiscal);
				fiscalHasar.AbrirComprobanteFiscal (FiscalPrinterLib.DocumentosFiscales.TICKET_FACTURA_A);
				if (fiscalHasar.HuboErrorFiscal) {
					Console.WriteLine ("Hubo error al abrir comprobante. Se cancelará el comprobante. " + descripcionErrorFiscal);
					fiscalHasar.CancelarComprobante ();
					abortar = true;
				}
				break;
			case SituacionIVA.EXENTO:
				fiscalHasar.DatosCliente (factura.Responsable.RazonSocial, factura.Responsable.CUIT.Replace ("-", ""), FiscalPrinterLib.TiposDeDocumento.TIPO_CUIT, FiscalPrinterLib.TiposDeResponsabilidades.RESPONSABLE_EXENTO, 1);
				if (fiscalHasar.HuboErrorFiscal)
					Console.WriteLine ("hubo error al cargar datos cliente:" + descripcionErrorFiscal);
				fiscalHasar.AbrirComprobanteFiscal (FiscalPrinterLib.DocumentosFiscales.TICKET_FACTURA_B);
				if (fiscalHasar.HuboErrorFiscal) {
					Console.WriteLine ("Hubo error al abrir comprobante. Se cancelará el comprobante. " + descripcionErrorFiscal);
					fiscalHasar.CancelarComprobante ();
					abortar = true;
				}
				break;
			}

			//IMPRIMO RENGLONES
			foreach (IItem renglon in factura.Items) {
				fiscalHasar.ImprimirItem (renglon.DescripcionCorta, renglon.Cantidad, renglon.Precio, renglon.IVA, renglon.Impuestos);
				if (fiscalHasar.HuboErrorFiscal) {
					Console.WriteLine ("Hubo error al abrir comprobante. Se cancelará el comprobante. " + descripcionErrorFiscal);
					fiscalHasar.CancelarComprobante ();
					abortar = true;
				}
			}

			//COBRANZA
			if (!abortar) {
				object vuelto;
				object numero;
				//IMPRIMO PAGOS
				if (recibo != null) {
					foreach (IItem ren in recibo.Items) {
						Console.WriteLine ((ren.Total + vueltoefectivo).ToString ());
						fiscalHasar.ImprimirPago (ren.Descripcion, ren.Total + vueltoefectivo, "", out vuelto);
						if (fiscalHasar.HuboErrorFiscal) {
							Console.WriteLine ("Hubo error al abrir comprobante. Se cancelará el comprobante. " + descripcionErrorFiscal);
							fiscalHasar.CancelarComprobante ();
							abortar = true;
						}
					}
				} else {
					fiscalHasar.ImprimirPago ("CUENTA CORRIENTE", factura.Total, "", out vuelto);
					if (fiscalHasar.HuboErrorFiscal) {
						Console.WriteLine ("Hubo error al abrir comprobante. Se cancelará el comprobante. " + descripcionErrorFiscal);
						fiscalHasar.CancelarComprobante ();
						abortar = true;
					}
				}
				if (!abortar) {
					fiscalHasar.CerrarComprobanteFiscal (1, out numero);
					factura.Numero = string.Format ("{0}-{1}", factura.PuntoDeVenta.PadLeft (4, Char.Parse ("0")), numero.ToString ().PadLeft (8, Char.Parse ("0")));
					Console.WriteLine (numero.ToString ());
				}
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
			fiscalHasar.AbrirComprobanteNoFiscal (TiposDeEstacion.ESTACION_TICKET);
			fiscalHasar.ImprimirTextoNoFiscal ("Cliente:");
			fiscalHasar.ImprimirTextoNoFiscal (remito.Responsable.RazonSocial);
			fiscalHasar.ImprimirTextoNoFiscal ("Articulos:");
			foreach (IItem renglon in remito.Items) {
				fiscalHasar.ImprimirTextoNoFiscal (string.Format ("{0} x $ {1} => $ {2}", renglon.Cantidad, renglon.Precio, renglon.Total));
				fiscalHasar.ImprimirTextoNoFiscal (renglon.DescripcionCorta);
			}
			fiscalHasar.ImprimirTextoNoFiscal (string.Format ("Importe: $ {0}", remito.Total));
			fiscalHasar.CerrarComprobanteNoFiscal (copias);
		}

		public void ImprimirRemitoProveedor (IComprobante remito)
		{
			ImprimirRemitoProveedor (remito, 1);
		}

		public void ImprimirRemitoProveedor (IComprobante remito, int copias)
		{
			fiscalHasar.AbrirComprobanteNoFiscal (TiposDeEstacion.ESTACION_TICKET);
			fiscalHasar.ImprimirTextoNoFiscal ("Proveedor:");
			fiscalHasar.ImprimirTextoNoFiscal (Limpiar (remito.Responsable.RazonSocial));
			fiscalHasar.ImprimirTextoNoFiscal ("Articulos:");
			foreach (IItem renglon in remito.Items) {
				fiscalHasar.ImprimirTextoNoFiscal (string.Format ("{0} x $ {1} => $ {2}", renglon.Cantidad, renglon.Precio, renglon.Total));
				fiscalHasar.ImprimirTextoNoFiscal (renglon.DescripcionCorta);
			}
			fiscalHasar.CerrarComprobanteNoFiscal (copias);
		}

		public void ImprimirComprobanteCuentaCorriente (IComprobante factura)
		{
			fiscalHasar.AbrirComprobanteNoFiscal (TiposDeEstacion.ESTACION_TICKET);
			fiscalHasar.ImprimirTextoNoFiscal ("Comprobante de Cuenta Corriente:");
			fiscalHasar.ImprimirTextoNoFiscal (Limpiar (factura.Responsable.RazonSocial));
			fiscalHasar.ImprimirTextoNoFiscal ("Articulos:");
			foreach (IItem renglon in factura.Items) {
				fiscalHasar.ImprimirTextoNoFiscal (string.Format ("{0} x $ {1} => $ {2}", renglon.Cantidad, renglon.Precio, renglon.Total));
				fiscalHasar.ImprimirTextoNoFiscal (renglon.DescripcionCorta);
			}
			fiscalHasar.ImprimirTextoNoFiscal (string.Format ("Importe: $ {0}", factura.Total));
			fiscalHasar.ImprimirTextoNoFiscal ("Firma:");
			fiscalHasar.AvanzarPapel (TiposDePapel.PAPEL_TICKET_Y_DIARIO, 2);
			fiscalHasar.ImprimirTextoNoFiscal ("Aclaracion:");
			fiscalHasar.AvanzarPapel (TiposDePapel.PAPEL_TICKET_Y_DIARIO, 2);
			fiscalHasar.CerrarComprobanteNoFiscal (1);
		}

		#endregion

		private string Limpiar (string cadena)
		{
			//TODO considerar modelos que tiene hasta 50 caracteres
			int length = 40;
			length = cadena.Length > length ? length : cadena.Length;
			cadena = cadena.Substring (0, length);
			return cadena;
		}
	}
}