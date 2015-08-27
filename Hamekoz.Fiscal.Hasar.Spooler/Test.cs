//
//  Test.cs
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
	public class Test
	{
		readonly IHasar hasar = new Hasar ();

		//hasar.GetConfigurationData();
		//hasar.GetWorkingMemory();
		//hasar.HistoryCapacity();
		//Console.WriteLine(hasar.HistoryCapacity().RegistrosUtilizados);
		//hasar.DailyClose(false);
		//hasar.DailyCloseByDate(new DateTime(2011,11,11),DateTime.Now,"Z");
		//hasar.DailyCloseByNumber(3,100,"T");
		//hasar.GetDailyReport("3","Z");
		//hasar.OpenFiscalReceipt("C");
		//hasar.PrintFiscalText("IMPRIME TEXTO FISCAL");
		//hasar.PrintLineItem("descripcion",2,50,21,"M",0,"T");
		//hasar.LastItemDiscount("descripcion",132,"M","T");
		//hasar.GeneralDiscount("descripcion",10,"M","T");
		//hasar.ReturnRecharge("descripcion",10,21,"M",0,"T","B");
		//hasar.ChargeNonRegisteredTax(10);
		//hasar.Perceptions(21,"Percepciones",10);
		//hasar.Subtotal("descripcion");
		//hasar.TotalTender("descripcion",10,"C");
		//hasar.OpenNonFiscalRecipt();
		//hasar.PrintNonFiscalText("descripciondescripciondescripciondescripciondescripcion");
		//hasar.CloseNonFiscalReceipt();
		//hasar.OpenDNFH("R","12345");
		//hasar.DNFHFarmacias(1);
		//hasar.DNFHReparto(1);
		//hasar.SetVoucherData1("Juan Perez","Visa","C","4545454545454545",new DateTime(2018,11,11),"C",01);
		//hasar.SetVoucherData2(1,1,1,1," ","N",1,"$150",11111111);
		//hasar.PrintVoucher(1);
		//hasar.BarCode(1,8,"N","P");
		//hasar.FeedReceipt(50);
		//hasar.FeedJournal(25);
		//hasar.FeedReceiptJournal(25);
		//hasar.GetDateTime();
		//Console.WriteLine(hasar.GetDateTime().FechaHora);
		//Console.WriteLine(hasar.GetDateTime().Hora);
		//hasar.GetHeaderTrailer(1);
		//hasar.GetFantasyName(2);
		//hasar.GetEmbarkNumber(1);
		//himpresora.statusImpresora("C084"));
		//Console.WriteLine(hasar.StatusRequest().NroUltimoTicket+hasar.StatusRequest().NroUltimoTicketFacturaA);
		//Console.WriteLine(hasar.DailyClose(true).NroUltimoTicketEmitido);
		//Console.WriteLine(hasar.GetInitData().ResponsabilidadIVA);
		//hasar.OpenDrawer();
		//hasar.GetPrinterVersion();
		//hasar.StatusRequest();
		//hasar.TotalTender("Cancela Comprobante",100,"C");

		public void CancelaComprobantes ()
		{
			hasar.Cancel ();
		}

		public void ImprimeFacturaConsumidorFinal ()
		{
			hasar.OpenFiscalReceipt ("T");
			hasar.PrintFiscalText ("IMPRIME TEXTO FISCAL");
			hasar.PrintLineItem ("Rosca de Pascua", 2, 50, 21, "M", 0, "T");
			hasar.PrintLineItem ("Postre Chocolate", 1, 70, 21, "M", 0, "T");
			hasar.PrintLineItem ("Alfajor Frutilla", 3, 8, 21, "M", 0, "T");
			hasar.LastItemDiscount ("descuento ultimo item", 5, "m", "T");
			hasar.ReturnRecharge ("bonificacion", 33, 21, "m", 0, "T", "B");
			hasar.GeneralDiscount ("descuento general", 7, "m", "T");
			hasar.TotalTender ("Efectivo", 300, "T");
			hasar.CloseFiscalReceipt ();
		}

		public void ImprimeFacturaA ()
		{
			hasar.CustomerData ("Nuevo Cliente", "30702383923", "I", "C", "Domicilio 1111");
			hasar.OpenFiscalReceipt ("A");
			hasar.PrintLineItem ("Rosca de Pascua", 2, 50, 21, "M", 0, "T");
			hasar.PrintLineItem ("Postre Chocolate", 1, 70, 21, "M", 0, "T");
			hasar.PrintLineItem ("Alfajor Frutilla", 3, 8, 21, "M", 0, "T");
			hasar.Perceptions (21, "Percepciones", 10);
			hasar.Subtotal ("P");
			hasar.TotalTender ("Efectivo", 300, "T");
			hasar.CloseFiscalReceipt ();
		}

		public void ImprimeNCConsumidorFinal ()
		{
			hasar.CustomerData ("Cliente", "33333333", "C", " ", "Domicilio");
			hasar.SetEmbarkNumber (1, "00001751");
			hasar.OpenDNFH ("S", "");
			hasar.PrintLineItem ("Rosca de Pascua", 2, 50, 21, "M", 0, "T");
			hasar.PrintLineItem ("Postre Chocolate", 1, 70, 21, "M", 0, "T");
			hasar.PrintLineItem ("Alfajor Frutilla", 3, 8, 21, "M", 0, "T");
			hasar.LastItemDiscount ("descuento ultimo item", 5, "m", "T");
			hasar.ReturnRecharge ("bonificacion", 33, 21, "m", 0, "T", "B");
			hasar.GeneralDiscount ("descuento general", 7, "m", "T");
			hasar.CloseDNFH ();
		}

		public void ImprimeNCA ()
		{
			hasar.CustomerData ("Nuevo Cliente", "30702383923", "I", "C", "Domicilio 1111");
			hasar.SetEmbarkNumber (1, "00000082");
			hasar.OpenDNFH ("R", "");
			hasar.PrintLineItem ("Rosca de Pascua", 2, 50, 21, "M", 0, "T");
			hasar.PrintLineItem ("Postre Chocolate", 1, 70, 21, "M", 0, "T");
			hasar.PrintLineItem ("Alfajor Frutilla", 3, 8, 21, "M", 0, "T");
			hasar.CloseDNFH ();
		}

		public void ImprimeComprobanteNoFiscal ()
		{
			hasar.OpenNonFiscalRecipt ();
			hasar.PrintNonFiscalText ("descripciondescripciondescripciondescripciondescripcion");
			hasar.PrintNonFiscalText ("descripciondescripciondescripciondescripciondescripcion");
			hasar.CloseNonFiscalReceipt ();
		}
	}
}