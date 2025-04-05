//
//  Parrafo.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
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
using System.Collections.Generic;

using iTextSharp.text;

namespace Hamekoz.Reportes
{
    public class Parrafo : IElemento
    {
        Paragraph parrafo;

        public string Texto { get; set; }

        public int Indentacion { get; set; }

        public Alineaciones Alineacion { get; set; }

        int size = 10;

        public int Size
        {
            get { return Size; }
            set { size = value; }
        }

        readonly IList<string> fragmentos = new List<string>();

        public void Agregar(string texto)
        {
            fragmentos.Add(texto);
        }

        #region IElemento implementation

        public IElement GetElemento()
        {
            Font fuente = FontFactory.GetFont(FontFactory.HELVETICA, size);
            if (Texto != null)
            {
                parrafo = new Paragraph(Texto, fuente);
            }
            else
            {
                parrafo = new Paragraph("", fuente);
            }
            foreach (string fragmento in fragmentos)
            {
                parrafo.Add(new Chunk(fragmento));
                parrafo.Add(" ");
            }
            parrafo.Alignment = (int)Alineacion;
            parrafo.FirstLineIndent = Indentacion * 20;
            return parrafo;
        }

        #endregion
    }
}