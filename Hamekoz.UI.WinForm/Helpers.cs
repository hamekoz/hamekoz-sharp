//
//  Helpers.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2016 Hamekoz  - www.hamekoz.com.ar
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Hamekoz.UI.WinForm
{
	public static class Helpers
	{
		/// <summary>
		/// realiza consulta generica para confirmar la eliminacion de un registro
		/// Está seguro de eliminar el registro ?
		/// </summary>
		/// <returns></returns>
		public static bool ConfirmaEliminarRegistro()
		{
			DialogResult result = MessageBox.Show("Está seguro de eliminar el registro ?", "Caption", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			return result == DialogResult.Yes;
		}

        public static void BaseForm(this Form form) {
            form.SuspendLayout();
            form.AutoScaleDimensions = new SizeF(6F, 13F);
            form.AutoScaleMode = AutoScaleMode.Font;
            form.ClientSize = new Size(284, 262);
            form.ForeColor = Color.Navy;
            form.Icon = Icono;
            form.MaximizeBox = false;
            form.ShowInTaskbar = false;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ResumeLayout(false);
        }

        public const string LogoFile = "Logo.png";
        public const string IconoFile = "Icono.ico";
        public const string BannerFile = "Banner.png";
        public const string ImagenesPath = "Imagenes";
        public const string DocumentosPath = "Documentos";

        /// <summary>
        /// Obtiene el icono del sistema
        /// </summary>
        /// <returns></returns>
        public static Icon Icono
        {
            get
            {
                Icon icono = null;
                try
                {
                    string pathIcono = Path.Combine(Application.StartupPath, ImagenesPath, IconoFile);
                    icono = new Icon(pathIcono);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al cargar el icono del modulo {0}", ex);
                }
                return icono;
            }
        }

        /// <summary>
        /// Obtiene el Logo del sistema
        /// </summary>
        /// <returns></returns>
        public static Image Logo
        {
            get
            {
                Image logo = null;
                try
                {
                    string logoPath = Path.Combine(Application.StartupPath, ImagenesPath, LogoFile);
                    logo = Image.FromFile(logoPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al cargar el logo del modulo {0}", ex);
                }
                return logo;
            }
        }

        /// <summary>
        /// Obtiene el Banner del sistema
        /// </summary>
        /// <returns></returns>
        public static Image Banner
        {
            get
            {
                Image banner = null;
                try
                {
                    string bannerPath = Path.Combine(Application.StartupPath, ImagenesPath, BannerFile);
                    banner = Image.FromFile(bannerPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al cargar el banner del modulo {0}", ex);
                }
                return banner;
            }
        }
    }
}
