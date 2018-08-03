//
//  MD5Helper.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2018 Hamekoz - www.hamekoz.com.ar
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
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Hamekoz.Core
{
	public static class MD5Extensions
	{
		public static string HashMD5 (this string archivo)
		{
			var fs = new FileStream (archivo, FileMode.Open, FileAccess.Read);
			var hash = new MD5CryptoServiceProvider ();
			Int64 currentPos = fs.Position;
			fs.Seek (0, SeekOrigin.Begin);
			var sb = new StringBuilder ();
			foreach (Byte b in hash.ComputeHash(fs))
				sb.Append (b.ToString ("X2"));
			fs.Close ();
			return sb.ToString ();
		}
	}
}

