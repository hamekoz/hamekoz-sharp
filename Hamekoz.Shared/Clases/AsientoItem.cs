//
//  AsientoItem.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//       Ezequiel Taranto <ezequiel89@gmail.com>
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

using Hamekoz.Core;

namespace Hamekoz.Negocio
{
    public partial class AsientoItem : IPersistible, IIdentifiable
    {
        //HACK no deberia tener constructor
        public AsientoItem()
        {
            CuentaContable = new CuentaContable();
            CentroDeCosto = new CentroDeCosto();
        }

        public int Id
        {
            get;
            set;
        }

        public CuentaContable CuentaContable
        {
            get;
            set;
        }

        internal Cotizacion Cotizacion
        {
            get;
            set;
        }

        decimal debe;

        public decimal Debe
        {
            get
            {
                return Math.Round(debe, 2);
            }
            set
            {
                debe = value;
            }
        }

        decimal haber;

        public decimal Haber
        {
            get
            {
                return Math.Round(haber, 2);
            }
            set
            {
                haber = value;
            }
        }

        public CentroDeCosto CentroDeCosto
        {
            get;
            set;
        }

        public Cheque Cheque;
    }
}