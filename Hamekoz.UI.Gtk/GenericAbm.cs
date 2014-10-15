//
//  GenericAbm.cs
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
using Hamekoz.Interfaces;

namespace Hamekoz.UI.Gtk
{
	public class GenericAbm<T> : Bin
	{
		#region GTK_GUI 
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::Gtk.HPaned hpaned;
		private global::Gtk.VBox vboxMenu;
		private global::Hamekoz.UI.Gtk.SearchableTreeView searchabletreeview;
		private global::Gtk.VBox vboxAbm;
		private global::Gtk.VBox vboxWidget;
		private global::Gtk.HButtonBox HBoxPrincipal;
		private global::Gtk.Button buttonAdd;
		private global::Gtk.Button buttonSave;
		private global::Gtk.Button buttonEdit;
		private global::Gtk.Button buttonDelete;
		private global::Gtk.Button buttonCancel;

		void Build()
		{
			global::Stetic.Gui.Initialize (this);

			// Widget Hamekoz.UI.Gtk.WidgetAbm
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Hamekoz.UI.Gtk.WidgetAbm";

			// Container child Hamekoz.UI.Gtk.WidgetAbm.Gtk.Container+ContainerChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));

			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			global::Gtk.Viewport w1 = new global::Gtk.Viewport ();
			w1.ShadowType = ((global::Gtk.ShadowType)(0));

			// Container child GtkViewport.Gtk.Container+ContainerChild
			this.hpaned = new global::Gtk.HPaned ();
			this.hpaned.CanFocus = true;
			this.hpaned.Name = "hpaned";
			this.hpaned.Position = 300;

			// Container child hpaned.Gtk.Paned+PanedChild
			this.vboxMenu = new global::Gtk.VBox ();
			this.vboxMenu.Name = "vboxMenu";
			this.vboxMenu.Spacing = 6;

			// Container child vboxMenu.Gtk.Box+BoxChild
			this.searchabletreeview = new global::Hamekoz.UI.Gtk.SearchableTreeView ();
			this.searchabletreeview.Events = ((global::Gdk.EventMask)(256));
			this.searchabletreeview.Name = "searchabletreeview";
			this.vboxMenu.Add (this.searchabletreeview);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vboxMenu [this.searchabletreeview]));
			w2.Position = 0;
			this.hpaned.Add (this.vboxMenu);
			global::Gtk.Paned.PanedChild w3 = ((global::Gtk.Paned.PanedChild)(this.hpaned [this.vboxMenu]));
			w3.Resize = false;

			// Container child hpaned.Gtk.Paned+PanedChild
			this.vboxAbm = new global::Gtk.VBox ();
			this.vboxAbm.Name = "vboxAbm";
			this.vboxAbm.Spacing = 6;

