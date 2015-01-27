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
		public Arba ()
		{
		}

		public static string CalculateMD5Hash(string input)
		{
			// step 1, calculate MD5 hash from input
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] inputBytes = Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);

			// step 2, convert byte array to hex string
			StringBuilder sb = new StringBuilder();
			foreach (byte item in hash) {
				sb.Append(item.ToString("X2"));
			}
			return sb.ToString();
		}

		/// <summary>
		/// Descargars the padron.
		/// </summary>
		/// <see cref="http://www.arba.gov.ar/Informacion/IBrutos/LinksIIBB/RegimenSujeto.asp"/>
		/// <param name="usuario">Usuario.</param>
		/// <param name="clave">Clave.</param>
		public static void DescargarPadron(string usuario, string clave){
			//UNDONE aun no esta finalizada la correcta descargar del padron usando webservice
			string archivo = "DFEServicioDescargaPadron";
			string extension = "xml";
			XmlWriterSettings settings = new XmlWriterSettings ();
			settings.Indent = true;
			StringBuilder builder = new StringBuilder ();

			using (XmlWriter writer = XmlWriter.Create (builder, settings)) {
				writer.WriteStartDocument ();
				writer.WriteStartElement ("DESCARGA-PADRON");
				writer.WriteStartElement ("fechaDesde");
				writer.WriteValue (string.Format ("{0:yyyyMM01}", DateTime.Now));
				writer.WriteEndElement ();
				writer.WriteStartElement ("fechaHasta");
				writer.WriteValue (string.Format ("{0:yyyyMM31}", DateTime.Now));
				writer.WriteEndElement ();
				writer.WriteEndElement ();
				writer.WriteEndDocument ();

				writer.Flush ();

			}
			string hash = CalculateMD5Hash (builder.ToString ());

			string file = string.Format ("{0}_{1}.{2}", archivo, hash, extension);

			string boundary = "-----------------------------740783281278532690174846201";
			string postdata = string.Format (@"{0}
Content-Disposition: form-data; name=""user""

{1}
{0}
Content-Disposition: form-data; name=""password""

{2}
{0}
Content-Disposition: form-data; name=""file""; filename=""{3}""
Content-Type: text/xml

{4}

{0}--"
				, boundary
				, usuario
				, clave
				, file
				, builder.ToString ()
			                  );
			Console.WriteLine (postdata);
			byte[] buffer = Encoding.ASCII.GetBytes (postdata);
			//byte[] buffer = Encoding.UTF8.GetBytes(postdata);

			string serverURL = "http://dfe.test.arba.gov.ar/DomicilioElectronico/SeguridadCliente/dfeServicioDescargaPadron.do";
			HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create (serverURL);
			myRequest.Method = "POST";
			myRequest.ContentType = string.Format("multipart/form-data;boundary={0}", boundary);
			myRequest.ContentLength = buffer.Length;
			Stream newStream = myRequest.GetRequestStream ();

			if (newStream != null) {
				newStream.Write (buffer, 0, buffer.Length);
				newStream.Close ();
				double bytesLeidos = 0;
				HttpWebResponse webresp;
				webresp = (HttpWebResponse)myRequest.GetResponse();
				Stream tmpStreamReader = webresp.GetResponseStream();
				Console.WriteLine ("Tipo de respuesta {0}", webresp.ContentType);
				string fileName = "Respuesta.zip";
				if (webresp.ContentType.Contains("xml")) {
					fileName = "Error.xml";
				}

				FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);

				byte[] bufferread = new byte[4096];
				int BytesRead=0;

				do
				{
					BytesRead = tmpStreamReader.Read(bufferread, 0, 4096);
					bytesLeidos = bytesLeidos + BytesRead;
					stream.Write(bufferread, 0, BytesRead);
				}
				while (BytesRead > 0);

				tmpStreamReader.Close();
				webresp.Close();
				stream.Close();
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
		public static void ImportarPadronUnificado(string archivo){
			FileStream stream = new FileStream(archivo , FileMode.Open, FileAccess.Read);
			StreamReader reader = new StreamReader(stream);
			DB dbagip = new DB () {
				ConnectionName = "Hamekoz.Argentina.Arba"
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
						, "marcaAltaBajaSujeto", registro.MarcaAltaBajaSujeto
						, "marcaCbioAlicuota", registro.MarcaCambioAlicuota
						, "alicuota", registro.Alicuota
						, "nroGrupo", registro.NumeroGrupo
					);
				} catch (Exception ex) {
					Console.WriteLine ("Error en importacion:\n\tRegistro: {0}\n\tError: {1}", linea, ex.Message);
				}
			}
			reader.Close();
		}
	}
}

