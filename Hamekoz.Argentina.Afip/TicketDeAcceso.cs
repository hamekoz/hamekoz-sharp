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
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509.Store;
using Org.BouncyCastle.Cms;

namespace Hamekoz.Argentina.Afip
{
	public class TicketDeAcceso
	{
		#region Propiedades

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

		#endregion

		public XmlDocument xmlLoginTicketRequest;
		public XmlDocument xmlLoginTicketResponse;

		public string RutaDelCertificadoFirmante;

		// OJO! NO ES THREAD-SAFE
		static UInt32 _globalUniqueID;

		public string ObtenerLoginTicketResponse (string argUrlWsaa, string argRutaCertX509Firmante, bool argVerbose)
		{
			RutaDelCertificadoFirmante = argRutaCertX509Firmante;
			CertificadosX509Lib.VerboseMode = argVerbose;

			string cmsFirmadoBase64;
			string loginTicketResponse;

			// PASO 1: Genero el Login Ticket Request
			xmlLoginTicketRequest = TicketDeRequerimientoDeAcceso ();

			// PASO 2: Firmo el Login Ticket Request 
			try {
				Console.WriteLine ("***Leyendo certificado: {0}", RutaDelCertificadoFirmante);

				X509Certificate2 certFirmante = CertificadosX509Lib.ObtieneCertificadoDesdeArchivo (RutaDelCertificadoFirmante);

				Console.WriteLine ("***Firmando: ");
				Console.WriteLine (xmlLoginTicketRequest.OuterXml);

				// Convierto el login ticket request a bytes, para firmar 
				Encoding EncodedMsg = Encoding.UTF8;
				byte[] msgBytes = EncodedMsg.GetBytes (xmlLoginTicketRequest.OuterXml); 

				// Firmo el msg y paso a Base64 
				byte[] encodedSignedCms = CertificadosX509Lib.FirmaBytesMensaje (msgBytes, certFirmante);
				cmsFirmadoBase64 = Convert.ToBase64String (encodedSignedCms);
			} catch (Exception excepcionAlFirmar) {
				throw new Exception ("***Error FIRMANDO el LoginTicketRequest : " + excepcionAlFirmar.Message);
			}

			// PASO 3: Invoco al WSAA para obtener el Login Ticket Response 
			try {
				Console.WriteLine ("***Llamando al WSAA en URL: {0}", argUrlWsaa);
				Console.WriteLine ("***Argumento en el request:");
				Console.WriteLine (cmsFirmadoBase64);

				var servicioWsaa = new  WSAA.LoginCMSService ();
				servicioWsaa.Url = argUrlWsaa;

				loginTicketResponse = servicioWsaa.loginCms (cmsFirmadoBase64);

				Console.WriteLine ("***LoguinTicketResponse: ");
				Console.WriteLine (loginTicketResponse);
			} catch (Exception excepcionAlInvocarWsaa) {
				throw new Exception ("***Error INVOCANDO al servicio WSAA : " + excepcionAlInvocarWsaa.Message);
			}


			// PASO 4: Analizo el Login Ticket Response recibido del WSAA 
			try {
				xmlLoginTicketResponse = new XmlDocument ();
				xmlLoginTicketResponse.LoadXml (loginTicketResponse);

				id = UInt32.Parse (xmlLoginTicketResponse.SelectSingleNode ("//uniqueId").InnerText);
				generacion = DateTime.Parse (xmlLoginTicketResponse.SelectSingleNode ("//generationTime").InnerText);
				expiracion = DateTime.Parse (xmlLoginTicketResponse.SelectSingleNode ("//expirationTime").InnerText);
				firma = xmlLoginTicketResponse.SelectSingleNode ("//sign").InnerText;
				token = xmlLoginTicketResponse.SelectSingleNode ("//token").InnerText;
			} catch (Exception excepcionAlAnalizarLoginTicketResponse) {
				throw new Exception ("***Error ANALIZANDO el LoginTicketResponse : " + excepcionAlAnalizarLoginTicketResponse.Message);
			}

			return loginTicketResponse;

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
	
	}

	/// <summary> 
	/// Libreria de utilidades para manejo de certificados 
	/// </summary> 
	/// <remarks></remarks> 
	static class CertificadosX509Lib
	{
		public static bool VerboseMode;

