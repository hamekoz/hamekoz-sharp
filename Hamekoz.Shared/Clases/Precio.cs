//
//  Precio.cs
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

using Hamekoz.Core;

namespace Hamekoz.Negocio
{
    public partial class Precio : IPersistible, ISearchable
    {
        public int Id
        {
            get;
            set;
        }

        public ListaDePrecios Lista
        {
            get;
            set;
        }

        public Articulo Articulo
        {
            get;
            set;
        }

        public decimal Importe
        {
            get;
            set;
        }

        public DateTime ModificadoEn
        {
            get;
            set;
        }

        public Empleado ModificadoPor
        {
            get;
            set;
        }

        public bool Modificado;

        #region ISearchable implementation

        public string ToSearchString()
        {
            return string.Format("[Precio: Id={0}, Lista={1}, Articulo={2}, Importe={3}, ModificadoEn={4}, ModificadoPor={5}, Rubro={6}, Codigo={7}]"
                , Id
                , Lista
                , Articulo
                , Importe
                , ModificadoEn
                , ModificadoPor
                , Articulo.Rubro.Descripcion
                , Articulo.Id
            );
        }

        #endregion
    }
}