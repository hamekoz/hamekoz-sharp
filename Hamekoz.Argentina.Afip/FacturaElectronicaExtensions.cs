﻿//
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
using Hamekoz.Fiscal;

namespace Hamekoz.Argentina.Afip
{
    public static class FacturaElectronicaExtensions
    {
        public const string homo_url = "http://wswhomo.afip.gov.ar/wsfev1/service.asmx?WSDL";
        const string url = "https://servicios1.afip.gov.ar/wsfev1/service.asmx?WSDL";
        //HACK por ahora uso una constante
#if DEBUG
        //HACK cpereyra para debug
        const long cuit = 20311864093;
#else
		//HACK Postres Balcarce
		const long cuit = 30655462224;
#endif


        static LoginTicketResponse loginTicketResponse;

        public static FEAuthRequest TA(string ta_path)
        {
            if (loginTicketResponse == null || loginTicketResponse.ExpirationTime > DateTime.Now)
            {
                if (!File.Exists(ta_path))
                    throw new FileNotFoundException("No existe el archivo", ta_path);
                string xml = File.ReadAllText(ta_path);
                loginTicketResponse = new LoginTicketResponse(xml);
            }
            if (loginTicketResponse.ExpirationTime <= DateTime.Now)
                throw new AFIPException("El Ticket de Acceso a los Web Servicios de AFIP a expirado, debe solicitar uno nuevo");
            return new FEAuthRequest
            {
                Cuit = cuit, //FIXME reemplazar por el cuit leido del TA
                Sign = loginTicketResponse.sign,
                Token = loginTicketResponse.token,
            };
        }

        public static object PuntosDeVenta(string ta_path)
        {
            var service = new wsfev1.ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap);
            var respuesta = service.FEParamGetPtosVenta(new FEParamGetPtosVentaRequest(new FEParamGetPtosVentaRequestBody(TA(ta_path))));
            return respuesta.Body.FEParamGetPtosVentaResult.ResultGet;
        }

        static IEnumerable<AlicIva> FEIVAS(this IComprobante comprobante)
        {
            //TODO revisar porque quizas el IVA exento tendria que tratarlo de otra forma
            return from i in comprobante.IVAItems.Where(i => i.IVA != IVA.Exento)
                   select new AlicIva
                   {
                       Id = (int)i.IVA,
                       BaseImp = (double)i.Neto,
                       Importe = (double)i.Importe
                   };
        }

        static IEnumerable<Tributo> FETributos(this IComprobante comprobante)
        {
            return from i in comprobante.Impuestos
                   select new Tributo
                   {
                       Alic = (double)i.Alicuota,
                       BaseImp = (double)i.BaseImponible,
                       Desc = i.Impuesto.Descripcion,
                       Id = 2, //HACK impuestos provinciales
                       Importe = (double)i.Importe,
                   };
        }

        static int ComprobanteTipo(this IComprobanteElectronico comprobante)
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

            throw new NotImplementedException("Tipo de comprobante no soportado");
        }

        public static void SolicitarCAE(this IComprobanteElectronico comprobante, string ta_path, Hamekoz.Core.ICallBack callback)
        {
            var service = new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap);
            //HACK para cambiar de produccion a homologacion
#if DEBUG
            service = new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap, homo_url);
