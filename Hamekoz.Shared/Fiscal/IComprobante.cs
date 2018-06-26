//
//  IComprobante.cs
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
using System.Collections.Generic;
using Hamekoz.Negocio;

namespace Hamekoz.Fiscal
{
	public interface IComprobanteBase
	{
		string Comprobante { get; }

		DateTime Emision { get; }

		DateTime Contable { get; }

		IResponsable Responsable { get; }

		decimal Total { get; }

		bool Anulado { get; }
	}

	public interface IComprobante
	{
		IResponsable Responsable { get; }

		string Numero { get; set; }

		string PuntoDeVenta { get; }

		DateTime Emision { get; }

		DateTime Contable { get; }

		IList<IItem> Items { get; }

		IList<IVAItem> IVAItems { get; }

		IList<ImpuestoItem> Impuestos { get; }

		decimal Neto { get; }

		decimal Total { get; }

		decimal Gravado { get; }

		decimal NoGravado { get; }

		decimal IVA { get; }

		decimal Tributos { get; }

		string Observaciones { get; }
	}
}
