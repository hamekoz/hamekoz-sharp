//
//  MasterWindow.cs
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
using Xwt;
using Xwt.Drawing;

namespace Hamekoz.UI
{
	public class MainWindow : Window
	{
		StatusIcon statusIcon;
		HPaned panel;
		TreeView menuTree;
		TreeStore store;

		DataField<string> nameCol = new DataField<string> ();
		DataField<ItemWidget> widgetCol = new DataField<ItemWidget> ();
		DataField<Image> iconCol = new DataField<Image> ();

		public StatusIcon StatusIcon {
			get {
				return statusIcon;
			}
		}

		public void SetIcon (Image icon)
		{
			Icon = icon;
			statusIcon.Image = icon;
		}

		public MainWindow ()
		{
			Title = "Hamekoz Xwt Demo Application";
			Width = 500;
			Height = 400;

			Icon = Image.FromResource (GetType (), Resources.Icon);

			try {
				statusIcon = Application.CreateStatusIcon ();
				statusIcon.Menu = new Menu ();
				statusIcon.Menu.Items.Add (new MenuItem ("About"));
				statusIcon.Image = Icon;
			} catch {
				Console.WriteLine ("Status icon could not be shown");
			}

			panel = new HPaned ();

			store = new TreeStore (nameCol, iconCol, widgetCol);
			menuTree = new TreeView ();
			menuTree.Columns.Add ("", iconCol, nameCol);

			menuTree.DataSource = store;
			menuTree.SelectionChanged += HandleMenuTreeSelectionChanged;

			panel.Panel1.Content = menuTree;

			panel.Panel2.Resize = true;

			Content = panel;

			CloseRequested += HandleCloseRequested;
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			if (statusIcon != null) {
				statusIcon.Dispose ();
			}
		}

		void HandleCloseRequested (object sender, CloseRequestedEventArgs args)
		{
			args.AllowClose = MessageDialog.Confirm ("Se va a cerrar la aplicación", Command.Ok);
			if (args.AllowClose)
				Application.Exit ();
		}

		void HandleMenuTreeSelectionChanged (object sender, EventArgs e)
		{
			if (menuTree.SelectedRow != null) {
				ItemWidget w = store.GetNavigatorAt (menuTree.SelectedRow).GetValue (widgetCol);
				if (w.Type != null) {
					if (w.Widget == null)
						w.Widget = (Widget)Activator.CreateInstance (w.Type);
					panel.Panel2.Content = w.Widget;
				} else {
					panel.Panel2.Content = new Label ("Select one item from menu");
				}
			}
		}

		public TreePosition AddWiget (TreePosition pos, string name, Type widgetType)
		{
			return store.AddNode (pos)
				.SetValue (nameCol, name)
				.SetValue (iconCol, Icon)
				.SetValue (widgetCol, new ItemWidget (widgetType))
				.CurrentPosition;
		}

		class ItemWidget
		{
			public ItemWidget (Type type)
			{
				Type = type;
			}

			public Type Type;
			public Widget Widget;
		}

		public void Splash ()
		{
			var splash = new Splash ();
			splash.Show ();
			Application.Invoke (() => splash.Run ());
			splash.Finish += delegate {
				splash.Close ();
				Show ();
			};
		}
	}
}

