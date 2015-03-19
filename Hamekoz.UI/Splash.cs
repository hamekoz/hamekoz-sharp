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
using Mono.Unix;

namespace Hamekoz.UI
{
	public class Splash : Window
	{
		Thread loadThread;

		VBox box;
		ProgressBar progressBar;
		ImageView imageView;
		Label info;

		public Splash ()
		{
			Decorated = false;
			ShowInTaskbar = false;
			Opacity = 0.5d;
			box = new VBox {
				Margin = -2,
			};
			imageView = new ImageView {
				Image = Image.FromResource (Resources.Splash),
			};
			progressBar = new ProgressBar {
				Indeterminate = true,
				TooltipText = Catalog.GetString ("Loading..."),
			};
			info = new Label {
				Text = Catalog.GetString ("Loading..."),
				TextAlignment = Alignment.Center,
			};
			box.PackStart (imageView);
			box.PackEnd (progressBar);
			box.PackEnd (info);

			Content = box;

			InitialLocation = WindowLocation.CenterScreen;
		}

		public void SetInfo (string text)
		{
			progressBar.TooltipText = text;
			info.Text = text;
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
					Console.WriteLine (Catalog.GetString ("Can not load image, using default splash.\nError: {0}"), ex.Message);
					imageView.Image = Image.FromResource (Resources.Splash);
				}
			}
		}

		public void Run ()
		{
			loadThread = new Thread (new ThreadStart (Preload));
			loadThread.Start ();
			Show ();
		}

		public delegate void PreloadHandler ();

		public PreloadHandler OnPreload;

		protected void Preload ()
		{
			var preload = OnPreload;
			if (preload != null) {
				preload ();
			}
			OnFinish ();
			Close ();
		}

		public delegate void FinishHandler ();

		public FinishHandler OnFinish;

		protected void Finish ()
		{
			var finish = OnFinish;
			if (finish != null) {
				finish ();
			}
		}
	}
}