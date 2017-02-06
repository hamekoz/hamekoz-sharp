//
//  MainWindow.cs
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
using Xwt;

namespace Hamekoz.Negocio.UI
{
	public class MainWindow : Window
	{
		public MainWindow ()
		{
			Title = "Hamekoz.Negocio.UI";
			Width = 400;
			Height = 400;

			var menu = new Menu ();

			var file = new MenuItem ("_File");
			file.SubMenu = new Menu ();
			file.SubMenu.Items.Add (new MenuItem ("_Open"));
			file.SubMenu.Items.Add (new MenuItem ("_New"));
			var mi = new MenuItem ("_Close");
			mi.Clicked += (sender, e) => Close ();
			file.SubMenu.Items.Add (mi);
			menu.Items.Add (file);
			MainMenu = menu;

			var sampleLabel = new Label ("Hamekoz.Negocio.UI");
			Content = sampleLabel;
		}

		protected override bool OnCloseRequested ()
		{
			var allow_close = MessageDialog.Confirm ("Hamekoz.Negocio.UI will be closed", Command.Ok);
			if (allow_close)
				Application.Exit ();
			return allow_close;
		}
	}
}


