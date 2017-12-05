//
//  App.Internal.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2017 Hamekoz - www.hamekoz.com.ar
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
using System.Runtime.InteropServices;
using Xwt;

namespace Hamekoz.PyME.UI.Desktop
{
	public static partial class App
	{
		static ToolkitType toolkitWindows = ToolkitType.Wpf;
		static ToolkitType toolkitLinux = ToolkitType.Gtk;
		static ToolkitType toolkitMacOS = ToolkitType.XamMac;

		public static ToolkitType ToolkitWindows {
			get { return toolkitWindows; }
			set { toolkitWindows = value; }
		}

		public static ToolkitType ToolkitLinux {
			get { return toolkitLinux; }
			set { toolkitLinux = value; }
		}

		public static ToolkitType ToolkitMacOS {
			get { return toolkitMacOS; }
			set { toolkitMacOS = value; }
		}

		public static void Run (ToolkitType toolkit, string[] args)
		{
			Application.Initialize (toolkit);

			OnRun (args);

			Application.Run ();

			OnExit ();
			Application.Dispose ();
		}

		public static void Run (string[] args)
		{
			ToolkitType? toolkit = null;
			foreach (var arg in args) {
				if (arg.ToLower ().StartsWith ("toolkittype", StringComparison.Ordinal) && arg.Contains ("=")) {
					var tkname = arg.Split ('=') [1].Trim ();
					try {
						toolkit = (ToolkitType?)Enum.Parse (typeof(ToolkitType), tkname, true);
					} catch (ArgumentException ex) {
						throw new ArgumentException ("Unknown Xwt Toolkit type: " + tkname, ex);
					}
				}
			}

			Run (toolkit ?? DetectPlatformToolkit (), args);
		}

		static ToolkitType DetectPlatformToolkit ()
		{
			switch (Environment.OSVersion.Platform) {
			case PlatformID.MacOSX:
				return ToolkitMacOS;
			case PlatformID.Unix:
				if (IsRunningOnMac ())
					return ToolkitMacOS;
				return ToolkitLinux;
			default:
				return ToolkitWindows;
			}
		}

		// from Xwt.GtkBackend.Platform
		static bool IsRunningOnMac ()
		{
			IntPtr buf = IntPtr.Zero;
			try {
				buf = Marshal.AllocHGlobal (8192);
				// This is a hacktastic way of getting sysname from uname ()
				if (uname (buf) == 0) {
					string os = Marshal.PtrToStringAnsi (buf);
					if (os.ToUpper () == "DARWIN")
						return true;
				}
				// Analysis disable once EmptyGeneralCatchClause
			} catch {
			} finally {
				if (buf != IntPtr.Zero)
					Marshal.FreeHGlobal (buf);
			}
			return false;
		}

		[DllImport ("libc")]
		static extern int uname (IntPtr buf);
	}
}