			// Container child vboxAbm.Gtk.Box+BoxChild
			this.vboxWidget = new global::Gtk.VBox ();
			this.vboxWidget.Name = "vboxWidget";
			this.vboxWidget.Spacing = 6;
			this.vboxAbm.Add (this.vboxWidget);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vboxAbm [this.vboxWidget]));
			w4.Position = 0;

			// Container child vboxAbm.Gtk.Box+BoxChild
			this.HBoxPrincipal = new global::Gtk.HButtonBox ();
			this.HBoxPrincipal.Name = "HBoxPrincipal";
			this.HBoxPrincipal.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(3));

			// Container child HBoxPrincipal.Gtk.ButtonBox+ButtonBoxChild
			this.buttonAdd = new global::Gtk.Button ();
			this.buttonAdd.CanFocus = true;
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.UseStock = true;
			this.buttonAdd.UseUnderline = true;
			this.buttonAdd.Label = "gtk-add";
			this.HBoxPrincipal.Add (this.buttonAdd);
			global::Gtk.ButtonBox.ButtonBoxChild w5 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.HBoxPrincipal [this.buttonAdd]));
			w5.Expand = false;
			w5.Fill = false;

			// Container child HBoxPrincipal.Gtk.ButtonBox+ButtonBoxChild
			this.buttonSave = new global::Gtk.Button ();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseStock = true;
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = "gtk-save";
			this.HBoxPrincipal.Add (this.buttonSave);
			global::Gtk.ButtonBox.ButtonBoxChild w6 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.HBoxPrincipal [this.buttonSave]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;

			// Container child HBoxPrincipal.Gtk.ButtonBox+ButtonBoxChild
			this.buttonEdit = new global::Gtk.Button ();
			this.buttonEdit.CanFocus = true;
			this.buttonEdit.Name = "buttonEdit";
			this.buttonEdit.UseStock = true;
			this.buttonEdit.UseUnderline = true;
			this.buttonEdit.Label = "gtk-edit";
			this.HBoxPrincipal.Add (this.buttonEdit);
			global::Gtk.ButtonBox.ButtonBoxChild w7 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.HBoxPrincipal [this.buttonEdit]));
			w7.Position = 2;
			w7.Expand = false;
			w7.Fill = false;

			// Container child HBoxPrincipal.Gtk.ButtonBox+ButtonBoxChild
			this.buttonDelete = new global::Gtk.Button ();
			this.buttonDelete.CanFocus = true;
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.UseStock = true;
			this.buttonDelete.UseUnderline = true;
			this.buttonDelete.Label = "gtk-delete";
			this.HBoxPrincipal.Add (this.buttonDelete);
			global::Gtk.ButtonBox.ButtonBoxChild w8 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.HBoxPrincipal [this.buttonDelete]));
			w8.Position = 3;
			w8.Expand = false;
			w8.Fill = false;

			// Container child HBoxPrincipal.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.HBoxPrincipal.Add (this.buttonCancel);
			global::Gtk.ButtonBox.ButtonBoxChild w9 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.HBoxPrincipal [this.buttonCancel]));
			w9.Position = 4;
			w9.Expand = false;
			w9.Fill = false;
			this.vboxAbm.Add (this.HBoxPrincipal);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vboxAbm [this.HBoxPrincipal]));
			w10.Position = 1;
			w10.Expand = false;
			w10.Fill = false;
			this.hpaned.Add (this.vboxAbm);
			w1.Add (this.hpaned);
			this.GtkScrolledWindow.Add (w1);
			this.Add (this.GtkScrolledWindow);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
		#endregion

		private int cacheId;

		private IAbmController<T> controller;

		public IAbmController<T> Controller {
			get {
				return controller;
			}
			set {
				controller = value;
				ClearSearchableTreeView ();
				LoadSearchableTreeView ();
				VerifyPermissions ();
			}
		}

		private GenericAbmWidget specificWidget;

		public GenericAbmWidget SpecificWidget {
			get {
				return specificWidget;
			}
			set {
				specificWidget = value;
			}
		}

		public GenericAbm ()
		{
			this.Build ();

			specificWidget = new GenericAbmWidget ();

			searchabletreeview.ChangeEvent += SearchableTreeViewChangeEvent;
			searchabletreeview.ActivateEvent += SearchableTreeViewActivateEvent;

			buttonAdd.Clicked += ButtonAddClicked;
			buttonCancel.Clicked += ButtonCancelClicked;
			buttonDelete.Clicked += ButtonDeleteClicked;
			buttonEdit.Clicked += ButtonEditClicked;
			buttonSave.Clicked += ButtonSaveHandler;
		}

		void LoadSearchableTreeView ()
		{
			ListStore listStore = new ListStore (typeof (String), typeof (int));
			foreach (IDescriptible descriptible in controller.List) {
				listStore.AppendValues (descriptible.Descripcion, descriptible.Id);
			}
			searchabletreeview.LoadList(listStore);
		}

		void ClearSearchableTreeView ()
		{
			searchabletreeview.LoadList (null);
			searchabletreeview.QueueDraw ();
		}

		void SearchableTreeViewChangeEvent ()
		{
			// DrawSpecificWidget ();
		}

		void SearchableTreeViewActivateEvent ()
		{
			DrawSpecificWidget ();
		}

		void DrawSpecificWidget ()
		{
			vboxWidget.Remove (specificWidget);
			cacheId = searchabletreeview.ActualId;
			LoadInSpecificWidget ();
			specificWidget.Sensitive = false;
			vboxWidget.Add (specificWidget);
			specificWidget.Show ();
		}

		void VerifyPermissions ()
		{
			buttonEdit.Sensitive = controller.CanEdit;
			buttonAdd.Sensitive = controller.CanAdd;
			buttonDelete.Sensitive = controller.CanDelete;

			buttonSave.Sensitive = (buttonEdit.Sensitive || buttonAdd.Sensitive);
			buttonCancel.Sensitive = (buttonEdit.Sensitive || buttonEdit.Sensitive);
		}

		void ButtonSaveHandler (object sender, EventArgs e)
		{
			if (specificWidget.WorkInProgress && !specificWidget.InstanceIsNull) {
				try {
					specificWidget.Save ();
					((Window)this.Toplevel).VentanaMensaje ("Se ha guardado correctamente");
					specificWidget.WorkInProgress = false;
					specificWidget.Sensitive = false;
					specificWidget.Clear ();
				} catch (Exception ex) {
					((Window)this.Toplevel).VentanaError (ex.Message);
				}
			}
		}

		void ButtonEditClicked (object sender, EventArgs e)
		{
			if (!specificWidget.OnInit) {
				specificWidget.Sensitive = true;
			}
		}

		void ButtonDeleteClicked (object sender, EventArgs e)
		{
			if (!specificWidget.OnInit) {
				if (((Window)this.Toplevel).VentanaConfirmacion ("Está seguro?")) {
					try {
						Controller.Remove(Controller.Get (specificWidget.Id));
						specificWidget.Sensitive = false;
						specificWidget.Clear ();
					} catch (Exception ex) {
						((Window)this.Toplevel).VentanaError (ex.Message);
					}
				}
			} 
		}

		void ButtonCancelClicked (object sender, EventArgs e)
		{
			if (specificWidget.WorkInProgress) {
				if (((Window)this.Toplevel).VentanaConfirmacion ("Está seguro?")) {
					if (specificWidget.OnNew) {
						specificWidget.New ();
					} else {
						LoadInSpecificWidget ();
					}
				}
			}
		}

		void ButtonAddClicked (object sender, EventArgs e)
		{
			if (specificWidget.WorkInProgress) {
				((Window)this.Toplevel).VentanaMensaje ("<b>Hay modificaciones sin guardar</b>\n" +
					"Debe guardar o cancelar antes de agregar");
			} else {
				DrawSpecificWidget ();
				specificWidget.Sensitive = true;
				specificWidget.New ();
			}
		}

		void LoadInSpecificWidget ()
		{
			specificWidget.OnNew = false;
			specificWidget.OnInit = false;
			specificWidget.Load(cacheId);
			specificWidget.WorkInProgress = false;
		}

		void NewInSpecificWidget ()
		{
			specificWidget.OnNew = true;
			specificWidget.OnInit = false;
			specificWidget.New ();
			specificWidget.WorkInProgress = false;
		}

		void ClearInSpecificWidget ()
		{
			specificWidget.OnInit = true;
			specificWidget.OnNew = false;
			specificWidget.Clear ();
			specificWidget.WorkInProgress = false;
		}
	}
}

