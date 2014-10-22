//
//  MasterTrayIcon.cs
//
//  Author:
//       Emiliano Gabriel Canedo <emilianocanedo@gmail.com>
//
//  Copyright (c) 2014 ecanedo
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
using Gtk;

namespace Hamekoz.UI.Gtk
{
	public class MasterTrayIcon
	{
		public StatusIcon icon;

		public event ChangeEventHandler QuitActivated, AboutActivated;

		public MasterTrayIcon (Gdk.Pixbuf image)
		{
			try
			{
				icon = new StatusIcon(image);
			}
			catch (Exception ex)
			{
				icon = StatusIcon.NewFromStock(Stock.Execute);
				Console.WriteLine(ex.Message);
			}

			icon.PopupMenu += PopUpEvent;
		}

		void PopUpEvent (object o, PopupMenuArgs args)
		{
			Menu popupMenu = new Menu ();
			ImageMenuItem menuItemQuit = new ImageMenuItem ("Salir");
			Image appimgQuit = new Image (Stock.Quit, IconSize.Menu);
			menuItemQuit.Image = appimgQuit;
			ImageMenuItem menuItemAbout = new ImageMenuItem ("Acerca de");
			Image appimgAbout = new Image (Stock.About, IconSize.Menu);
			menuItemAbout.Image = appimgAbout;
			popupMenu.Add (menuItemAbout);
			popupMenu.Add (menuItemQuit);
			menuItemQuit.Activated += delegate {
				QuitActivated();
			};
			menuItemAbout.Activated += delegate {
				AboutActivated();
			};
			popupMenu.ShowAll ();
			popupMenu.Popup ();
		}
	}
}

