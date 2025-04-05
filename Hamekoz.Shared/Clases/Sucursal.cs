//
//  Sucursal.cs
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
using System.Collections.Generic;

namespace Hamekoz.Negocio
{
    //UNDONE completar clase sucursal
    public partial class Sucursal : CentroDeCosto

    /* Cambio no fusionado mediante combinación del proyecto 'Hamekoz.Core(net9.0)'
    Antes:
            string nombre;
    Después:
            readonly string nombre;
    */
    {
        readonly string nombre;
        //TODO hay que separar la logica de centro de costo de la sucursal y hacer Nombre autopropiedad
        public string Nombre
        {
            get
            {
                return Descripcion;
            }
            set
            {
                Descripcion = value;
            }
        }

        public Estados Estado
        {
            get;
            set;
        }

        public Domicilio Domicilio
        {
            get;
            set;
        }

        public string Telefono
        {
            get;
            set;
        }

        public string Fax
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public ListaDePrecios ListaDePrecios
        {
            get;
            set;
        }

        public IList<Contacto> Contactos
        {
            get;
            set;
        }

        public string Observaciones
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}