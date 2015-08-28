//
//  Splash.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2014 Hamekoz
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
using System.Drawing;
using System.Windows.Forms;

namespace Hamekoz.UI.WinForm
{
	public partial class Splash : Form
	{
		delegate void CambiarLeyendaDelegate (string leyenda);

		delegate void SplashDelegate ();

		public Splash ()
		{
			InitializeComponent ();
			labelApp.Text = String.Format ("{0} v{1}", Application.ProductName, Application.ProductVersion);
			labelSplash.Parent = pictureBoxSplash;
			labelApp.Parent = pictureBoxSplash;
		}

		/// <summary>
		/// Establece la imagen del splash. Recomendado 450x300 px
		/// </summary>
		public Image Imagen {
			get { return pictureBoxSplash.Image; }
			set { pictureBoxSplash.Image = value; }
		}

		/// <summary>
		/// Carga una imagen desde una ruta a archivo.
		/// </summary>
		public string ImagePath {
			set {
				try {
					Imagen = Image.FromFile (value);
				} catch (Exception ex) {
					Console.WriteLine ("Fallo al carcar la imagen del splash {0}", ex.Message);
				}
			}
		}

		/// <summary>
		/// Cambia la leyenda actual del splash
		/// </summary>
		/// <param name="leyenda">Mesaje a mostrar</param>
		public void CambiarLeyenda (string leyenda)
		{
			if (InvokeRequired) {
				BeginInvoke (new CambiarLeyendaDelegate (CambiarLeyenda), leyenda);
				return;
			}

			labelSplash.Text = leyenda;
		}

		/// <summary>
		/// Cierra el Splash de inicializacion
		/// </summary>
		public void Cerrar ()
		{
			if (InvokeRequired) {
				BeginInvoke (new SplashDelegate (Cerrar));
				return;
			}
			Close ();
		}

		/// <summary>
		/// Muestra en pantalla el splash con la barra de progreso activa
		/// </summary>
		public void Mostrar ()
		{
			if (InvokeRequired) {
				BeginInvoke (new SplashDelegate (Mostrar));
				return;
			}
			Show ();
			Application.Run (this);
		}
	}
}