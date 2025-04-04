//
//  Grupo.cs
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
    public class Grupo : IElemento
    {
        readonly IList<IElemento> contenido = new List<IElemento>();
        readonly IList<Grupo> subgrupos = new List<Grupo>();

        public Parrafo Texto { get; set; }

        public bool Numerado { get; set; }

        public bool SaltarPagina { get; set; }

        public string Titulo { get; set; }

        Section Parent { get; set; }

        public void AgregarSubgrupo(Grupo subgrupo)
        {
            subgrupos.Add(subgrupo);
        }

        public void Agregar(IElemento elemento)
        {
            contenido.Add(elemento);
        }


        #region IElemento implementation

        public IElement GetElemento()
        {
            Section grupo;
            var texto = (Paragraph)Texto.GetElemento();
            if (Parent == null)
            {
                grupo = new ChapterAutoNumber(texto);
            }
            else
            {
                grupo = Parent.AddSection(texto);
            }

            foreach (Grupo subgrupo in subgrupos)
            {
                subgrupo.Parent = grupo;
                subgrupo.GetElemento();
            }

            foreach (IElemento item in contenido)
            {
                grupo.Add(item.GetElemento());
            }

            if (!Numerado)
            {
                grupo.NumberDepth = 0;
            }

            if (Titulo != string.Empty)
            {
                grupo.BookmarkTitle = Titulo;
            }

            grupo.BookmarkOpen = true;
            grupo.TriggerNewPage = SaltarPagina;
            return grupo;
        }

        #endregion
    }
}