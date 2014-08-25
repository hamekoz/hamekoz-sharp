using System.Drawing;
using System.Windows.Forms;
using System;

namespace Hamekoz.UI.WinForm
{
    public partial class Splash : Form
    {
        delegate void CambiarLeyendaDelegate(string leyenda);
        delegate void SplashDelegate();

        public Splash()
        {
            InitializeComponent();
            labelApp .Text= String.Format("{0} v{1}", Application.ProductName, Application.ProductVersion);
            labelSplash.Parent = pictureBoxSplash;
            labelApp.Parent = pictureBoxSplash;
        }

        /// <summary>
        /// Establece la imagen del splash. Recomendado 450x300 px
        /// </summary>
        public Image Imagen
        {
            get { return pictureBoxSplash.Image; }
            set { pictureBoxSplash.Image = value; }
        }

        /// <summary>
        /// Carga una imagen desde una ruta a archivo.
        /// </summary>
        public string ImagePath
        {
            set
            {
                try
                {
                    this.Imagen = System.Drawing.Image.FromFile(value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fallo al carcar la imagen del splash", ex.Message);
                }
            }
        }
         
        /// <summary>
        /// Cambia la leyenda actual del splash
        /// </summary>
        /// <param name="leyenda">Mesaje a mostrar</param>
        public void CambiarLeyenda(string leyenda)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new CambiarLeyendaDelegate(CambiarLeyenda), leyenda);
                return;
            }

            labelSplash.Text = leyenda;
        }

        /// <summary>
        /// Cierra el Splash de inicializacion
        /// </summary>
        public void Cerrar()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new SplashDelegate(Cerrar));
                return;
            }
            this.Close();        
        }

        /// <summary>
        /// Muestra en pantalla el splash con la barra de progreso activa
        /// </summary>
        public void Mostrar()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new SplashDelegate(Mostrar));
                return;
            }
            this.Show();
            Application.Run(this);
        }
    }
}