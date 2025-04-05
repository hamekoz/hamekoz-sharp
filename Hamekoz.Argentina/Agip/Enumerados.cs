//
//  Enumerados.cs
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
namespace Hamekoz.Argentina.Agip
{
    public enum TipoDeOperacion
    {
        Retencion = 1,
        Percepcion = 2,
    }

    public enum TipoDeComprobante
    {
        Factura = 1,
        NotaDeDebito = 2,
        OrdenDePago = 3,
        BoletaDeDeposito = 4,
        LiquidacionDePago = 5,
        CertificadoDeObra = 6,
        Recibo = 7,
        ContDeLocacionDeServicio = 8,
        OtroComprobante = 9,
    }

    public enum Letra
    {
        A = 'A',
        B = 'B',
        C = 'C',
        M = 'M',
    }

    public enum TipoDeDocumento
    {
        CUIT = 3,
        CUIL = 2,
        CDI = 1,
    }

    public enum SituacionIngresosBrutos
    {
        Local = 1,
        ConvenioMultilateral = 2,
        NoInscripto = 4,
        RegimenSimplificado = 5,
    }

    public enum SituacionFrenteAlIVA
    {
        ResponsableInscripto = 1,
        Excento = 2,
        Monotributo = 4,
    }
}