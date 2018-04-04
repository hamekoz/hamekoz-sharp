//
//  TRA.cs
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
using System.IO;
using System.Linq;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;

namespace Hamekoz.Argentina.Afip
{
	class Program
	{

		[STAThread]
		static void Main (string[] args)
		{
			const string DEFAULT_URLWSAAWSDL = "https://wsaa.afip.gov.ar/ws/services/LoginCms?WSDL";

			try {
				// First load a Certificate, filename/path and certificate password
				Cert = ReadCertFromFile ("/balcarce/objetos/postresbalcarce.crt", "");

				//  Select a binary file
				const string filename = "/balcarce/objetos/postresbalcarce.key";

				var ticket = new TicketDeAcceso ();
				ticket.ObtenerLoginTicketResponse (DEFAULT_URLWSAAWSDL, "/balcarce/objetos/postresbalcarce.crt", true);

				// Get the file
				var f = new FileStream (filename, System.IO.FileMode.Open);

				// Reading through this code stub to be sure I get it all :-)  [ Different subject entirely ]
				var fileContent = ReadFully (f);

				// Create the generator
				var dataGenerator = new CmsEnvelopedDataStreamGenerator ();

				// Add receiver
				// Cert is the user's X.509 Certificate set bellow
				dataGenerator.AddKeyTransRecipient (Cert);

				// Make the output stream
				var outStream = new FileStream (filename + ".p7m", FileMode.Create);

				// Sign the stream
				var cryptoStream = dataGenerator.Open (outStream, CmsEnvelopedGenerator.Aes128Cbc);

				// Store in our binary stream writer and write the signed content
				var binWriter = new BinaryWriter (cryptoStream);
				binWriter.Write (fileContent);
			} catch (Exception ex) {
				Console.WriteLine ("So, you wanna make an exception huh! : " + ex.ToString ());
				Console.ReadKey ();
			}

		}



		public static byte[] ReadFully (Stream stream)
		{
			stream.Seek (0, 0);
			var buffer = new byte[32768];
			using (var ms = new MemoryStream ()) {
				while (true) {
					int read = stream.Read (buffer, 0, buffer.Length);
					if (read <= 0)
						return ms.ToArray ();
					ms.Write (buffer, 0, read);
				}
			}
		}

		public static Org.BouncyCastle.X509.X509Certificate Cert { get; set; }

		// This reads a certificate from a file.
		// Thanks to: http://blog.softwarecodehelp.com/2009/06/23/CodeForRetrievePublicKeyFromCertificateAndEncryptUsingCertificatePublicKeyForBothJavaC.aspx
		public static X509Certificate ReadCertFromFile (string strCertificatePath, string strCertificatePassword)
		{
			try {
				// Create file stream object to read certificate
				var keyStream = new FileStream (strCertificatePath, FileMode.Open, FileAccess.Read);

				// Read certificate using BouncyCastle component
				var inputKeyStore = new Pkcs12Store ();
				inputKeyStore.Load (keyStream, strCertificatePassword.ToCharArray ());

				//Close File stream
				keyStream.Close ();

				var keyAlias = inputKeyStore.Aliases.Cast<string> ().FirstOrDefault (n => inputKeyStore.IsKeyEntry (n));

				// Read Key from Alieases  
				if (keyAlias == null)
					throw new NotImplementedException ("Alias");

				//Read certificate into 509 format
				return (X509Certificate)inputKeyStore.GetCertificate (keyAlias).Certificate;
			} catch (Exception ex) {
				Console.WriteLine ("So, you wanna make an exception huh! : " + ex.ToString ());
				Console.ReadKey ();
				return null;
			}
		}

	}
}

