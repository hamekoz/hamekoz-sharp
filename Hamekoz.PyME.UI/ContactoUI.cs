//
//  ContactoUI.cs
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
using Hamekoz.Negocio;
using Hamekoz.UI;
using Xwt;

namespace Hamekoz.PyME.UI
{
	public class ContactoUI : VBox, IItemUI<Contacto>
	{
		readonly TextEntry nombre = new TextEntry ();
		readonly TextEntry cargo = new TextEntry ();
		readonly TextEntry telefono = new TextEntry ();
		readonly TextEntry email = new TextEntry ();
		readonly TextEntry observaciones = new TextEntry ();

		public Contacto Contacto {
			get { return Item; }
		}

		public ContactoUI ()
		{
			PackStart (new Label ("Nombre y Apellido"));
			PackStart (nombre);
			PackStart (new Label ("Cargo"));
			PackStart (cargo);
			PackStart (new Label ("Teléfono"));
			PackStart (telefono);
			PackStart (new Label ("E-mail"));
			PackStart (email);
			PackStart (new Label ("Observaciones"));
			PackStart (observaciones);

			MinWidth = 400;
		}

		#region IItemUI implementation

		public bool HasItem ()
		{
			return Item != null;
		}

		public void ValuesRefresh ()
		{
			nombre.Text = Contacto.Nombre;
			cargo.Text = Contacto.Cargo;
			telefono.Text = Contacto.Telefono;
			email.Text = Contacto.Email;
			observaciones.Text = Contacto.Observaciones;
		}

		public void ValuesTake ()
		{
			Contacto.Nombre = nombre.Text;
			Contacto.Cargo = cargo.Text;
			Contacto.Telefono = telefono.Text;
			Contacto.Email = email.Text;
			Contacto.Observaciones = observaciones.Text;
		}

		public void ValuesClean ()
		{
			Item = null;
			nombre.Text = string.Empty;
			cargo.Text = string.Empty;
			telefono.Text = string.Empty;
			email.Text = string.Empty;
			observaciones.Text = string.Empty;
		}

		public void Editable (bool editable)
		{
			nombre.ReadOnly = !editable;
			cargo.ReadOnly = !editable;
			telefono.ReadOnly = !editable;
			email.ReadOnly = !editable;
			observaciones.ReadOnly = !editable;
		}

		public Contacto Item {
			get;
			set;
		}

		#endregion
	}
}

