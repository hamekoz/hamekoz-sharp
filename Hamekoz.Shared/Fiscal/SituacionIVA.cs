//
//  SituacionIVA.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2015 Hamekoz - www.hamekoz.com.ar
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

namespace Hamekoz.Fiscal
{
    //TODO deberia ser un enumerado de TiposDeResponsables segun la tabla de AFIP
    [Obsolete("Usar TipoDeResponsable")]
    //Mapeado con el PADRON Afip
    public enum SituacionIVA
    {
        SIN_DATO = 0,
        CONSUMIDOR_FINAL = 1,
        MONOTRIBUTO = 2,
        RESPONSABLE_INSCRIPTO = 3,
        EXENTO = 4
    }

    /// <summary>
    /// Tipos de responsables.
    /// </summary>
    /// <description>Lista de responsabilidades frente al IVA segun AFIP</description>
    /// <see ref="https://www.afip.gob.ar/fe/documentos/TABLA%20TIPO%20RESPONSABLES%20V.0%20%2025082010.xls"/>
    public enum TipoDeResponsable
    {
        Sin_Dato = 0,
        IVA_Responsable_Inscripto = 1,
        IVA_Responsable_no_Inscripto = 2,
        IVA_no_Responsable = 3,
        IVA_Sujeto_Exento = 4,
        Consumidor_Final = 5,
        Responsable_Monotributo = 6,
        Sujeto_no_Categorizado = 7,
        Proveedor_del_Exterior = 8,
        Cliente_del_Exterior = 9,
        IVA_Liberado_Ley_Nro_19640 = 10,
        IVA_Responsable_Inscripto_Agente_de_Percepción = 11,
        Pequeño_Contribuyente_Eventual = 12,
        Monotributista_Social = 13,
        Pequeño_Contribuyente_Eventual_Social = 14,
    }
}