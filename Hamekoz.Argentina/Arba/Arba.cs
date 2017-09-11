//
//  Arba.cs
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
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Hamekoz.Data;

namespace Hamekoz.Argentina.Arba
{
	public static class Arba
	{
		static string CalcularHashMD5 (string archivo)
		{
			var fs = new FileStream (archivo, FileMode.Open, FileAccess.Read);
			var hash = new MD5CryptoServiceProvider ();
			Int64 currentPos = fs.Position;
			fs.Seek (0, SeekOrigin.Begin);
			var sb = new StringBuilder ();
			foreach (Byte b in hash.ComputeHash(fs)) {
				sb.Append (b.ToString ("X2"));
			}
			fs.Close ();
			return sb.ToString ();
		}

		static string ContenidoDeArchivo (string archivo)
		{
			var stream = new FileStream (archivo, FileMode.Open, FileAccess.Read);
			string cadena = string.Empty;
			if (stream != null) {
				stream.Position = 0;
				var reader = new StreamReader (stream);

				while (reader.Peek () > -1) {
					cadena = cadena + reader.ReadLine () + "\r\n";
				}

				reader.Close ();
				stream.Close ();
			}
			return cadena;
		}

		public static void DescargarPadron (string usuario, string clave, int año, int mes)
		{
			DescargarPadron (usuario, clave, año, mes, string.Empty);
		}

		/// <summary>
		/// Descargars the padron.
		/// </summary>
		/// <see href="http://www.arba.gov.ar/Informacion/IBrutos/LinksIIBB/RegimenSujeto.asp"/>
		/// <param name="usuario">Usuario.</param>
		/// <param name="clave">Clave.</param>
		/// <param name = "año">Año del padron a descargar</param>
		/// <param name = "mes">Mes del padron a descargar</param>
		/// <param name = "destino">Ruta destino de la descarga</param>
		public static void DescargarPadron (string usuario, string clave, int año, int mes, string destino)
		{
			const string serverURL = "http://dfe.arba.gov.ar/DomicilioElectronico/SeguridadCliente/dfeServicioDescargaPadron.do";
			const string archivo = "DFEServicioDescargaPadron";
			const string extension = "xml";
			string archivoConExtension = string.Format ("{0}{1}.{2}", Path.GetTempPath (), archivo, extension);

			var settings = new XmlWriterSettings ();
			settings.Indent = true;
			settings.Encoding = Encoding.GetEncoding ("ISO-8859-1");

			using (XmlWriter writer = XmlWriter.Create (archivoConExtension, settings)) {
				writer.WriteStartDocument ();
				writer.WriteStartElement ("DESCARGA-PADRON");
				writer.WriteStartElement ("fechaDesde");
				writer.WriteValue (string.Format ("{0:yyyyMMdd}", new DateTime (año, mes, 1)));
				writer.WriteEndElement ();
				writer.WriteStartElement ("fechaHasta");
				writer.WriteValue (string.Format ("{0:yyyyMMdd}", new DateTime (año, mes, 1).AddMonths (1).AddDays (-1)));
				writer.WriteEndElement ();
				writer.WriteEndElement ();
				writer.WriteEndDocument ();
				writer.Flush ();
			}

			string hash = CalcularHashMD5 (archivoConExtension);
			string archivoConHash = string.Format ("{0}{1}_{2}.{3}", Path.GetTempPath (), archivo, hash, extension);
			File.Copy (archivoConExtension, archivoConHash, true);
			string contenido = ContenidoDeArchivo (archivoConHash);

			const string boundary = "AaB03x";

			string postdata = string.Format ("--{0}\r\n" +
			                  "Content-Disposition: form-data; name=\"user\"\r\n\r\n{1}" +
			                  "\r\n" +
			                  "--{0}\r\n" +
			                  "Content-Disposition: form-data; name=\"password\"\r\n\r\n{2}" +
			                  "\r\n" +
			                  "--{0}\r\n" +
			                  "Content-Disposition: form-data; name=\"file\"; filename={3}" +
			                  "\r\n" +
			                  "Content-Type: text/xml\r\n\r\n{4}" +
			                  "--{0}--"
				, boundary
				, usuario
				, clave
				, archivoConHash
				, contenido
			                  );
			byte[] buffer = Encoding.ASCII.GetBytes (postdata);
			HttpWebRequest consulta = WebRequest.CreateHttp (serverURL);
			consulta.Method = "POST";
			consulta.ContentType = string.Format ("multipart/form-data;boundary={0}", boundary);
			consulta.ContentLength = buffer.Length;
			Stream newStream = consulta.GetRequestStream ();

			if (newStream != null) {
				newStream.Write (buffer, 0, buffer.Length);
				newStream.Close ();

				var respuesta = (HttpWebResponse)consulta.GetResponse ();
				Stream streamRespuesta = respuesta.GetResponseStream ();
				string archivoRespuesta = string.Format ("{0}ARBA-PadronRGS{1:D4}{2:D2}.zip", destino, año, mes);
				if (respuesta.ContentType.Contains (extension)) {
					archivoRespuesta = string.Format ("{0}{1}-Error.xml", destino, archivo);
				}

				var streamWriter = new FileStream (archivoRespuesta, FileMode.Create, FileAccess.Write);

				var bufferRead = new byte[4096];
				int bytesRead;

				do {
					bytesRead = streamRespuesta.Read (bufferRead, 0, 4096);
					streamWriter.Write (bufferRead, 0, bytesRead);
				} while (bytesRead > 0);

				streamWriter.Close ();
				streamRespuesta.Close ();
				respuesta.Close ();
			}
		}

