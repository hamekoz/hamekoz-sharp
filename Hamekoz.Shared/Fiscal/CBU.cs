//
//  CBU.cs
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
using System.Linq;

namespace Hamekoz.Argentina
{
    public static class CBU
    {
        public static bool ValidarCBU(this string cbu)
        {
            if (cbu == null)
                return false;
            if (cbu == string.Empty)
                return false;
            cbu = cbu.Replace("-", string.Empty);
            cbu = cbu.Replace(" ", string.Empty);
            if (cbu.Length != 22)
                return false;
            if (!cbu.All(char.IsDigit))
                return false;

            //TODO completar algoritmo de verificacion
            //https://es.wikipedia.org/wiki/Clave_Bancaria_Uniforme
            return true;
        }

        public static string ComponerCBU(CodigoDeBanco banco, int sucursal, string cuenta)
        {
            char cero = char.Parse("0");
            cuenta = cuenta.PadLeft(13, cero);
            string bancoCodigo = ((int)banco).ToString().PadLeft(3, cero);
            string sucursalCodigo = sucursal.ToString().PadLeft(4, cero);

            int B0 = int.Parse(bancoCodigo[0].ToString());
            int B1 = int.Parse(bancoCodigo[1].ToString());
            int B2 = int.Parse(bancoCodigo[2].ToString());

            int S0 = int.Parse(sucursalCodigo[0].ToString());
            int S1 = int.Parse(sucursalCodigo[1].ToString());
            int S2 = int.Parse(sucursalCodigo[2].ToString());
            int S3 = int.Parse(sucursalCodigo[3].ToString());

            int C1 = int.Parse(cuenta[1].ToString());
            int C2 = int.Parse(cuenta[2].ToString());
            int C3 = int.Parse(cuenta[3].ToString());
            int C4 = int.Parse(cuenta[4].ToString());
            int C5 = int.Parse(cuenta[5].ToString());
            int C6 = int.Parse(cuenta[6].ToString());
            int C7 = int.Parse(cuenta[7].ToString());
            int C8 = int.Parse(cuenta[8].ToString());
            int C9 = int.Parse(cuenta[9].ToString());
            int C10 = int.Parse(cuenta[10].ToString());
            int C11 = int.Parse(cuenta[11].ToString());
            int C12 = int.Parse(cuenta[12].ToString());

            int bloqueUnoSuma = (B0 * 7 + B1 + B2 * 3 + S0 * 9 + S1 * 7 + S2 + S3 * 3) % 10;
            int bloqueUnoVerificador = bloqueUnoSuma == 0 ? bloqueUnoSuma : 10 - bloqueUnoSuma;

            int bloqueDosSuma = (C1 * 9 + C2 * 7 + C3 + C4 * 3 + C5 * 9 + C6 * 7 + C7 + C8 * 3 + C9 * 9 + C10 * 7 + C11 + C12 * 3) % 10;
            int bloqueDosVerificador = bloqueDosSuma == 0 ? bloqueDosSuma : 10 - bloqueDosSuma;

            return string.Format("{0:D3}{1:D4}{2:D1}-{3:D13}{4:D1}"
                , bancoCodigo
                , sucursal
                , bloqueUnoVerificador
                , long.Parse(cuenta)
                , bloqueDosVerificador
            );
        }

