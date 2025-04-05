//
//  Empleado.cs
//
//  Author:
//       Ezequiel Taranto <ezequiel89@gmail.com>
//
//  Copyright (c) 2016 
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
    public partial class Empleado : IPersistible, ISearchable
    {
        #region IPersistible implementation

        public int Id { get; set; }

        #endregion

        public string NombresApellidos { get; set; }

        public string Dni { get; set; }

        #region ISearchable implementation

        public string ToSearchString()
        {
            return string.Format("[Empleado: Id={0}, NombresApellidos={1}, Dni={2}]", Id, NombresApellidos, Dni);
        }

        #endregion
    }
}