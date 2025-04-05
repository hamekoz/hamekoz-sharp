//
//  CuentaCorriente.cs
//
//  Author:
//       Juan Angel Dinamarca <juan.angel.dinamarca@gmail.com>
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

using Hamekoz.Core;

namespace Hamekoz.Negocio
{
    public partial class CuentaCorriente : IPersistible, IComprobanteVencimiento
    {
        public Type comprobanteType;
        public int comprobanteId;
        public int asientoId;

        public DateTime Emision
        {
            get;
            set;
        }

        public Comprobante Comprobante
        {
            get;
            set;
        }

        public string Numero
        {
            get;
            set;
        }

        public DateTime Vencimiento
        {
            get;
            set;
        }

        public decimal Deudor
        {
            get;
            set;
        }

        public decimal Acreedor
        {
            get;
            set;
        }

        public decimal Restante
        {
            get;
            set;
        }

        public decimal Saldo
        {
            get;
            set;
        }

        public string Estado
        {
            get
            {
                return Vencimiento < DateTime.Now.Date && Restante > 0 ? "V" : string.Empty;
            }
        }
    }
}