//
//  TipoDeTributo.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2018 Hamekoz - www.hamekoz.com.ar
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
namespace Hamekoz.Fiscal
{
	/// <summary>
	/// Tipo de otros tributos
	/// </summary>
	/// <description>Lista de tipos de otros tributos segun AFIP</description>
	/// <see href="http://www.afip.gob.ar/fe/documentos/otros_Tributos.xlsx"/>
	//TODO revisar si corresponde usar un enumerado o si seria mejor trabajarlo con una clase para poder referenciar al webservice
	public enum TipoDeTributo
	{
		Impuestos_nacionales = 1,
		Impuestos_provinciales = 2,
		Impuestos_municipales = 3,
		Impuestos_internos = 4,
		IIBB = 5,
		Percepcion_de_IVA = 6,
		Percepcion_de_IIBB = 7,
		Percepciones_por_Impuestos_Municipales = 8,
		Otras_Percepciones = 9,
		Impuesto_interno_a_nivel_item = 10,
		Percepcion_de_IVA_a_no_Categorizado = 13,
		Retencion_IIGG_RG_830 = 14,
		Retencion_IVA_RG_3873 = 15,
		Pago_a_cuenta_IVA_RG_3873 = 16,
		Percepcion_IVA_RG_3873 = 17,
		Retencion_IVA_RG_2616_2009 = 18,
		Retencion_Ganancias_RG_2616_2009 = 19,
		Otros = 99,
	}
}

