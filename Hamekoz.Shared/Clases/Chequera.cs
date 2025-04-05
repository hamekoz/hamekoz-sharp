//
//  Chequera.cs
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

namespace Hamekoz.Negocio
{
    public partial class Chequera : IPersistible, IIdentifiable
    {
        #region IIdentifiable implementation

        public int Id
        {
            get;
            set;
        }

        #endregion

        public Banco Banco
        {
            get;
            set;
        }

        public int Desde
        {
            get;
            set;
        }

        public int Hasta
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ultimo numero de cheque utilizado.
        /// </summary>
        /// <value>The ultimo numero de cheque utilizado.</value>
        public int Ultimo
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("Chequera {0} {1} del {2} al {3}", Id, Banco.Nombre, Desde, Hasta);
        }
    }
}