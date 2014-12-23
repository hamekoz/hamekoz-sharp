//
//  MasterWindow.cs
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
	public partial class MasterWindow : Window
	{
		public Widget actualWidget;

		public static MasterTrayIcon tray;
		private MasterTreeView menu;

		public event ChangeEventHandler QuitActivated, AboutActivated, ButtonUserClicked;
		public event MenuSelectionHandler OnChangeSelection;

		string smallIconPath;

		public string WindowTitle {
			get {
				return this.Title;
			}
			set {
				this.Title = value;
			}
		}

		public string SmallIconPath {
			get {
				return smallIconPath;
			}
			set {
				this.Icon = new Gdk.Pixbuf (System.IO.Path.Combine (System.AppDomain.CurrentDomain.BaseDirectory, 
					value));
					
				tray = new MasterTrayIcon(new Gdk.Pixbuf (System.IO.Path.Combine (System.AppDomain.CurrentDomain.BaseDirectory, 
					value)));

				tray.icon.Activate += delegate {
					this.Visible = !this.Visible;
				};

				tray.QuitActivated += delegate {
					QuitActivated();
				};

				tray.AboutActivated += delegate {
					AboutActivated();
				};

				smallIconPath = value;
			}
		}

		string xmlPath;

		public string XmlPath {
			get {
				return xmlPath;
			}
			set {
				menu = new MasterTreeView (ref treeviewMenuPrincipal, value);

				menu.OnChangeSelection += delegate(string Id) {
					OnChangeSelection (Id);
				};

				xmlPath = value;
			}
		}

		public bool ButtonUserVisibility {
			get {
				return buttonUser.Visible;
			}
			set {
				if (menu != null) {
					menu.Initializer ();
				}
				buttonUser.Visible = value;
			}
		}

		public string ButtonUserLabel {
			get {
				return buttonUser.Label;
			}
			set {
				buttonUser.Label = value;
			}
		}

		public MasterWindow() : base(WindowType.Toplevel)
		{
			Build();

			ButtonUserVisibility = false;

			this.Maximize ();

			buttonUser.Clicked += delegate {
				ButtonUserClicked();
			};
		}

		public void AddWidget(Widget widget)
		{
			RemoveWidget ();
			actualWidget = widget;
			vboxWidget.Add (actualWidget);
			actualWidget.Show ();
		}

		public void RemoveWidget()
		{
			vboxWidget.Remove (actualWidget);
		}

		protected void OnDeleteEvent(object sender, DeleteEventArgs a)
		{
			if (!Supervisor.Instance.WorkInProgress) {
				Application.Quit();
			} else {
				DialogSave guardar = new DialogSave ();
				ResponseType result = (ResponseType)guardar.Run ();

				if (result == ResponseType.Accept) {
					Supervisor.Instance.RunSaveEvent();
					Application.Quit();
				}

				if (result == ResponseType.Close) {
					Supervisor.Instance.WorkInProgress = false;
					Application.Quit();
				}

				if (result == ResponseType.Cancel) {
					a.RetVal = true;
				}
			}
		}
	}
}