        //UNDONE Completar enumerado de codigos de banco
        public enum CodigoDeBanco
        {
            BANCO_DE_LA_PROVINCIA_DE_BUENOS_AIRES = 14,
            /*
            A.B.N AMRO BANK N.V. = 5,
            BANCO DE GALICIA Y BUENOS AIRES S.A. = 7,
            010 LLOYDS TSB BANK plc. = 10,
            011 BANCO DE LA NACION ARGENTINA = 11,
            014 BANCO DE LA PROVINCIA DE BUENOS AIRES = 14,
            015 BANKBOSTON, NATIONAL ASSOCIATION = 15,
            016 CITIBANK N.A. = 16,
            017 BBVA BANCO FRANCES S.A. = 17,
            018 THE BANK OF TOKYO - MITSUBISHI, LTD. = 18,
            020 BANCO DE LA PROVINCIA DE CORDOBA S.A. = 20,
            027 BANCO SOCIETE GENERALE S.A. = 27,
            029 BANCO DE LA CIUDAD DE BUENOS AIRES = 29,
            034 BANCO PATAGONIA SUDAMERIS S.A. = 34,

            044 BANCO HIPOTECARIO S.A. = 44,

            045 BANCO DE SAN JUAN S.A. = 45,
            046 BANCO DO BRASIL S.A. = 46,
            060 BANCO DEL TUCUMAN S.A. = 60, 
            065 BANCO MUNICIPAL DE ROSARIO = 65,
            072 BANCO RIO DE LA PLATA S.A. = 72,
            079 BANCO REGIONAL DE CUYO S.A. = 79,
            083 BANCO DEL CHUBUT S.A. = 83,
            086 BANCO DE SANTA CRUZ S.A. = 86,
            093 BANCO DE LA PAMPA SOCIEDAD DE ECONOMIA MIXTA = 93,
            094 BANCO DE CORRIENTES S.A. = 94,
            097 BANCO PROVINCIA DEL NEUQUEN S.A. = 97,
            137 BANCO EMPRESARIO DE TUCUMAN COOP. LTDO. = 137,
            147 BANCO B. I. CREDITANSTALT S.A. = 147,

            150 HSBC BANK ARGENTINA S.A. = 150,
            165 J P MORGAN CHASE BANK SUCURSAL BUENOS AIRES = 165,
            191 BANCO CREDICOOP COOP. LTDO. = 191,
            198 BANCO DE VALORES S.A. = 198,
            247 BANCO ROELA S.A. = 247,
            254 BANCO MARIVA S.A. = 254,
            259 BANCO ITAU BUEN AYRE S.A. = 259,
            262 BANK OF AMERICA, NATIONAL ASSOCIATION = 262,
            265 BANCA NAZIONALE DEL LAVORO S.A. = 265,
            266 BNP PARIBAS = 266,
            268 BANCO PROVINCIA DE TIERRA DEL FUEGO = 268,
            269 BANCO DE LA REPUBLICA ORIENTAL DEL URUGUAY = 269,
            277 BANCO SAENZ S.A. = 277,
            281 BANCO MERIDIAN S.A. = 281,
            285 BANCO MACRO BANSUD S.A. = 285,
            293 BANCO MERCURIO S.A. = 293,

            294 ING BANK N.V. = 294,
            295 AMERICAN EXPRESS BANK LTD. S.A. = 295,
            297 BANCO BANEX S.A. = 297,
            299 BANCO COMAFI S.A.
            300 BANCO DE INVERSION Y COMERCIO EXTERIOR S.A.
            301 BANCO PIANO S.A.
            303 BANCO FINANSUR S.A.
            305 BANCO JULIO S.A.
            306 BANCO PRIVADO DE INVERSIONES S.A.
            309 NUEVO BANCO DE LA RIOJA S.A.
            310 BANCO DEL SOL S.A.
            311 NUEVO BANCO DEL CHACO S.A.
            312 M. B. A. BANCO DE INVERSIONES S.A.
            315 BANCO DE FORMOSA S.A.
            319 BANCO CMF S.A.
            321 BANCO DE SANTIAGO DEL ESTERO S.A.
            322 NUEVO BANCO INDUSTRIAL DE AZUL S.A.
            325 DEUTSCHE BANK S.A.
            330 NUEVO BANCO DE SANTA FE S.A.
            331 BANCO CETELEM ARGENTINA S.A.
            332 BANCO DE SERVICIOS FINANCIEROS S.A.
            335 BANCO COFIDIS S.A.

            336 BANCO BRADESCO ARGENTINA S.A.
            338 BANCO DE SERVICIOS Y TRANSACCIONES S.A.
            339 RCI BANQUE
            340 BACS BANCO DE CREDITO Y SECURITIZACION S.A.
            386 NUEVO BANCO DE ENTRE RIOS S.A.
            387 NUEVO BANCO SUQUIA S.A.
            388 NUEVO BANCO BISEL S.A.
            389 BANCO COLUMBIA S.A.
            */
        }
    }
}