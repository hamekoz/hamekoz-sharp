//
//  ControladorFiscal.cs
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
using Hamekoz.Fiscal;

namespace Hamekoz.Fiscal.Hasar.Spooler
{
	public class ControladorFiscal : IFiscalHasar
	{
		#region IFiscalHasar implementation

		public void ImprimirComprobanteCuentaCorriente (IComprobante factura)
		{
			throw new NotImplementedException ();
		}

		public void ImprimirFacturaProveedor (IComprobante factura)
		{
			throw new NotImplementedException ();
		}

		public void ImprimirRecibo (IComprobante recibo)
		{
			throw new NotImplementedException ();
		}

		public void ImprimirRemitoCliente (IComprobante remito)
		{
			throw new NotImplementedException ();
		}

		public void ImprimirRemitoProveedor (IComprobante remito)
		{
			throw new NotImplementedException ();
		}

		public void ImprimirTicketFactura (IComprobante factura, IComprobante recibo, double vueltoefectivo)
		{
			throw new NotImplementedException ();
		}

		public void Iniciar ()
		{
			throw new NotImplementedException ();
		}

		public void Iniciar (int puertoSerie)
		{
			throw new NotImplementedException ();
		}

		public void ReporteX ()
		{
			throw new NotImplementedException ("Metodo no implementado para conexion por spooler");
		}

		public void ReporteZ ()
		{
			throw new NotImplementedException ();
		}

		public void ReporteZ (IZeta zeta)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}
