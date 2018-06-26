//
//  ICallBack.cs
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

namespace Hamekoz.Core
{
	public interface ICallBack
	{
		CallBack CallBack { get; }
	}

	public delegate void MessageHandler (string title, string message);
	public delegate bool ConfirmationHandler (string title, string message);

	public class CallBack
	{
		public event MessageHandler Message;

		public void OnMessage (string title, string message)
		{
			var handler = Message;
			if (handler != null)
				handler (title, message);
		}

		public event MessageHandler Warning;

		public void OnWarning (string title, string message)
		{
			var handler = Warning;
			if (handler != null)
				handler (title, message);
		}

		public event MessageHandler Error;

		public void OnError (string title, string message)
		{
			var handler = Error;
			if (handler != null)
				handler (title, message);
		}

		public event ConfirmationHandler Confirmation;

		public bool OnConfirmation (string title, string message)
		{
			var handler = Confirmation;
			return handler == null || handler (title, message);
		}
	}
}

