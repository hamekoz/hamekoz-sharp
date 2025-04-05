//
//  IComprobanteElectronico.cs
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

using Hamekoz.Argentina;
using Hamekoz.Negocio;

namespace Hamekoz.Fiscal
{
    public interface IComprobanteElectronico : IComprobante
    {
        NumeracionDeComprobante Tipo { get; }

        string NumeroAFIP { get; set; }

        string CAE { get; set; }

        string VencimientoCAE { get; set; }

        string ComentariosAFIP { get; set; }
    }

    public static class ComprobanteElectronicoExtension
    {
        public static string BarcodeText(this IComprobanteElectronico comprobante, Empresa emisor)
        {
            string barcode = string.Format("{0}{1:00}{2}{3}{4}", emisor.CUIT.Limpiar(), comprobante.Tipo.Codigo, comprobante.Tipo.Pre.PadLeft(4, '0'), comprobante.CAE.Trim(), comprobante.VencimientoCAE.Trim());

            int checksum = 0;
            bool par = false;
            foreach (var letra in barcode)
            {
                checksum += int.Parse(letra.ToString()) * (par ? 1 : 3);
                par = !par;
            }
            checksum = checksum % 10;
            checksum = checksum == 0 ? checksum : 10 - checksum;

            return barcode + checksum;
        }

        public static DateTime VencimientoCAE(this IComprobanteElectronico comprobante)
        {
            string vencimientoCae = string.Format("{0}/{1}/{2}"
                , comprobante.VencimientoCAE.Substring(6, 2)
                , comprobante.VencimientoCAE.Substring(4, 2)
                , comprobante.VencimientoCAE.Substring(0, 4));
            return DateTime.Parse(vencimientoCae);
        }
    }
}