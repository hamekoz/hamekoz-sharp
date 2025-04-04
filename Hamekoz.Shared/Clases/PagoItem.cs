//
//  PagoItem.cs
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
using Hamekoz.Fiscal;

namespace Hamekoz.Negocio
{
    //TODO ver si es posible unificar con ReciboItem
    public partial class PagoItem : IItem, IIdentifiable
    {
        #region IIdentifiable implementation

        public int Id
        {
            get;
            set;
        }

        #endregion

        public int Renglon
        {
            get;
            set;
        }

        public CuentaContable CuentaContable
        {
            get;
            set;
        }

        public Cheque Cheque
        {
            get;
            set;
        }

        string ChequeToString()
        {
            return Cheque == null ? string.Empty : string.Format("Cheque: {0} Nro {1} al {2:d}", Cheque.Banco, Cheque.Numero, Cheque.Cobro);
        }

        #region IItem implementation

        public string Codigo
        {
            get
            {
                return CuentaContable == null ? string.Empty : CuentaContable.Codigo.ToString();
            }
        }

        string descripcion;

        public string Descripcion
        {
            get
            {
                return CuentaContable == null ? descripcion : string.Format("{0} {1}", CuentaContable, ChequeToString());
            }
            set { descripcion = value; }
        }

        string IItemControladorFiscal.DescripcionCorta
        {
            get
            {
                return CuentaContable == null ? descripcion : CuentaContable.Cuenta;
            }
        }

        public int Lote
        {
            get
            {
                return 0;
            }
        }

        public decimal Cantidad
        {
            get;
            set;
        }

        public decimal Precio
        {
            get;
            set;
        }

        decimal IItem.Neto
        {
            get
            {
                return Total; //UNDONE revisar
            }
        }

        IVA IItem.Iva
        {
            get
            {
                return IVA.NoCorresponde;
            }
        }

        decimal IItem.ImporteIVA
        {
            get
            {
                return 0;
            }
        }

        decimal IItem.Impuestos
        {
            get
            {
                return 0;
            }
        }

        public decimal Total
        {
            get;
            set;
        }

        #endregion
    }
}