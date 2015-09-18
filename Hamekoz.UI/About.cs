//
//  About.cs
//
//  Author:
//       Juan Angel Dinamarca <juan.angel.dinamarca@gmail.com>
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
using Mono.Unix;
using Xwt;
using Xwt.Drawing;
using System.Reflection;

namespace Hamekoz.UI
{
	public class About : Dialog
	{
		//FIXME textarea's readonly property allows to modify the paragraph

		readonly ImageView logo;
		Label companyName;
		Label copyRight;

		HPaned panel = new HPaned {
			HeightRequest = 90,
		};
		ToggleButton credits = new ToggleButton {
			Label = Catalog.GetString ("Credits"),
			Image = Icons.Starred.WithSize (IconSize.Small),
		};
		ToggleButton license = new ToggleButton {
			Label = Catalog.GetString ("License"),
		};
		Button close = new Button {
			Label = Catalog.GetString ("Close"),
			Image = Icons.Delete.WithSize (IconSize.Small),
		};

		TextArea creditsText = new TextArea {
			Text = string.Format (Catalog.GetString ("Written by {0}"),	"\n" + "Claudio Rodrigo Pereyra Diaz \n" + "Ezequiel Carlos Taranto \n" + "Emiliano Gabriel Canedo \n" + "Juan Angel Dinamarca \n" + ""),
			ReadOnly = true,
			TextAlignment = Alignment.Center,			
		};

		ScrollView licenseScroll = new ScrollView ();
		ScrollView creditsScroll = new ScrollView ();

		TextArea licenseText = new TextArea {
			Text = "GNU Lesser General Public License v3.0\n\n" +
			"This program is free software: you can redistribute it and/or modify\n" +
			"it under the terms of the GNU Lesser General Public License as published by\n" +
			"the Free Software Foundation, either version 3 of the License, or\n" +
			"(at your option) any later version.\n\n" +
			"This program is distributed in the hope that it will be useful,\n" +
			"but WITHOUT ANY WARRANTY; without even the implied warranty of\n" +
			"MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the\n" +
			"GNU Lesser General Public License for more details.\n\n" +
			"You should have received a copy of the GNU Lesser General Public License\n" +
			"along with this program.  If not, see <http://www.gnu.org/licenses/>.\n" +
			"",
			ReadOnly = true,
		};

		public About ()
		{
			logo = new ImageView {
				Image = Image.FromResource (Resources.Logo),
			};

			companyName = new Label {
				Text = "Hamekoz",
				TextAlignment = Alignment.Center,
				MarginTop = 10,
			};

			copyRight = new Label {
				Text = "Copyright: Hamekoz",
				TextAlignment = Alignment.Center,
				MarginTop = 10,
			};

			var aboutVBox = new VBox ();
			var aboutHBox = new HBox ();

			aboutHBox.PackStart (credits, true);
			aboutHBox.PackStart (license, true);
			aboutHBox.PackStart (close, true);

			creditsScroll.Content = creditsText;
			licenseScroll.Content = licenseText;
			panel.Panel1.Content = copyRight;
			aboutVBox.PackStart (logo);
			aboutVBox.PackStart (companyName);
			aboutVBox.PackStart (panel);
			aboutVBox.PackStart (aboutHBox);

			credits.Clicked += delegate {
				if (credits.Active) {
					panel.Panel1.Content = creditsScroll;
					license.Active = !credits.Active;
				}
				BothButtons ();
			};

			license.Clicked += delegate {
				if (license.Active) {
					panel.Panel1.Content = licenseScroll;
					credits.Active = !license.Active;
				}
				BothButtons ();
			};

			close.Clicked += delegate {
				Close ();
			};
		
			Icon = Image.FromResource (Resources.Icon); 
			Content = aboutVBox;
			Width = 300;
		}

		public void BothButtons ()
		{
			if (!credits.Active & !license.Active) {
				panel.Panel1.Content = copyRight;
			}
		}

		public string CompanyName {
			get {
				return companyName.Text;
			}
			set {
				companyName.Text = value;
			}
		}

		public string CopyRight {
			get {
				return copyRight.Text;
			}
			set {
				copyRight.Text = value;
			}
		}

		public Image Logo {
			get {
				return logo.Image;
			}
			set {
				logo.Image = value;
			}
		}
	}
}

