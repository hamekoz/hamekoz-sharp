using System;
using System.Data;
using System.Data.Common;

namespace Hamekoz.Data
{
    public static class PlanillaDeCalculo
    {
        static readonly DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");

        /// <summary>
        /// Lee la estructura del esquema del libro excel y devuelve dicha estructura en un dataTable.
        /// Se utiliza para obtener los nombres de las "hojas" en los libros excel.
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public static DataTable LeerEsquemaLibro(string archivo)
        {
            //Los archivos de excel lo manejamos con el namespace "System.Data.OleDb".
            //Crearemos un dataset y un providerfactories el cual utilizaremos para leer es esquela del libro de excel:

            DataTable worksheets = null;
            //Excel 2003
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + archivo + ";Extended Properties=Excel 8.0;";
            //Excel 2007 en adelante
            //string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + archivo + ";Extended Properties=Excel 12.0;";

            //Con esta instrución leeremos el esquema del libro de excel
            try
            {
                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                worksheets = connection.GetSchema("Tables");
            }
            catch (Exception)
            {
                Console.WriteLine("Se produjo un error. Puede ser que la hoja de calculo a abrir no exista o posea un esquema diferente.");
            }
            return worksheets;
        }

        /// <summary>
        /// Lee la estructura de columnas de una hoja excel.
        /// Antes de utilizarse deben obtenerse los nombres de las hojas.
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="nombreHoja"></param>
        /// <returns></returns>
        public static DataTable LeerEsquemaHoja(string archivo, string nombreHoja)
        {
            DataTable columns = null;
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + archivo + ";Extended Properties=Excel 8.0;";

            //una vez leido el esquema del libro de excel, leeremos el esquema de la hoja,
            //para obtener las calumnas. Mediante la siguiente instrucción obtendremos lo mencionado.
            try
            {
                string[] restrictions = { null, null, nombreHoja, null };
                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                columns = connection.GetSchema("Columns", restrictions);
            }
            catch (Exception)
            {
                Console.WriteLine("Se produjo un error. Puede ser que la hoja de calculo a abrir no exista o posea un esquema diferente.");
            }
            return columns;
        }

        /// <summary>
        /// Devuelve en un dataTable una hoja completa de excel. Se debe obtener previamente el
        /// nombre de la hoja.
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="hoja"></param>
        /// <returns></returns>
        public static DataTable LeerDatosHoja(string archivo, string hoja)
        {
            var dsMsExcel = new DataSet();
            //leeremos los datos de la hoja de calculo de excel, con la siguiente instrucción
            //y lo mostraremos en un DataGridView
            try
            {
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + archivo + ";Extended Properties=Excel 8.0;";
                DbDataAdapter adapter = factory.CreateDataAdapter();
                DbCommand selectCommand = factory.CreateCommand();

                selectCommand.CommandText = "SELECT * FROM [" + hoja + "]";
                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
                selectCommand.Connection = connection;

                adapter.SelectCommand = selectCommand;
                dsMsExcel.Tables.Clear();
                adapter.Fill(dsMsExcel);

            }
            catch (Exception ex1)
            {
                Console.WriteLine("Se produjo un error. Puede ser que la hoja de calculo a abrir no exista o posea un esquema diferente. {0}", ex1.Message);
            }
            return dsMsExcel.Tables[0];
        }
    }
}