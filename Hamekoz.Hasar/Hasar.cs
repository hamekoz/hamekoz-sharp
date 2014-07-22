//
//  Hasar.cs
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
using System.Text; //para usar Encoding.ASCII

namespace Hamekoz.Hasar
{
	public class Hasar : IHasar
	{
		public Hasar ()
		{
			impresora.Conectar("127.0.0.1",1600);
		}

		string separador = ((char)28).ToString();
		Impresora impresora = new Impresora();

		public string strreverse(string text)
		{
			if (text == null) return null;
			char[] array = text.ToCharArray();
			Array.Reverse(array);
			return new String(array);
		}

		public void statusImpresora(string respuesta){
			string[] error_impresora = new string[16]; 
			error_impresora[0] = "";
			error_impresora[1] = "";
			error_impresora[2] = "Error de impresora. ";
			error_impresora[3] = "Impresora OffLine. ";
			error_impresora[4] = "Falta papel del diario. ";
			error_impresora[5] = "Falta papel de tickets. ";
			error_impresora[6] = "Buffer de impresora lleno. ";
			error_impresora[7] = ""; //Buffer de impresora vacio"
			error_impresora[8] = "";
			error_impresora[9] = ""; //'Siempre 0
			error_impresora[10] = ""; //'Siempre 0
			error_impresora[11] = ""; //'Siempre 0
			error_impresora[12] = ""; //'Siempre 0
			error_impresora[13] = ""; //'Siempre 0
			error_impresora[14] = ""; //"Cajón de dinero cerrado o ausente. "
			error_impresora[15] = ""; 
							
			string respuestaBit = Convert.ToString(Convert.ToInt16(respuesta, 16), 2);	
			respuestaBit = strreverse(respuestaBit);						
			for (int i = 0; i<respuestaBit.Length; i++){							
				if (respuestaBit[i].ToString() == "1" && error_impresora[i] != ""){
					//if (respuestaBit[i].ToString() == "1"){
					string mensaje = string.Format("Error de Impresora {0}: {1}",i,error_impresora[i]);
					Console.WriteLine(mensaje);
					//throw new Exception(mensaje);
					///Console.WriteLine(Convert.ToString(Convert.ToInt16(arrayRespuesta[0], 16), 2));
					//Console.WriteLine(respuestaBit);
					}
			}
		}

		public void statusFiscal(string respuesta){

			string[] error_fiscal = new string[16];
			error_fiscal[0] = "Error en chequeo de memoria fiscal. ";
			error_fiscal[1] = "Error en chequeo de la memoria de trabajo. ";
			error_fiscal[2] = "Carga de bateria baja. ";
			error_fiscal[3] = "Comando desconocido. ";
			error_fiscal[4] = "Datos no validos de un campo. ";
			error_fiscal[5] = "Comando no valido para el estado fiscal actual. ";
			error_fiscal[6] = "Desborde del total. ";
			error_fiscal[7] = "Memoria fiscal llena. ";
			error_fiscal[8] = "Memoria fiscal a punto de llenarse. ";
			error_fiscal[9] = "";
			error_fiscal[10] = "";
			error_fiscal[11] = "Error en el ingreso de fecha. ";
			error_fiscal[12] = "Recibo fiscal abierto. ";
			error_fiscal[13] = "Recibo abierto. ";
			error_fiscal[14] = "Factura abierta. ";
			error_fiscal[15] = "";

			string respuestaBit = Convert.ToString(Convert.ToInt16(respuesta, 16), 2);
			respuestaBit = strreverse(respuestaBit);				
			for (int i = 0; i<respuestaBit.Length; i++){			
				if (respuestaBit[i].ToString() == "1" && error_fiscal[i] != ""){
				//if (respuestaBit[i].ToString() == "1"){
					string mensaje = string.Format("Error Fiscal {0}: {1}",i,error_fiscal[i]);
					Console.WriteLine(mensaje);
					//throw new Exception(mensaje);
				}
			}
		}

		#region IHasar implementation

		public StatusRequest StatusRequest ()
		{
			StatusRequest c = new StatusRequest();
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.NroUltimoTicket = arrayRespuesta[2];
			c.NroUltimoTicketFacturaA = arrayRespuesta[4];
			return c;
		}

		public void StatRPN ()
		{
			throw new NotImplementedException ();
		}

		public GetConfigurationData GetConfigurationData ()
		{
			GetConfigurationData c = new GetConfigurationData();
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.LimiteDatos = float.Parse(arrayRespuesta[2]);
			c.LImiteTicket = float.Parse(arrayRespuesta[3]);
			c.Cambio = arrayRespuesta[6];
			c.Leyendas = arrayRespuesta[7];
			c.TipoDeCorte = arrayRespuesta[8];
			return c;
		}

