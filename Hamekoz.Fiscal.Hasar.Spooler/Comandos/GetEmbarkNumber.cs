//
//  GetEmbarkNumber.cs
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

namespace Hamekoz.Fiscal.Hasar.Spooler
{
    public class GetEmbarkNumber : Comando
    {
        const string cmd = "ö";

        public string Texto { get; set; }

        readonly int nroLinea;

        public string Comando()
        {
            return string.Format("{0}{1}{2}", cmd, separador, nroLinea);
        }

        public GetEmbarkNumber(int nroLinea)
        {
            this.nroLinea = nroLinea;
        }
    }
}