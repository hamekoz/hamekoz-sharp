//
//  IDGI.cs
//
//  Author:
//		 Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//       Ezequiel Taranto <ezequiel89@gmail.com>
//
//  Copyright (c) 2014 Hamekoz - www.hamekoz.com.ar
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

namespace Hamekoz.Fiscal.Hasar.Spooler
{

    /// <summary>
    /// Interface de acceso a comandos para DGI de impresora fiscal
    /// </summary>
    public interface IDGI
    {
        #region Comandos para uso de la DGI

        /// <summary>
        /// Procesador de comandos DGI
        /// </summary>
        void DGICommandProcessor();

        /// <summary>
        /// Reporte auditoria DGI por fechas
        /// </summary>
        void DGIRequestByDate();

        /// <summary>
        /// Reporte auditoria DGI por Z
        /// </summary>
        void DGIRequestByNumber();

        /// <summary>
        /// Comando de baja de controlador fiscal
        /// </summary>
        void KillEmprom();

        #endregion
    }
}