		public void GetGeneralConfigurationData ()
		{
			throw new NotImplementedException ();
		}

		public GetInitData GetInitData ()
		{
			GetInitData c = new GetInitData();
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.CUIT = double.Parse(arrayRespuesta[2]);
			c.RazonSocial = arrayRespuesta[3];
			c.NroRegistro = arrayRespuesta[4];
			c.FechaDeInicializacion = DateTime.Parse(arrayRespuesta[5]);
			c.NroPV = int.Parse(arrayRespuesta[6]);
			c.InicioDeActividades = DateTime.Parse(arrayRespuesta[7]);
			c.NroIngresosBrutos = arrayRespuesta[8];
			c.ResponsabilidadIVA = arrayRespuesta[9];
			return c;
		}

		public void GetPrinterVersion ()
		{
			Encoding enc = Encoding.GetEncoding(437);
			string comando = enc.GetString(new byte[]{127});
			//string comando = string.Format(Encoding.ASCII.GetString(new byte[]{127}));
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public HistoryCapacity HistoryCapacity ()
		{
			HistoryCapacity c = new HistoryCapacity();
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.RegistrosUtilizados = int.Parse(arrayRespuesta[3]);
			return c;
		}

		public DailyClose DailyClose (bool zeta)
		{
			DailyClose c = new DailyClose(zeta);
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.NroDeInforme = int.Parse(arrayRespuesta[2]);
			c.CantidadDocFiscalesCancelados = int.Parse(arrayRespuesta[3]);
			c.CantidadDocNoFiscalesHomologados = int.Parse(arrayRespuesta[4]);
			c.CantidadDocNoFiscales = int.Parse(arrayRespuesta[5]);
			c.CantidadDocFiscalesEmitidos = int.Parse(arrayRespuesta[6]);
			c.NroUltimoTicketEmitido = arrayRespuesta[8];
			c.NroUltimoTicketFacturaA = arrayRespuesta[9];
			c.MontoVendidoDocFiscales = float.Parse(arrayRespuesta[10]);
			c.MontoIVA = float.Parse(arrayRespuesta[11]);
			c.MontoImpuestosInternos = float.Parse(arrayRespuesta[13]);
			c.MontoPercepciones = float.Parse(arrayRespuesta[14]);
			c.NroUltimoTicketNotaCreditoBC = arrayRespuesta[15];
			c.NroUltimoTicketNotaCreditoA = arrayRespuesta[16];
			c.MontoCreditoEnTicketsNC = float.Parse(arrayRespuesta[17]);
			c.MontoIVANC = float.Parse(arrayRespuesta[18]);
			c.MontoImpuestosInternosNC = float.Parse(arrayRespuesta[19]);
			c.MontoPersepcionesTicketNC = float.Parse(arrayRespuesta[20]);
			return c;
		}

		public void DailyCloseByDate (DateTime fechaInicio, DateTime fechaFinal, string tipoDatos)
		{
			string comando = string.Format(":{0}{1:yyMMdd}{0}{2:yyMMdd}{0}{3}",separador,fechaInicio,fechaFinal,tipoDatos); 
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void DailyCloseByNumber (float nroZInicial, float nroZFinal, string tipo)
		{
			string comando = string.Format(";{0}{1}{0}{2}{0}{3}",separador,nroZInicial,nroZFinal,tipo); 
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public GetDailyReport GetDailyReport (string nroZ, string calificadorZ)
		{
			GetDailyReport c = new GetDailyReport(nroZ,calificadorZ);
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.FechaCierre = arrayRespuesta[2];
			c.NroZ = int.Parse(arrayRespuesta[3]);
			c.NroUltimoTicket = int.Parse(arrayRespuesta[4]);
			c.NroUltimoFacturaA = int.Parse(arrayRespuesta[5]);
			c.MontoVendido = float.Parse(arrayRespuesta[6]);
			c.IVA = float.Parse(arrayRespuesta[7]);
			c.ImpuestosInternos = float.Parse(arrayRespuesta[8]);
			c.MontoPercepciones = float.Parse(arrayRespuesta[9]);
			c.NroUltimoNC = int.Parse(arrayRespuesta[11]);
			c.NroUltimoNCA = int.Parse(arrayRespuesta[12]);
			c.MontoCreditoNC = float.Parse(arrayRespuesta[13]);
			c.MontoIVANC = float.Parse(arrayRespuesta[14]);
			c.MontoImpuestosInternosNC = float.Parse(arrayRespuesta[15]);
			c.MontoPercepcionesNC = float.Parse(arrayRespuesta[16]);
			return c;
		}

		public GetWorkingMemory GetWorkingMemory ()
		{
			GetWorkingMemory c = new GetWorkingMemory();
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.CantidadTicketsCancelados = int.Parse(arrayRespuesta[2]);
			c.CantidadDocNoFiscalesEmitidos = int.Parse(arrayRespuesta[3]);
			c.CantidadDocFiscalesEmitidos = int.Parse(arrayRespuesta[4]);
			c.NroUltimoTicketEmitido = int.Parse(arrayRespuesta[5]);
			c.NroUltimoDocFiscalAEmitido = int.Parse(arrayRespuesta[6]);
			c.MontoVentasEnDocFiscales = float.Parse(arrayRespuesta[7]);
			c.MontoIVAEnDocFicales = float.Parse(arrayRespuesta[8]);
			c.MontoImpuestosInternosEnDocFiscales = float.Parse(arrayRespuesta[9]);
			c.MontoPercepcionesEnDocFiscales = float.Parse(arrayRespuesta[10]);
			c.NroUltimoTicketNCEmitido = int.Parse(arrayRespuesta[12]);
			c.NroUltimoTIcketNCAEmitido = int.Parse(arrayRespuesta[13]);
			c.MontoCreditoNC = float.Parse(arrayRespuesta[14]);
			c.MontoIVANC = float.Parse(arrayRespuesta[15]);
			c.MontoImpuestosInternosNC = float.Parse(arrayRespuesta[16]);
			c.MontoPercepcionesNC = float.Parse(arrayRespuesta[17]);
			c.CantidadNCCanceladas = int.Parse(arrayRespuesta[20]);
			return c;
		}

		public SendFirstIVA SendFirstIVA ()
		{
			SendFirstIVA c = new SendFirstIVA();
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.NroRegistro = int.Parse(arrayRespuesta[2]);
			c.AlicuotaIVA = float.Parse(arrayRespuesta[3]);
			c.MontoIVA = float.Parse(arrayRespuesta[4]);
			c.MontoImpuestosInternos = float.Parse(arrayRespuesta[5]);
			c.VentaNeta = float.Parse(arrayRespuesta[7]);
			return c;
		}

		public void NextIVATransmission ()
		{
			string comando = string.Format("q");
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			//TODO Implementar respuestas
		}

		public void GetLastExecutionError ()
		{
			throw new NotImplementedException ();
		}

		public void GetFirstLogBlock ()
		{
			throw new NotImplementedException ();
		}

		public void GetNextLogBlock ()
		{
			throw new NotImplementedException ();
		}

		public void GetAuditFirstBlock (DateTime fechaZInicial, DateTime fechaZFinal, string califZ, string compresion, string juntaJornadasXML)
		{
			throw new NotImplementedException ();
		}

		public void GetAuditNextBlock ()
		{
			throw new NotImplementedException ();
		}

		public void DefineErasableZRange (float nroZ)
		{
			throw new NotImplementedException ();
		}

		public void GetErasableZRange (float nroZ)
		{
			throw new NotImplementedException ();
		}

		public void GetDocumentFirstBlock (float nroInicial, float nroFinal, string califTipoDocumento, string compresion, string juntaJornadasXML)
		{
			throw new NotImplementedException ();
		}

		public void GetDocumentNextBlock ()
		{
			throw new NotImplementedException ();
		}

		public void OpenFiscalReceipt (string tipoDocumento)
		{
			string comando = string.Format("@{0}{1}{0}{2}",separador,tipoDocumento,"T");
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void PrintFiscalText (string texto)
		{
			string comando = string.Format("A{0}{1}{0}{2}",separador,texto,0);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void PrintLineItem (string texto, float cantidad, float monto, float IVA, string imputacion, float coeficiente, string califMonto)
		{
			texto = texto.Length > 20 ? texto.Substring(0,20) : texto;
			string comando = string.Format("B{0}{1}{0}{2:###0.00}{0}{3:###0.00}{0}{4:###0.00}{0}{5}{0}{6:###0.00}{0}{7}{0}{8}",separador,texto,cantidad,monto,IVA,imputacion,coeficiente,0,califMonto).Replace(",",".");
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void LastItemDiscount (string texto, float monto, string imputacion, string califMonto)
		{
			texto = texto.Length > 20 ? texto.Substring(0,20) : texto;
			string comando = string.Format("U{0}{1}{0}{2:###0.00}{0}{3}{0}{4}{0}{5}",separador,texto,monto,imputacion,0,califMonto).Replace(",",".");
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void GeneralDiscount (string texto, float monto, string imputacion, string califMonto)
		{
			texto = texto.Length > 20 ? texto.Substring(0,20) : texto;
			string comando = string.Format("T{0}{1}{0}{2:###0.00}{0}{3}{0}{4}{0}{5}",separador,texto,monto,imputacion,0,califMonto).Replace(",",".");
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void ReturnRecharge (string texto, float monto, float IVA, string imputacion, float coefImp, string total, string califOper)
		{
			texto = texto.Length > 20 ? texto.Substring(0,20) : texto;
			string comando = string.Format("m{0}{1}{0}{2:###0.00}{0}{3:###0.00}{0}{4}{0}{5:###0.00}{0}{6}{0}{7}{0}{8}",separador,texto,monto,IVA,imputacion,coefImp,0,total,califOper).Replace(",",".");
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void ChargeNonRegisteredTax (float monto)
		{
			string comando = string.Format("a{0}{1:###0.00}",separador,monto).Replace(",",".");
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);		
		}

		public void Perceptions (float IVA, string texto, float monto)
		{
			texto = texto.Length > 20 ? texto.Substring(0,20) : texto;
			string comando = string.Format("`{0}{1:###0.00}{0}{2}{0}{3:###0.00}",separador,IVA,texto,monto).Replace(",",".");
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public Subtotal Subtotal (string impresion)
		{
			Subtotal c = new Subtotal(impresion);
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.CantidadItemsVendidos = float.Parse(arrayRespuesta[2]);
			c.MontoVentas = float.Parse(arrayRespuesta[3]);
			c.MontoIVA = float.Parse(arrayRespuesta[4]);
			c.MontoPagado = float.Parse(arrayRespuesta[5]);
			c.MontoImpuestosInternos = float.Parse(arrayRespuesta[7]);
			return c;

		}

		public TotalTender TotalTender (string texto, float monto, string vuelto)
		{
			TotalTender c = new TotalTender(texto,monto,vuelto);
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.Vuelto = float.Parse(arrayRespuesta[2]);
			return c;
		}

		public CloseFiscalReceipt CloseFiscalReceipt ()
		{
			CloseFiscalReceipt c = new CloseFiscalReceipt();
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.NroComprobanteFiscalEmitido = int.Parse(arrayRespuesta[2]);
			return c;
		}

		public void OpenNonFiscalRecipt ()
		{
			string comando = string.Format("H");
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void OpenNonFiscalSlip ()
		{
			string comando = string.Format("G");
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void PrintNonFiscalText (string texto)
		{
			texto = texto.Length > 40 ? texto.Substring(0,40) : texto;
			string comando = string.Format("I{0}{1}{0}{2}",separador,texto,0);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void CloseNonFiscalReceipt ()
		{
			string comando = string.Format("J");
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public OpenDNFH OpenDNFH (string tipoDocumento, string identificacionNroDocumento)
		{
			OpenDNFH c = new OpenDNFH(tipoDocumento,identificacionNroDocumento);
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.NroDNFHAbierto = int.Parse(arrayRespuesta[2]);
			return c;
		}

		public void PrintDNFHInfo (string texto, float parametroDisplay, string cantidadUnidades)
		{
			throw new NotImplementedException ();
		}

		public void PrintSignDNFH (string firmaAclaracionOtrasLeyendas)
		{
			throw new NotImplementedException ();
		}

		public void ReceiptText (string texto)
		{
			throw new NotImplementedException ();
		}

		public CloseDNFH CloseDNFH ()
		{
			CloseDNFH c = new CloseDNFH();
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.NroDNFHEmitido = int.Parse(arrayRespuesta[2]);
			return c; 
		}

		public void DNFHFarmacias (int cantidadEjemplares)
		{
			string comando = string.Format("h{0}{1}",separador,cantidadEjemplares);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void DNFHReparto (int cantidadEjemplares)
		{
			string comando = string.Format("i{0}{1}",separador,cantidadEjemplares);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void SetVoucherData1 (string nombreCliente, string nombreTarjetaCredito, string califOperacion, string nroTarjeta, DateTime fechaVencimientoTarjeta, string tipoTarjetaUsada, float cantidadCuotas)
		{
			string comando = string.Format("j{0}{1}{0}{2}{0}{3}{0}{4}{0}{5:yyMM}{0}{6}{0}{7}",separador,nombreCliente,nombreTarjetaCredito,califOperacion,nroTarjeta,fechaVencimientoTarjeta,tipoTarjetaUsada,cantidadCuotas);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void SetVoucherData2 (float codigoComercio, float nroTerminal, float nroLote, float nroCupon, string ingresoDatosTarjeta, string tipoOperacion, float nroAutorizacion, string importe, float nroComprobanteFiscal)
		{
			string comando = string.Format("k{0}{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}",separador,codigoComercio,nroTerminal,nroLote,nroCupon,ingresoDatosTarjeta,tipoOperacion,nroAutorizacion,importe,nroComprobanteFiscal);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void SetVaucherOptions (string espacio, float lineas, string nroDocumento, string nroTelefono)
		{
			throw new NotImplementedException ();
		}

		public void PrintVoucher (float cantidad)
		{
			string comando = string.Format("I{0}{1}",separador,cantidad);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void Cancel ()
		{
			string comando = string.Format("ÿ"); 
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void Reprint ()
		{
			throw new NotImplementedException ();
		}

		public void BarCode (float tipo, float dato, string numerico, string momento)
		{
			string comando = string.Format("Z{0}{1}{0}{2}{0}{3}{0}{4}",separador,tipo,dato,numerico,momento);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void FeedReceipt (float cantidad)
		{
			string comando = string.Format("P{0}{1}",separador,cantidad);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void FeedJournal (float cantidad)
		{
			string comando = string.Format("Q{0}{1}",separador,cantidad);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void FeedReceiptJournal (float cantidad)
		{
			string comando = string.Format("R{0}{1}",separador,cantidad);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void SetDateTime (DateTime fecha, DateTime hora)
		{
			string comando = string.Format("X{0}{1:yyMM}{0}{2:hhmmss}",separador,fecha,hora);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public GetDateTime GetDateTime ()
		{
			GetDateTime c = new GetDateTime();
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.FechaHora = DateTime.ParseExact(arrayRespuesta[2]+arrayRespuesta[3],"yyMMddhhmmss",System.Globalization.CultureInfo.InvariantCulture);
			return c; 
		}

		public void SetHeaderTrailer (int linea, string texto)
		{
			string comando = string.Format("]{0}{1}{0}{2}",separador,linea,texto);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public GetHeaderTrailer GetHeaderTrailer (int linea)
		{
			GetHeaderTrailer c = new GetHeaderTrailer(linea);
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.texto = arrayRespuesta[2];
			return c; 
		}

		public CustomerData CustomerData (string nombre, string cuit, string responsabilidadIVA, string tipoDocumento, string domicilio)
		{
			CustomerData c = new CustomerData(nombre, cuit, responsabilidadIVA, tipoDocumento, domicilio);
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			return c; 
		}

		public void SetFantasyName (int linea, string texto)
		{
			string comando = string.Format("_{0}{1}{1}{2}",separador,linea,texto);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public GetFantasyName GetFantasyName (int linea)
		{
			GetFantasyName c = new GetFantasyName(linea);
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.Texto = arrayRespuesta[2];
			return c;
		}

		public void SetEmbarkNumber (float linea, string texto)
		{
			string comando = string.Format("ô{0}{1}{0}{2}",separador,linea,texto);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public GetEmbarkNumber GetEmbarkNumber (int linea)
		{
			GetEmbarkNumber c = new GetEmbarkNumber(linea);
			impresora.EnviarComando(c.Comando());
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
			c.Texto = arrayRespuesta[2];
			return c;
		}

		public void ChangeBussinessStartupDate (DateTime fecha)
		{
			string comando = string.Format("x{0}{1:yyMMdd}",separador,fecha);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void SetUserLinesByZone ()
		{
			throw new NotImplementedException ();
		}

		public void GetUserLinesByZone ()
		{
			throw new NotImplementedException ();
		}

		public void OpenDrawer ()
		{
			string comando = string.Format("{");
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void WriteDisplay (string campo, string mensaje)
		{
			string comando = string.Format(Encoding.ASCII.GetString(new byte[]{178})+"{0}{1}{0}{2}{0}{3}",separador,"f",campo,mensaje);
			impresora.EnviarComando(comando);
			string [] arrayRespuesta = impresora.LeerRespuesta();
			statusImpresora(arrayRespuesta[0]);
			statusFiscal(arrayRespuesta[1]);
		}

		public void SetNetworkParameters (string ip, string mascaraDeRed, string gateway)
		{
			throw new NotImplementedException ();
		}

		public void GetNetworkParameters ()
		{
			throw new NotImplementedException ();
		}

		public void SetMailServerConfiguration (string ip, string puerto, string email)
		{
			throw new NotImplementedException ();
		}

		public void GetMailServerConfiguration ()
		{
			throw new NotImplementedException ();
		}

		public void SendDocByEmail ()
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

