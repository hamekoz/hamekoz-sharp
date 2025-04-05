//
//  Impuesto.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2017 Hamekoz - www.hamekoz.com.ar
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
using Hamekoz.Core;
using Hamekoz.Negocio;

namespace Hamekoz.Negocio
{
    public partial class Impuesto : IPersistible, IIdentifiable, IDescriptible
    {
        #region IDescriptible implementation

        public int Id
        {
            get;
            set;
        }

        public string Descripcion
        {
            get;
            set;
        }

        public CuentaContable CuentaContable
        {
            get;
            set;
        }

        public bool Inactivo
        {
            get;
            set;
        }

        #endregion

        public override string ToString()
        {
            return Descripcion;
        }
    }
}