		/// <summary> 
		/// Firma mensaje 
		/// </summary> 
		/// <param name="argBytesMsg">Bytes del mensaje</param> 
		/// <param name="argCertFirmante">Certificado usado para firmar</param> 
		/// <returns>Bytes del mensaje firmado</returns> 
		/// <remarks></remarks> 
		public static byte[] FirmaBytesMensaje (byte[] argBytesMsg, X509Certificate2 argCertFirmante)
		{
			try {
				// Pongo el mensaje en un objeto ContentInfo (requerido para construir el obj SignedCms) 
				var infoContenido = new ContentInfo (argBytesMsg);
				var cmsFirmado = new SignedCms (infoContenido);

				// Creo objeto CmsSigner que tiene las caracteristicas del firmante 
				var cmsFirmante = new CmsSigner (argCertFirmante);
				cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;

				if (VerboseMode) {
					Console.WriteLine ("***Firmando bytes del mensaje...");
				}
				// Firmo el mensaje PKCS #7 
				cmsFirmado.ComputeSignature (cmsFirmante);

				if (VerboseMode) {
					Console.WriteLine ("***OK mensaje firmado");
				}

				// Encodeo el mensaje PKCS #7. 
				return cmsFirmado.Encode ();
			} catch (Exception excepcionAlFirmar) {
				throw new Exception ("***Error al firmar: " + excepcionAlFirmar.Message);
			}
		}

		/// <summary> 
		/// Firma mensaje 
		/// </summary> 
		/// <param name="argBytesMsg">Bytes del mensaje</param> 
		/// <param name="argCertFirmante">Certificado usado para firmar</param> 
		/// <returns>Bytes del mensaje firmado</returns> 
		/// <remarks></remarks> 
		public static byte[] FirmaBytesMensajeBC (byte[] argBytesMsg, X509Certificate2 argCertFirmante)
		{
			AsymmetricKeyParameter pKey = null;
			Org.BouncyCastle.X509.X509Certificate pCertificate = null;
			byte [] asn1_cms = null;
			IX509Store cstore = null;
			String LoginTicketRequest_xml;
			String SignerDN = null;

			//
			// Manage Keys & Certificates
			//
			try {
				// Create a keystore using keys from the pkcs#12 p12file
				KeyStore ks = KeyStore.getInstance("pkcs12");
				FileInputStream p12stream = new FileInputStream ( p12file ) ;
				ks.load(p12stream, p12pass.toCharArray());
				p12stream.close();

				// Get Certificate & Private key from KeyStore
				pKey = (PrivateKey) ks.getKey(signer, p12pass.toCharArray());
				pCertificate = (X509Certificate)ks.getCertificate(signer);
				SignerDN = pCertificate.getSubjectDN().toString();

				// Create a list of Certificates to include in the final CMS
				ArrayList<X509Certificate> certList = new ArrayList<X509Certificate>();
				certList.add(pCertificate);

				if (Security.getProvider("BC") == null) {
					Security.addProvider(new BouncyCastleProvider());
				}

				cstore = CertStore.getInstance("Collection", new CollectionCertStoreParameters (certList), "BC");
			} 
			catch (Exception e) {
				e.StackTrace;
			} 


			//
			// Create CMS Message
			//
			try {
				// Create a new empty CMS Message
				var gen = new CmsSignedDataGenerator();

				// Add a Signer to the Message
				gen.AddSigner(pKey, pCertificate, CmsSignedGenerator.DigestSha1);

				// Add the Certificate to the Message
				gen.AddCertificates(cstore);

				// Add the data (XML) to the Message
				CmsProcessable data = new CmsProcessableByteArray(EncodedMsg.GetBytes (xmlLoginTicketRequest.OuterXml);EncodedMsg.GetBytes (xmlLoginTicketRequest.OuterXml);LoginTicketRequest_xml.getBytes());

				// Add a Sign of the Data to the Message
				CmsSignedData signed = gen.Generate("BC",data, true);	

				// 
				asn1_cms = signed.GetEncoded();
			} 
			catch (Exception e) {
				e.StackTrace;
			} 

			return (asn1_cms);
		}


		/// <summary> 
		/// Lee certificado de disco 
		/// </summary> 
		/// <param name="argArchivo">Ruta del certificado a leer.</param> 
		/// <returns>Un objeto certificado X509</returns> 
		/// <remarks></remarks> 
		public static X509Certificate2 ObtieneCertificadoDesdeArchivo (string argArchivo)
		{
			var objCert = new X509Certificate2 ();

			try {
				//objCert.Import (Microsoft.VisualBasic.FileIO.FileSystem.ReadAllBytes (argArchivo));
				return objCert;
			} catch (Exception excepcionAlImportarCertificado) {
				throw new Exception ("argArchivo=" + argArchivo + " excepcion=" + excepcionAlImportarCertificado.Message + " " + excepcionAlImportarCertificado.StackTrace);

			}
		}
	}
}