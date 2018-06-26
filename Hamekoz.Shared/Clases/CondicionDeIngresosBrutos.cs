//
//  CondicionDeIngresosBrutos.cs
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

namespace Hamekoz.Negocio
{
	//TODO renombrar a SituacionDeIngresosBrutos
	public partial class CondicionDeIngresosBrutos
	{
		public TipoDeContribuyenteDeIngresosBrutos Tipo {
			get;
			set;
		}

		public JurisdiccionDeIngresosBrutos Jurisdiccion {
			get;
			set;
		}

		public string Numero {
			get;
			set;
		}

		public CondicionDeIngresosBrutos ()
		{
			//FIX aca no deberia inicializarse objetos
			Tipo = new TipoDeContribuyenteDeIngresosBrutos ();
			Jurisdiccion = new JurisdiccionDeIngresosBrutos ();
		}

		public override string ToString ()
		{
			return Tipo.Descripcion;
		}
	}
}
