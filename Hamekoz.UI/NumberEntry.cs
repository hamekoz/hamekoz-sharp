//
//  NumberEntry.cs
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
using Xwt;

namespace Hamekoz.UI
{
	public class NumberEntry : HBox
	{
		//TODO definir metodo		ValueChanged

		readonly TextEntry entry = new TextEntry {
			TextAlignment = Alignment.End,
		};

		readonly Button increment = new Button {
			BackgroundColor = Xwt.Drawing.Colors.White,
			CanGetFocus = false,
			Image = Icons.ListAddSymbolic.WithSize (IconSize.Small),
			Style = ButtonStyle.Flat,
		};
		readonly Button decrement = new Button {
			BackgroundColor = Xwt.Drawing.Colors.White,
			CanGetFocus = false,
			Image = Icons.ListRemoveSymbolic.WithSize (IconSize.Small),
			Style = ButtonStyle.Borderless,
		};

		#region SpinButton equivalent properties

		public int Digits {
			get;
			set;
		}

		public double IncrementValue {
			get;
			set;
		}

		public double MaximumValue {
			get;
			set;
		} = double.MaxValue;

		public double MinimumValue {
			get;
			set;
		} = double.MinValue;

		double number;

		public double Value {
			get {
				return number;
			}
			set {
				number = value;
				string format = string.Format ("F{0}", Digits);
				entry.Text = number.ToString (format);
				CheckRange ();
			}
		}

		#endregion

		void ParseNumber ()
		{
			entry.BackgroundColor = Xwt.Drawing.Colors.White;
			entry.TooltipText = string.Empty;
			entry.Text = entry.Text.Replace (".", ",");
			if (!double.TryParse (entry.Text, out number)) {
				entry.BackgroundColor = Xwt.Drawing.Colors.Red;
				entry.TooltipText = "El valor ingresado no se corresponde con un valor numerico";
			} else {
				Value = Math.Round (number, Digits);
				OnValueChanged (null);
			}
			CheckRange ();
			if (number < MinimumValue) {
				entry.BackgroundColor = Xwt.Drawing.Colors.Red;
				entry.TooltipText = string.Format ("El valor debe ser inferior o igual a {0}", MinimumValue);
			}
			if (number > MaximumValue) {
				entry.BackgroundColor = Xwt.Drawing.Colors.Red;
				entry.TooltipText = string.Format ("El valor debe ser superior o igual a {0}", MinimumValue);
			}
		}

		void CheckRange ()
		{
			increment.Sensitive = Value < MaximumValue;
			decrement.Sensitive = Value > MinimumValue;
		}

		public NumberEntry ()
		{
			Spacing = 0;
			PackStart (entry, true, true);
			PackEnd (increment);
			PackEnd (decrement);

			entry.Activated += (sender, e) => ParseNumber ();
			entry.LostFocus += (sender, e) => ParseNumber ();

			increment.Clicked += delegate {
				Value = Value + IncrementValue;
				ParseNumber ();
			};

			decrement.Clicked += delegate {
				Value = Value - IncrementValue;
				ParseNumber ();
			};
		}

		protected virtual void OnValueChanged (EventArgs e)
		{
			if (ValueChanged != null)
				ValueChanged (this, e);
		}

		public EventHandler ValueChanged;
	}
}

