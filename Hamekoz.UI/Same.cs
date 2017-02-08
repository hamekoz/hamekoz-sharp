//
//  Same.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2016 Hamekoz - www.hamekoz.com.ar
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
using Hamekoz.Core;
using Humanizer;
using Mono.Unix;
using Xwt;

namespace Hamekoz.UI
{
	/// <summary>
	/// Same. Search, Add, Modify, Erase
	/// </summary>
	public abstract class Same<T> : VBox where T : ISearchable
	{
		readonly protected HBox actionBox = new HBox {
			ExpandHorizontal = true,
			ExpandVertical = true,
			HorizontalPlacement = WidgetPlacement.Fill,
			VerticalPlacement = WidgetPlacement.Fill,
		};

		readonly ScrollView scroller = new ScrollView {
			BorderVisible = false,
			VerticalScrollPolicy = ScrollPolicy.Automatic,
			HorizontalScrollPolicy = ScrollPolicy.Automatic,
		};

		public IItemUI<T>  Widget {
			get { return scroller.Content as IItemUI<T>; }
			set { scroller.Content = (Widget)value; }
		}

		public IController<T> Controller {
			get;
			set;
		}

		readonly Button buscar = new Button {
			Label = Catalog.GetString ("Search"),
			Image = Icons.EditFind.WithSize (IconSize.Medium),
		};

		readonly Button agregar = new Button {
			Label = Catalog.GetString ("Add"),
			Image = Icons.New.WithSize (IconSize.Medium),
		};
		readonly Button modificar = new Button {
			Label = Catalog.GetString ("Modify"),
			Sensitive = false,
			Image = Icons.Edit.WithSize (IconSize.Medium),
		};
		readonly Button eliminar = new Button {
			Label = Catalog.GetString ("Erase"),
			Sensitive = false,
			Image = Icons.Delete.WithSize (IconSize.Medium),
		};
		readonly Button grabar = new Button {
			Label = Catalog.GetString ("Save"),
			Sensitive = false,
			Image = Icons.Save.WithSize (IconSize.Medium),
		};
		readonly Button cancelar = new Button {
			Label = Catalog.GetString ("Cancel"),
			Sensitive = false,
			Image = Icons.ProcessStop.WithSize (IconSize.Medium),
		};

		bool isNew;

		public ISearchDialog<T> Dialogo {
			get;
			set;
		}

		protected Same ()
		{
			buscar.Clicked += delegate {
				Dialogo.Refresh ();
				var r = Dialogo.Run (ParentWindow);
				if (r == Command.Ok) {
					if (Dialogo.SelectedItem == null) {
						return;
					}
					var item = Dialogo.SelectedItem;
					Controller.Load (item);
					Widget.Item = item;
					Widget.ValuesRefresh ();
					Editable (false);
					isNew = false;
				}
				Dialogo.Hide ();
			};

			agregar.Clicked += delegate {
				var item = Controller.Create ();
				Widget.Item = item;
				Widget.ValuesRefresh ();
				Editable (true);
				isNew = true;
			};

			modificar.Clicked += delegate {
				Editable (Widget.HasItem ());
			};

			eliminar.Clicked += delegate {
				if (Widget.HasItem () && MessageDialog.Confirm (string.Format (Catalog.GetString ("Are you sure to erase this {0}?"), typeof(T).Name.Humanize ()), Command.Yes)) {
					Controller.Remove (Widget.Item);
					Widget.ValuesClean ();
					Editable (false);
				}
			};

			grabar.Clicked += delegate {
				try {
					Widget.ValuesTake ();
					Controller.Save (Widget.Item);
					Widget.ValuesRefresh ();
					Editable (false);
				} catch (ValidationDataException ex) {
					MessageDialog.ShowWarning (Catalog.GetString ("Data is not valid"), ex.Message);
				}
			};

			cancelar.Clicked += delegate {
				if (isNew)
					Widget.ValuesClean ();
				else {
					//FIX no deberia tener que recargar el objeto si no se persisten los cambios
					Controller.Load (Widget.Item);
					Widget.ValuesRefresh ();
				}
				Editable (false);
			};

			actionBox.PackStart (buscar, true, true);
			actionBox.PackStart (agregar, true, true);
			actionBox.PackStart (modificar, true, true);
			actionBox.PackStart (eliminar, true, true);
			actionBox.PackStart (grabar, true, true);
			actionBox.PackStart (cancelar, true, true);

			PackStart (scroller, true, true);
			PackEnd (actionBox, false, true);
		}

		public void Editable (bool editable)
		{
			buscar.Sensitive = !editable;
			agregar.Sensitive = !editable;
			modificar.Sensitive = !editable && Widget.HasItem ();
			eliminar.Sensitive = !editable && Widget.HasItem ();
			grabar.Sensitive = editable && Widget.HasItem ();
			cancelar.Sensitive = editable && Widget.HasItem ();
			Widget.Editable (editable);
		}
	}
}

