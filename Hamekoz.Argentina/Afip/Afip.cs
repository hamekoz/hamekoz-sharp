//
//  Afip.cs
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
using System.Data;
using System.IO;
using System.Net;

using Hamekoz.Data;

namespace Hamekoz.Argentina.Afip
{
    public static class Afip
    {
        public static bool Enpadronado(string cuit)
        {
            //TODO verificar los datos en linea por defecto, y en caso de falla en la base local.
            var sql = string.Format("SELECT COUNT(*) FROM afip.dbo.padron WHERE CUIT = {0}", cuit.Limpiar());
            return (int)DB.Instancia.SqlToScalar(sql) > 0;
        }

        public static RegistroPadron Load(string cuit)
        {
            string sql;
            sql = string.Format(
                "SELECT * FROM afip.dbo.padron WHERE CUIT = {0}"
                , cuit.Limpiar()
            );
            using (DataSet dataset = DB.Instancia.SqlToDataSet(sql))
            {
                if (dataset.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = dataset.Tables[0].Rows[0];
                    return new RegistroPadron
                    {
                        CUIT = dr.Field<long>("cuit"),
                        ImpuestoGanancias = dr.Field<string>("impGanancias"),
                        ImpuestoIVA = dr.Field<string>("impiva"),
                        Monotributo = dr.Field<string>("monotributo"),
                        IntegranteSociedad = dr.Field<string>("integrantesoc"),
                        Empleador = dr.Field<string>("empleador"),
                        ActividadMonotributo = dr.Field<string>("actividadmonotributo")
                    };
                }
                else
                {
                    var mensaje = string.Format("El CUIT {0} no se encuentra enpadronado en AFIP", cuit.Formato());
                    throw new DataException(mensaje);
                }
            }
        }

        public static void DescargarPadron(bool conDenominacion)
        {
            DescargarPadron(conDenominacion, string.Empty);
        }

        public static void DescargarPadron(bool conDenominacion, string destino)
        {
            const string urlBase = "http://www.afip.gob.ar/genericos/cInscripcion/archivos";
            string archivo = string.Format("{0}apellidoNombreDenominacion", conDenominacion ? "" : "SIN");
            string url = string.Format("{0}/{1}.zip", urlBase, archivo);
            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(url, string.Format("{0}AFIP-Padron-{1}-{2:yyyyMMdd}.zip", destino, archivo, DateTime.Now));
            }
        }

        public static void ImportarPadronUnificado(string archivo, bool denominacion)
        {
            var stream = new FileStream(archivo, FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(stream);
            var dbafip = new DB
            {
                ConnectionName = "Hamekoz.Argentina.Afip"
            };
            while (!reader.EndOfStream)
            {
                string linea = reader.ReadLine();
                try
                {
                    var registro = new RegistroPadron(linea, denominacion);
                    //TODO cambiar SP por consulta de texto plana
                    //TODO controlar la existencia de la tabla en la base de datos.
                    //UNDONE considerar la posibilidad de almacenar la denominacion
                    dbafip.SP("padronTmpActualizar"
                        , "cuit", registro.CUIT
                        , "impGanancias", registro.ImpuestoGanancias
                        , "impiva", registro.ImpuestoIVA
                        , "monotributo", registro.Monotributo
                        , "integrantesoc", registro.IntegranteSociedad
                        , "empleador", registro.Empleador
                        , "actividadmonotributo", registro.ActividadMonotributo
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en importacion:\n\tRegistro: {0}\n\tError: {1}", linea, ex.Message);
                }
            }
            reader.Close();
        }
    }
}