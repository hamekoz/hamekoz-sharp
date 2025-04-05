//
//  TiposDeResposableAFIP.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <rodrigo@hamekoz.com.ar>
//
//  Copyright (c) 2014 Hamekoz - www.hamekoz.com.ar
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

namespace Hamekoz.Fiscal
{
    public enum TiposDeResposableAFIP
    {
        ResponsableInscripto = 1,
        ResponsableNoInscripto = 2,
        NoResponsable = 3,
        SujetoExento = 4,
        ConsumidorFinal = 5,
        ResponsableMonotributo = 6,
        SujetoNoCategorizado = 7,
        ProveedorDelExterior = 8,
        ClienteDelExterior = 9,
        LiberadoLeyNro19640 = 10,
        ResponsableInscriptoAgenteDePercepción = 11,
        PequeñoContribuyenteEventual = 12,
        MonotributistaSocial = 13,
        PequeñoContribuyenteEventualSocial = 14,
    }
}