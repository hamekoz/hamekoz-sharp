//
//  SearchableComboBoxEntry.cs
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
    public class SearchableComboBoxEntry<T>
    {
        private IList<T> cacheList;
        private bool enterState = false;

        public SearchableComboBoxEntry (ref ComboBoxEntry combo, IList<T> list)
        {
            ComboBoxHelpers.LoadByList (combo, list);
            combo.Entry.Activated += new EventHandler (OnComboBoxActivated); 
			//combo.KeyPressEvent += new KeyPressEventHandler (OnComboBoxTabPress);
            combo.GrabNotify += new GrabNotifyHandler (OnComboBoxGrabbed);
            combo.Changed += new EventHandler (OnComboBoxChanged);
            cacheList = list;
        }

        /// <summary>
        /// Dumpea lista filtrada (Accion de boton o PopUp)
        /// </summary>
        /// <param name="o">O.</param>
        /// <param name="args">Arguments.</param>
        private void OnComboBoxGrabbed (object o, GrabNotifyArgs args)
        {
            ComboBoxEntry combo = o as ComboBoxEntry;
            if (!args.WasGrabbed) {
                ComboBoxHelpers.LoadByEntry (combo, cacheList);
            }
        }

		/*
        /// <summary>
        /// Autocompleta en funcion al filtro
        /// </summary>
        /// <param name="o">O.</param>
        /// <param name="args">Arguments.</param>
        private void OnComboBoxTabPress (object o, KeyPressEventArgs args)
        {
            ComboBoxEntry combo = o as ComboBoxEntry;
            if (args.Event.Key == Gdk.Key.Tab) {
                ComboBoxHelpers.LoadByList (combo, cacheList);
                string filter = combo.Entry.Text;
                ComboBoxHelpers.SetByFilter (combo, filter, cacheList);
            }
        }
		*/

        /// <summary>
        /// Suscriptora a evento de cambio general
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnComboBoxChanged (object sender, EventArgs e)
        {
            ComboBoxEntry combo = sender as ComboBoxEntry;
            if (enterState) {
                combo.Popup ();
            }
            enterState = false;
        }

        /// <summary>
        /// Al precionar enter, habilita changed
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnComboBoxActivated (object sender, EventArgs e)
        {
            Entry entry = sender as Entry;
            enterState = true;
            //HACK implementacion con bajo nivel de robustez. Refactorizar
            entry.InsertText ("");
        }
    }
}

