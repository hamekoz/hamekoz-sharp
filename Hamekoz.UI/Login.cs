//
//  Login.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
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
using Xwt;
using Xwt.Drawing;
using Mono.Unix;

namespace Hamekoz.UI
{
	public class Login : Dialog
	{
		readonly TextEntry userEntry;
		readonly PasswordEntry passwordEntry;
		readonly Label info;

		public Login ()
		{
			var table = new Table ();
			var image = new ImageView {
				Image = Icons.UserInfo.WithBoxSize (96),
			};
			userEntry = new TextEntry {
				PlaceholderText = Catalog.GetString ("User"),
			};
			passwordEntry = new PasswordEntry {
				PlaceholderText = Catalog.GetString ("Password"),
			};
			var userLabel = new Label {
				Text = Catalog.GetString ("User"),
				TextAlignment = Alignment.Center,
			};
			var passwordLabel = new Label {
				Text = Catalog.GetString ("Password"),
				TextAlignment = Alignment.Center,
			};
			info = new Label {
				TextAlignment = Alignment.Center,
				TextColor = new Color (1, 0, 0),
				Visible = false,
			};
			userEntry.Activated += delegate {
				passwordEntry.SetFocus ();
			};
			passwordEntry.Activated += OnAutenticate;

			table.Add (image, 0, 0, 4);
			table.Add (userLabel, 1, 0);
			table.Add (userEntry, 1, 1);
			table.Add (passwordLabel, 1, 2);
			table.Add (passwordEntry, 1, 3);
			table.Add (info, 0, 4, colspan: 2);

			Content = table;

			userEntry.SetFocus ();
			Resizable = false;
			ShowInTaskbar = false;
			Title = Catalog.GetString ("Login");
			Icon = Image.FromResource (GetType (), Resources.Icon);
		}

		public delegate void AutenticateHandler (string user, string password);

		public event AutenticateHandler Autenticate;

		protected virtual void OnAutenticate (object sender, EventArgs e)
		{
			var handler = Autenticate;
			if (handler != null) {
				try {
					handler (userEntry.Text, passwordEntry.Password);
					Respond (Command.Ok);
					Close ();
				} catch (Exception ex) {
					passwordEntry.Password = string.Empty;
					info.Text = ex.Message;
					info.Visible = true;
				}
			}
		}
	}
}