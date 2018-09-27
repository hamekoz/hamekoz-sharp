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

		public new double WidthRequest {
			get {
				return entry.WidthRequest;
			}
			set {
				entry.WidthRequest = value;
			}
		}

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
		} = 1;

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
				number = Math.Round (value, Digits);
				string format = string.Format ("F{0}", Digits);
				entry.Text = number.ToString (format);
				entry.TooltipText = string.Empty;
				CheckRange ();
			}
		}

		bool isIndeterminate;

		public bool IsIndeterminate {
			get { return isIndeterminate; }
			set {
				isIndeterminate = value;
				if (value)
					entry.Text = string.Empty;
				else
					Value = MinimumValue;
			}
		}

		#endregion

		public bool ReadOnly {
			get {
				return entry.ReadOnly;
			}
			set { 
				entry.ReadOnly = value;
				increment.Visible = !value;
				decrement.Visible = !value;
			}
		}

		void ParseNumber ()
		{
			increment.BackgroundColor = Xwt.Drawing.Colors.White;
			decrement.BackgroundColor = Xwt.Drawing.Colors.White;
			TooltipText = string.Empty;
			entry.Text = entry.Text.Replace (".", ",");
			if (!double.TryParse (entry.Text, out number)) {
				if (entry.Text != string.Empty) {
					TooltipText = "El valor ingresado no es numérico";
					increment.BackgroundColor = Xwt.Drawing.Colors.Red;
					decrement.BackgroundColor = Xwt.Drawing.Colors.Red;
				}
			} else {
				if (number < MinimumValue) {
					Value = Math.Round (MinimumValue, Digits);
				} else if (number > MaximumValue) {
					Value = Math.Round (MaximumValue, Digits);
				} else {
					Value = Math.Round (number, Digits);	
				}
				OnValueChanged (null);
			}
			CheckRange ();
		}

		void CheckRange ()
		{
			increment.Sensitive = Value < MaximumValue;
			decrement.Sensitive = Value > MinimumValue;
		}

		public NumberEntry ()
		{
			Name = "NumberEntry";
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
			//TODO evaluar si seria correcto disparar este evento solo cuando el componente tiene sensibilidad
			if (ValueChanged != null)
				ValueChanged (this, e);
		}

		public event EventHandler ValueChanged;

		public new void SetFocus ()
		{
			entry.SetFocus ();
		}
	}
}

