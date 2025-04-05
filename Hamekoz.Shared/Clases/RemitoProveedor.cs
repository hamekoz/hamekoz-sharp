//
//  RemitoProveedor.cs
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
using System.Collections.Generic;
using System.Linq;

using Hamekoz.Core;
using Hamekoz.Fiscal;

namespace Hamekoz.Negocio
{
    //FIX IRemito, deberia implementar IComprobante
    public partial class RemitoProveedor : IPersistible, IIdentifiable, IComprobante, IComprobanteBase, IRemito
    {
        string ISearchable.ToSearchString()
        {
            throw new NotImplementedException();
        }

        #region IPersistible implementation

        public int Id
        {
            get;
            set;
        }

        #endregion

        public Proveedor Proveedor
        {
            get;
            set;
        }

        public string Numero
        {
            get;
            set;
        }

        public NumeracionDeComprobante Tipo
        {
            get;
            set;
        }

        public DateTime Emision
        {
            get;
            set;
        }

        public DateTime Contable
        {
            get;
            set;
        }

        public Flete Flete
        {
            get;
            set;
        }

        public Pedido Pedido
        {
            get;
            set;
        }

        public int Bultos
        {
            get;
            set;
        }

        public decimal ValorAsegurado
        {
            get;
            set;
        }

        public Domicilio DomicilioDeEntrega
        {
            get;
            set;
        }

        public string Observaciones
        {
            get;
            set;
        }

        public IList<RemitoItem> Items
        {
            get;
            set;
        } = new List<RemitoItem>();

        public bool Anulado
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Tipo.Abreviatura, Tipo.Letra, Numero);
        }

        #region IRemito

        IDescriptible IRemito.Destinatario
        {
            get
            {
                return Proveedor;
            }
            set
            {
                Proveedor = (Proveedor)value;
            }
        }

        #endregion


        #region IComprobante

        IResponsable IComprobante.Responsable
        {
            get
            {
                return Proveedor;
            }
        }

        string IComprobante.PuntoDeVenta
        {
            get
            {
                return Tipo.Pre;
            }
        }

        IList<IItem> IComprobante.Items
        {
            get
            {
                return Items.Cast<IItem>().ToList();
            }
        }

        IList<IVAItem> IComprobante.IVAItems
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IList<ImpuestoItem> IComprobante.Impuestos
        {
            get
            {
                return new List<ImpuestoItem>();
            }
        }

        decimal IComprobante.Total
        {
            get
            {
                return Items.Sum(r => r.Total);
            }
        }

        decimal IComprobante.Neto
        {
            get
            {
                return Items.Sum(r => r.Neto);
            }
        }

        decimal IComprobante.IVA
        {
            get
            {
                return Items.Sum(r => r.ImporteIVA);
            }
        }

        decimal IComprobante.Gravado
        {
            get
            {
                return Items.Sum(r => r.Neto);
            }
        }

        decimal IComprobante.NoGravado
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        decimal IComprobante.Exento
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        decimal IComprobante.Tributos
        {
            get
            {
                return Items.Sum(r => r.Impuestos);
            }
        }

        #endregion

        #region IComprobanteBase

        string IComprobanteBase.Comprobante
        {
            get
            {
                return ToString();
            }
        }

        DateTime IComprobanteBase.Emision
        {
            get
            {
                return Emision.Date;
            }
        }

        IResponsable IComprobanteBase.Responsable
        {
            get
            {
                return Proveedor;
            }
        }

        decimal IComprobanteBase.Total
        {
            get
            {
                return Items.Sum(r => r.Total);
            }
        }

        #endregion
    }
}