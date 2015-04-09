//
//  Supervisor.cs
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
using System.Collections.Generic;
using Hamekoz.Interfaces;

namespace Hamekoz.UI.Gtk
{
	public class Supervisor : ISupervisor
	{
		#if !MSWindows
		public static List<Notifications.Notification> notificaciones = new List<Notifications.Notification> ();
		#endif

		public static int ContadorThread {
			get {
				#if !MSWindows
				return notificaciones.Count;
				#else
				return 0;
				#endif
			}
		}

		public event SaveEventHandler SaveEvent;

		public void RunSaveEvent ()
		{
			OnSaveEvent ();
		}

		void OnSaveEvent ()
		{
			if (SaveEvent != null)
				SaveEvent ();
		}

		private static Supervisor instance;

		private Supervisor ()
		{
		}

		public static Supervisor Instance {
			get {
				if (instance == null) {
					instance = new Supervisor ();
				}
				return instance;
			}
		}

		private bool workInProgress;

		public bool WorkInProgress {
			get {
				return workInProgress;
			}
			set {
				workInProgress = value;
			}
		}
	}
}

