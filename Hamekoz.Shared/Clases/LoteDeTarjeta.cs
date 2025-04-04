//
//  LoteDeTarjeta.cs
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
using System;

using Hamekoz.Core;

namespace Hamekoz.Negocio
{
    //TODO ver si es posible unificar con PagoItem
    [Obsolete("Realmente no se utiliza para hacer sequimiento, y podria omitirse completamente")]
    public partial class LoteDeTarjeta : IIdentifiable, IPersistible
    {
        #region IIdentifiable implementation

        public int Id
        {
            get;
            set;
        }

        #endregion

        public int Numero
        {
            get;
            set;
        }

        public DateTime Fecha
        {
            get;
            set;
        } = DateTime.Now.Date;

        public decimal Importe
        {
            get;
            set;
        }

        public bool Acreditado
        {
            get;
            set;
        }

        public Asiento Asiento
        {
            get;
            set;
        }

        public CuentaContable CuentaContable
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Numero.ToString();
        }
    }

}