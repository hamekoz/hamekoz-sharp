//
//  Ticket.cs
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
using System.Text;
using System.Xml;
using System.IO;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Engines;
using System.Collections.Generic;
using Org.BouncyCastle.X509;

namespace Hamekoz.Argentina.Afip
{
	public class TicketDeAcceso
	{
		// Entero de 32 bits sin signo que identifica el requerimiento
		public UInt32 id;
		// Momento en que fue generado el requerimiento
		public DateTime generacion;

		public DateTime Generacion {
			get {
				return generacion;
			}
		}

		// Momento en el que expira la solicitud
		public DateTime expiracion;

		public DateTime Expiracion {
			get {
				return expiracion;
			}
		}

		// Identificacion del WSN para el cual se solicita el TA
		public string Servicio {
			get;
			set;
		}

		// Firma de seguridad recibida en la respuesta
		string firma;

		public string Firma {
			get {
				return firma;
			}
		}

		// Token de seguridad recibido en la respuesta
		string token;

		public string Token {
			get {
				return token;
			}
		}

		X509Certificate certificadoFirmante;
		System.Security.Cryptography.X509Certificates.X509Certificate2 x509certificate2;

		public void CargarCertificadoFirmante (string path)
		{
			x509certificate2 = new System.Security.Cryptography.X509Certificates.X509Certificate2 ();
			x509certificate2.Import (path);
			var key = System.Security.Cryptography.AsymmetricAlgorithm.Create ();

			//x509certificate2.PrivateKey = key;
			var parser = new X509CertificateParser ();
			string certString = File.OpenText (path).ReadToEnd ();
			X509Certificate certi = parser.ReadCertificate (Encoding.UTF8.GetBytes (certString));


			var cert = DotNetUtilities.FromX509Certificate (x509certificate2);

			certificadoFirmante = cert;
		}

		//XmlDocument solicitudXML;
		XmlDocument respuestaXML;

		public string RutaDelCertificadoFirmante;


		// OJO! NO ES THREAD-SAFE
		static UInt32 _globalUniqueID;

		/// <summary> 
		/// Construye un Login Ticket obtenido del WSAA 
		/// </summary> 
		/// <param name="argRutaCertX509Firmante">Ruta del certificado X509 (con clave privada) usado para firmar</param> 
		/// <remarks></remarks> 
		public void Obtener (string argRutaCertX509Firmante)
		{
			RutaDelCertificadoFirmante = argRutaCertX509Firmante;
			CargarCertificadoFirmante (RutaDelCertificadoFirmante);
			var key = ReadAsymmetricKeyParameter ("/balcarce/objetos/postresbalcarce.key");
			XmlDocument tra = TicketDeRequerimientoDeAcceso ();
			Encoding EncodedMsg = Encoding.UTF8;
			byte[] msgBytes = EncodedMsg.GetBytes (tra.OuterXml);
			var signedData = SignData (tra.OuterXml, key);
			var signedDataByteArray = EncodedMsg.GetBytes (signedData);

			var cmsFirmadoBase64 = Convert.ToBase64String (signedDataByteArray);

			string respuesta;

			// PASO 3: Invoco al WSAA para obtener el Login Ticket Response 
			try {
				var wsaa = new  WSAA.LoginCMSService ();
				respuesta = wsaa.loginCms (cmsFirmadoBase64);
			} catch (Exception ex) {
				throw new Exception ("Error INVOCANDO al servicio WSAA : " + ex.Message);
			}

			// PASO 4: Analizo el Login Ticket Response recibido del WSAA 
			try {
				respuestaXML = new XmlDocument ();
				respuestaXML.LoadXml (respuesta);

				id = UInt32.Parse (respuestaXML.SelectSingleNode ("//uniqueId").InnerText);
				generacion = DateTime.Parse (respuestaXML.SelectSingleNode ("//generationTime").InnerText);
				expiracion = DateTime.Parse (respuestaXML.SelectSingleNode ("//expirationTime").InnerText);
				firma = respuestaXML.SelectSingleNode ("//sign").InnerText;
				token = respuestaXML.SelectSingleNode ("//token").InnerText;
			} catch (Exception ex) {
				throw new Exception ("Error ANALIZANDO el LoginTicketResponse : " + ex.Message);
			}
		}

		XmlDocument TicketDeRequerimientoDeAcceso ()
		{

			const string solicitudPlantillaXML = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";
			XmlDocument solicitudXML;
			XmlNode xmlNodoUniqueId;
			XmlNode xmlNodoGenerationTime;
			XmlNode xmlNodoExpirationTime;
			XmlNode xmlNodoService;

			// PASO 1: Genero el Login Ticket Request 
			#if !DEBUG
			try {
			#endif
			solicitudXML = new XmlDocument ();
			solicitudXML.LoadXml (solicitudPlantillaXML);

			xmlNodoUniqueId = solicitudXML.SelectSingleNode ("//uniqueId");
			xmlNodoGenerationTime = solicitudXML.SelectSingleNode ("//generationTime");
			xmlNodoExpirationTime = solicitudXML.SelectSingleNode ("//expirationTime");
			xmlNodoService = solicitudXML.SelectSingleNode ("//service");

			var now = DateTime.Now;

			xmlNodoUniqueId.InnerText = Convert.ToString (_globalUniqueID);
			xmlNodoGenerationTime.InnerText = now.ToString ("s");
			xmlNodoExpirationTime.InnerText = now.AddHours (12).ToString ("s");
			xmlNodoService.InnerText = Servicio;

			_globalUniqueID += 1;

			return solicitudXML;
			#if !DEBUG
			} catch (Exception ex) {
				throw new Exception ("Error GENERANDO el Ticket de requerimiento de acceso : " + ex.Message);
			}
			#endif
		}

		public RsaPrivateCrtKeyParameters ReadAsymmetricKeyParameter (string pemFilename)
		{
			var fileStream = File.OpenText (pemFilename);
			var pemReader = new Org.BouncyCastle.OpenSsl.PemReader (fileStream);
			var KeyParameter = (AsymmetricKeyParameter)pemReader.ReadObject ();
			return (RsaPrivateCrtKeyParameters)KeyParameter;
		}

		public string SignData (string msg, RsaPrivateCrtKeyParameters privKey)
		{
			try {
				byte[] msgBytes = Encoding.UTF8.GetBytes (msg);

				ISigner signer = SignerUtilities.GetSigner ("SHA1withRSA");
				signer.Init (true, privKey);
				signer.BlockUpdate (msgBytes, 0, msgBytes.Length);
				byte[] sigBytes = signer.GenerateSignature ();

				return Convert.ToBase64String (sigBytes);
			} catch (Exception exc) {
				Console.WriteLine ("Signing Failed: " + exc);
				return null;
			}
		}

		static AsymmetricKeyParameter readPrivateKey (string privateKeyFileName)
		{
			AsymmetricCipherKeyPair keyPair;

			using (var reader = File.OpenText (privateKeyFileName))
				keyPair = (AsymmetricCipherKeyPair)new Org.BouncyCastle.OpenSsl.PemReader (reader).ReadObject ();

			return keyPair.Private;
		}
	}
}

