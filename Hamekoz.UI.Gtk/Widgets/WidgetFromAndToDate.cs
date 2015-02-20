//
//  WidgetFromAndToDate.cs
//
//  Author:
//       Emiliano Gabriel Canedo <emilianocanedo@gmail.com>
//		 Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2015 Hamekoz
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

namespace Hamekoz.UI.Gtk
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class WidgetFromAndToDate : Bin
	{
		public string LabelFrom {
			get {
				return labelFrom.Text;
			}
			set {
				labelFrom.Text = value;
			}
		}

		public string LabelTo {
			get {
				return labelTo.Text;
			}
			set {
				labelTo.Text = value;
			}
		}

		public delegate void DateChangedHandler ();

		public event DateChangedHandler ChangeFrom;

		protected virtual void OnChangeFrom ()
		{
			var handler = ChangeFrom;
			if (handler != null)
				handler ();
		}

		public event DateChangedHandler ChangeTo;

		protected virtual void OnChangeTo ()
		{
			var handler = ChangeTo;
			if (handler != null)
				handler ();
		}

		public DateTime Desde {
			get {
				return datepickerFrom.Date;
			}
			set {
				datepickerFrom.Date = value;
			}
		}

		public DateTime Hasta {
			get {
				return datepickerTo.Date;
			}
			set {
				datepickerTo.Date = value;
			}
		}

		public WidgetFromAndToDate ()
		{
			this.Build ();

			datepickerFrom.DefaultDate = DateTime.Now;
			datepickerTo.DefaultDate = DateTime.Now;

			datepickerFrom.DateChanged += delegate {
				OnChangeFrom ();
			};

			datepickerTo.DateChanged += delegate {
				OnChangeTo ();
			};
		}

		public WidgetFromAndToDate (DateTime desde, DateTime hasta)
		{
			this.Build ();

			datepickerFrom.DefaultDate = desde;
			datepickerTo.DefaultDate = hasta;

			datepickerFrom.DateChanged += delegate {
				OnChangeFrom ();
			};

			datepickerTo.DateChanged += delegate {
				OnChangeTo ();
			};
		}
	}
}

