//
//  ComprobanteItemUI.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2018 Hamekoz - www.hamekoz.com.ar
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
using Hamekoz.Fiscal;
using Hamekoz.Negocio;
using Hamekoz.UI;
using Humanizer;
using Xwt;

namespace Hamekoz.PyME.UI
{
	public class ComprobanteItemUI : VBox, IItemUI<ComprobanteItem>
	{
		#region IItemUI implementation

		public bool HasItem ()
		{
			return Item != null;
		}

		public void ValuesRefresh ()
		{
			descripcion.TextEntry.Text = Item.Descripcion;
			cantidad.Value = (double)Item.Cantidad;
			precio.Value = (double)Item.Precio;
			neto.Value = (double)Item.Neto;
			iva.SelectedItem = Item.Iva;
			precioConIVA.Active = Item.PrecioConIVA;
			if (iva.SelectedItem == null)
				iva.SelectedIndex = 0;
		}

		public void ValuesTake ()
		{
			Item.Descripcion = descripcion.TextEntry.Text;
			Item.Iva = (IVA)iva.SelectedItem;
			Item.Cantidad = (decimal)cantidad.Value;
			Item.Precio = (decimal)precio.Value;
			Item.PrecioConIVA = precioConIVA.Active;
		}

		public void ValuesClean ()
		{
			descripcion.TextEntry.Text = string.Empty;
			cantidad.Value = 0;
			precio.Value = 0;
			neto.Value = 0;
			iva.SelectedIndex = 0;
			precioConIVA.Active = false;
		}

		public void Editable (bool editable)
		{
			descripcion.Sensitive = editable;
			cantidad.Sensitive = editable;
			precio.Sensitive = editable;
		}

		public ComprobanteItem Item {
			get;
			set;
		}

		#endregion

		string[] descripciones = { };

		public string[] Descripciones {
			get {
				return descripciones;
			}
			set {
				descripciones = value;
				descripcion.TextEntry.SetCompletions (descripciones);
				descripcion.Items.Clear ();
				foreach (var concepto in descripciones)
					descripcion.Items.Add (concepto);
			}
		}

		readonly ComboBoxEntry descripcion = new ComboBoxEntry {
			MinWidth = 300
		};

		readonly NumberEntry cantidad = new NumberEntry {
			Digits = 2,
			MinimumValue = 0,
			MaximumValue = double.MaxValue,
		};

		readonly NumberEntry precio = new NumberEntry {
			Digits = 2,
			MinimumValue = 0,
			MaximumValue = double.MaxValue,
		};
		readonly ComboBox iva = new ComboBox ();
		readonly NumberEntry neto = new NumberEntry {
			Digits = 2,
			MinimumValue = 0,
			MaximumValue = double.MaxValue,
			Sensitive = false
		};

		readonly CheckBox precioConIVA = new CheckBox {
			Label = "El precio incluye IVA",
			Sensitive = false
		};

		public void IVAs (string letra = "")
		{
			iva.Items.Clear ();
			switch (letra) {
			case "E":
				iva.Items.Add (IVA.NoGravado, IVA.NoGravado.Humanize ());
				break;
			default:
				iva.Items.Add (IVA.Veintiuno, IVA.Veintiuno.Humanize ());
				iva.Items.Add (IVA.Exento, IVA.Exento.Humanize ());
				iva.Items.Add (IVA.DiezCinco, IVA.DiezCinco.Humanize ());
				iva.Items.Add (IVA.Veintisiete, IVA.Veintisiete.Humanize ());
				iva.Items.Add (IVA.DosCinco, IVA.DosCinco.Humanize ());
				iva.Items.Add (IVA.Cero, IVA.Cero.Humanize ());
				iva.Items.Add (IVA.NoGravado, IVA.NoGravado.Humanize ());
				break;
			}
		}

		public ComprobanteItemUI ()
		{
			IVAs ();

			PackStart (new Label ("Descripción"));
			PackStart (descripcion);
			PackStart (new Label ("IVA"));
			PackStart (iva);
			PackStart (new Label ("Cantidad"));
			PackStart (cantidad);
			PackStart (new Label ("Precio"));
			PackStart (precio);
			PackStart (precioConIVA);
			PackStart (new Label ("Neto"));
			PackStart (neto);

			cantidad.ValueChanged += delegate {
				neto.Value = cantidad.Value * precio.Value;
			};
			precio.ValueChanged += delegate {
				neto.Value = cantidad.Value * precio.Value;
			};
		}
	}
}

