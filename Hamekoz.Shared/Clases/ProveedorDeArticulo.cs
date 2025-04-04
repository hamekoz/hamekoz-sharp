using System;

using Hamekoz.Core;

namespace Hamekoz.Negocio
{
    public partial class ProveedorDeArticulo : IPersistible, IIdentifiable
    {
        #region IIdentifiable implementation

        public int Id
        {
            get;
            set;
        }

        #endregion

        public ProveedorDeArticulo()
        {
            //HACK no deberia inicializarse el objeto en el constructor
            Proveedor = new Proveedor();
            Articulo = new Articulo();
        }

        public Articulo Articulo
        {
            get;
            set;
        }

        public Proveedor Proveedor
        {
            get;
            set;
        }

        public double Puntaje
        {
            get
            {
                return Proveedor.PuntajeDeEvaluacion;
            }
        }

        public decimal Precio
        {
            get;
            set;
        }

        //TODO convertir a tipo DateTime?
        public DateTime UltimaCompra
        {
            get;
            set;
        } = DateTime.Now.Date;

        public int Demora
        {
            get;
            set;
        }

        bool inactivo;

        public bool Inactivo
        {
            get
            {
                return inactivo || Proveedor?.Estado == Estados.Baja || Articulo?.Estado == Estados.Baja;
            }
            set
            {
                inactivo = value;
            }
        }

        /// <summary>
        /// Gets or sets the codigo.
        /// </summary>
        /// <value>Codigo segun el proveedor.</value>
        public string Codigo
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>Nombre segun el proveedor.</value>
        public string Nombre
        {
            get;
            set;
        } = string.Empty;
    }
}