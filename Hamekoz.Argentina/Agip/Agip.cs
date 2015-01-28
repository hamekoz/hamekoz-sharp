//
//  Agip.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2014 Hamekoz
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
using System.IO;
using Hamekoz.Data;
using System.Diagnostics;

namespace Hamekoz.Argentina.Agip
{
	public class Agip
	{
		public static void Exportar(List<RegistroImportacionRetencionPercepcion> registros, string archivo)
		{
			StreamWriter sw = File.CreateText (archivo);
			foreach (var registro in registros) {
				sw.WriteLine (registro.ToFixedString ());
			}
			sw.Close ();
		}

		public static void Exportar(List<RegistroImportacionNotaDeCredito> registros, string archivo)
		{

			StreamWriter sw = File.CreateText (archivo);
			foreach (var registro in registros) {
				sw.WriteLine (registro.ToFixedString ());
			}
			sw.Close ();
		}

		public static void DescargarPadronDeContribuyentesDeRiesgoFiscal()
		{
			Process.Start ("http://www.agip.gov.ar/web/banners-comunicacion/alto_riesgo_fiscal.htm");
		}

		public static void DescargarPadronDeContribuyentesConAlicuotasDiferenciales()
		{
			Process.Start ("http://www.agip.gov.ar/web/agentes-recaudacion/padron-.html");
		}

		/// <summary>
		/// Importar un archivo del padron unificado.
		/// Validos para:
		/// - Padrón de Riesgo Fiscal
		/// - Padrón de contribuyentes exentos, de actividades promovidas, de nuevos emprendimientos y con alícuotas diferenciales.
		/// <see cref="http://www.agip.gov.ar/web/files/DISENOODEREGISTROPADRONUNIFICADO.pdf"/>
		/// <seealso cref="http://www.agip.gov.ar/web/banners-comunicacion/alto_riesgo_fiscal.htm"/>
		/// <seealso cref="http://www.agip.gov.ar/web/agentes-recaudacion/padron-.html"/>
		/// </summary>
		/// <param name="archivo">Ruta absoluta al archivo.</param>
		public static void ImportarPadronUnificado(string archivo){
			FileStream stream = new FileStream(archivo , FileMode.Open, FileAccess.Read);
			StreamReader reader = new StreamReader(stream);
			DB dbagip = new DB () {
				ConnectionName = "Hamekoz.Argentina.Agip"
			};
			while (!reader.EndOfStream)
			{
				string  linea = reader.ReadLine();
				try {
					RegistroPadronUnificado registro = new RegistroPadronUnificado (linea);
				//TODO cambiar SP por consulta de texto plana
				//TODO controlar la existencia de la tabla en la base de datos.
				dbagip.SP ("padronTmpActualizar"
					, "fechaPublicacion", registro.FechaDePublicacion
					, "cuit", registro.CUIT
					, "fechaVigenciaDesde", registro.FechaVigenciaDesde
					, "fechaVigenciaHasta", registro.FechaVigenciaHasta
					, "tipoContrInscr", registro.TipoDeContribuyenteInscripto
					, "marcaAltaBajaSujeto", registro.MarcaAltaSujeto
					, "marcaCbioAlicuota", registro.MarcaAlicuota
					, "alicuotaPercepcion", registro.AlicuotaPercepcion
					, "alicuotaRetencion", registro.AlicuotaRetencion
					, "nroGrupoPercepcion", registro.NumeroGrupoPercepcion
					, "nroGrupoRetencion", registro.NumeroGrupoRetencion
				);
				} catch (Exception ex) {
					Console.WriteLine ("Error en importacion:\n\tRegistro: {0}\n\tError: {1}", linea, ex.Message);
				}
			}
			reader.Close();
		}
	}
}

