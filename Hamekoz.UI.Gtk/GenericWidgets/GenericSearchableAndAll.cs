//
//  GenericSearchableAndAll.cs
//
//  Author:
//       Emiliano Gabriel Canedo <emilianocanedo@gmail.com>
//
//  Copyright (c) 2015 ecanedo
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
using Hamekoz.Core;

namespace Hamekoz.UI.Gtk
{
	public class GenericSearchableAndAll<T> : Bin
	{
		#region GTK_GUI

		private global::Gtk.Table table;

		private global::Gtk.DrawingArea drawingarea1;

		private global::Gtk.DrawingArea drawingarea2;

		private global::Gtk.DrawingArea drawingarea3;

		private global::Gtk.DrawingArea drawingarea4;

		private global::Gtk.VBox vbox2;

		private global::Gtk.Label labelTitle;

		private global::Hamekoz.UI.Gtk.SearchableTreeView searchabletreeview;

		private global::Gtk.CheckButton checkbutton;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Hamekoz.UI.Gtk.WidgetSearchableAndAll
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Hamekoz.UI.Gtk.WidgetSearchableAndAll";
			// Container child Hamekoz.UI.Gtk.WidgetSearchableAndAll.Gtk.Container+ContainerChild
			this.table = new global::Gtk.Table (((uint)(3)), ((uint)(3)), false);
			this.table.Name = "table";
			this.table.RowSpacing = ((uint)(6));
			this.table.ColumnSpacing = ((uint)(6));
			// Container child table.Gtk.Table+TableChild
			this.drawingarea1 = new global::Gtk.DrawingArea ();
			this.drawingarea1.Name = "drawingarea1";
			this.table.Add (this.drawingarea1);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table [this.drawingarea1]));
			w1.LeftAttach = ((uint)(1));
			w1.RightAttach = ((uint)(2));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table.Gtk.Table+TableChild
			this.drawingarea2 = new global::Gtk.DrawingArea ();
			this.drawingarea2.Name = "drawingarea2";
			this.table.Add (this.drawingarea2);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table [this.drawingarea2]));
			w2.TopAttach = ((uint)(1));
			w2.BottomAttach = ((uint)(2));
			w2.LeftAttach = ((uint)(2));
			w2.RightAttach = ((uint)(3));
			// Container child table.Gtk.Table+TableChild
			this.drawingarea3 = new global::Gtk.DrawingArea ();
			this.drawingarea3.Name = "drawingarea3";
			this.table.Add (this.drawingarea3);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table [this.drawingarea3]));
			w3.TopAttach = ((uint)(1));
			w3.BottomAttach = ((uint)(2));
			// Container child table.Gtk.Table+TableChild
			this.drawingarea4 = new global::Gtk.DrawingArea ();
			this.drawingarea4.Name = "drawingarea4";
			this.table.Add (this.drawingarea4);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table [this.drawingarea4]));
			w4.TopAttach = ((uint)(2));
			w4.BottomAttach = ((uint)(3));
			w4.LeftAttach = ((uint)(1));
			w4.RightAttach = ((uint)(2));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table.Gtk.Table+TableChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.labelTitle = new global::Gtk.Label ();
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Xalign = 0F;
			this.labelTitle.LabelProp = global::Mono.Unix.Catalog.GetString ("Title");
			this.vbox2.Add (this.labelTitle);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.labelTitle]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.searchabletreeview = new global::Hamekoz.UI.Gtk.SearchableTreeView ();
			this.searchabletreeview.WidthRequest = 350;
			this.searchabletreeview.HeightRequest = 160;
			this.searchabletreeview.Events = ((global::Gdk.EventMask)(256));
			this.searchabletreeview.Name = "searchabletreeview";
			this.searchabletreeview.ActualId = 0;
			this.vbox2.Add (this.searchabletreeview);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.searchabletreeview]));
			w6.Position = 1;
			// Container child vbox2.Gtk.Box+BoxChild
			this.checkbutton = new global::Gtk.CheckButton ();
			this.checkbutton.CanFocus = true;
			this.checkbutton.Name = "checkbutton";
			this.checkbutton.Label = global::Mono.Unix.Catalog.GetString ("Todos");
			this.checkbutton.Active = true;
			this.checkbutton.DrawIndicator = true;
			this.checkbutton.UseUnderline = true;
			this.vbox2.Add (this.checkbutton);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.checkbutton]));
			w7.Position = 2;
			w7.Expand = false;
			w7.Fill = false;
			this.table.Add (this.vbox2);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table [this.vbox2]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.LeftAttach = ((uint)(1));
			w8.RightAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			this.Add (this.table);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}

		#endregion

		public bool CheckVisibility {
			get {
				return checkbutton.Visible;
			}
			set {
				checkbutton.Visible = value;
			}
		}

		public string LabelAll {
			get {
				return checkbutton.Label;
			}
			set {
				checkbutton.Label = value;
			}
		}

		public string LabelTitle {
			get {
				return labelTitle.Text;
			}
			set {
				labelTitle.Text = value;
			}
		}

		static private IAbmController<T> controller;

		public IAbmController<T> Controller {
			get {
				return controller;
			}
			set {
				controller = value;
				searchabletreeview.LoadList (Objects);
			}
		}

		public event Hamekoz.UI.Gtk.ChangedHandler ChangeAll;

		protected virtual void OnChangeAll ()
		{
			var handler = ChangeAll;
			if (handler != null)
				handler ();
		}

		public event ObjectHandler EventGetObject;

		protected virtual void OnEventGetObject (object obj)
		{
			var handler = EventGetObject;
			if (handler != null)
				handler (obj);
		}

		ListStore objects;

		public ListStore Objects {
			get {
				if (objects == null) {
					objects = new ListStore (typeof(String), typeof(int));
					foreach (IDescriptible descriptible in controller.List) {
						objects.AppendValues (descriptible.Descripcion, descriptible.Id);
					}
				}
				return objects;
			}
		}

		public bool All {
			get {
				return checkbutton.Active;
			}
		}

		public GenericSearchableAndAll ()
		{
			this.Build ();

			searchabletreeview.ChangeEvent += delegate {
				checkbutton.Active = false;
				OnEventGetObject ((object)controller.Get (searchabletreeview.ActualId));
			};

			checkbutton.Clicked += delegate {
				OnChangeAll ();
			};
		}
	}
}

