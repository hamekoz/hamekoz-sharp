//
//  Welcome.cs
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
using System.Reflection;
using Mono.Unix;

namespace Hamekoz.UI
{
	public class Welcome : VBox
	{
		readonly ImageView logo;
		Label primaryInfo;
		Label secondaryInfo;
		Label errorInfo;

		public Welcome ()
		{
			Assembly assembly = Assembly.GetEntryAssembly ();
			AssemblyName assemblyName = assembly.GetName ();
			Version version = assemblyName.Version;

			logo = new ImageView {
				Image = Image.FromResource (Resources.Logo),
			};

			primaryInfo = new Label {
				Text = string.Format (Catalog.GetString ("Welcome to {0}"), assemblyName.Name),
				TextAlignment = Alignment.Center,
				Font = Font.WithSize (15),
				MarginTop = 10,
			};
			secondaryInfo = new Label {
				Text = string.Format (Catalog.GetString ("Powered by {0}"), "Hamekoz"),
				TextAlignment = Alignment.Center,
				Font = Font.WithSize (11),
			};
			var versionLabel = new Label {
				Text = string.Format (Catalog.GetString ("Version {0}"), version),
				TextAlignment = Alignment.Center,
				MarginTop = 3,
			};
			errorInfo = new Label {
				TextColor = new Color (1, 0, 0),
				TextAlignment = Alignment.Center,
				MarginTop = 10,
			};
			PackStart (logo);
			PackStart (primaryInfo);
			PackStart (secondaryInfo);
			PackStart (versionLabel);
			PackStart (errorInfo);

			VerticalPlacement = WidgetPlacement.Center;
			HorizontalPlacement = WidgetPlacement.Center;
			ExpandHorizontal = true;
			ExpandVertical = true;
		}

		public string PrimaryInfo {
			get {
				return primaryInfo.Text;
			}
			set {
				primaryInfo.Text = value;
			}
		}

		public string SecondaryInfo {
			get {
				return secondaryInfo.Text;
			}
			set {
				secondaryInfo.Text = value;
			}
		}

		public string ErrorInfo {
			get {
				return errorInfo.Text;
			}
			set {
				errorInfo.Text = value;
			}
		}

		string logoURI = string.Empty;

		public string LogoURI {
			get {
				return logoURI;
			}
			set {
				try {
					logo.Image = Image.FromFile (value);
					logoURI = value;
				} catch (Exception ex) {
					Console.WriteLine (Catalog.GetString ("Can not load image, using default logo.\nError: {0}"), ex.Message);
					logo.Image = Image.FromResource (Resources.Logo);
				}
			}
		}
	}
}

