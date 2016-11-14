//
//  ReportChooser.cs
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
using Mono.Unix;
using Xwt;

namespace Hamekoz.UI
{
	public class ReportChooser<T> : ItemChooser<T>
	{
		public event EventHandler PdfClicked, XlsClicked, ExportClicked;

		public readonly Button pdf = new Button {
			Label = "PDF",
			ExpandHorizontal = true,
			HorizontalPlacement = WidgetPlacement.Fill,
			Image = Icons.Document.WithSize (IconSize.Medium),
			ImagePosition = ContentPosition.Left,
		};
		public readonly Button xlsx = new Button {
			Label = "XLSX",
			ExpandHorizontal = true,
			HorizontalPlacement = WidgetPlacement.Fill,
			Image = Icons.Spreadsheet.WithSize (IconSize.Medium),
			ImagePosition = ContentPosition.Left,
		};
		readonly Button export = new Button {
			Label = Catalog.GetString ("Export"),
			ExpandHorizontal = true,
			HorizontalPlacement = WidgetPlacement.Fill,
			Image = Icons.SaveAs.WithSize (IconSize.Medium),
			ImagePosition = ContentPosition.Left,
		};

		public ReportChooser ()
		{
			pdf.Clicked += OnPdfClicked;
			xlsx.Clicked += OnXlsxClicked;
			export.Clicked += OnExportClicked;
			AddAction (pdf);
			AddAction (xlsx);
			AddAction (export);
		}

		void OnExportClicked (object sender, EventArgs e)
		{
			var handler = ExportClicked;
			if (handler != null)
				handler (sender, e);
		}

		void OnXlsxClicked (object sender, EventArgs e)
		{
			var handler = XlsClicked;
			if (handler != null)
				handler (sender, e);
		}

		void OnPdfClicked (object sender, EventArgs e)
		{
			var handler = PdfClicked;
			if (handler != null)
				handler (sender, e);
		}
	}
}

