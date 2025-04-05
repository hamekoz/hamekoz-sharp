//
//  LoginTicketResponse.cs
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
using System.Xml;

namespace Hamekoz.Argentina.Afip
{
    public class LoginTicketResponse
    {
        public long cuit;
        public string source;
        public string destination;
        public string uniqueId;
        public string generationTime;

        public DateTime GenerationTime
        {
            get
            {
                return DateTime.Parse(generationTime);
            }
        }

        public string expirationTime;

        public DateTime ExpirationTime
        {
            get
            {
                return DateTime.Parse(expirationTime);
            }
        }

        public string token;
        public string sign;

        public LoginTicketResponse(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var element = doc.DocumentElement;
            source = element.SelectSingleNode("/loginTicketResponse/header/source").InnerText;
            destination = element.SelectSingleNode("/loginTicketResponse/header/destination").InnerText;
            uniqueId = element.SelectSingleNode("/loginTicketResponse/header/uniqueId").InnerText;
            generationTime = element.SelectSingleNode("/loginTicketResponse/header/generationTime").InnerText;
            expirationTime = element.SelectSingleNode("/loginTicketResponse/header/expirationTime").InnerText;
            sign = element.SelectSingleNode("/loginTicketResponse/credentials/sign").InnerText;
            token = element.SelectSingleNode("/loginTicketResponse/credentials/token").InnerText;
            //FIXME en produccion la linea destination tiene un formato distinto a homologacion hay que ver como contemplar para obtener bien el cuit
            //cuit = long.Parse (destination.Replace ("SERIALNUMBER=CUIT ", "").Remove (destination.IndexOf (',')));
        }
    }
}