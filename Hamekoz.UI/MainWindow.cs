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
using Mono.Unix;
using Xwt;
using Xwt.Drawing;

namespace Hamekoz.UI
{
	public class MainWindow : Window
	{
		public void SetMenuLabel (string text)
		{
			menuTree.Columns [0].Title = text;
		}

		StatusIcon statusIcon;
		HPaned panel;
		TreeView menuTree;
		readonly TreeStore store;

		readonly DataField<string> nameCol = new DataField<string> ();
		readonly DataField<ItemWidget> widgetCol = new DataField<ItemWidget> ();
		readonly DataField<Image> iconCol = new DataField<Image> ();

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
			Title = string.Format (Catalog.GetString ("{0} Demo Application"), "Hamekoz Xwt");
			InitialLocation = WindowLocation.CenterScreen;
			Icon = Image.FromResource (Resources.Icon);
			WindowState = WindowState.Maximized;
			try {
				statusIcon = Application.CreateStatusIcon ();
				statusIcon.Menu = new Menu ();
				var about = new MenuItem {
					Label = Catalog.GetString ("About"),
					Image = Icons.Starred.WithSize (IconSize.Small),
				};
				var exit = new MenuItem {
					Label = Catalog.GetString ("Exit"),
					Image = Icons.Delete.WithSize (IconSize.Small),
				};
				statusIcon.Menu.Items.Add (about);
				statusIcon.Menu.Items.Add (exit);
				about.Clicked += AboutClicked;
				exit.Clicked += ExitClicked;
				statusIcon.Image = Icon;
			} catch {
				Console.WriteLine (Catalog.GetString ("Status icon could not be shown"));
			}

			panel = new HPaned ();

			store = new TreeStore (nameCol, iconCol, widgetCol);
			menuTree = new TreeView ();
			menuTree.Columns.Add ("", iconCol, nameCol);

			menuTree.DataSource = store;
			menuTree.SelectionChanged += HandleMenuTreeSelectionChanged;
			menuTree.MinWidth = 220;

			panel.Panel1.Content = menuTree;
			panel.Panel1.Content.MarginRight = 5;

			panel.Panel2.Resize = true;

			Content = panel;

			CloseRequested += HandleCloseRequested;

			InitialLocation = WindowLocation.CenterScreen;
		}

		public delegate void AboutHandler ();

		public AboutHandler OnAboutClicked;

		void AboutClicked (object sender, EventArgs e)
		{
			var about = OnAboutClicked;
			if (about != null) {
				about ();
			} else {
				var log = new About ();
				log.Run ();			
			}
		}

		void ExitClicked (object sender, EventArgs e)
		{
			Close ();
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
			args.AllowClose = MessageDialog.Confirm (Catalog.GetString ("The application will be closed"), Command.Ok);
			if (args.AllowClose)
				Application.Exit ();
		}

		Label SelectItemLabel = new Label {
			Text = Catalog.GetString ("Select one item from menu"),
			TextAlignment = Alignment.Center
		};

		Label AccessDeniedLabel = new Label {
			Text = Catalog.GetString ("Access Denied. You do not have sufficient permissions."),
			TextAlignment = Alignment.Center
		};

		void HandleMenuTreeSelectionChanged (object sender, EventArgs e)
		{
			if (menuTree.SelectedRow != null) {
				ItemWidget w = store.GetNavigatorAt (menuTree.SelectedRow).GetValue (widgetCol);
				if (w.Type != null) {
					if (AllowAccess (w)) {
						if (w.Widget == null)
							w.Widget = (Widget)Activator.CreateInstance (w.Type);
						panel.Panel2.Content = w.Widget;
					} else {
						panel.Panel2.Content = AccessDeniedLabel;
					}
				} else {
					panel.Panel2.Content = SelectItemLabel;
				}
			}
			panel.Panel2.Content.MarginLeft = 5;
		}

		public virtual bool AllowAccess (ItemWidget widget)
		{
			return true;
		}

		public TreePosition AddWiget (TreePosition pos, string name)
		{
			return store.AddNode (pos)
				.SetValue (nameCol, name)
				.CurrentPosition;
		}

		public TreePosition AddWiget (TreePosition pos, string name, Type widgetType)
		{
			return store.AddNode (pos)
				.SetValue (nameCol, name)
			//.SetValue (iconCol, Icon)
				.SetValue (widgetCol, new ItemWidget (widgetType))
				.CurrentPosition;
		}

		public TreePosition AddWiget (TreePosition pos, string name, Type type, Widget widget)
		{
			var item = new ItemWidget (type) { Widget = widget };
			return store.AddNode (pos)
				.SetValue (nameCol, name)
			//.SetValue (iconCol, Icon)
				.SetValue (widgetCol, item)
				.CurrentPosition;
		}

		public class ItemWidget
		{
			public ItemWidget (Type type)
			{
				Type = type;
			}

			public Type Type;
			public Widget Widget;
		}
	}
}

