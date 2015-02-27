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

namespace Hamekoz.UI
{
	public class Login : Dialog
	{
		Label userLabel;
		TextEntry userEntry;
		Label passwordLabel;
		PasswordEntry passwordEntry;
		Label info;

		public Login ()
		{
			var box = new VBox ();
			userEntry = new TextEntry () { 
				PlaceholderText = "User",
			};
			passwordEntry = new PasswordEntry () { 
				PlaceholderText = "Password",
			};
			userLabel = new Label () {
				Text = "User",
			};
			passwordLabel = new Label () {
				Text = "Password",
			};
			info = new Label () {
				TextColor = new Color (1, 0, 0),
			};
			userEntry.Activated += delegate {
				passwordEntry.SetFocus ();
			};
			box.PackStart (userLabel);
			box.PackStart (userEntry);
			box.PackStart (passwordLabel);
			box.PackStart (passwordEntry);
			passwordEntry.Activated += OnAutenticate;
			box.PackStart (info);
			box.VerticalPlacement = WidgetPlacement.Center;
			box.HorizontalPlacement = WidgetPlacement.Center;
			box.ExpandHorizontal = false;
			box.ExpandVertical = false;
			Content = box;
			userEntry.SetFocus ();
			Resizable = false;
			Opacity = 0.5d;
			ShowInTaskbar = false;
			Title = "Login";
			Icon = Image.FromResource (GetType (), Resources.Icon);
		}

		public string UserLabel {
			get { 
				return userLabel.Text;
			}
			set {
				userLabel.Text = value;
				userEntry.PlaceholderText = value;
			}
		}

		public string PasswordLabel {
			get { 
				return passwordLabel.Text;
			}
			set {
				passwordLabel.Text = value;
				passwordEntry.PlaceholderText = value;
			}
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
				}
			}
		}
	}
}