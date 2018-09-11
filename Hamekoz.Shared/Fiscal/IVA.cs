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
using System;
using System.ComponentModel;

namespace Hamekoz.Fiscal
{
	/// <summary>
	/// Condición de IVA en operación
	/// </summary>
	/// <see href="http://www.afip.gob.ar/fe/documentos/OperacionCondicionIVA.xls"/>
	public enum IVA
	{
		/// <summary>
		/// No Corresponde. NO APLICA PARA FACTURA ELECTRÓNICA.
		/// </summary>
		[DescriptionAttribute ("N/C")]
		NoCorresponde = 0,
		/// <summary>
		/// No Gravado
		/// </summary>
		[DescriptionAttribute ("No Gravado")]
		NoGravado = 1,
		/// <summary>
		/// IVA EXENTO
		/// </summary>
		[DescriptionAttribute ("Exento")]
		Exento = 2,
		/// <summary>
		/// IVA 0 %
		/// </summary>
		[DescriptionAttribute ("0 %")]
		Cero = 3,
		/// <summary>
		/// IVA 10.5 %
		/// </summary>
		[DescriptionAttribute ("10.5 %")]
		DiezCinco = 4,
		/// <summary>
		/// IVA 21 %
		/// </summary>
		[DescriptionAttribute ("21 %")]
		Veintiuno = 5,
		/// <summary>
		/// IVA 27 %
		/// </summary>
		[DescriptionAttribute ("27 %")]
		Veintisiete = 6,
		/// <summary>
		/// Gravado. SOLO PARA CONTROLADORES FISCALES.
		/// </summary>
		[DescriptionAttribute ("Gravado")]
		Gravado = 7,
		/// <summary>
		/// IVA 5 %
		/// </summary>
		[DescriptionAttribute ("5 %")]
		Cinco = 8,
		/// <summary>
		/// IVA 2.5 %
		/// </summary>
		[DescriptionAttribute ("2.5 %")]
		DosCinco = 9,
	}

	public static class IVAExtension
	{
		public static decimal Alicuota (this IVA iva)
		{
			switch (iva) {
			case IVA.Veintiuno:
				return 21m;
			case IVA.DiezCinco:
				return 10.5m;
			case IVA.Veintisiete:
				return 27m;
			case IVA.DosCinco:
				return 2.5m;
			case IVA.Cinco:
				return 5m;
			default:
				return 0m;
			}
		}

		public static IVA Get (decimal iva)
		{
			int valor = Convert.ToInt32 (Math.Round (iva, 0));
			switch (valor) {
			case 21:
				return IVA.Veintiuno;
			case 10:
				return IVA.DiezCinco;
			case 27:
				return IVA.Veintisiete;
			case 2:
				return IVA.DosCinco;
			case 5:
				return IVA.Cinco;
			case 0:
				return IVA.Cero;
			default:
				return IVA.NoCorresponde;
			}
		}

		public static IVA Get (double iva)
		{
			int valor = Convert.ToInt32 (Math.Round (iva, 0));
			switch (valor) {
			case 21:
				return IVA.Veintiuno;
			case 10:
				return IVA.DiezCinco;
			case 27:
				return IVA.Veintisiete;
			case 2:
				return IVA.DosCinco;
			case 5:
				return IVA.Cinco;
			case 0:
				return IVA.Cero;
			default:
				return IVA.NoCorresponde;
			}
		}

		public static IVA Get (float iva)
		{
			int valor = Convert.ToInt32 (Math.Round (iva, 0));
			switch (valor) {
			case 21:
				return IVA.Veintiuno;
			case 10:
				return IVA.DiezCinco;
			case 27:
				return IVA.Veintisiete;
			case 2:
				return IVA.DosCinco;
			case 5:
				return IVA.Cinco;
			case 0:
				return IVA.Cero;
			default:
				return IVA.NoCorresponde;
			}
		}
	}
}
