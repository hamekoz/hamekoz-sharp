//
//  Agip.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Hamekoz.Data;

namespace Hamekoz.Argentina.Agip
{
	public static class Agip
	{
		public static void Exportar (List<RegistroImportacionRetencionPercepcion> registros, string archivo)
		{
			StreamWriter sw = File.CreateText (archivo);
			foreach (var registro in registros) {
				sw.WriteLine (registro.ToFixedString ());
			}
			sw.Close ();
		}

		public static void Exportar (List<RegistroImportacionNotaDeCredito> registros, string archivo)
		{

			StreamWriter sw = File.CreateText (archivo);
			foreach (var registro in registros) {
				sw.WriteLine (registro.ToFixedString ());
			}
			sw.Close ();
		}

		public static void DescargarPadronDeContribuyentesRegimenGeneral ()
		{
			Process.Start ("http://www.agip.gob.ar/agentes/agentes-de-recaudacion-e-informacion");
		}

		public static void DescargarPadronDeContribuyentesDeRiesgoFiscal ()
		{
			Process.Start ("http://www.agip.gov.ar/web/banners-comunicacion/alto_riesgo_fiscal.htm");
		}

		public static void DescargarPadronDeContribuyentesConAlicuotasDiferenciales ()
		{
			Process.Start ("http://www.agip.gov.ar/web/agentes-recaudacion/padron-.html");
		}

		public static void DescargarPadronDeContribuyentesRegimenSimplificado ()
		{
			Process.Start ("http://www.agip.gob.ar/agentes/agentes-de-recaudacion/ib-agentes-recaudacion/padrones/ag-rec-padron-contribuyentes");
		}


		/// <summary>
		/// Importar un archivo del padron unificado.
		/// Validos para:
		/// - Padrón de Riesgo Fiscal
		/// - Padrón de contribuyentes exentos, de actividades promovidas, de nuevos emprendimientos y con alícuotas diferenciales.
		/// <see href="http://www.agip.gov.ar/web/files/DISENOODEREGISTROPADRONUNIFICADO.pdf"/>
		/// <seealso href="http://www.agip.gov.ar/web/banners-comunicacion/alto_riesgo_fiscal.htm"/>
		/// <seealso href="http://www.agip.gov.ar/web/agentes-recaudacion/padron-.html"/>
		/// </summary>
		/// <param name="archivo">Ruta absoluta al archivo.</param>
		public static void ImportarPadronUnificado (string archivo)
		{
			var stream = new FileStream (archivo, FileMode.Open, FileAccess.Read);
			var reader = new StreamReader (stream);
			var dbagip = new DB {
				ConnectionName = "Hamekoz.Argentina.Agip"
			};
			while (!reader.EndOfStream) {
				string linea = reader.ReadLine ();
				try {
					var registro = new RegistroPadronUnificado (linea);
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
			reader.Close ();
		}

		public const decimal NetoMinimoImponibleParaPercepcion = 300;

		/// <summary>
		/// Alicuotas the retencion.
		/// </summary>
		/// <returns>Devuelve la alicuota o -1 si no esta en el padron</returns>
		/// <param name="cuit">Cuit</param>
		public static decimal AlicuotaPercepcion (string cuit)
		{
			//TODO consultar alicuota en linea
			var dbagip = new DB {
				ConnectionName = "Hamekoz.Argentina.Agip"
			};
			string sql = string.Format ("SELECT alicuotaPercepcion FROM agip.dbo.padron WHERE cuit = '{0}' AND '{1:d}' BETWEEN fechaVigenciaDesde AND fechaVigenciaHasta"
				, cuit.Limpiar ()
				, DateTime.Now.Date);
			decimal alicuota = -1;
			var dataset = dbagip.SqlToDataSet (sql);
			if (dataset.Tables [0].Rows.Count > 0) {
				alicuota = decimal.Parse (dataset.Tables [0].Rows [0] ["alicuotaPercepcion"].ToString ());
			}
			return alicuota;
		}


		/// <summary>
		/// Alicuotas the retencion.
		/// </summary>
		/// <returns>Devuelve la alicuota o -1 si no esta en el padron</returns>
		/// <param name="cuit">Cuit</param>
		public static decimal AlicuotaRetencion (string cuit)
		{
			//TODO consultar alicuota en linea
			var dbagip = new DB {
				ConnectionName = "Hamekoz.Argentina.Agip"
			};
			string sql = string.Format ("SELECT alicuotaRetencion FROM agip.dbo.padron WHERE cuit = '{0}' AND '{1:d}' BETWEEN fechaVigenciaDesde AND fechaVigenciaHasta"
				, cuit.Limpiar ()
				, DateTime.Now.Date);
			decimal alicuota = -1;
			var dataset = dbagip.SqlToDataSet (sql);
			if (dataset.Tables [0].Rows.Count > 0) {
				alicuota = decimal.Parse (dataset.Tables [0].Rows [0] ["alicuotaRetencion"].ToString ());
			}
			return alicuota;
		}
	}
}

