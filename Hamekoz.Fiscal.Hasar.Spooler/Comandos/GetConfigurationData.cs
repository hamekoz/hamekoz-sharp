//
//  GetConfigurationData.cs
//
//  Author:
//       Ezequiel Taranto <ezequiel89@gmail.com>
//
//  Copyright (c) 2014 Hamekoz
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
	public class GetConfigurationData
	{
		const string cmd = "f";

		public float LimiteDatos { get; set; }

		public float LImiteTicket { get; set; }

		public string Cambio { get; set; }

		public string Leyendas { get; set; }

		public string TipoDeCorte { get; set; }

		public string Comando ()
		{
			return cmd;
		}
	}
}

