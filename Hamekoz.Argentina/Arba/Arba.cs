//
//  Arba.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2015 Hamekoz
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
using System.Net;
using System.IO;
using Hamekoz.Data;
using System.Xml;
using System.Text;
using System.Security.Cryptography;

namespace Hamekoz.Argentina.Arba
{
	public class Arba
	{
		private static string CalcularHashMD5 (string archivo)
		{
			FileStream fs = new FileStream (archivo, FileMode.Open, FileAccess.Read);
			MD5CryptoServiceProvider hash = new MD5CryptoServiceProvider ();
			Int64 currentPos = fs.Position;
			fs.Seek (0, SeekOrigin.Begin);
			StringBuilder sb = new StringBuilder ();
			foreach (Byte b in hash.ComputeHash(fs)) {
				sb.Append (b.ToString ("X2"));
			}
			fs.Close ();
			return sb.ToString ();
		}

		private static string ContenidoDeArchivo (string archivo)
		{
			FileStream stream = new FileStream (archivo, FileMode.Open, FileAccess.Read);
			string cadena = string.Empty;
			if (stream != null) {
				stream.Position = 0;
				StreamReader reader = new StreamReader (stream);
				//cadena = reader.ReadToEnd ();

				while (reader.Peek () > -1) {
					cadena = cadena + reader.ReadLine () + "\r\n";
				}

				reader.Close ();
				stream.Close ();
			}
			return cadena;
		}

		public static void DescargarPadron (string usuario, string clave, int año, int mes){
			DescargarPadron(usuario, clave, año, mes, string.Empty);
		}

		/// <summary>
		/// Descargars the padron.
		/// </summary>
		/// <see cref="http://www.arba.gov.ar/Informacion/IBrutos/LinksIIBB/RegimenSujeto.asp"/>
		/// <param name="usuario">Usuario.</param>
		/// <param name="clave">Clave.</param>
		public static void DescargarPadron (string usuario, string clave, int año, int mes, string destino)
		{
			string serverURL = "http://dfe.arba.gov.ar/DomicilioElectronico/SeguridadCliente/dfeServicioDescargaPadron.do";
			string archivo = "DFEServicioDescargaPadron";
			string extension = "xml";
			string archivoConExtension = string.Format ("{0}{1}.{2}", Path.GetTempPath (), archivo, extension);

			XmlWriterSettings settings = new XmlWriterSettings ();
			settings.Indent = true;
			settings.Encoding = Encoding.GetEncoding ("ISO-8859-1");

			using (XmlWriter writer = XmlWriter.Create (archivoConExtension, settings)) {
				writer.WriteStartDocument ();
				writer.WriteStartElement ("DESCARGA-PADRON");
				writer.WriteStartElement ("fechaDesde");
				writer.WriteValue (string.Format ("{0:yyyyMMdd}", new DateTime (año, mes, 1)));
				writer.WriteEndElement ();
				writer.WriteStartElement ("fechaHasta");
				writer.WriteValue (string.Format ("{0:yyyyMMdd}", new DateTime (año, mes + 1, 1).AddDays (-1)));
				writer.WriteEndElement ();
				writer.WriteEndElement ();
				writer.WriteEndDocument ();
				writer.Flush ();
			}

			string hash = CalcularHashMD5 (archivoConExtension);
			string archivoConHash = string.Format ("{0}{1}_{2}.{3}", Path.GetTempPath (), archivo, hash, extension);
			File.Copy (archivoConExtension, archivoConHash, true);
			string contenido = ContenidoDeArchivo (archivoConHash);

			string boundary = "AaB03x";

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
			HttpWebRequest consulta = HttpWebRequest.CreateHttp (serverURL);
			consulta.Method = "POST";
			consulta.ContentType = string.Format ("multipart/form-data;boundary={0}", boundary);
			consulta.ContentLength = buffer.Length;
			Stream newStream = consulta.GetRequestStream ();

			if (newStream != null) {
				newStream.Write (buffer, 0, buffer.Length);
				newStream.Close ();

				HttpWebResponse respuesta = (HttpWebResponse)consulta.GetResponse ();
				Stream streamRespuesta = respuesta.GetResponseStream ();
				string archivoRespuesta = string.Format ("{0}ARBA-PadronRGS{1:D4}{2:D2}.zip", destino, año, mes);
				if (respuesta.ContentType.Contains (extension)) {
					archivoRespuesta = string.Format ("{0}{1}-Error.xml", destino, archivo, año, mes);
					;
				}

				FileStream streamWriter = new FileStream (archivoRespuesta, FileMode.Create, FileAccess.Write);

				byte[] bufferRead = new byte[4096];
				int bytesRead = 0;

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
		/// <see cref="http://www.arba.gov.ar/Informacion/IBrutos/LinksIIBB/RegimenSujeto.asp"/>
		/// </summary>
		/// <param name="archivo">Ruta absoluta al archivo.</param>
		public static void ImportarPadronUnificado (string archivo)
		{
			FileStream stream = new FileStream (archivo, FileMode.Open, FileAccess.Read);
			StreamReader reader = new StreamReader (stream);
			DB dbagip = new DB () {
				ConnectionName = "Hamekoz.Argentina.Arba"
			};
			while (!reader.EndOfStream) {
				string linea = reader.ReadLine ();
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
	}
}

