//
//  IVA.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2017 Hamekoz - www.hamekoz.com.ar
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
using System.ComponentModel;

//TODO quizas deberia moverse a un espacio de nombres mas adecuado como Hamekoz.Argentina.AFIP
namespace Hamekoz.Negocio
{
	/// <summary>
	/// Condición de IVA en operación
	/// </summary>
	/// <see cref="http://www.afip.gob.ar/fe/documentos/OperacionCondicionIVA.xls"/>
	public enum IVA
	{
		/// <summary>
		/// No Corresponde. NO APLICA PARA FACTURA ELECTRÓNICA.
		/// </summary>
		[DescriptionAttribute ("NO CORRESPONDE")]
		NoCorresponde = 0,
		/// <summary>
		/// No Gravado
		/// </summary>
		[DescriptionAttribute ("NO GRAVADO")]
		NoGravado = 1,
		/// <summary>
		/// IVA EXENTO
		/// </summary>
		[DescriptionAttribute ("IVA EXENTO")]
		Exento = 2,
		/// <summary>
		/// IVA 0 %
		/// </summary>
		[DescriptionAttribute ("IVA 0 %")]
		Diferencial5 = 3,
		/// <summary>
		/// IVA 10.5 %
		/// </summary>
		[DescriptionAttribute ("IVA 10.5 %")]
		Diferencial1 = 4,
		/// <summary>
		/// IVA 21 %
		/// </summary>
		[DescriptionAttribute ("IVA 21 %")]
		General = 5,
		/// <summary>
		/// IVA 27 %
		/// </summary>
		[DescriptionAttribute ("IVA 27 %")]
		Diferencial2 = 6,
		/// <summary>
		/// Gravado. SOLO PARA CONTROLADORES FISCALES.
		/// </summary>
		[DescriptionAttribute ("GRAVADO")]
		Gravado = 7,
		/// <summary>
		/// IVA 5 %
		/// </summary>
		[DescriptionAttribute ("IVA 5 %")]
		Diferencial4 = 8,
		/// <summary>
		/// IVA 2.5 %
		/// </summary>
		[DescriptionAttribute ("IVA 2.5 %")]
		Diferencial3 = 9,
	}

	public static class IVAExtension
	{
		public static decimal Alicuota (this IVA iva)
		{
			switch (iva) {
			case IVA.General:
				return 21m;
			case IVA.Diferencial1:
				return 10.5m;
			case IVA.Diferencial2:
				return 27m;
			case IVA.Diferencial3:
				return 2.5m;
			case IVA.Diferencial4:
				return 5m;
			case IVA.Diferencial5:
			case IVA.Exento:
				return 0m;
			default:
				return 0;
			}
		}
	}
}