#endif
            var ta = TA(ta_path);

            //TODO ahora podria utilizar comprobante.Tipo.Codigo validando que sea un valor adecuado
            int tipo = ComprobanteTipo(comprobante);
            int punto_de_venta = int.Parse(comprobante.Tipo.Pre);

            var fECompUltimoAutorizadoRequestBody = new FECompUltimoAutorizadoRequestBody(ta, punto_de_venta, tipo);
            var fECompUltimoAutorizadoRequest = new FECompUltimoAutorizadoRequest(fECompUltimoAutorizadoRequestBody);

            var fECompUltimoAutorizado = service.FECompUltimoAutorizado(fECompUltimoAutorizadoRequest);
            int numero = fECompUltimoAutorizado.Body.FECompUltimoAutorizadoResult.CbteNro;
            numero++;

            var feCabReq = new FECAECabRequest
            {
                CantReg = 1,
                CbteTipo = tipo,
                PtoVta = punto_de_venta
            };

            var fEDetRequest = new FECAEDetRequest
            {
                CbteDesde = numero,
                CbteFch = comprobante.Emision.ToString("yyyyMMdd"),
                CbteHasta = numero,
                CbtesAsoc = null, //TODO en caso de que sean comprobantes vinculados
                Compradores = null, //TODO revisar porque no encuentro documentacion
                                    //HACK producto
                Concepto = 1,
                DocNro = long.Parse(comprobante.Responsable.CUIT.Limpiar()),
                //HACK CUIT
                DocTipo = 80,
                ImpIVA = (double)comprobante.IVA, //TODO debe excluirse el importe exento, para tipo C debe ser 0
                ImpNeto = (double)(comprobante.Gravado - comprobante.Exento),
                ImpOpEx = (double)comprobante.Exento,
                ImpTotal = (double)comprobante.Total,
                //TODO en caso de ser de tipo C debe ser siempre 0
                ImpTotConc = (double)comprobante.NoGravado,
                ImpTrib = (double)comprobante.Tributos,
                Iva = comprobante.FEIVAS().ToList(),
                MonCotiz = 1, //TODO pasar la cotizacion correcta
                MonId = "PES", //TODO pasar la moneda correcta
                Opcionales = null,
                Tributos = comprobante.Tributos > 0 ? comprobante.FETributos().ToList() : null,
            };

            var feDetReq = new List<FECAEDetRequest>();
            feDetReq.Add(fEDetRequest);

            var solicitud = new FECAERequest
            {
                FeCabReq = feCabReq,
                FeDetReq = feDetReq.ToList()
            };

            var fECAESolicitarRequestBody = new FECAESolicitarRequestBody(ta, solicitud);
            var fECAESolicitarRequest = new FECAESolicitarRequest(fECAESolicitarRequestBody);

            FECAEResponse fECAEResponse = service.FECAESolicitar(fECAESolicitarRequest).Body.FECAESolicitarResult;

            if (fECAEResponse.Events != null)
            {
                foreach (var evento in fECAEResponse.Events)
                {
                    string mensaje = string.Format("Código: {0}. Mensaje: {1}", evento.Code, evento.Msg);
                    callback.CallBack.OnMessage("Evento AFIP", mensaje);
                }
            }

            if (fECAEResponse.Errors != null)
            {
                var errores = new StringBuilder();
                foreach (var error in fECAEResponse.Errors)
                {
                    errores.AppendFormat("Código: {0}. Mensaje: {1}", error.Code, error.Msg);
                    errores.AppendLine();
                }
                throw new AFIPException(errores.ToString());
            }

            //TODO refactorizar para soportar un lote de comprobantes

            var observaciones = new StringBuilder();
            if (fECAEResponse.FeDetResp[0].Observaciones != null)
            {
                foreach (var observacion in fECAEResponse.FeDetResp[0].Observaciones)
                {
                    observaciones.AppendFormat("Código: {0}. Mensaje: {1}", observacion.Code, observacion.Msg);
                    observaciones.AppendLine();
                }

                callback.CallBack.OnWarning("Comprobante con observaciones", observaciones.ToString());
            }

            if (fECAEResponse.FeDetResp[0].Resultado == "R")
            {
                string errorAfip = string.Format("Comprobante rechazado.\n{0}", observaciones);
                throw new AFIPException(errorAfip);
            }

            //TODO ver si el formato seria correcto
            comprobante.Tipo.UltimoNumero = numero;
            comprobante.NumeroAFIP = string.Format("{0:0000}-{1:00000000}", punto_de_venta, numero);
            comprobante.Numero = comprobante.NumeroAFIP;
            comprobante.CAE = fECAEResponse.FeDetResp[0].CAE;
            comprobante.VencimientoCAE = fECAEResponse.FeDetResp[0].CAEFchVto;
            comprobante.ComentariosAFIP = observaciones.ToString();
        }
    }
}