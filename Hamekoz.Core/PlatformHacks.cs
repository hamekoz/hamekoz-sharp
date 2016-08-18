//
//  PlatformHacks.cs
//
//  Author:
//       Ezequiel Taranto <ezequiel89@gmail.com>
//
//  Copyright (c) 2016 Hamekoz
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
using System.Text;
using System.Runtime.InteropServices;

namespace Hamekoz.Core
{
	public static class PlatformHacks
	{
		[DllImport ("libc")] // Linux
		private static extern int prctl (int option, byte[] arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5);

		[DllImport ("libc")] // BSD
		private static extern void setproctitle (byte[] fmt, byte[] str_arg);

		public static void SetProcessName (string name)
		{
			if (Environment.OSVersion.Platform != PlatformID.Unix) {
				return;
			}

			try {
				if (prctl (15 /* PR_SET_NAME */, Encoding.ASCII.GetBytes (name + "\0"),
					    IntPtr.Zero, IntPtr.Zero, IntPtr.Zero) != 0) {

					throw new ApplicationException ("Error setting process name: " +
					Mono.Unix.Native.Stdlib.GetLastError ());
				}
			} catch (EntryPointNotFoundException) {

				setproctitle (Encoding.ASCII.GetBytes ("%s\0"),
					Encoding.ASCII.GetBytes (name + "\0"));
			}
		}

		public static void TrySetProcessName (string name)
		{
			try {
				SetProcessName (name);
			} catch {
			}
		}
	}
}