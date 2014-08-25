//
//  GeneralHelpers.cs
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

using Gtk;
using System;
using System.Collections.Generic;

namespace Hamekoz.UI.Gtk
{
    public class GeneralHelpers
    {
        //FIXME dependent-function
        public static void HabilitarControles (Container widget, bool estado)
        {
            foreach (var control in widget.AllChildren) {
                if (control is Container) {
                    GeneralHelpers.HabilitarControles((Container)control, estado);
                }
				if (control is Entry) {
					((Entry)control).Sensitive = estado;
                }
				if (control is TreeView) {
					((TreeView)control).Sensitive = estado;
				}
				if (control is ComboBox) {
					((ComboBox)control).Sensitive = estado; 
                }
                if (control is TextView) {
                    ((TextView)control).Sensitive = estado;
                }
                if (control is Button) {
					if (((Button)control).Parent.Name != "HBoxPrincipal") {
                        ((Button)control).Sensitive = estado;
                    }
                }
            }           
        }

        //FIXME dependent-function
        public static void SetTextTextview (TextView view, string text)
        {
            TextBuffer buffer;
            buffer = view.Buffer;
            buffer.Text = text;
        }

        //FIXME dependent-function
        public static void SetControlesVacios (Container widget)
        {
            foreach (var control in widget.AllChildren) {
                if (control is Container) {
                    GeneralHelpers.SetControlesVacios ((Container)control);
                }
                if (control is Entry) {
                    ((Entry)control).Text = "";
                }
                if (control is ComboBox) {
                    ((ComboBox)control).Active = 0;
                }
                if (control is ComboBoxEntry) {
                    ((ComboBoxEntry)control).Active = 0;
                }
                if (control is TextView) {
                    SetTextTextview (((TextView)control), "");
                }
            }           
        }

        //FIXME dependent-function
        public static bool VentanaConfirmacion (Window ventana)
        {
            MessageDialog md = new MessageDialog (ventana, 
                DialogFlags.DestroyWithParent,
                MessageType.Question,
                ButtonsType.YesNo,
                "¿Estás Seguro?");

            ResponseType result = (ResponseType)md.Run ();

            if (result == ResponseType.Yes) {
                md.Destroy();
                return true;
            } else {
                md.Destroy ();
                return false;
            }
        }

        //FIXME dependent-function
        public static void VentanaError (Window ventana, string mensaje)
        {
            MessageDialog md = new MessageDialog (ventana,
                DialogFlags.DestroyWithParent,
                MessageType.Error,
                ButtonsType.Ok, 
                mensaje);

            ResponseType result = (ResponseType)md.Run ();

            if (result == ResponseType.Ok)
                md.Destroy ();
            else {
                md.Destroy ();
            }
        }

        //FIXME dependent-function
        public static void VentanaMensaje (Window ventana, string mensaje)
        {
            MessageDialog md = new MessageDialog (ventana,
                DialogFlags.DestroyWithParent,
                MessageType.Info,
                ButtonsType.Ok, 
                mensaje);

            ResponseType result = (ResponseType)md.Run ();

            if (result == ResponseType.Ok)
                md.Destroy ();
            else {
                md.Destroy ();
            }
        }

        /// <summary>
        /// Function to compare strings for search systems.
        /// </summary>
        /// <returns><c>true</c>, if comparison is ok, <c>false</c> otherwise.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="current">Current.</param>
        public static bool SearchCompare (string filter, string current)
        {
			return SearchCompareRecursive (filter, current, 0);
			/* int matches = 0;
            for (int i = 0; i < filter.Length; i++) {
                if (current[i] == filter[i]) {
                    matches++;
                } else {
                    break;
                }
            }

            if (matches == filter.Length) {
                return true;
            } else {
                return false;
            } */
        }

		public static bool SearchCompareRecursive (string filter, string current, int currentPosition)
		{
			if (currentPosition < current.Length && filter.Length <= (current.Length - currentPosition)) {

				current = current.ToLower ();
				filter = filter.ToLower ();

				int matches = 0;
				for (int i = 0; i < filter.Length; i++) {
					if (current[currentPosition] == filter[i]) {
						matches++;
						currentPosition++;
					} else {
						break;
					}
				}

				if (matches == filter.Length) {
					return true;
				} else {
					return SearchCompareRecursive (filter, current, currentPosition+1);
				}

			} else {
				return false;
			}
		}
    }
}

