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
using Humanizer;
using Xwt;

namespace Hamekoz.UI
{
	/// <summary>
	/// Lame. List, Add, Modify, Erase
	/// </summary>
	public abstract class Lame<T> : ItemChooser<T>, IPermisible
	{
		#region IPermisible implementation

		Permiso permiso = new Permiso ();

		public Permiso Permiso {
			get {
				return permiso;
			}
			set {
				permiso = value;
				Editable (false);
			}
		}

		#endregion

		new IItemUI<T> Widget {
			get { return base.Widget as IItemUI<T>; }
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
			Label = Application.TranslationCatalog.GetString ("_Add"),
			Image = Icons.New.WithSize (IconSize.Medium),
			UseMnemonic = true
		};
		readonly Button modificar = new Button {
			Label = Application.TranslationCatalog.GetString ("_Modify"),
			Image = Icons.DocumentProperties.WithSize (IconSize.Medium),
			Sensitive = false,
			UseMnemonic = true
		};
		readonly Button eliminar = new Button {
			Label = Application.TranslationCatalog.GetString ("_Erase"),
			Image = Icons.Delete.WithSize (IconSize.Medium),
			Sensitive = false,
			UseMnemonic = true
				
		};
		readonly Button grabar = new Button {
			Label = Application.TranslationCatalog.GetString ("_Save"),
			Image = Icons.Save.WithSize (IconSize.Medium),
			Sensitive = false,
			UseMnemonic = true
		};
		readonly Button cancelar = new Button {
			Label = Application.TranslationCatalog.GetString ("_Cancel"),
			Image = Icons.ProcessStop.WithSize (IconSize.Medium),
			Sensitive = false,
			UseMnemonic = true
		};

		bool isNew;

		public bool AddVisible {
			get {
				return agregar.Visible;
			}
			set {
				agregar.Visible = value;
			}
		}

		public bool ModifyVisible {
			get {
				return modificar.Visible;
			}
			set {
				modificar.Visible = value;
			}
		}

		public bool EraseVisible {
			get {
				return eliminar.Visible;
			}
			set {
				eliminar.Visible = value;
			}
		}

		protected Lame ()
		{
			SelectedItemChanged += delegate {
				var item = List.SelectedItem;
				if (item != null) {
					Controller.Load (item);
					Widget.Item = item;
					Widget.ValuesRefresh ();
					Editable (false);
					isNew = false;
				}
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
				if (Widget.HasItem () && MessageDialog.Confirm (string.Format (Application.TranslationCatalog.GetString ("Are you sure to erase this {0}?"), typeof(T).Name.Humanize ()), Command.Yes)) {
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
					MessageDialog.ShowWarning (Application.TranslationCatalog.GetString ("Data is not valid"), ex.Message);
				}
			};

			cancelar.Clicked += delegate {
				if (isNew)
					Widget.ValuesClean ();
				else {
					//FIX no deberia tener que recargar el objeto si no se persisten los cambios
					controller.Load (Widget.Item);
					Widget.ValuesRefresh ();
				}
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
			agregar.Sensitive = !editable && Permiso.Agregar;
			modificar.Sensitive = !editable && Permiso.Modificar && Widget.HasItem ();
			eliminar.Sensitive = !editable && Permiso.Eliminar && Widget.HasItem ();
			grabar.Sensitive = editable && Widget.HasItem ();
			cancelar.Sensitive = editable && Widget.HasItem ();
			Widget.Editable (editable);
		}
	}
}

