//
//  IVAItemUI.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2017 Hamekoz - www.hamekoz.com.ar
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
	public class IVAItemUI : VBox, IItemUI<IVAItem>
	{
		protected ComboBox iva = new ComboBox ();

		readonly NumberEntry neto = new NumberEntry {
			Digits = 2,
			IncrementValue = 0.01,
			MaximumValue = double.MaxValue,
			MinimumValue = 0,
			Value = 0
		};

		readonly NumberEntry importe = new NumberEntry {
			Digits = 2,
			IncrementValue = 0.01,
			MaximumValue = double.MaxValue,
			MinimumValue = 0,
			Value = 0
		};

		void CalularImporte ()
		{
			if (iva.SelectedItem != null) {
				importe.Value = neto.Value * (double)((IVA)iva.SelectedItem).Alicuota () / 100;	
			} else {
				importe.Value = 0;
			}
		}

		public IVAItemUI ()
		{
			foreach (IVA item in Enum.GetValues(typeof(IVA)))
				iva.Items.Add (item, item.Humanize ());

			PackStart (new Label ("IVA"));
			PackStart (iva);
			PackStart (new Label ("Neto"));
			PackStart (neto);
			PackStart (new Label ("Importe"));
			PackStart (importe);

			iva.SelectedItem = IVA.Veintiuno;

			iva.SelectionChanged += (sender, e) => CalularImporte ();
			neto.ValueChanged += (sender, e) => CalularImporte ();
			neto.LostFocus += (sender, e) => CalularImporte ();
		}

		#region IItemUI implementation

		public bool HasItem ()
		{
			return Item != null;
		}

		public void ValuesRefresh ()
		{
			iva.SelectedItem = Item.IVA;
			neto.Value = (double)Item.Neto;
			importe.Value = (double)Item.Importe;
		}

		public void ValuesTake ()
		{
			if (HasItem ()) {
				Item.IVA = (IVA)iva.SelectedItem;
				Item.Neto = (decimal)neto.Value;
				Item.Importe = (decimal)importe.Value;
			}
		}

		public void ValuesClean ()
		{
			iva.SelectedItem = IVA.Veintiuno;
			neto.Value = 0;
			importe.Value = 0;
		}

		public void Editable (bool editable)
		{
			iva.Sensitive = editable;
			neto.Sensitive = editable;
		}

		public IVAItem Item {
			get;
			set;
		}

		#endregion
	}
}
