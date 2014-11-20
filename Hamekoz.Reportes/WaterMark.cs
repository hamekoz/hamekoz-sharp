//
//  WaterMark.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <rodrigo@hamekoz.com.ar>
//
//  Copyright (c) 2014 Hamekoz
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
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Hamekoz.Reportes
{
	/*
 * add a class that inherits from PdfPageEventHelper
 */
	public class Watermark : EncabezdoPieDePagina
	{
		private Font font = new Font (
			Font.FontFamily.HELVETICA, 40, Font.BOLD, new GrayColor (0.75f)
		);

		private Image image;

		public string ImagePath {
			get {
				return image.Url.AbsolutePath;
			}
			set {
				image = Image.GetInstance (value);
			}
		}

		// set transparency, see commented section below; 'image watermark'
		private PdfGState state = new PdfGState () {
			FillOpacity = 0.3F,
			StrokeOpacity = 0.3F
		};

		private string waterMarkText = string.Empty;

		public string WaterMarkText {
			get {
				return waterMarkText;
			}
			set {
				waterMarkText = value;
			}
		}

		public Watermark ()
		{
			image = Image.GetInstance ("http://www.hamekoz.com.ar/assets/logo-3327cf1683862cd164f3d76995915e24.png");
			image.SetAbsolutePosition (200, 400);
		}
		/*
 * override OnEndPage() to suite your needs;
 * here we write directly **under** each page's existing content
 */
		public override void OnEndPage (PdfWriter writer, Document document)
		{
			base.OnEndPage (writer, document);
			/*
 * text watermark
 */
			if (waterMarkText != string.Empty ) {
				ColumnText.ShowTextAligned (
					writer.DirectContentUnder,
					Element.ALIGN_CENTER, new Phrase (waterMarkText, font),
					300, 400, 45
				);
			}
			/*
 * image watermark
 */
			if (image != null) {
				PdfContentByte cb = writer.DirectContentUnder;
				cb.SaveState ();
				cb.SetGState (state);
				cb.AddImage (image);
				cb.RestoreState ();
			}
		}
	}
}