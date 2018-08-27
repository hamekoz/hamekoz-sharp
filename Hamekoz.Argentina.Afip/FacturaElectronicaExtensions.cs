//
//  FacturaElectronicaExtensions.cs
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hamekoz.Argentina.Afip.wsfev1;

namespace Hamekoz.Argentina.Afip
{
	public static class FacturaElectronicaExtensions
	{
		//HACK por ahora uso una constante
		const long cuit = 30655462224;

		public static FEAuthRequest TA ()
		{
			//HACK por ahora uso una constante
			string xml = File.ReadAllText ("/home/cpereyra/.wine/drive_c/ta.xml");
			var loginTicketResponse = new LoginTicketResponse (xml);
			if (loginTicketResponse.ExpirationTime <= DateTime.Now) {
				//TODO informar ticket expirado y solicitar un nuevo ticket
			}
			return new FEAuthRequest {
				Cuit = cuit,
				Sign = loginTicketResponse.sign,
				Token = loginTicketResponse.token,
			};
		}

		public static object PuntosDeVenta ()
		{
			var service = new Hamekoz.Argentina.Afip.wsfev1.Service ();
			var respuesta = service.FEParamGetPtosVenta (TA ());
			return respuesta.ResultGet;
		}

		static IEnumerable<AlicIva> FEIVAS (this Hamekoz.Fiscal.IComprobante comprobante)
		{
			return from i in comprobante.IVAItems
			       select new AlicIva {
				Id = 5, //HACK IVA 21%
				BaseImp = (double)i.Neto,
				Importe = (double)i.Importe
			};
		}

		static IEnumerable<Tributo> FETributos (this Hamekoz.Fiscal.IComprobante comprobante)
		{
			return from i in comprobante.Impuestos
			       select new Tributo {
				Alic = (double)i.Alicuota,
				BaseImp = (double)i.BaseImponible,
				Desc = i.Impuesto.Descripcion,
				Id = 2, //HACK impuestos provinciales
				Importe = (double)i.Importe,
			};
		}

		static int ComprobanteTipo (this Hamekoz.Negocio.Comprobante comprobante)
		{
			var tipo = comprobante.Tipo;

			if (tipo.Abreviatura == "FC" && tipo.Letra == "A")
				return 1;
			if (tipo.Abreviatura == "ND" && tipo.Letra == "A")
				return 2;
			if (tipo.Abreviatura == "NC" && tipo.Letra == "A")
				return 3;

			if (tipo.Abreviatura == "FC" && tipo.Letra == "B")
				return 6;
			if (tipo.Abreviatura == "ND" && tipo.Letra == "B")
				return 7;
			if (tipo.Abreviatura == "NC" && tipo.Letra == "B")
				return 8;

			throw new NotImplementedException ("Tipo de comprobante no soportado");
		}

		public static void SolicitarCAE (this Hamekoz.Negocio.ComprobanteCliente comprobante, Hamekoz.Core.ICallBack callback)
		{
			//TODO poder cambiar de homologacion a produccion
			var service = new Service ();
			var ta = TA ();

			int tipo = ComprobanteTipo (comprobante);
			int punto_de_venta = int.Parse (comprobante.Tipo.Pre);

			var fECompUltimoAutorizado = service.FECompUltimoAutorizado (ta, punto_de_venta, tipo);
			int numero = fECompUltimoAutorizado.CbteNro;
			numero++;

			var feCabReq = new FECAECabRequest {
				CantReg = 1,
				CbteTipo = 1,
				PtoVta = punto_de_venta
			};

			var compradores = new List<Comprador> ();
			compradores.Add (new Comprador {
				DocNro = long.Parse (comprobante.Cliente.CUIT.Limpiar ()),
				DocTipo = 80, //HACK CUIT
			});

			var fEDetRequest = new FECAEDetRequest {
				CbteDesde = numero,
				CbteFch = comprobante.Emision.ToString ("yyyyMMdd"),
				CbteHasta = numero,
				CbtesAsoc = null, //TODO en caso de que sean comprobantes vinculados
				Compradores = null, //TODO revisar porque no encuentro documentacion
				//HACK producto
				Concepto = 1,
				DocNro = long.Parse (comprobante.Cliente.CUIT.Limpiar ()),
				//HACK CUIT
				DocTipo = 80,
				ImpIVA = (double)comprobante.IVA, //TODO debe excluirse el importe exento, para tipo C debe ser 0
				ImpNeto = (double)comprobante.Neto,
				//HACK deberia tener el campo en la clase Comprobante
				ImpOpEx = (double)comprobante.IVAItems.FirstOrDefault (i => i.IVA == Hamekoz.Negocio.IVA.Exento).Importe,
				ImpTotal = (double)comprobante.Total,
				//TODO en caso de ser de tipo C debe ser siempre 0
				ImpTotConc = (double)comprobante.NoGravado,
				ImpTrib = (double)comprobante.Tributos,
				Iva = comprobante.FEIVAS ().ToArray (),
				MonCotiz = 1,
				MonId = "PES",
				Opcionales = null,
				Tributos = comprobante.Tributos > 0 ? comprobante.FETributos ().ToArray () : null,
			};

			var feDetReq = new List<FECAEDetRequest> ();
			feDetReq.Add (fEDetRequest);

			var solicitud = new  FECAERequest {
				FeCabReq = feCabReq,
				FeDetReq = feDetReq.ToArray ()
			};

			FECAEResponse fECAEResponse = service.FECAESolicitar (TA (), solicitud);

			if (fECAEResponse.Events.Length > 0) {
				foreach (var evento in fECAEResponse.Events) {
					string mensaje = string.Format ("Código: {0}. Mensaje: {1}", evento.Code, evento.Msg);
					callback.CallBack.OnMessage ("Eventos AFIP", mensaje);
				}
			}

			if (fECAEResponse.Errors.Length > 0) {
				var errores = new StringBuilder ();
				foreach (var error in fECAEResponse.Errors) {
					errores.AppendFormat ("Código: {0}. Mensaje: {1}", error.Code, error.Msg);
					errores.AppendLine ();
				}
				throw new Exception (errores.ToString ());
			}

			//TODO refactorizar para soportar un lote de comprobantes

			comprobante.CAE = fECAEResponse.FeDetResp [0].CAE;
			comprobante.VencimientoCAE = fECAEResponse.FeDetResp [0].CAEFchVto;

			if (fECAEResponse.FeDetResp [0].Observaciones.Length > 0) {
				var observaciones = new StringBuilder ();
				foreach (var observacion in fECAEResponse.Errors) {
					observaciones.AppendFormat ("Código: {0}. Mensaje: {1}", observacion.Code, observacion.Msg);
					observaciones.AppendLine ();
				}
				comprobante.ComentariosAFIP = observaciones.ToString ();
				callback.CallBack.OnWarning ("Advertencia", "Comprobante registrado con observaciones");
			}
		}
	}
}

