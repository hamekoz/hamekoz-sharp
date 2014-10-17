//
//  GenericAbmWidget.cs
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
using Hamekoz.Interfaces;

namespace Hamekoz.UI.Gtk
{
	public class GenericAbmWidget : Bin
	{ 
		protected bool onInit = true;
		public bool OnInit {
			get { return onInit; }
			set { onInit = value; }
		}

		protected bool onNew = false;
		public bool OnNew {
			get { return onNew; }
			set { onNew = value; }
		}

		protected int id;
		public int Id {
			get { return id; }
			set { id = value; }
		}

		protected bool objectInstance;
		public virtual bool ObjectInstance {
			get { return objectInstance; }
			set { objectInstance = value; }
		}

		public virtual void Load(int id) {}
		public virtual void Save() {}
		public virtual void New() {}
	}
}

