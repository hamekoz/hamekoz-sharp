//
//  ReportPdfPageEvent.cs
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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Hamekoz.Reportes
{
	public class ReportPdfPageEvent : PdfPageEventHelper
	{
		// This is the contentbyte object of the writer
		PdfContentByte contentByte;

		// we will put the final number of pages in a template
		PdfTemplate template;

		/// <summary>
		/// The header and footer font.
		/// </summary>
		readonly Font font = FontFactory.GetFont (FontFactory.HELVETICA_BOLD, 8, BaseColor.GRAY);

		/// <summary>
		/// The water font.
		/// </summary>
		Font waterFont = FontFactory.GetFont (FontFactory.HELVETICA, 40, BaseColor.LIGHT_GRAY);

		// This keeps track of the creation time
		DateTime printTime = DateTime.Now;

		// set transparency, see commented section below; 'image watermark'
		PdfGState state = new PdfGState {
			FillOpacity = waterMarkOpacity,
			StrokeOpacity = waterMarkOpacity
		};

		#region Properties

		public bool HasHeaderAndFooter{ get; set; }

		public bool HasWaterMarkImage{ get; set; }

		public bool HasWaterMarkText{ get; set; }

		string header = string.Empty;

		public string Header {
			get { return header; }
			set { header = value; }
		}

		string headerLeft = string.Empty;

		public string HeaderLeft {
			get { return headerLeft; }
			set { headerLeft = value; }
		}

		string headerRight = string.Empty;

		public string HeaderRight {
			get { return headerRight; }
			set { headerRight = value; }
		}

		string waterMarkText = string.Empty;

		public string WaterMarkText {
			get {
				return waterMarkText;
			}
			set {
				waterMarkText = value;
			}
		}

		static float waterMarkOpacity = 0.1F;

		public float WaterMarkOpacity {
			get {
				return waterMarkOpacity;
			}
			set {
				waterMarkOpacity = value;
			}
		}

		Image waterMarkImage;

		public string WaterMarkImagePath {
			get {
				if (waterMarkImage == null) {
					waterMarkImage = Image.GetInstance (Constants.HamekozLogo);
				}
				return waterMarkImage.Url.AbsolutePath;
			}
			set {
				try {
					waterMarkImage = Image.GetInstance (value);
				} catch (InvalidOperationException ex) {
					Console.WriteLine ("Fallo al obtener el archivo, usando imagen por defecto.\n Detalle: {0}", ex);
				}
			}
		}

		#endregion

		public override void OnOpenDocument (PdfWriter writer, Document document)
		{
			printTime = DateTime.Now;
			contentByte = writer.DirectContent;
			template = contentByte.CreateTemplate (50, 50);
		}

		public override void OnStartPage (PdfWriter writer, Document document)
		{
			base.OnStartPage (writer, document);

			if (HasHeaderAndFooter) {
				PrintHeader (document);
				PrintFooter (writer, document);
			}
		}

		void PrintHeader (Document document)
		{
			var headerTable = new PdfPTable (3);

			headerTable.SetWidthPercentage (new float[] { 20, 60, 20 }, document.PageSize);

			headerTable.DefaultCell.Border = Rectangle.NO_BORDER;
			headerTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

			var headerLeftCell = new PdfPCell (new Phrase (8, headerLeft, font));
			headerLeftCell.Border = Rectangle.NO_BORDER;
			headerTable.AddCell (headerLeftCell);

			var headerCenterCell = new PdfPCell (new Phrase (8, header, font));
			headerCenterCell.HorizontalAlignment = Element.ALIGN_CENTER;
			headerCenterCell.Border = Rectangle.NO_BORDER;
			headerTable.AddCell (headerCenterCell);

			var headerRightCell = new PdfPCell (new Phrase (8, headerRight, font));
			headerRightCell.Border = Rectangle.NO_BORDER;
			headerRightCell.HorizontalAlignment = Element.ALIGN_RIGHT;
			headerTable.AddCell (headerRightCell);

			contentByte.MoveTo (
				document.PageSize.GetLeft (document.LeftMargin),
				document.PageSize.GetTop (document.TopMargin - 5)
			);
			contentByte.LineTo (
				document.PageSize.GetRight (document.RightMargin),
				document.PageSize.GetTop (document.TopMargin - 5)
			);
			contentByte.Stroke ();

			headerTable.WriteSelectedRows (
				0, -1,
				document.PageSize.GetLeft (document.LeftMargin),
				document.PageSize.GetTop (document.TopMargin - 20),
				contentByte
			);
		}

		void PrintFooter (PdfWriter writer, Document document)
		{
			//TODO localizar los textos para poder traducirlos
			string generated = String.Format (
				                   "Generado el {0} {1}",
				                   printTime.ToShortDateString (),
				                   printTime.ToShortTimeString ()
			                   );

			//TODO localizar los textos para poder traducirlos
			String pageof = string.Format ("Página {0} de ", writer.PageNumber);
			float len = font.BaseFont.GetWidthPoint (pageof, font.Size) + 2;

			contentByte.AddTemplate (
				template,
				document.PageSize.GetLeft (document.LeftMargin) + len,
				document.PageSize.GetBottom (document.BottomMargin - 10)
			);

			var footerTable = new PdfPTable (3);
			footerTable.DefaultCell.Border = Rectangle.NO_BORDER;
			footerTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

			var footerLeftCell = new PdfPCell (new Phrase (8, pageof, font));
			footerLeftCell.Border = Rectangle.NO_BORDER;
			footerTable.AddCell (footerLeftCell);

			var footerCenterCell = new PdfPCell (new Phrase (8, generated, font));
			footerCenterCell.HorizontalAlignment = Element.ALIGN_CENTER;
			footerCenterCell.Border = Rectangle.NO_BORDER;
			footerTable.AddCell (footerCenterCell);

			var footerRightCell = new PdfPCell (new Phrase (8, Constants.PoweredBy, font));
			footerRightCell.Border = Rectangle.NO_BORDER;
			footerRightCell.HorizontalAlignment = Element.ALIGN_RIGHT;
			footerTable.AddCell (footerRightCell);

			contentByte.MoveTo (
				document.PageSize.GetLeft (document.LeftMargin),
				document.PageSize.GetBottom (document.BottomMargin)
			);
			contentByte.LineTo (
				document.PageSize.GetRight (document.RightMargin),
				document.PageSize.GetBottom (document.BottomMargin)
			);
			contentByte.Stroke ();

			footerTable.WriteSelectedRows (0, -1,
				document.PageSize.GetLeft (document.LeftMargin),
				document.PageSize.GetBottom (document.BottomMargin),
				contentByte
			);
		}

		public sealed override void OnEndPage (PdfWriter writer, Document document)
		{
			base.OnEndPage (writer, document);

			if (HasWaterMarkText) {
				ColumnText.ShowTextAligned (
					writer.DirectContent,
					Element.ALIGN_CENTER,
					new Phrase (waterMarkText, waterFont),
					300, 400, 45
				);
			}
			if (HasWaterMarkImage) {
				waterMarkImage.ScaleToFit (document.PageSize);
				waterMarkImage.SetAbsolutePosition (
					(document.PageSize.Width - waterMarkImage.PlainWidth) / 2,
					(document.PageSize.Height - waterMarkImage.PlainHeight) / 2
				);
				contentByte.SaveState ();
				contentByte.SetGState (state);
				contentByte.AddImage (waterMarkImage);
				contentByte.RestoreState ();
			}
		}

		public sealed override void OnCloseDocument (PdfWriter writer, Document document)
		{
			base.OnCloseDocument (writer, document);
			template.BeginText ();
			template.SetFontAndSize (font.BaseFont, font.Size);
			template.SetColorFill (font.Color);
			template.SetTextMatrix (0, 0);
			template.ShowText ((writer.PageNumber - 1).ToString ());
			template.EndText ();
		}
	}
}
