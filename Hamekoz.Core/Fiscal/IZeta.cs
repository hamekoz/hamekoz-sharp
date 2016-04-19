//
//  IZeta.cs
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

namespace Hamekoz.Fiscal
{
	//UNDONE refactorizar nombres de propiedades pasar todo los valores monetarios a decimal
	public interface IZeta
	{
		int NumeroReporte { get; set; }

		int CantidadDFCancelados { get; set; }

		int CantidadDNFHEmitidos { get; set; }

		int CantidadDNFEmitidos { get; set; }

		int CantidadDFEmitidos { get; set; }

		int UltimoDocFiscalBC { get; set; }

		int UltimoDocFiscalA { get; set; }

		double MontoVentasDocFiscal { get; set; }

		double MontoIVADocFiscal { get; set; }

		double MontoImpInternosDocFiscal { get; set; }

		double MontoPercepcionesDocFiscal { get; set; }

		double MontoIVANoInscriptoDocFiscal { get; set; }

		int UltimaNotaCreditoBC { get; set; }

		int UltimaNotaCreditoA { get; set; }

		double MontoVentasNotaCredito { get; set; }

		double MontoIVANotaCredito { get; set; }

		double MontoImpInternosNotaCredito { get; set; }

		double MontoPercepcionesNotaCredito { get; set; }

		double MontoIVANoInscriptoNotaCredito { get; set; }

		int UltimoRemito { get; set; }

		int CantidadNCCanceladas { get; set; }

		int CantidadDFBCEmitidos { get; set; }

		int CantidadDFAEEmitidos { get; set; }

		int CantidadNCBCEmitidos { get; set; }

		int CantidadNCAEmitidos { get; set; }
	}
}
