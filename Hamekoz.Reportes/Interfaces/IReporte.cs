//
//  IReporte.cs
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
namespace Hamekoz.Reportes
{
	public interface IReporte
	{
		string FileName { get; set; }

		string Titulo { get; set; }

		string Asunto { get; set; }

		string Autor { get; set; }

		string Creador { get; set; }

		string Empresa { get; set; }

		bool Apaisado { get; set; }

		bool HasEncabezadoPieDePagina { get; set; }

		bool HasTituloPrimerPagina { get; set; }

		bool HasAsuntoPrimerPagina { get; set; }

		string MarcaDeAguaImagenUri { get; set; }

		string MarcaDeAguaTexto { get; set; }

		float MarcaDeAguaTransparencia { get; set; }

		float MargenSuperior { get; set; }

		float MargenInferior { get; set; }

		float MargenDerecho { get; set; }

		float MargenIzquierdo { get; set; }

		void Agregar (IElemento elemento);

		void Iniciar ();

		void Abrir ();

		void NuevaPagina ();

		void NuevaLineaDivisoria ();
	}
}