//
//  Permiso.cs
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
namespace Hamekoz.Core
{
    public partial class Permiso
    {
        public int Guid
        {
            get;
            set;
        }

        public bool Acceso
        {
            get;
            set;
        }

        public bool Agregar
        {
            get;
            set;
        }

        public bool Modificar
        {
            get;
            set;
        }

        public bool Eliminar
        {
            get;
            set;
        }

        public bool Imprimir
        {
            get;
            set;
        }

        public bool OtrasAcciones
        {
            get;
            set;
        }
    }
}