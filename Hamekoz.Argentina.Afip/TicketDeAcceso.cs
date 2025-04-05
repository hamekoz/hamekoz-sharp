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
using System.Collections;
using System.Text;
using System.Xml;

using Hamekoz.Argentina.Afip.wsaa;

namespace Hamekoz.Argentina.Afip
{
    public class TicketDeAcceso
    {
        // Entero de 32 bits sin signo que identifica el requerimiento
        public UInt32 id;
        // Momento en que fue generado el requerimiento
        public DateTime generacion;

        public DateTime Generacion
        {
            get
            {
                return generacion;
            }
        }

        // Momento en el que expira la solicitud
        public DateTime expiracion;

        public DateTime Expiracion
        {
            get
            {
                return expiracion;
            }
        }

        // Identificacion del WSN para el cual se solicita el TA
        public string Servicio
        {
            get;
            set;
        }

        // Firma de seguridad recibida en la respuesta
        string firma;

        public string Firma
        {
            get
            {
                return firma;
            }
        }

        // Token de seguridad recibido en la respuesta
        string token;

        public string Token
        {
            get
            {
                return token;
            }
        }

        //		X509Certificate certificadoFirmante;
        //
        //		public void CargarCertificadoFirmante (string path)
        //		{
        //			certificadoFirmante = new X509Certificate2 ();
        //			certificadoFirmante.Import (path);
        //		}

        XmlDocument solicitudXML;
        XmlDocument respuestaXML;
        public string RutaDelCertificadoFirmante;
        const string solicitudPlantillaXML = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";

        // OJO! NO ES THREAD-SAFE
        static UInt32 _globalUniqueID;

        /// <summary> 
        /// Construye un Login Ticket obtenido del WSAA 
        /// </summary> 
        /// <param name="argServicio">Servicio al que se desea acceder</param> 
        /// <param name="argUrlWsaa">URL del WSAA</param> 
        /// <param name="argRutaCertX509Firmante">Ruta del certificado X509 (con clave privada) usado para firmar</param> 
        /// <param name="argVerbose">Nivel detallado de descripcion? true/false</param> 
        /// <remarks></remarks> 
        public void Obtener(string argServicio, string argUrlWsaa, string argRutaCertX509Firmante, bool argVerbose)
        {

            RutaDelCertificadoFirmante = argRutaCertX509Firmante;

            string cmsFirmadoBase64 = string.Empty;
            string respuesta;

            XmlNode xmlNodoUniqueId;
            XmlNode xmlNodoGenerationTime;
            XmlNode xmlNodoExpirationTime;
            XmlNode xmlNodoService;

            // PASO 1: Genero el Login Ticket Request 
            try
            {
                solicitudXML = new XmlDocument();
                solicitudXML.LoadXml(solicitudPlantillaXML);

                xmlNodoUniqueId = solicitudXML.SelectSingleNode("//uniqueId");
                xmlNodoGenerationTime = solicitudXML.SelectSingleNode("//generationTime");
                xmlNodoExpirationTime = solicitudXML.SelectSingleNode("//expirationTime");
                xmlNodoService = solicitudXML.SelectSingleNode("//service");

                var now = DateTime.Now;

                xmlNodoGenerationTime.InnerText = now.ToString("s");
                xmlNodoExpirationTime.InnerText = now.AddHours(12).ToString("s");
                xmlNodoUniqueId.InnerText = Convert.ToString(_globalUniqueID);
                xmlNodoService.InnerText = Servicio;

                _globalUniqueID += 1;

            }
            catch (Exception ex)
            {
                throw new Exception("Error GENERANDO el Ticket de acceso : " + ex.Message);
            }

            // PASO 2: Firmo el Login Ticket Request 
            try
            {
                // Convierto el login ticket request a bytes, para firmar 
                Encoding EncodedMsg = Encoding.UTF8;
                byte[] msgBytes = EncodedMsg.GetBytes(solicitudXML.OuterXml);
                byte[] encodedSignedCms;
                // Firmo el msg y paso a Base64 
                try
                {
                    var certList = new ArrayList();
                    //					CMSTypedData msg = new CMSProcessableByteArray ("Hello world!".getBytes ());
                    //
                    //					certList.add (signCert);
                    //
                    //					Store certs = new JcaCertStore (certList);
                    //
                    //					var gen = new  CMSSignedDataGenerator ();
                    //					ContentSigner sha1Signer = new JcaContentSignerBuilder ("SHA1withRSA").setProvider ("BC").build (signKP.getPrivate ());
                    //
                    //					gen.addSignerInfoGenerator (
                    //						new JcaSignerInfoGeneratorBuilder (
                    //							new JcaDigestCalculatorProviderBuilder ().setProvider ("BC").build ())
                    //						.build (sha1Signer, signCert));
                    //
                    //					gen.addCertificates (certs);
                    //
                    //					CMSSignedData sigData = gen.generate (msg, false);
                    //
                    //					cmsFirmadoBase64 = Convert.ToBase64String (encodedSignedCms);
                    //					// Pongo el mensaje en un objeto ContentInfo (requerido para construir el obj SignedCms) 
                    //					var infoContenido = new System.Security.Cryptography.Pkcs.ContentInfo (msgBytes);
                    //					var cmsFirmado = new SignedCms (infoContenido);
                    //
                    //					// Creo objeto CmsSigner que tiene las caracteristicas del firmante 
                    //					var cmsFirmante = new CmsSigner (certificadoFirmante);
                    //					cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;
                    //
                    //					// Firmo el mensaje PKCS #7 
                    //					cmsFirmado.ComputeSignature (cmsFirmante);
                    //					// Encodeo el mensaje PKCS #7. 
                    //					encodedSignedCms = cmsFirmado.Encode ();
                }
                catch (Exception excepcionAlFirmar)
                {
                    throw new Exception("***Error al firmar: " + excepcionAlFirmar.Message);
                }
            }
            catch (Exception excepcionAlFirmar)
            {
                throw new Exception("***Error FIRMANDO el LoginTicketRequest : " + excepcionAlFirmar.Message);
            }

            // PASO 3: Invoco al WSAA para obtener el Login Ticket Response 
            try
            {
                var wsaa = new wsaa.LoginCMSClient();
                respuesta = wsaa.loginCms(new loginCmsRequest(new loginCmsRequestBody(cmsFirmadoBase64))).Body.loginCmsReturn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error INVOCANDO al servicio WSAA : " + ex.Message);
            }

            // PASO 4: Analizo el Login Ticket Response recibido del WSAA 
            try
            {
                respuestaXML = new XmlDocument();
                respuestaXML.LoadXml(respuesta);

                id = UInt32.Parse(respuestaXML.SelectSingleNode("//uniqueId").InnerText);
                generacion = DateTime.Parse(respuestaXML.SelectSingleNode("//generationTime").InnerText);
                expiracion = DateTime.Parse(respuestaXML.SelectSingleNode("//expirationTime").InnerText);
                firma = respuestaXML.SelectSingleNode("//sign").InnerText;
                token = respuestaXML.SelectSingleNode("//token").InnerText;
            }
            catch (Exception ex)
            {
                throw new Exception("Error ANALIZANDO el LoginTicketResponse : " + ex.Message);
            }
        }
    }
}