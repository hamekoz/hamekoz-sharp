//
//  RegistroDeImportacionIVA.cs
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
using System;

namespace Hamekoz.Argentina.SIAP
{
    public class RegistroDeImportacionIVA
    {
        //TODO cpereyra generalizar para utlizar en otros reportes y lugares

        /// <summary>
        /// Gets or sets the codigo.
        /// </summary>
        /// <value>The codigo. Entero posicion 1 a 3</value>
        public int Codigo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the cuit.
        /// </summary>
        /// <value>The cuit. Posicion 4 a 16</value>
        public string Cuit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fecha.
        /// </summary>
        /// <value>The fecha. Posicion 17 a 26</value>
        public DateTime Fecha
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the detalle. 16 caracteres
        /// </summary>
        /// <value>The detalle. Posicion 27 a 42</value>
        public string Detalle
        {
            get;
            set;
        }

        public string DetalleParte1()
        {
            return Detalle.Substring(0, 8);
        }

        public string DetalleParte2()
        {
            return Detalle.Substring(Detalle.Length - 8, 8);
        }

        /// <summary>
        /// Gets or sets the monto.
        /// </summary>
        /// <value>The monto. Posicion 43 a 58 </value>
        public double Monto
        {
            get;
            set;
        }

        public string ToFixedString()
        {
            return string.Format("{0:D3}{1}{2:d}{3}{4:0000000000000.00}"
                    , Codigo
                    , Cuit.PadRight(13).Substring(0, 13)
                    , Fecha
                    , Detalle.PadRight(16).Substring(0, 16)
                    , Monto
            );
        }
    }
}