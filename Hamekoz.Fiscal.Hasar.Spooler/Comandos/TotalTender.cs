//
//  TotalTender.cs
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
	public class TotalTender : Comando
	{
		const string cmd = "D";

		public float Vuelto { get; set; }

		readonly string texto;
		readonly float montoPagado;
		readonly string cancelacionOVuelto;

		public string Comando ()
		{
			return string.Format ("{0}{1}{2}{1}{3:###0.00}{1}{4}{1}{5}", cmd, separador, texto, montoPagado, cancelacionOVuelto, 0).Replace (",", ".");
		}

		public TotalTender (string texto, float montoPagado, string cancelacionOVuelto)
		{
			this.texto = texto;
			this.montoPagado = montoPagado;
			this.cancelacionOVuelto = cancelacionOVuelto;
		}
	}
}

