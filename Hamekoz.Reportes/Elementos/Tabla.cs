//
//  Tabla.cs
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
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Hamekoz.Reportes
{
	public class Tabla : IElemento
	{
		float fuenteTamaño = 7f;

		public float FuenteTamaño {
			get {
				return fuenteTamaño;
			}
			set {
				fuenteTamaño = value;
			}
		}

		public bool OcultarTitulosDeColumnas { get; set; }

		List<Columna> columnas = new List<Columna> ();

		public List<Columna> Columnas {
			get { return columnas; }
			set { columnas = value; }
		}

		readonly List<Object> datos = new List<Object> ();

		public void AgregarDato (Object dato)
		{
			datos.Add (dato);
		}

		readonly List<Object> totales = new List<Object> ();

		public void AgregarTotal (Object total)
		{
			totales.Add (total);
		}

		public bool MostrarBordes { get; set; }

		public PdfPCell GetNewHeader (Columna columna)
		{
			Font font = FontFactory.GetFont (Font.FontFamily.HELVETICA.ToString (), fuenteTamaño, Font.BOLD);
			var phrase = new Phrase (columna.Nombre, font);
			var pdfPCell = new PdfPCell (tabla.DefaultCell);
			pdfPCell.PaddingBottom = 3;
			if (MostrarBordes) {
				pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
			} else {
				pdfPCell.BorderWidthBottom = 0.5f;
			}

			pdfPCell.Phrase = phrase;
			pdfPCell.HorizontalAlignment = (int)columna.Alineacion;
			return pdfPCell;
		}

		public PdfPCell GetNewTotal (Object total)
		{
			Font font = FontFactory.GetFont (Font.FontFamily.HELVETICA.ToString (), fuenteTamaño, Font.BOLD);
			var pdfPCell = new PdfPCell (tabla.DefaultCell);
			pdfPCell.PaddingTop = 2;
			if (MostrarBordes) {
				pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
			} else {
				pdfPCell.BorderWidthTop = 0.5f;
			}
			pdfPCell.PaddingBottom = 3;
			pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
			if (total is string) {
				Alineaciones alineacionColumna = columnas [totales.IndexOf (total) % columnas.Count].Alineacion;
				switch (alineacionColumna) {
				case Alineaciones.Izquierda:
					pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
					break;
				case Alineaciones.Centrado:
					pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
					break;
				case Alineaciones.Justificado:
					pdfPCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
					break;
				case Alineaciones.Derecha:
					pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
					break;
				default:
					pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
					break;
				}
			}
			if (total is double) {
				total = string.Format ("{0:#0.00}", total);
			}
			if (total is decimal) {
				total = string.Format ("{0:C}", total);
			}
			if (total is float) {
				total = string.Format ("{0:#0.0000}", total);
			}

			var phrase = new Phrase (total.ToString (), font);
			pdfPCell.Phrase = phrase;
			return pdfPCell;
		}

		public PdfPCell GetNewData (Object dato)
		{
			Font font = FontFactory.GetFont (Font.FontFamily.HELVETICA.ToString (), fuenteTamaño);
			var pdfPCell = new PdfPCell (tabla.DefaultCell);
			pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
			if (dato is string) {
				Alineaciones alineacionColumna = columnas [datos.IndexOf (dato) % columnas.Count].Alineacion;
				switch (alineacionColumna) {
				case Alineaciones.Izquierda:
					pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
					break;
				case Alineaciones.Centrado:
					pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
					break;
				case Alineaciones.Justificado:
					pdfPCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
					break;
				case Alineaciones.Derecha:
					pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
					break;
				default:
					pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
					break;
				}
			}
			if (dato is double) {
				dato = string.Format ("{0:#0.00}", dato);
			}
			if (dato is decimal) {
				dato = string.Format ("{0:C}", dato);
			}
			if (dato is float) {
				dato = string.Format ("{0:#0.0000}", dato);
			}

			var phrase = new Phrase (dato.ToString (), font);
			pdfPCell.Phrase = phrase;
			return pdfPCell;
		}

		float[] AnchoDeColumnas ()
		{
			var anchos = new List<float> ();
			foreach (var columna in Columnas) {
				anchos.Add (columna.Ancho);
			}
			return anchos.ToArray ();
		}

		#region IElemento implementation

		PdfPTable tabla;

		public IElement GetElemento ()
		{
			tabla = new PdfPTable (columnas.Count);
			tabla.SpacingBefore = 3f;
			tabla.SpacingAfter = 3f;
			if (!MostrarBordes) {
				tabla.DefaultCell.Border = Rectangle.NO_BORDER;
			}
			tabla.DefaultCell.BackgroundColor = new BaseColor (System.Drawing.Color.Transparent);
			tabla.WidthPercentage = 100;
			if (!OcultarTitulosDeColumnas) {
				tabla.HeaderRows = 1;
				foreach (Columna columna in Columnas) {
					tabla.AddCell (GetNewHeader (columna));
				}
			}
			foreach (Object dato in datos) {
				tabla.AddCell (GetNewData (dato));
			}
			foreach (Object total in totales) {
				tabla.AddCell (GetNewTotal (total));
			}

			tabla.SetWidths (AnchoDeColumnas ());

			return tabla;
		}

		#endregion
	}
}