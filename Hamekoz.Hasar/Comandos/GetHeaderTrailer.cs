//
//  GetHeaderTrailer.cs
//
//  Author:
//       Ezequiel Taranto <ezequiel89@gmail.com>
//
//  Copyright (c) 2014 etaranto
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

namespace Hamekoz.Hasar
{
	public class GetHeaderTrailer : Comando
	{
		const string cmd = "^";

		public string texto { get; set; }

		int NroLineaEncabezamiento;

		public string Comando ()
		{
			return string.Format ("{0}{1}{2}", cmd, separador, NroLineaEncabezamiento);
		}

		public GetHeaderTrailer (int NroLineaEncabezamiento)
		{
			this.NroLineaEncabezamiento = NroLineaEncabezamiento;
		}
	}
}

