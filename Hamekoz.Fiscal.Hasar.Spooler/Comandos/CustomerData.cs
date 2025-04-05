//
//  CustomerData.cs
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
    public class CustomerData : Comando
    {
        const string cmd = "b";

        public string texto { get; set; }

        readonly string nombre;
        readonly string cuit;
        readonly string responsabilidadIVA;
        readonly string tipoDocumento;
        readonly string domicilio;


        public string Comando()
        {
            return string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}{1}{6}", cmd, separador, nombre, cuit, responsabilidadIVA, tipoDocumento, domicilio);
        }

        public CustomerData(string nombre, string cuit, string responsabilidadIVA, string tipoDocumento, string domicilio)
        {
            this.nombre = nombre;
            this.cuit = cuit;
            this.responsabilidadIVA = responsabilidadIVA;
            this.tipoDocumento = tipoDocumento;
            this.domicilio = domicilio;
        }
    }
}