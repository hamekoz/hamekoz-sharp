//
//  IFiscalHasar.cs
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

namespace Hamekoz.Fiscal
{
	public interface IFiscalHasar
	{
		void ImprimirComprobanteCuentaCorriente (IComprobante comprobante);

		void ImprimirFacturaProveedor (IComprobante factura);

		void ImprimirRecibo (IComprobante recibo);

		void ImprimirRemitoCliente (IComprobante remito);

		void ImprimirRemitoProveedor (IComprobante remito);

		void ImprimirTicketFactura (IComprobante factura, IComprobante recibo, double vueltoefectivo);

		void Iniciar ();

		void Iniciar (int puertoSerie);

		/// <summary>
		/// Imprime el reporte X
		/// </summary>
		void ReporteX ();

		/// <summary>
		/// Imprime el reporte Z para cerrar la jornada fiscal
		/// </summary>
		void ReporteZ ();

		/// <summary>
		/// Imprime el reporte Z para cerrar la jornada fiscal devolviendo los valores impresos
		/// </summary>
		/// <param name="NumeroReporte"></param>
		/// <param name="CantidadDFCancelados"></param>
		/// <param name="CantidadDNFHEmitidos"></param>
		/// <param name="CantidadDNFEmitidos"></param>
		/// <param name="CantidadDFEmitidos"></param>
		/// <param name="UltimoDocFiscalBC"></param>
		/// <param name="UltimoDocFiscalA"></param>
		/// <param name="MontoVentasDocFiscal"></param>
		/// <param name="MontoIVADocFiscal"></param>
		/// <param name="MontoImpInternosDocFiscal"></param>
		/// <param name="MontoPercepcionesDocFiscal"></param>
		/// <param name="MontoIVANoInscriptoDocFiscal"></param>
		/// <param name="UltimaNotaCreditoBC"></param>
		/// <param name="UltimaNotaCreditoA"></param>
		/// <param name="MontoVentasNotaCredito"></param>
		/// <param name="MontoIVANotaCredito"></param>
		/// <param name="MontoImpInternosNotaCredito"></param>
		/// <param name="MontoPercepcionesNotaCredito"></param>
		/// <param name="MontoIVANoInscriptoNotaCredito"></param>
		/// <param name="UltimoRemito"></param>
		/// <param name="CantidadNCCanceladas"></param>
		/// <param name="CantidadDFBCEmitidos"></param>
		/// <param name="CantidadDFAEEmitidos"></param>
		/// <param name="CantidadNCBCEmitidos"></param>
		/// <param name="CantidadNCAEmitidos"></param>
		void ReporteZ (IZeta zeta);
	}
}
