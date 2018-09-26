//
//  ComprobantePDF.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2018 Hamekoz - www.hamekoz.com.ar
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
using System.Linq;
using Hamekoz.Fiscal;
using Hamekoz.Negocio;
using Hamekoz.Reportes;
using Humanizer;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Hamekoz.Argentina.Afip
{
	//TODO se podria convertir en clase static con metodo extension ToPdf(Emisor)
	public class ComprobantePDF
	{
		public static string BasePath {
			get;
			set;
		} = Path.GetTempPath ();

		public Comprobante Comprobante { 
			get; 
			set;
		}

		public Empresa Emisor { 
			get; 
			set;
		}

		static Font fuente = FontFactory.GetFont (FontFactory.HELVETICA, 9);
		static Font fuenteTitulo = FontFactory.GetFont (FontFactory.HELVETICA, 9, Font.BOLD);
		static Font fuenteTotal = FontFactory.GetFont (FontFactory.HELVETICA, 12, Font.BOLD);
		static Font fuenteBarcodeText = FontFactory.GetFont (FontFactory.HELVETICA, 6);

		public void ToPdf ()
		{
			if (Comprobante == null)
				throw new ParametrosInsuficientesException ("Debe indicar un comprobante");

			//HACK ruta template deberia ser un parametro
			string filename = Path.Combine (BasePath, Comprobante + ".pdf");

			var document = new Document (PageSize.A4);
			PdfWriter.GetInstance (document, new FileStream (filename, FileMode.Create));
			document.Open ();

			var	encabezadoTable = new PdfPTable (3) { WidthPercentage = 100 };
			encabezadoTable.SetTotalWidth (new float[] { 200, 25, 200 });
			encabezadoTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
			encabezadoTable.DefaultCell.VerticalAlignment = Element.ALIGN_BASELINE;
			encabezadoTable.DefaultCell.Border = Rectangle.NO_BORDER;

			var auxCell = new PdfPCell (new Phrase ("")) {
				Rowspan = 7,
				Border = Rectangle.NO_BORDER,
			};

			Image logo = Image.GetInstance (Emisor.LogoBanner);
			logo.Alignment = 1;
			encabezadoTable.AddCell (logo);
			var letraParrafo = new Phrase (Comprobante.Tipo.Letra, FontFactory.GetFont (FontFactory.HELVETICA, 32, Font.BOLD));
			var letraCell = new PdfPCell (letraParrafo) {
				Border = Rectangle.BOX,
				VerticalAlignment = Element.ALIGN_CENTER,
				HorizontalAlignment = Element.ALIGN_CENTER
			};
			encabezadoTable.AddCell (letraCell);
			encabezadoTable.AddCell (new Phrase (string.Format ("{0}\n{1}", Comprobante.Tipo.Descripcion, Comprobante.Numero), FontFactory.GetFont (FontFactory.HELVETICA, 18, Font.BOLD)));

			encabezadoTable.AddCell (new Phrase (Emisor.RazonSocial.ToUpper (), FontFactory.GetFont (FontFactory.HELVETICA, 15, Font.BOLD)));
			encabezadoTable.AddCell (new Phrase (string.Format ("COD.{0:00}", Comprobante.Tipo.Codigo), FontFactory.GetFont (FontFactory.HELVETICA, 6, Font.BOLD)));
			encabezadoTable.AddCell (new Phrase (string.Format ("Fecha: {0:dd/MM/yyyy}", Comprobante.Emision), FontFactory.GetFont (FontFactory.HELVETICA, 12, Font.BOLD)));

			encabezadoTable.AddCell (new Phrase ("Endulzando generaciones", FontFactory.GetFont (FontFactory.HELVETICA, 9, Font.ITALIC)));
			encabezadoTable.AddCell (auxCell);
			//TODO la cantidad de paginas deberia ser estampado segun la cantidad de paginas
			encabezadoTable.AddCell (new Phrase ("Original - Página 1 de 1", fuente));

			encabezadoTable.AddCell (new Phrase (Emisor.Domicilio, fuente));
			encabezadoTable.AddCell (new Phrase (Emisor.Tipo.Humanize (), fuente));

			encabezadoTable.AddCell (new Phrase (string.Format ("{0} - {1}", Emisor.Email, Emisor.Web), fuente));
			encabezadoTable.AddCell (new Phrase (string.Format ("CUIT: {0} - IIBB: {1}", Emisor.CUIT, Emisor.NumeroDeIngresosBrutos), fuente));

			encabezadoTable.AddCell (new Phrase (Emisor.Telefonos, fuente));
			encabezadoTable.AddCell (new Phrase (string.Format ("Inicio de actividad: {0:dd/MM/yyyy}", Emisor.InicioDeActividad), fuente));

			var encabezadoCell = new PdfPCell (encabezadoTable) { Border = Rectangle.BOX };
			var encabezadoBoxTable = new PdfPTable (1) { WidthPercentage = 100 };
			encabezadoBoxTable.AddCell (encabezadoCell);

			var	receptorTable = new PdfPTable (4) {
				WidthPercentage = 100
			};
			receptorTable.DefaultCell.Border = Rectangle.NO_BORDER;
			receptorTable.SetTotalWidth (new float[] { 110, 200, 80, 200 });

			receptorTable.AddCell (new Phrase ("Razón Social:", fuenteTitulo));
			receptorTable.AddCell (new PdfPCell (new Phrase (Comprobante.Responsable.RazonSocial, fuente)) {
				Colspan = 3,
				Border = Rectangle.NO_BORDER
			});
			receptorTable.AddCell (new Phrase ("Dirección:", fuenteTitulo));
			receptorTable.AddCell (new PdfPCell (new Phrase (Comprobante.Responsable.Domicilio ?? string.Empty, fuente)) {
				Colspan = 3,
				Border = Rectangle.NO_BORDER
			});
			receptorTable.AddCell (new Phrase ("Condición de IVA:", fuenteTitulo));
			receptorTable.AddCell (new Phrase (Comprobante.Responsable.Tipo.Humanize (), fuente));
			receptorTable.AddCell (new Phrase ("CUIT:", fuenteTitulo));
			receptorTable.AddCell (new Phrase (Comprobante.Responsable.CUIT, fuente));
			receptorTable.AddCell (new Phrase ("Condición de pago:", fuenteTitulo));
			//HACK porque las NC no tiene definida la condicion de pago
			receptorTable.AddCell (new Phrase ((Comprobante.CondicionDePago ?? new CondicionDePago ()).Descripcion.ToLower ().Titleize (), fuente));
			receptorTable.AddCell (new Phrase ("Vencimiento:", fuenteTitulo));
			receptorTable.AddCell (new Phrase (Comprobante.Vencimiento.ToShortDateString (), fuente));

			if (Comprobante.Remito != null && Comprobante.Remito.Id > 0) {
				var remitoCliente = Comprobante.Remito as RemitoCliente;
				receptorTable.AddCell (new Phrase ("Remito Nº:", fuenteTitulo));
				receptorTable.AddCell (new Phrase (Comprobante.Remito.Numero, fuente));
				receptorTable.AddCell (new Phrase (""));
				receptorTable.AddCell (new Phrase (""));
				receptorTable.AddCell (new Phrase ("Domicilio de entrega:", fuenteTitulo));
				receptorTable.AddCell (new PdfPCell (new Phrase (remitoCliente.DomicilioDeEntrega.ToString () ?? string.Empty, fuente)) {
					Colspan = 3,
					Border = Rectangle.NO_BORDER
				});
			}

			var receptorCell = new PdfPCell (receptorTable) { Border = Rectangle.BOX };
			var receptorBoxTable = new PdfPTable (1) { SpacingBefore = 5f, WidthPercentage = 100 };
			receptorBoxTable.AddCell (receptorCell);

			var itemsTable = new PdfPTable (5) { SpacingBefore = 5f, WidthPercentage = 100 };
			itemsTable.SetTotalWidth (new float[] { 45, 300, 50, 70, 75 });

			itemsTable.DefaultCell.PaddingLeft = 3;
			itemsTable.DefaultCell.PaddingRight = 3;
			itemsTable.DefaultCell.Border = Rectangle.BOX;
			itemsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
			itemsTable.AddCell (new Phrase ("Código", fuenteTitulo));
			itemsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
			itemsTable.AddCell (new Phrase ("Descripción", fuenteTitulo));
			itemsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
			itemsTable.AddCell (new Phrase ("Cantidad", fuenteTitulo));
			itemsTable.AddCell (new Phrase ("Precio", fuenteTitulo));
			itemsTable.AddCell (new Phrase ("Importe", fuenteTitulo));

			itemsTable.DefaultCell.PaddingBottom = 0;
			itemsTable.DefaultCell.PaddingTop = 0;

			itemsTable.DefaultCell.Border = Rectangle.NO_BORDER;
			itemsTable.DefaultCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
			if (Comprobante.Items != null) {
				//TODO puedo agrupar los items del mismo concepto de distinto lote seria aca o al generar la factura de un remito
				foreach (var item in Comprobante.Items) {
					itemsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
					itemsTable.AddCell (new Phrase (item.Codigo, fuente));
					itemsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
					itemsTable.AddCell (new Phrase (item.Descripcion, fuente));
					itemsTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
					itemsTable.AddCell (new Phrase (item.Cantidad.ToString (), fuente));
					itemsTable.AddCell (new Phrase (item.Precio.ToString ("C2"), fuente));
					if (Comprobante.Tipo.Letra == "A") {
						itemsTable.AddCell (new Phrase (item.Neto.ToString ("C2"), fuente));
					} else {
						itemsTable.AddCell (new Phrase (item.Total.ToString ("C2"), fuente));
					}
				}
				//Relleno con espacios en blanco
				//FIXME deberia haber una forma mas eficiente de rellenar la tabla con espacios
				if (CalculatePdfPTableHeight (itemsTable) < 430) {
					while (CalculatePdfPTableHeight (itemsTable) < 430) {
						itemsTable.AddCell (new Phrase (" ", fuente));
						itemsTable.AddCell (new Phrase (" ", fuente));
						itemsTable.AddCell (new Phrase (" ", fuente));
						itemsTable.AddCell (new Phrase (" ", fuente));
						itemsTable.AddCell (new Phrase (" ", fuente));
					}
				}
			}
			itemsTable.DefaultCell.Border = itemsTable.DefaultCell.Border | Rectangle.BOTTOM_BORDER;
			itemsTable.AddCell (new Phrase (" ", fuente));
			itemsTable.AddCell (new Phrase (" ", fuente));
			itemsTable.AddCell (new Phrase (" ", fuente));
			itemsTable.AddCell (new Phrase (" ", fuente));
			itemsTable.AddCell (new Phrase (" ", fuente));
			itemsTable.HeaderRows = 1;

			var totalesTable = new PdfPTable (2) {
				HorizontalAlignment = 2,
				WidthPercentage = 100,
			};
			totalesTable.SetTotalWidth (new float[] { 200, 92 });

			var bw = new ZXing.BarcodeWriter {
				Format = ZXing.BarcodeFormat.ITF,
			};
			bw.Options = new ZXing.Common.EncodingOptions { Height = 35, Margin = 20, PureBarcode = true };
			var fe = Comprobante as IComprobanteElectronico;
			var barcodeText = fe.BarcodeText (Emisor);
			var barcodeImage = new System.Drawing.Bitmap (bw.Write (barcodeText));
			Image barcode = Image.GetInstance (barcodeImage, BaseColor.WHITE);

			var caeCell = new PdfPCell (new Phrase (string.Format ("C.A.E. Nº {0} Fecha Vto. CAE: {1:dd/MM/yyyy}", fe.CAE.Trim (), fe.VencimientoCAE ()), fuenteTitulo)) {
				HorizontalAlignment = Element.ALIGN_CENTER,
				Border = Rectangle.NO_BORDER
			};

			var barcodeCell = new PdfPCell (barcode, true) { Border = Rectangle.NO_BORDER };
			var barcodeTextCell = new PdfPCell (new Phrase (barcodeText, fuenteBarcodeText)) {
				HorizontalAlignment = Element.ALIGN_CENTER,
				Border = Rectangle.NO_BORDER,
			};

			var observacionesCell = new PdfPCell (new Phrase (Comprobante.Observaciones, fuente)) {
				HorizontalAlignment = Element.ALIGN_JUSTIFIED,
				Border = Rectangle.NO_BORDER,
				Rowspan = 10,
			};

			var barcodeTable = new PdfPTable (1);
			barcodeTable.AddCell (caeCell);
			barcodeTable.AddCell (barcodeCell);
			barcodeTable.AddCell (barcodeTextCell);
			barcodeTable.AddCell (observacionesCell);

			totalesTable.DefaultCell.Border = Rectangle.NO_BORDER;
			totalesTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
			if (Comprobante.Tipo.Letra == "A") {
				if (Comprobante.Gravado > 0) {
					totalesTable.AddCell (new Phrase ("Neto Gravado", fuenteTitulo));
					totalesTable.AddCell (new Phrase (Comprobante.Gravado.ToString ("C2"), fuenteTitulo));	
				}
				if (Comprobante.NoGravado > 0) {
					totalesTable.AddCell (new Phrase ("No Gravado", fuenteTitulo));
					totalesTable.AddCell (new Phrase (Comprobante.NoGravado.ToString ("C2"), fuenteTitulo));
				}
				if (Comprobante.Exento > 0) {
					totalesTable.AddCell (new Phrase ("Exento", fuenteTitulo));
					totalesTable.AddCell (new Phrase (Comprobante.Exento.ToString ("C2"), fuenteTitulo));
				}
				foreach (var iva in Comprobante.IVAItems.Where(i => i.Importe > 0)) {
					totalesTable.AddCell (new Phrase ("IVA " + iva.IVA.Humanize (), fuenteTitulo));
					totalesTable.AddCell (new Phrase (iva.Importe.ToString ("C2"), fuenteTitulo));
				}
			} else {
				totalesTable.AddCell (new Phrase ("Subtotal", fuenteTitulo));
				//TODO podria ser una propiedad Subtotal de comprobante
				totalesTable.AddCell (new Phrase ((Comprobante.Total - Comprobante.Tributos).ToString ("C2"), fuenteTitulo));
			}
			foreach (var impuesto in Comprobante.Impuestos.Where(i => i.Importe > 0)) {
				totalesTable.AddCell (new Phrase (impuesto.Impuesto.Descripcion.Transform (To.LowerCase, To.TitleCase), fuenteTitulo));
				totalesTable.AddCell (new Phrase (impuesto.Importe.ToString ("C2"), fuenteTitulo));
			}
			totalesTable.DefaultCell.Border = Rectangle.TOP_BORDER;
			totalesTable.AddCell (new Phrase ("TOTAL", fuenteTotal));
			totalesTable.AddCell (new Phrase (Comprobante.Total.ToString ("C2"), fuenteTotal));

			var resumenTable = new PdfPTable (2) {
				WidthPercentage = 100
			};
			resumenTable.DefaultCell.Border = Rectangle.NO_BORDER;
			resumenTable.AddCell (barcodeTable);
			resumenTable.AddCell (totalesTable);

			document.Add (encabezadoBoxTable);
			document.Add (receptorBoxTable);
			itemsTable.KeepTogether = false;
			document.Add (itemsTable);
			document.Add (resumenTable);

			document.Close ();

			Image img = Image.GetInstance (Emisor.LogoMarcaDeAgua);
			img.ScalePercent (30f);
			img.SetAbsolutePosition (
				(document.PageSize.Width - img.PlainWidth) / 2,
				(document.PageSize.Height - img.PlainHeight) / 2
			);

			byte[] bytes = File.ReadAllBytes (filename);

			using (var stream = new MemoryStream ()) {
				var reader = new PdfReader (bytes);

				using (var stamper = new PdfStamper (reader, stream)) {
					int pages = reader.NumberOfPages;
					for (int i = 1; i <= pages; i++) {
						//ColumnText.ShowTextAligned (stamper.GetUnderContent (i), Element.ALIGN_RIGHT, new Phrase (string.Format ("{0} de {1} ", i, pages), fuente), 50f, 20f, 0);
						PdfContentByte watermark = stamper.GetUnderContent (i);
						watermark.AddImage (img);
					}
				}
				bytes = stream.ToArray ();
			}
			File.WriteAllBytes (filename, bytes);
			Process.Start (filename);
		}

		static float CalculatePdfPTableHeight (PdfPTable table)
		{
			using (var stream = new MemoryStream ()) {
				using (var document = new Document (PageSize.A4)) {
					using (PdfWriter writer = PdfWriter.GetInstance (document, stream)) {
						document.Open ();
						table.WriteSelectedRows (0, table.Rows.Count, 0, 0, writer.DirectContent);
						document.Close ();
						return table.TotalHeight;
					}
				}
			}
		}
	}
}

