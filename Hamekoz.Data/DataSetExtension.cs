//
//  DataSetExtension.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2010 Hamekoz
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
using System.Text;
using System.Data;
using System.IO;

namespace Hamekoz.Data
{
    public enum Delimitador
    {
        Coma = 44,
        PuntoComa = 59,
        Tabulacion = 9,
        Guion = 45
    }

    public static class DataSetExtension
    {
        /// <summary>
        /// Exporta el contenido de un DataSet a un archivo CSV.
        /// </summary>
        /// <param name="dataSet">DataSet a exportar</param>
        /// <param name="delimitador">Delimitador de celda a utilizar</param>
        /// <returns>Resultado de la exportacion</returns>
        public static void ToCSV(this DataSet dataSet, Delimitador delimitador, string filename)
        {
            StringBuilder dataToExport = new StringBuilder();
            StreamWriter sw = new StreamWriter(filename);
            foreach (DataTable dtExport in dataSet.Tables)
            {
                StringBuilder headerToExport = new StringBuilder();
                foreach (DataColumn dCol in dtExport.Columns)
                {
                    headerToExport.Append((char)34 + dCol.ColumnName + (char)34 + (char)delimitador);
                }
                headerToExport.Remove(headerToExport.Length - 1, 1);
                headerToExport.Append(Environment.NewLine);
                dataToExport.Append(headerToExport);
                StringBuilder bodyToExport = new StringBuilder();
                foreach (DataRow dRow in dtExport.Rows)
                {
                    foreach (object obj in dRow.ItemArray)
                    {
                        bodyToExport.Append(obj.ToString() + (char)delimitador);
                    }
                    bodyToExport.Remove(bodyToExport.Length - 1, 1);
                    bodyToExport.Append(Environment.NewLine);
                }
                dataToExport.Append(bodyToExport);
                dataToExport.Append(Environment.NewLine);
                dataToExport.Append(Environment.NewLine);
            }

            sw.Write(dataToExport);
            sw.Close();
        }

        /// <summary>
        /// Exporta el contenido de la primer tabla del DataSet a un archivo CSV realizando corte y control
        /// </summary>
        /// <param name="dataSet">DataSet que contiene la tabla a exportar</param>
        /// <param name="delimitador">Delimitador de celda a utilizar</param>
        /// <param name="colControl">Columna a utilizar para el corte y control</param>
        /// <param name="ColValor">Columna que representa los datos a presentar en forma horizontal para cada corte realizado</param>
        /// <returns>Resultado de la exportacion</returns>
        /// <remarks>
        /// El primer conjunto de datos representa el nombre de las columnas, salvo para la columna control que se corresponde con el nombre de la columna de la tabla.
        /// Cada corte debe contene la misma cantida de renglones para que se corresponde con las columnas al realizar el pivoteo.
        /// </remarks>
        public static void ToCSVPivot(this DataSet dataSet, Delimitador delimitador, string filename, int colControl, int ColValor)
        {
            StringBuilder dataToExport = new StringBuilder();
            DataTable dtExport = dataSet.Tables[0];
            StringBuilder bodyToExport = new StringBuilder();
            StreamWriter sw = new StreamWriter(filename);
            //Inicializo la variable de control
            object control = dtExport.Rows[0].ItemArray[colControl];
            //Agrego el primer objeto del control a renglon a exportar
            bodyToExport.Append(dtExport.Columns[colControl].ColumnName + (char)delimitador);
            foreach (DataRow dRow in dtExport.Rows)
            {
                //Cuando cambia la variable de control
                if (!dRow.ItemArray[colControl].Equals(control))
                {
                    //Agrego al cuerpo del archivo el renglon generado
                    bodyToExport.Remove(bodyToExport.Length - 1, 1);
                    bodyToExport.Append(Environment.NewLine);
                    dataToExport.Append(bodyToExport);
                    //Reninicalizo las variables de control para el nuevo corte
                    bodyToExport = new StringBuilder();
                    control = dRow.ItemArray[colControl];
                    bodyToExport.Append(control.ToString() + (char)delimitador);
                }
                //Siempre agrego el valor de la columna valor al renglon a exportar
                bodyToExport.Append(dRow[ColValor].ToString() + (char)delimitador);
            }
            //Agrego al cuerpo del archivo el ultimo renglon exportado
            bodyToExport.Remove(bodyToExport.Length - 1, 1);
            //Finalizo la composicion del archivo
            dataToExport.Append(bodyToExport);
            dataToExport.Append(Environment.NewLine);
            dataToExport.Append(Environment.NewLine);


            sw.Write(dataToExport);
            sw.Close();
        }
    }
}