﻿//
//  OpenDNFH.cs
//
//  Author:
//       Ezequiel Taranto <ezequiel89@gmail.com>
//
//  Copyright (c) 2014 Hamekoz - www.hamekoz.com.ar
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

//TODO revisar el parametro con codigo comentado
namespace Hamekoz.Fiscal.Hasar.Spooler
{
    public class OpenDNFH : Comando
    {
        const string cmd = "Ç";

        public int NroDNFHAbierto { get; set; }

        readonly string tipoDocumento;
        //string identificacionNroDocumento;

        //Encoding enc = Encoding.GetEncoding(437);
        //const string cmd = enc.GetString(new byte[]{128});

        public string Comando()
        {
            //return string.Format("{0}{1}{2}{1}{3}{1}{4}",cmd,separador,TipoDocumento,"S",identificacionNroDocumento);
            return string.Format("{0}{1}{2}{1}{3}", cmd, separador, tipoDocumento, "T");
        }

        public OpenDNFH(string tipoDocumento, string identificacionNroDocumento)
        {
            this.tipoDocumento = tipoDocumento;
            //this.identificacionNroDocumento = identificacionNroDocumento;
        }
    }
}