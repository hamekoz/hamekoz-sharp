//
//  Reporte.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2010 Hamekoz
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
using System.Collections.Generic;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace Hamekoz.Reportes
{
	public class Reporte : IReporte
	{
		List<IElemento> elementos;

		public void Agregar (IElemento elemento)
        {
            elementos.Add (elemento);
        }

		protected Document document;
		protected PdfWriter pdfWriter;

        private string filename = string.Format("{0}Reporte-{1}.pdf", Path.GetTempPath(), DateTime.Now.ToFileTime());

		public Reporte ()
		{
			document = new Document ();
			pdfWriter = PdfWriter.GetInstance (document, new FileStream (filename, FileMode.Create));
			elementos = new List<IElemento> ();
		}

        /// <summary>
        /// Crea un reporte indicando el nombre de archivo pdf destino
        /// </summary>
        /// <param name="nombreDeArchivo">Nombre del archivo sin extension.</param>
        public Reporte(string nombreDeArchivo)
        {
            nombreDeArchivo = String.Join("", nombreDeArchivo.Split(Path.GetInvalidFileNameChars()));
            filename = string.Format("{0}{1}-{2}.pdf", Path.GetTempPath(), nombreDeArchivo, DateTime.Now.ToFileTime());
            document = new Document();
            pdfWriter = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
            elementos = new List<IElemento>();
        }

        public void Iniciar ()
		{
			this.SetInfo ();
			document.SetPageSize (PageSize.A4);
			//int margen = 0;
			//document.SetMargins (margen, margen, margen, margen);
			//document.SetPageSize (PageSize.A4.Rotate ());

			if (HasEncabezadoPieDePagina) {
				MostrarEncabezadoYPieDePagina ();
			}

			document.Open ();

			if (HasTituloPrimerPagina) {
				MostrarTitulo ();
			}
			if (HasAsuntoPrimerPagina) {
				MostrarAsunto ();
			}
			if (HasTituloPrimerPagina || HasAsuntoPrimerPagina) {
				NuevaLineaDivisoria ();
			}
			foreach (IElemento elemento in elementos) {
				//Console.WriteLine ("Generando elemento al documento...");
				document.Add (elemento.GetElemento ());
			}
		}

		private void SetInfo ()
		{
			document.AddProducer ();
			document.AddCreationDate ();

			document.AddAuthor (autor);
			document.AddCreator (creador);
			document.AddSubject (asunto);
			document.AddTitle (titulo);
		}

		private void MostrarEncabezadoYPieDePagina ()
		{
			document.SetMargins (document.LeftMargin, document.RightMargin, document.TopMargin + 20, document.BottomMargin + 15);

			EncabezdoPieDePagina pageEventHandler = new EncabezdoPieDePagina ();

			pageEventHandler.HeaderFont = FontFactory.GetFont (BaseFont.COURIER_BOLD, 10, Font.BOLD);
			pageEventHandler.Title = Titulo;
			pageEventHandler.HeaderLeft = Empresa;
			pageEventHandler.HeaderRight = Creador;

			pdfWriter.PageEvent = pageEventHandler;
		}

		private void MostrarTitulo ()
		{
			Font fuenteTitulo = FontFactory.GetFont (FontFactory.HELVETICA_BOLD, 22);
			Paragraph ptitulo = new Paragraph (Titulo, fuenteTitulo);
			ptitulo.Alignment = Element.ALIGN_CENTER;
			document.Add (ptitulo);
		}

		private void MostrarAsunto ()
		{
			Font fuenteAsunto = FontFactory.GetFont (FontFactory.HELVETICA_BOLD, 14);
			Paragraph pasunto = new Paragraph (Asunto, fuenteAsunto);
			pasunto.Alignment = Element.ALIGN_CENTER;
			document.Add (pasunto);
		}

		#region IReporte implementation
		private string titulo = string.Empty;
		public string Titulo {
			get {
				return titulo;
			}
			set {
				titulo = value;
			}
		}

		private string asunto = string.Empty;
		public string Asunto {
			get {
				return asunto;
			}
			set {
				asunto = value;
			}
		}

		private string autor = string.Empty;
		public string Autor {
			get {
				return autor;
			}
			set {
				autor = value;
			}
		}

		private static string assemblyName = System.Reflection.Assembly.GetExecutingAssembly ().GetName().Name;
		private static string assemblyVersion = System.Reflection.Assembly.GetExecutingAssembly ().GetName ().Version.ToString (2);

		private string creador = assemblyName + " " + assemblyVersion;
		public string Creador {
			get {
				return creador;
			}
			set {
				creador = value;
			}
		}

		private string empresa = string.Empty;
		public string Empresa {
			get {
				return empresa;
			}
			set {
				empresa = value;
			}
		}

		private string usuario = string.Empty;
		public string Usuario {
			get {
				return usuario;
			}
			set {
				usuario = value;
			}
		}

		public bool HasEncabezadoPieDePagina { get; set; }

		public bool HasTituloPrimerPagina { get; set; }

		public bool HasAsuntoPrimerPagina { get; set; }

		public void Abrir ()
		{
			this.Iniciar ();

			document.Close ();
			System.Diagnostics.Process.Start (filename);
		}

		public void NuevaPagina ()
		{
			document.NewPage ();
		}

		public void NuevaLineaDivisoria ()
		{
			LineSeparator linea = new LineSeparator ();
			Chunk espacio = new Chunk (" ", FontFactory.GetFont (FontFactory.HELVETICA, 4));
			document.Add (new Paragraph (espacio));
			document.Add (linea);
			document.Add (new Paragraph (espacio));
		}

		#endregion
	}
}