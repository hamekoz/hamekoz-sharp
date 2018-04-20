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
using System.Diagnostics;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using RawPrint;

namespace Hamekoz.Reportes
{
	public class Reporte : IReporte
	{
		protected Document document;
		protected PdfWriter pdfWriter;

		public Reporte ()
		{
			document = new Document ();
			MargenDerecho = document.RightMargin;
			MargenIzquierdo = document.LeftMargin;
			MargenInferior = document.BottomMargin;
			MargenSuperior = document.TopMargin;
		}

		public bool Previsualizar {
			get;
			set;
		} = true;

		public float MargenSuperior {
			get;
			set;
		}

		public float MargenInferior {
			get;
			set;
		}

		public float MargenDerecho {
			get;
			set;
		}

		public float MargenIzquierdo {
			get;
			set;
		}

		bool hasTitleAndSubjetOnAllPages;

		public bool HasTitleAndSubjetOnAllPages {
			get {
				return hasTitleAndSubjetOnAllPages;
			}
			set {
				hasTitleAndSubjetOnAllPages = value;
				HasTituloPrimerPagina = !value && HasTituloPrimerPagina;
				HasAsuntoPrimerPagina = !value && HasAsuntoPrimerPagina;
			}
		}

		public void Iniciar ()
		{
			pdfWriter = PdfWriter.GetInstance (document, new FileStream (FileName, FileMode.Create));
			SetInfo ();

			document.SetMargins (MargenIzquierdo, MargenDerecho, MargenSuperior, MargenInferior);

			if (Apaisado) {
				document.SetPageSize (PageSize.A4.Rotate ());
			} else {
				document.SetPageSize (PageSize.A4);
			}

			var pageEventHandler = new ReportPdfPageEvent {
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
				pageEventHandler.ShowGeneratedInfo = ShowGeneratedInfo;
			}

			if (marcaDeAguaTexto != string.Empty) {
				pageEventHandler.HasWaterMarkText = true;
				pageEventHandler.WaterMarkText = MarcaDeAguaTexto;
			}

			if (marcaDeAguaImagenUri != string.Empty) {
				pageEventHandler.HasWaterMarkImage = true;
				pageEventHandler.WaterMarkImagePath = MarcaDeAguaImagenUri;
				pageEventHandler.WaterMarkOpacity = marcaDeAguaTransparencia;
			}

			if (HasTitleAndSubjetOnAllPages) {
				pageEventHandler.HasTitleAndSubjet = true;
				pageEventHandler.Title = Titulo;
				pageEventHandler.Subjet = Asunto;
				document.SetMargins (
					document.LeftMargin,
					document.RightMargin,
					document.TopMargin + 65,
					document.BottomMargin
				);
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
		}

		void SetInfo ()
		{
			document.AddProducer ();
			document.AddCreationDate ();

			document.AddAuthor (autor);
			document.AddKeywords (empresa);
			document.AddCreator (string.Format ("{0} (Powered by {1})", Constants.GeneratedBy, Constants.PoweredBy));
			document.AddSubject (asunto);
			document.AddTitle (titulo);
		}

		void MostrarTitulo ()
		{
			Font fuenteTitulo = FontFactory.GetFont (FontFactory.HELVETICA_BOLD, 22);
			var ptitulo = new Paragraph (Titulo, fuenteTitulo);
			ptitulo.Alignment = Element.ALIGN_CENTER;
			document.Add (ptitulo);
		}

		void MostrarAsunto ()
		{
			Font fuenteAsunto = FontFactory.GetFont (FontFactory.HELVETICA_BOLD, 14);
			var pasunto = new Paragraph (Asunto, fuenteAsunto);
			pasunto.Alignment = Element.ALIGN_CENTER;
			document.Add (pasunto);
		}

		#region IReporte implementation

		string marcaDeAguaImagenUri = string.Empty;

		public string MarcaDeAguaImagenUri {
			get {
				return marcaDeAguaImagenUri;
			}
			set {
				marcaDeAguaImagenUri = value;
			}
		}

		string marcaDeAguaTexto = string.Empty;

		public string MarcaDeAguaTexto {
			get {
				return marcaDeAguaTexto;
			}
			set {
				marcaDeAguaTexto = value;
			}
		}

		float marcaDeAguaTransparencia = 0.1F;

		public float MarcaDeAguaTransparencia {
			get {
				return marcaDeAguaTransparencia;
			}
			set {
				marcaDeAguaTransparencia = value;
			}
		}

		string filename = string.Empty;

		public string FileName {
			get {
				if (filename == string.Empty) {
					filename = string.Format ("{0}{1}-{2}.pdf", Path.GetTempPath (), titulo, DateTime.Now.ToFileTime ());
				}
				return filename;
			}
			set { filename = value; }
		}

		string titulo = "Hamekoz Report";

		public string Titulo {
			get {
				return titulo;
			}
			set {
				titulo = value;
			}
		}

		string asunto = string.Empty;

		public string Asunto {
			get {
				return asunto;
			}
			set {
				asunto = value;
			}
		}

		string autor = string.Empty;

		public string Autor {
			get {
				return autor;
			}
			set {
				autor = value;
			}
		}

		string creador = Constants.GeneratedBy;

		public string Creador {
			get {
				return creador;
			}
			set {
				creador = value;
			}
		}

		string empresa = string.Empty;

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
			if (!document.IsOpen ()) {
				Iniciar ();
			}
			var tabla = elemento as Tabla;
			if (tabla != null && tabla.PosicionAbsoluta) {
				var pdfTable = elemento.GetElemento () as PdfPTable;
				pdfTable.TotalWidth = document.PageSize.Width;
				pdfTable.WriteSelectedRows (0, -1, tabla.PosicionX, document.PageSize.Height - tabla.PosicionY, pdfWriter.DirectContent);
			} else {
				document.Add (elemento.GetElemento ());	
			}

		}

		public void Agregar (IElement elemento)
		{
			if (!document.IsOpen ()) {
				Iniciar ();
			}
			document.Add (elemento);
		}

		public void Abrir ()
		{
			document.Close ();
			if (Previsualizar) {
				Process.Start (FileName);	
			}
		}

		public void Imprimir ()
		{
			Imprimir (string.Empty);
		}

		public void Imprimir (string printerName)
		{
			Imprimir (printerName, 1);
		}

		public void Imprimir (string printerName, int copias)
		{
			if (!File.Exists (FileName))
				document.Close ();
			switch (Environment.OSVersion.Platform) {
			case PlatformID.Unix:
			case PlatformID.MacOSX:
				string argumentos = string.Format ("-n {0} -t '{1}' '{2}'", copias, Titulo, FileName);
				if (!string.IsNullOrWhiteSpace (printerName))
					argumentos = string.Format ("-d '{0}' {1}", printerName, argumentos);
				Process.Start ("lp", argumentos);
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				IPrinter printer = new Printer ();
				printer.PrintRawFile (printerName, FileName, Titulo);
				break;
			default:
				throw new PlatformNotSupportedException ();
				break;
			}
		}

		public void NuevaPagina ()
		{
			document.NewPage ();
		}

		public void NuevaLineaDivisoria ()
		{
			var linea = new LineSeparator ();
			var espacio = new Chunk (" ", FontFactory.GetFont (FontFactory.HELVETICA, 4));
			document.Add (new Paragraph (espacio));
			document.Add (linea);
			document.Add (new Paragraph (espacio));
		}


		public bool ShowGeneratedInfo {
			get;
			set;
		} = true;

		#endregion
	}
}