		/// <summary>
		/// Importar un archivo del padron unificado.
		/// Validos para:
		/// - Padrón de Retenciones
		/// - Padrón de Percepciones
		/// <see href="http://www.arba.gov.ar/Informacion/IBrutos/LinksIIBB/RegimenSujeto.asp"/>
		/// </summary>
		/// <param name="archivo">Ruta absoluta al archivo.</param>
		public static void ImportarPadronUnificado (string archivo)
		{
			//HACK esto deberia tener una estructura de almacenamiento mas generica de acuerdo al registro
			//TODO esto puede almacenarse siempre en la misma tabla con consultas sobre CUIT, Publicacion y Regimen
			var stream = new FileStream (archivo, FileMode.Open, FileAccess.Read);
			var reader = new StreamReader (stream);
			var dbagip = new DB {
				ConnectionName = "Hamekoz.Argentina.Arba"
			};
			while (!reader.EndOfStream) {
				string linea = reader.ReadLine ();
				try {
					var registro = new RegistroPadronUnificado (linea);
					//TODO cambiar SP por consulta de texto plana
					//TODO controlar la existencia de la tabla en la base de datos.
					dbagip.SP ("padronTmpActualizar"
						, "fechaPublicacion", registro.Publicacion
						, "cuit", registro.CUIT
						, "fechaVigenciaDesde", registro.VigenciaDesde
						, "fechaVigenciaHasta", registro.VigenciaHasta
						, "tipoContrInscr", registro.TipoDeContribuyenteInscripto
						, "marcaAltaBajaSujeto", registro.MarcaAltaBajaSujeto
						, "marcaCbioAlicuota", registro.MarcaCambioAlicuota
						, "alicuota", registro.Alicuota
						, "nroGrupo", registro.NumeroGrupo
					);
				} catch (Exception ex) {
					Console.WriteLine ("Error en importacion:\n\tRegistro: {0}\n\tError: {1}", linea, ex.Message);
				}
			}
			reader.Close ();
		}

		/// <summary>
		/// Alicuotas the retencion.
		/// </summary>
		/// <returns>Devuelve la alicuota o -1 si no esta en el padron</returns>
		/// <param name="cuit">Cuit</param>
		public static decimal AlicuotaPercepcion (string cuit)
		{
			//TODO consultar alicuota en linea
			var dbagip = new DB {
				ConnectionName = "Hamekoz.Argentina.Arba"
			};
			//TODO validar la fecha con el periodo de vigencia
			string sql = string.Format ("SELECT ISNULL(Alicuota, -1) FROM arba.dbo.PadronPercepciones WHERE cuit = {0}", cuit.Limpiar ());
			return decimal.Parse (dbagip.SqlToScalar (sql).ToString ());
		}


		/// <summary>
		/// Alicuotas the retencion.
		/// </summary>
		/// <returns>Devuelve la alicuota o -1 si no esta en el padron</returns>
		/// <param name="cuit">Cuit</param>
		public static decimal AlicuotaRetencion (string cuit)
		{
			//TODO consultar alicuota en linea
			var dbarba = new DB {
				ConnectionName = "Hamekoz.Argentina.Arba"
			};
			//TODO validar la fecha con el periodo de vigencia
			string sql = string.Format ("SELECT Alicuota FROM arba.dbo.PadronRetenciones WHERE cuit = {0}", cuit.Limpiar ());
			decimal alicuota = -1;
			var dataset = dbarba.SqlToDataSet (sql);
			if (dataset.Tables [0].Rows.Count > 0) {
				alicuota = decimal.Parse (dataset.Tables [0].Rows [0] ["Alicuota"].ToString ());
			}
			return alicuota;
		}
	}
}

