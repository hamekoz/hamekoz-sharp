//
//  Icons.cs
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
using Xwt.Drawing;

namespace Hamekoz.UI
{
	public static class Icons
	{
		public static readonly Image Document = Image.FromResource ("Hamekoz.UI.Icons.x-office-document.png");
		public static readonly Image Spreadsheet = Image.FromResource ("Hamekoz.UI.Icons.x-office-spreadsheet.png");
		public static readonly Image Save = Image.FromResource ("Hamekoz.UI.Icons.document-save.png");
		public static readonly Image SaveAs = Image.FromResource ("Hamekoz.UI.Icons.document-save-as.png");
		public static readonly Image UserInfo = Image.FromResource ("Hamekoz.UI.Icons.user-info.png");
		public static readonly Image EditFind = Image.FromResource ("Hamekoz.UI.Icons.edit-find.png");
	}
}

