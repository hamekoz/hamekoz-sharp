//
//  GenericTreeViewPickerAndAll.cs
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
	public class GenericTreeViewPickerAndAll<T> : Bin
	{
		#region GTK_GUI

		private global::Gtk.Table table1;

		private global::Gtk.DrawingArea drawingarea1;

		private global::Gtk.DrawingArea drawingarea2;

		private global::Gtk.DrawingArea drawingarea3;

		private global::Gtk.DrawingArea drawingarea4;

		private global::Gtk.VBox vbox2;

		private global::Gtk.Label labelTitle;

		private global::Hamekoz.UI.Gtk.TreeViewPicker treeviewpicker;

		private global::Gtk.CheckButton checkbutton;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Hamekoz.UI.Gtk.WidgetTreeViewPickerAndAll
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Hamekoz.UI.Gtk.WidgetTreeViewPickerAndAll";
			// Container child Hamekoz.UI.Gtk.WidgetTreeViewPickerAndAll.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table (((uint)(3)), ((uint)(3)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.drawingarea1 = new global::Gtk.DrawingArea ();
			this.drawingarea1.Name = "drawingarea1";
			this.table1.Add (this.drawingarea1);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1 [this.drawingarea1]));
			w1.LeftAttach = ((uint)(1));
			w1.RightAttach = ((uint)(2));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.drawingarea2 = new global::Gtk.DrawingArea ();
			this.drawingarea2.Name = "drawingarea2";
			this.table1.Add (this.drawingarea2);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.drawingarea2]));
			w2.TopAttach = ((uint)(1));
			w2.BottomAttach = ((uint)(2));
			w2.LeftAttach = ((uint)(2));
			w2.RightAttach = ((uint)(3));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.drawingarea3 = new global::Gtk.DrawingArea ();
			this.drawingarea3.Name = "drawingarea3";
			this.table1.Add (this.drawingarea3);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1 [this.drawingarea3]));
			w3.TopAttach = ((uint)(1));
			w3.BottomAttach = ((uint)(2));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.drawingarea4 = new global::Gtk.DrawingArea ();
			this.drawingarea4.Name = "drawingarea4";
			this.table1.Add (this.drawingarea4);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1 [this.drawingarea4]));
			w4.TopAttach = ((uint)(2));
			w4.BottomAttach = ((uint)(3));
			w4.LeftAttach = ((uint)(1));
			w4.RightAttach = ((uint)(2));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
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
			this.treeviewpicker = new global::Hamekoz.UI.Gtk.TreeViewPicker ();
			this.treeviewpicker.Events = ((global::Gdk.EventMask)(256));
			this.treeviewpicker.Name = "treeviewpicker";
			this.treeviewpicker.ActualId = 0;
			this.vbox2.Add (this.treeviewpicker);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.treeviewpicker]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
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
			w7.PackType = ((global::Gtk.PackType)(1));
			w7.Position = 2;
			w7.Expand = false;
			w7.Fill = false;
			this.table1.Add (this.vbox2);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1 [this.vbox2]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.LeftAttach = ((uint)(1));
			w8.RightAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			this.Add (this.table1);
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
				treeviewpicker.LoadList (Objects);
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

		public GenericTreeViewPickerAndAll ()
		{
			this.Build ();

			treeviewpicker.ChangeEvent += delegate {
				checkbutton.Active = false;
				OnEventGetObject ((object)controller.Get (treeviewpicker.ActualId));
			};

			checkbutton.Clicked += delegate {
				OnChangeAll ();
			};
		}
	}
}

