//
//  Lame.cs
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
using Xwt;

//TODO Internacionalizar textos
namespace Hamekoz.UI
{
	public interface ILameItem<T>
	{
		bool HasItem ();

		T Item { get; set; }

		void ValuesRefresh ();

		void ValuesTake ();

		void ValuesClean ();

		void Editable (bool editable);
	}

	/// <summary>
	/// Lame. List, Add, Modify, Erase
	/// </summary>
	public abstract class Lame<T> : ItemChooser<T>
	{
		new ILameItem<T> Widget {
			get { return base.Widget as ILameItem<T>; }
			set { base.Widget = (Widget)value; }
		}

		IController<T> controller;

		public IController<T> Controller {
			get {
				return controller;
			}
			set {
				controller = value;
				List.List = controller.List;
			}
		}

		readonly Button agregar = new Button {
			Label = "Agregar",
			Image = Icons.New.WithSize (IconSize.Medium),
		};
		readonly Button modificar = new Button {
			Label = "Modificar",
			Sensitive = false,
			Image = Icons.Edit.WithSize (IconSize.Medium),
		};
		readonly Button eliminar = new Button {
			Label = "Eliminar",
			Sensitive = false,
			Image = Icons.Delete.WithSize (IconSize.Medium),
		};
		readonly Button grabar = new Button {
			Label = "Grabar",
			Sensitive = false,
			Image = Icons.Save.WithSize (IconSize.Medium),
		};
		readonly Button cancelar = new Button {
			Label = "Cancelar",
			Sensitive = false,
			Image = Icons.ProcessStop.WithSize (IconSize.Medium),
		};

		bool isNew;

		protected Lame ()
		{
			SelectedItemChanged += delegate {
				var item = List.SelectedItem;
				Controller.Load (item);
				Widget.Item = item;
				Widget.ValuesRefresh ();
				Editable (false);
				isNew = false;
			};

			agregar.Clicked += delegate {
				var item = Controller.Create ();
				Widget.Item = item;
				Widget.ValuesRefresh ();
				List.UnselectAll ();
				Editable (true);
				isNew = true;
			};

			modificar.Clicked += delegate {
				Editable (Widget.HasItem ());
			};

			eliminar.Clicked += delegate {
				if (Widget.HasItem () && MessageDialog.Confirm ("¿Está seguro que quiere eliminar?", Command.Yes)) {
					Controller.Remove (Widget.Item);
					List.List.Remove (Widget.Item);
					List.Refresh ();
					Widget.ValuesClean ();
					Editable (false);
				}
			};

			grabar.Clicked += delegate {
				try {
					Widget.ValuesTake ();
					Controller.Save (Widget.Item);
					List.List = Controller.List;
					List.Refresh ();
					List.SelectedItem = Widget.Item;
					Editable (false);
				} catch (ValidationDataException ex) {
					MessageDialog.ShowWarning ("Datos no validos", ex.Message);
				}
			};

			cancelar.Clicked += delegate {
				if (isNew)
					Widget.ValuesClean ();
				else
					Widget.ValuesRefresh ();
				Editable (false);
			};

			AddAction (agregar);
			AddAction (modificar);
			AddAction (eliminar);
			AddAction (grabar);
			AddAction (cancelar);
		}

		public void Editable (bool editable)
		{
			List.Sensitive = !editable;
			agregar.Sensitive = !editable;
			modificar.Sensitive = !editable && Widget.HasItem ();
			eliminar.Sensitive = !editable && Widget.HasItem ();
			grabar.Sensitive = editable && Widget.HasItem ();
			cancelar.Sensitive = editable && Widget.HasItem ();
			Widget.Editable (editable);
		}
	}
}

