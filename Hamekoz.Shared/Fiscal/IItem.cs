//
//  IItem.cs
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

namespace Hamekoz.Fiscal
{
	public interface IItem
	{
		string Codigo { get; }

		string Descripcion { get; }

		//TODO separar la descripcion corta en otra iterfaz que implemente IItem
		[Obsolete ("Usar interfaz IItemControladorFiscal")]
		string DescripcionCorta { get; }

		decimal Cantidad { get; }

		decimal Precio { get; }

		decimal Neto { get; }

		IVA Iva { get; }

		decimal ImporteIVA { get; }

		decimal Impuestos { get; }

		decimal Total { get; }
	}

	//TODO revisar si corresponde heredar de IItem o que sea una implementacion independiente para los casos de controladores fiscales
	interface IItemControladorFiscal : IItem
	{
		string DescripcionCorta { get; }
	}
}
