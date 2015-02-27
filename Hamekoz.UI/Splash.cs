//
//  Splash.cs
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
using System.Threading;
using System.Threading.Tasks;

namespace Hamekoz.UI
{
	public class Splash : Window
	{
		delegate void SplashDelegate ();

		VBox box;
		ProgressBar progressBar;
		ImageView imageView;

		public Splash ()
		{
			Decorated = false;
			ShowInTaskbar = false;
			this.Opacity = 0.5d;
			box = new VBox () {
				Margin = -2,
			};
			progressBar = new ProgressBar () {
				Indeterminate = true,
				TooltipText = "Loading...",
			};
			imageView = new ImageView () {
				Image = Image.FromResource (Resources.Splash),
			};
			box.PackStart (imageView);
			box.PackEnd (progressBar);
			Content = box;
		}

		public void SetInfo (string info)
		{
			progressBar.TooltipText = info;
		}

		string splashURI = string.Empty;

		public string SplashURI {
			get {
				return splashURI;
			}
			set {
				try {
					imageView.Image = Image.FromFile (value);
					splashURI = value;
				} catch (Exception ex) {
					Console.WriteLine ("Can not load image, using default splash.\nError: {0}", ex.Message);
					imageView.Image = Image.FromResource (Resources.Splash);
				}
			}
		}

		public event EventHandler Finish;

		public void Run ()
		{
			for (int i = 0; i < 100000; i++) {
				Console.WriteLine (i.ToString ());
			}
			//Finish ();
		}
	}
}

