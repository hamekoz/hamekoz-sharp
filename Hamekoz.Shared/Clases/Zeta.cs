//
//  Zeta.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2016 Hamekoz - www.hamekoz.com.ar
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

using Hamekoz.Fiscal;

namespace Hamekoz.Negocio
{
    //UNDONE refactorizar
    public partial class Zeta : IZeta
    {
        public int NumeroReporte { get; set; }

        public int CantidadDFCancelados { get; set; }

        public int CantidadDNFHEmitidos { get; set; }

        public int CantidadDNFEmitidos { get; set; }

        public int CantidadDFEmitidos { get; set; }

        public int UltimoDocFiscalBC { get; set; }

        public int UltimoDocFiscalA { get; set; }

        public double MontoVentasDocFiscal { get; set; }

        public double MontoIVADocFiscal { get; set; }

        public double MontoImpInternosDocFiscal { get; set; }

        public double MontoPercepcionesDocFiscal { get; set; }

        public double MontoIVANoInscriptoDocFiscal { get; set; }

        public int UltimaNotaCreditoBC { get; set; }

        public int UltimaNotaCreditoA { get; set; }

        public double MontoVentasNotaCredito { get; set; }

        public double MontoIVANotaCredito { get; set; }

        public double MontoImpInternosNotaCredito { get; set; }

        public double MontoPercepcionesNotaCredito { get; set; }

        public double MontoIVANoInscriptoNotaCredito { get; set; }

        public int UltimoRemito { get; set; }

        public int CantidadNCCanceladas { get; set; }

        public int CantidadDFBCEmitidos { get; set; }

        public int CantidadDFAEEmitidos { get; set; }

        public int CantidadNCBCEmitidos { get; set; }

        public int CantidadNCAEmitidos { get; set; }
    }
}