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
using System.Reflection;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace Hamekoz.Reportes
{
	public class Reporte : IReporte
	{
		List<IElemento> elementos;

		protected Document document;
		protected PdfWriter pdfWriter;

		public Reporte ()
		{
			document = new Document ();
			margenDerecho = document.RightMargin;
			margenIzquierdo = document.LeftMargin;
			margenInferior = document.BottomMargin;
			margenSuperior = document.TopMargin;
			elementos = new List<IElemento> ();
		}

		float margenSuperior;

		public float MargenSuperior {
			get {
				return margenSuperior;
			}
			set {
				margenSuperior = value;
			}
		}

		float margenInferior;

		public float MargenInferior {
			get {
				return margenInferior;
			}
			set {
				margenInferior = value;
			}
		}

		float margenDerecho;

		public float MargenDerecho {
			get {
				return margenDerecho;
			}
			set {
				margenDerecho = value;
			}
		}

		float margenIzquierdo;

		public float MargenIzquierdo {
			get {
				return margenIzquierdo;
			}
			set {
				margenIzquierdo = value;
			}
		}

		public void Iniciar ()
		{
			pdfWriter = PdfWriter.GetInstance (document, new FileStream (FileName, FileMode.Create));
			this.SetInfo ();

			document.SetMargins (margenIzquierdo, margenDerecho, margenSuperior, margenInferior);

			if (Apaisado) {
				document.SetPageSize (PageSize.A4.Rotate ());
			} else {
				document.SetPageSize (PageSize.A4);
			}

			//TODO definir bien cuales son las propiedades a asignar
			ReportPdfPageEvent pageEventHandler = new ReportPdfPageEvent () {
				HasHeaderAndFooter = HasEncabezadoPieDePagina,
			};

			if (HasEncabezadoPieDePagina) {
				document.SetMargins (
					document.LeftMargin,
					document.RightMargin,
					document.TopMargin + 20,
					document.BottomMargin + 15
				);
				pageEventHandler.Header = Titulo;
				pageEventHandler.HeaderLeft = Empresa;
				pageEventHandler.HeaderRight = Creador;
			}

			if (marcaDeAguaTexto != string.Empty) {
				pageEventHandler.HasWaterMarkText = true;
				pageEventHandler.WaterMarkText = MarcaDeAguaTexto;
			}
			if (marcaDeAguaImagenUri != string.Empty) {
				pageEventHandler.HasWaterMarkImage = true;
				pageEventHandler.WaterMarkImagePath = MarcaDeAguaImagenUri;
			}

			pdfWriter.PageEvent = pageEventHandler;

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
				document.Add (elemento.GetElemento ());
			}
		}

		private void SetInfo ()
		{
			document.AddProducer ();
			document.AddCreationDate ();

			document.AddAuthor (autor);
			document.AddKeywords (empresa);
			document.AddCreator (string.Format ("{0} (Powered by {1})", Constants.GeneratedBy, Constants.PoweredBy));
			document.AddSubject (asunto);
			document.AddTitle (titulo);
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

		private string marcaDeAguaImagenUri = string.Empty;

		public string MarcaDeAguaImagenUri {
			get {
				return marcaDeAguaImagenUri;
			}
			set {
				marcaDeAguaImagenUri = value;
			}
		}

		private string marcaDeAguaTexto = string.Empty;

		public string MarcaDeAguaTexto {
			get {
				return marcaDeAguaTexto;
			}
			set {
				marcaDeAguaTexto = value;
			}
		}

		private string filename = string.Empty;

		public string FileName {
			get {
				if (filename == string.Empty) {
					filename = string.Format ("{0}{1}-{2}.pdf", Path.GetTempPath (), titulo, DateTime.Now.ToFileTime ());
				}
				return filename;
			}
			set { filename = value; }
		}

		private string titulo = "Hamekoz Report";

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

		private string creador = Constants.GeneratedBy;

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

		public bool Apaisado { get; set; }

		public bool HasEncabezadoPieDePagina { get; set; }

		public bool HasTituloPrimerPagina { get; set; }

		public bool HasAsuntoPrimerPagina { get; set; }

		public void Agregar (IElemento elemento)
		{
			elementos.Add (elemento);
		}

		public void Abrir ()
		{
			this.Iniciar ();

			document.Close ();
			System.Diagnostics.Process.Start (FileName);
		}

		#endregion

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
	}
}