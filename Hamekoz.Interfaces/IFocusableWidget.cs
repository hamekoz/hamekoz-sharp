//
//  IFocusableWidget.cs
//
//  Author:
//       Krzysztof Marecki <marecki.krzysztof@gmail.com>
//		 Emiliano Canedo <emilianocanedo@gmail.com>
//
//	Copyright (c) 2010 Krzysztof Marecki
//  Copyright (c) 2014 Emiliano Canedo
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

namespace Hamekoz.Interfaces
{
	public interface IFocusableWidget
	{
		event EventHandler FocusOut;
	}
}

