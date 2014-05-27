//
//  Texto.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2010 Hamekoz
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

using iTextSharp.text;

namespace Hamekoz.Reportes
{
	public class Texto : IElemento
	{
		Paragraph parrafo;

		public Texto (string texto)
		{
			parrafo = new Paragraph (texto);
		}

		public Texto (int indentacion, params string[] texto)
		{
			parrafo = new Paragraph ();
			parrafo.FirstLineIndent = indentacion * 20;
			foreach (string fragmento in texto) {
				parrafo.Add (new Chunk(fragmento));
				parrafo.Add(" ");
			}
		}

        public Texto(Alineaciones alineacion, params string[] texto)
        {
            parrafo = new Paragraph();
            parrafo.Alignment = (int)alineacion;
            foreach (string fragmento in texto)
            {
                parrafo.Add(new Chunk(fragmento));
                parrafo.Add(" ");
            }
        }


		#region IElemento implementation
		public IElement GetElemento ()
		{
			return parrafo;
		}
		#endregion
	}
}

