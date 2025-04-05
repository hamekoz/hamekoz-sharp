//
//  CuentaContable.cs
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
using System.ComponentModel;

using Hamekoz.Core;

namespace Hamekoz.Negocio
{
    //UNDONE ver si esto es necesario persistilo
    public enum CuentaBase
    {
        Activo = 1,
        Pasivo,
        Ingreso,
        Egreso,
    }

    public static class CuentaBaseExtension
    {
        public static int Inicio(this CuentaBase cuenta)
        {
            switch (cuenta)
            {
                case CuentaBase.Activo:
                    return 10000;
                case CuentaBase.Pasivo:
                    return 32900;
                case CuentaBase.Ingreso:
                    return 55000;
                case CuentaBase.Egreso:
                    return 77500;
                default:
                    return 0;
            }
        }

        public static int Fin(this CuentaBase cuenta)
        {
            switch (cuenta)
            {
                case CuentaBase.Activo:
                    return 32890;
                case CuentaBase.Pasivo:
                    return 54990;
                case CuentaBase.Ingreso:
                    return 77490;
                case CuentaBase.Egreso:
                    return 100000;
                default:
                    return 0;
            }
        }
    }

    public partial class CuentaContable : IPersistible, IIdentifiable, IDescriptible
    {
        #region Enums

        //UNDONE Confirmar los tipos de cuentas contables que puede haber
        public enum Tipos
        {
            SinDato,
            Efectivo,
            Cheque,
        }

        //TODO deberia utilizarse la clase Moneda
        public enum Monedas
        {
            [DescriptionAttribute("SIN MONEDA")]
            SINMONEDA,
            PESOS,
            DOLARES,
            BONOS,
            LECOP,
        }

        #endregion

        public int Id
        {
            get;
            set;
        }

        public int Codigo
        {
            get;
            set;
        }

        public string Cuenta
        {
            get;
            set;
        }

        public CuentaBase Base
        {
            get;
            set;
        }

        //HACK revisar atributos de acceso
        public int cuentaSumaId;

        public CuentaContable Suma
        {
            get;
            set;
        }

        public IList<CuentaContable> Cuentas
        {
            get;
            set;
        }

        public bool Modificable
        {
            get;
            set;
        }

        public Tipos Tipo
        {
            get;
            set;
        }

        //TODO deberia utilizarse la clase Moneda
        public Monedas Moneda
        {
            get;
            set;
        }

        //HACK revisar atributos de acceso
        public int bancoId;

        public Banco Banco
        {
            get;
            set;
        }

        public bool RecibeAsientos
        {
            get;
            set;
        }

        #region Propiedades calculadas

        public decimal Saldo
        {
            get;
            set;
        }

        #endregion

        public int Tabulacion
        {
            get;
            set;
        }

        public bool Inactiva
        {
            get;
            set;
        }

        public IDictionary<int, CuentaContable> Hijas
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Descripcion;
        }

        public string Descripcion
        {
            get
            {
                return string.Format("{0} {1}", Codigo, Cuenta);
            }
        }
    }
}