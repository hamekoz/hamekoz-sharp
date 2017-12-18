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
using System;
using Hamekoz.Core;
using Humanizer;
using Xwt;

namespace Hamekoz.UI
{
	/// <summary>
	/// Same. Search, Add, Modify, Erase
	/// </summary>
	public abstract class Same<T> : VBox, IPermisible where T : ISearchable
	{
		#region IPermisible implementation

		Permiso permiso = new Permiso ();

		public Permiso Permiso {
			get {
				return permiso;
			}
			set {
				permiso = value;
				Editable (AllowEdit);
			}
		}

		#endregion

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

		public IItemUI<T> Widget {
			get { return scroller.Content as IItemUI<T>; }
			set { scroller.Content = (Widget)value; }
		}

		public IController<T> Controller {
			get;
			set;
		}

		readonly Button buscar = new Button {
			Label = Application.TranslationCatalog.GetString ("_Find"),
			Image = Icons.EditFind.WithSize (IconSize.Medium),
			UseMnemonic = true
		};

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

		readonly Button imprimir = new Button {
			Label = Application.TranslationCatalog.GetString ("_Print"),
			Image = Icons.PrintPreview.WithSize (IconSize.Medium),
			Sensitive = false,
			UseMnemonic = true,
			Visible = false,
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

		public bool PrintVisible {
			get {
				return imprimir.Visible;
			}
			set {
				imprimir.Visible = value;
			}
		}

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
				Widget.ValuesClean ();
				Widget.Item = item;
				Widget.ValuesRefresh ();
				Editable (true);
				isNew = true;
			};

			modificar.Clicked += delegate {
				Editable (Widget.HasItem ());
			};

			eliminar.Clicked += delegate {
				if (Widget.HasItem () && MessageDialog.Confirm (string.Format (Application.TranslationCatalog.GetString ("Are you sure to erase this {0}?"), typeof(T).Name.Humanize ()), Command.Yes)) {
					Controller.Remove (Widget.Item);
					Widget.ValuesClean ();
					Editable (false);
				}
			};

			grabar.Clicked += delegate {
				try {
					Widget.ValuesTake ();
					OnBeforeSave ();
					Controller.Save (Widget.Item);
					Widget.ValuesRefresh ();
					Editable (false);
					OnAfterSave ();
				} catch (ValidationDataException ex) {
					MessageDialog.ShowWarning (Application.TranslationCatalog.GetString ("Data is not valid"), ex.Message);
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

			imprimir.Clicked += (sender, e) => OnPrint ();

			actionBox.PackStart (buscar, true, true);
			actionBox.PackStart (agregar, true, true);
			actionBox.PackStart (modificar, true, true);
			actionBox.PackStart (eliminar, true, true);
			actionBox.PackStart (grabar, true, true);
			actionBox.PackStart (cancelar, true, true);
			actionBox.PackStart (imprimir, true, true);

			PackStart (scroller, true, true);
			PackEnd (actionBox, false, true);
		}

		bool allowEdit;

		public bool AllowEdit {
			get {
				return allowEdit;
			}
			set {
				allowEdit = value;
				Editable (allowEdit);
			}
		}

		public virtual void Editable (bool editable)
		{
			allowEdit = editable;
			buscar.Sensitive = !editable;
			agregar.Sensitive = !editable && Permiso.Agregar;
			modificar.Sensitive = !editable && Permiso.Modificar && Widget.HasItem ();
			eliminar.Sensitive = !editable && Permiso.Eliminar && Widget.HasItem ();
			grabar.Sensitive = editable && Widget.HasItem ();
			cancelar.Sensitive = editable && Widget.HasItem ();
			imprimir.Sensitive = !editable && Widget.HasItem () && Permiso.Imprimir;
			Widget.Editable (editable);
		}

		public event EventHandler BeforeSave;

		void OnBeforeSave ()
		{
			var handler = BeforeSave;
			if (handler != null)
				handler (this, null);
		}

		public event EventHandler AfterSave;

		void OnAfterSave ()
		{
			var handler = AfterSave;
			if (handler != null)
				handler (this, null);
		}

		public event EventHandler Print;

		void OnPrint ()
		{
			var handler = Print;
			if (handler != null)
				handler (this, null);
		}
	}
}

