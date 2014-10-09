//
//  WidgetAbm.cs
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
using System.Collections.Generic;
using Hamekoz.Interfaces;

namespace Hamekoz.UI.Gtk
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class WidgetAbm : Bin
	{
		/*private IAbmController controller;

		public IAbmController Controller {
			get {
				return controller;
			}
			set {
				controller = value;

				ListStore listStore = new ListStore (typeof (String), typeof (int));
				foreach (IDescriptible descriptible in controller.List) {
					listStore.AppendValues (descriptible.Descripcion, descriptible.Id);
				}
				searchabletreeview.LoadList(listStore);
			}
		}*/

		private Widget specificWidget;

		public Widget SpecificWidget {
			get {
				return specificWidget;
			}
			set {
				specificWidget = value;
			}
		}

		public WidgetAbm ()
		{
			this.Build ();

			buttonAdd.Clicked += ButtonAddClicked;
			buttonCancel.Clicked += ButtonCancelClicked;
			buttonDelete.Clicked += ButtonDeleteClicked;
			buttonEdit.Clicked += ButtonEditClicked;
			buttonSave.Clicked += ButtonSaveHandler;
		}

		void ButtonSaveHandler (object sender, EventArgs e)
		{

		}

		void ButtonEditClicked (object sender, EventArgs e)
		{

		}

		void ButtonDeleteClicked (object sender, EventArgs e)
		{

		}

		void ButtonCancelClicked (object sender, EventArgs e)
		{

		}

		void ButtonAddClicked (object sender, EventArgs e)
		{

		}
	}
}

