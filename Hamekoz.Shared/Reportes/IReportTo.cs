//
//  IReportTo.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2016 Hamekoz - www.hamekoz.com.ar
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

namespace Hamekoz.Reportes
{
    public interface IReportToPdf
    {
        /// <summary>
        /// Pdf this instance.
        /// </summary>
        void ToPdf();

        /// <summary>
        /// Pdf the specified filename.
        /// </summary>
        /// <param name="filename">Filename.</param>
        void ToPdf(string filename);
    }

    public interface IReportToXls : IReportToXlsx
    {
        /// <summary>
        /// Xls this instance.
        /// </summary>
        void ToXls();

        /// <summary>
        /// Xls the specified filename.
        /// </summary>
        /// <param name="filename">Filename.</param>
        void ToXls(string filename);
    }

    public interface IReportToXlsx
    {
        /// <summary>
        /// Xlsx this instance.
        /// </summary>
        void ToXlsx();

        /// <summary>
        /// Xlsx the specified filename.
        /// </summary>
        /// <param name="filename">Filename.</param>
        void ToXlsx(string filename);
    }

    public interface IReportToTxt
    {
        /// <summary>
        /// Txt this instance.
        /// </summary>
        void ToTxt();

        /// <summary>
        /// Txt the specified filename.
        /// </summary>
        /// <param name="filename">Filename.</param>
        void ToTxt(string filename);
    }

    public interface IReportToCSV
    {
        /// <summary>
        /// CSV this instance.
        /// </summary>
        void ToCSV();

        /// <summary>
        /// CSV the specified filename.
        /// </summary>
        /// <param name="filename">Filename.</param>
        void ToCSV(string filename);
    }
}