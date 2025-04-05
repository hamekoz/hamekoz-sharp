//
//  Proveedor.cs
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

using Hamekoz.Core;
using Hamekoz.Fiscal;

namespace Hamekoz.Negocio
{
    public partial class Proveedor : IDescriptible, IResponsable
    {
        public int Id
        {
            get;
            set;
        }

        public string NombreFantasia
        {
            get;
            set;
        }

        public string RazonSocial
        {
            get;
            set;
        }

        public string CUIT
        {
            get;
            set;
        }

        public string IIBB
        {
            get;
            set;
        }

        public string DNI
        {
            get;
            set;
        }

        public Domicilio Domicilio
        {
            get;
            set;
        }

        public CondicionDePago CondicionDePago
        {
            get;
            set;
        }

        public Estados Estado
        {
            get;
            set;
        }

        public TipoDeResponsable Tipo
        {
            get;
            set;
        }

        IList<Telefono> Telefonos
        {
            get;
            set;
        }

        public string Telefono
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public double PuntajeDeEvaluacion
        {
            get;
            set;
        }

        public override string ToString()
        {
            return RazonSocial;
        }

        string IDescriptible.Descripcion
        {
            get
            {
                return RazonSocial;
            }
        }

        string IResponsable.Domicilio
        {
            get
            {
                return Domicilio.ToString();
            }
        }
    }
}