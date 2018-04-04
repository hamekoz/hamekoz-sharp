//
//  App.cs
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

namespace Hamekoz.PyME.UI.Desktop
{
	public static partial class App
	{
		static MainWindow MainWindow;

		static App ()
		{
			ToolkitWindows = ToolkitType.Wpf;
			ToolkitLinux = ToolkitType.Gtk3;
			ToolkitMacOS = ToolkitType.XamMac;
		}

		static void OnRun (string[] args)
		{
			MainWindow = new MainWindow ();
			MainWindow.Show ();
		}

		static void OnExit ()
		{
			if (MainWindow != null)
				MainWindow.Dispose ();
		}
	}
}


