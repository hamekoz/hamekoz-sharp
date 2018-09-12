//
//  MailSender.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2017 Hamekoz - www.hamekoz.com.ar
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
using System.Net.Mail;
using System.Reflection;
using System.Text;
using Hamekoz.Core;

namespace Hamekoz.Core
{
	public static class MailSender
	{
		//TODO implementar usando MailKit y MimeKit
		public static SmtpClient SmtpClient {
			get;
			set;
		} = new SmtpClient {
			Credentials = new System.Net.NetworkCredential (Remitente, "%j4&Z*8S"),
			Port = 587,
			DeliveryMethod = SmtpDeliveryMethod.Network,
			UseDefaultCredentials = false,
			Host = "smtp.gmail.com",
		};

		public static string Remitente {
			get;
			set;
		} = "developer@hamekoz.com.ar";

		public static string Developer {
			get;
			set;
		} ="rodrigo@hamekoz.com.ar";

		public static void Notificar (string destinatario, string asunto, string mensaje)
		{
			var mail = new MailMessage (Remitente, destinatario) {
				Subject = asunto,
				Body = mensaje,
			};
			Notificar (mail);
		}

		public static void Notificar (MailMessage mensaje)
		{
			#if DEBUG
			mensaje.To.Clear ();
			mensaje.CC.Clear ();
			mensaje.Bcc.Clear ();
			mensaje.To.Add (MailSender.Developer);
			mensaje.Subject = string.Format ("DEBUG - {0}", mensaje.Subject);
			#endif
			SmtpClient.Send (mensaje);
		}

		public static void Notificar (Exception exception, string ui = "")
		{	
			if (exception is NotImplementedException)
				return;
			if (exception is ValidationDataException)
				return;
			try {
				Assembly assembly = Assembly.GetEntryAssembly ();
				AssemblyName assemblyName = assembly.GetName ();
				Version version = assemblyName.Version;

				string asunto = string.Format ("{0}: Excepción no controlada en sistema.", assemblyName.FullName);
				var mensaje = new StringBuilder ();
				mensaje.AppendLine ("Ocurrio un error no controlado en el sistema");
				mensaje.AppendLine ();
				mensaje.Append ("Fecha: ");
				mensaje.AppendLine (DateTime.Now.ToString ());
				mensaje.Append ("Version: ");
				mensaje.AppendLine (version.ToString ());
				mensaje.Append ("Equipo: ");
				mensaje.AppendLine (Environment.MachineName);
				mensaje.Append ("Sistema operativo: ");
				mensaje.AppendLine (Environment.OSVersion.VersionString);
				mensaje.Append ("Usuario del sistema operativo: ");
				mensaje.AppendLine (Environment.UserName);
				mensaje.Append ("Mensaje de excepción: ");
				mensaje.AppendLine (exception.Message);
				mensaje.AppendLine ("UI Class:");
				mensaje.AppendLine (ui);
				mensaje.AppendLine ();
				mensaje.AppendLine ("Exception Stack trace:");
				mensaje.AppendLine (exception.StackTrace);

				Notificar (Developer, asunto, mensaje.ToString ());
			} catch (Exception ex) {
				Console.WriteLine ("No se pudo enviar la notificacion de la excepcion no controlada: {0}", ex.Message);
			}
		}
	}
}