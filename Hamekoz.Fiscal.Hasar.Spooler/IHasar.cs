//
//  IHasar.cs
//
//  Author:
//		 Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//       Ezequiel Taranto <ezequiel89@gmail.com>
//
//  Copyright (c) 2014 Hamekoz - www.hamekoz.com.ar
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

namespace Hamekoz.Fiscal.Hasar.Spooler
{
    /// <summary>
    /// Interface de acceso a comandos de Impresora Fiscal Hasar
    /// </summary>
    public interface IHasar
    {
        #region Comandos de diagnostico y consulta

        /// <summary>
        /// Consulta de estado
        /// </summary>
        StatusRequest StatusRequest();

        /// <summary>
        /// Consulta de estado intermedio
        /// </summary>
        void StatRPN();

        /// <summary>
        /// Consulta de configuracion
        /// </summary>
        GetConfigurationData GetConfigurationData();

        /// <summary>
        /// Consulta de configuracion general
        /// </summary>
        void GetGeneralConfigurationData();

        /// <summary>
        /// Consulta datos de inicializacion
        /// </summary>
        GetInitData GetInitData();

        /// <summary>
        /// Consulta de versión de controlador fiscal
        /// </summary>
        void GetPrinterVersion();

        #endregion

        #region Comandos de control fiscal

        /// <summary>
        /// Capacidad restante
        /// </summary>
        HistoryCapacity HistoryCapacity();

        /// <summary>
        /// Cierre de jornada fiscal
        /// </summary>
        /// <param name = "zeta"></param>
        DailyClose DailyClose(bool zeta);

        /// <summary>
        /// Reporte de auditoria por fechas
        /// </summary>
        /// <param name="fechaInicio">Fecha inicio (AAMMDD).</param>
        /// <param name="fechaFinal">Fecha final (AAMMDD).</param>
        /// <param name="tipoDatos">Tipo datos ("T":datos globales, "otro": datos por Z).</param>
        void DailyCloseByDate(DateTime fechaInicio, DateTime fechaFinal, string tipoDatos);

        /// <summary>
        /// Reporte de auditoria por numero de Z
        /// </summary>
        /// <param name="nroZInicial">Nro Z inicial.</param>
        /// <param name="nroZFinal">Nro Z final.</param>
        /// <param name="tipo">Tipo de datos.</param>
        void DailyCloseByNumber(float nroZInicial, float nroZFinal, string tipo);

        /// <summary>
        /// Reporte de registro diario
        /// </summary>
        /// <param name="nroZ">Nro z.</param>
        /// <param name="calificadorZ">Calificador de z o fecha.</param>
        GetDailyReport GetDailyReport(string nroZ, string calificadorZ);

        /// <summary>
        /// Consulta de memoria RAM
        /// </summary>
        GetWorkingMemory GetWorkingMemory();

        /// <summary>
        /// Iniciar informacion de IVA
        /// </summary>
        SendFirstIVA SendFirstIVA();

        /// <summary>
        /// Continuar informacion de IVA
        /// </summary>
        void NextIVATransmission();

        /// <summary>
        /// Obtener ultimo error de ejecucion
        /// </summary>
        void GetLastExecutionError();

        /// <summary>
        /// Obtener primer bloque del log interno
        /// </summary>
        void GetFirstLogBlock();

        /// <summary>
        /// Obtener proximo bloque del log interno
        /// </summary>
        void GetNextLogBlock();

        /// <summary>
        /// Obtener primer bloque de registro de cinta de auditoria
        /// </summary>
        /// <param name="fechaZInicial">Fecha Z inicial.</param>
        /// <param name="fechaZFinal">Fecha Z final.</param>
        /// <param name="califZ">Calif z.</param>
        /// <param name="compresion">Compresion.</param>
        /// <param name="juntaJornadasXML">Junta jornadas XM.</param>
        void GetAuditFirstBlock(DateTime fechaZInicial, DateTime fechaZFinal, string califZ, string compresion, string juntaJornadasXML);

        /// <summary>
        /// Obtener siguiente bloque de registro de cinta de auditoria
        /// </summary>
        void GetAuditNextBlock();

        /// <summary>
        /// Definir rango de zetas borrables
        /// </summary>
        /// <param name="nroZ">Nro z.</param>
        void DefineErasableZRange(float nroZ);

        /// <summary>
        /// Obtener rango de zetas borrables
        /// </summary>
        /// <param name="nroZ">Nro z.</param>
        void GetErasableZRange(float nroZ);

        /// <summary>
        /// Obtener primer bloque de rango de documentos
        /// </summary>
        /// <param name="nroInicial">Nro inicial.</param>
        /// <param name="nroFinal">Nro final.</param>
        /// <param name="califTipoDocumento">Calif tipo documento.</param>
        /// <param name="compresion">Compresion.</param>
        /// <param name="juntaJornadasXML">Junta jornadas XM.</param>
        void GetDocumentFirstBlock(float nroInicial, float nroFinal, string califTipoDocumento, string compresion, string juntaJornadasXML);

        /// <summary>
        /// Obtener siguiente bloque de rango de documentos
        /// </summary>
        void GetDocumentNextBlock();

        #endregion

        #region Comandos de comprobante fiscal

        /// <summary>
        /// Abre comprobante fiscal
        /// </summary>
        /// <param name="tipoDocumento">Tipo documento.</param>
        void OpenFiscalReceipt(string tipoDocumento);

        /// <summary>
        /// Imprime texto fiscal
        /// </summary>
        /// <param name="texto">Texto.</param>
        void PrintFiscalText(string texto);

        /// <summary>
        /// Imprime item
        /// </summary>
        /// <param name="texto">Texto.</param>
        /// <param name="cantidad">Cantidad.</param>
        /// <param name="monto">Monto.</param>
        /// <param name="IVA">IV.</param>
        /// <param name="imputacion">Imputacion ("M":suma, "m":resta).</param>
        /// <param name="coeficiente">Coeficiente.</param>
        /// <param name="califMonto">Calificador monto.</param>
        void PrintLineItem(string texto, float cantidad, float monto, float IVA, string imputacion, float coeficiente, string califMonto);

        /// <summary>
        /// Descuento sobre el ultimo item
        /// </summary>
        /// <param name="texto">Texto.</param>
        /// <param name="monto">Monto.</param>
        /// <param name="imputacion">Imputacion ("M":suma, "m":resta).</param>
        /// <param name="califMonto">Calif monto.</param>
        void LastItemDiscount(string texto, float monto, string imputacion, string califMonto);

        /// <summary>
        /// Descuento general
        /// </summary>
        /// <param name="texto">Texto.</param>
        /// <param name="monto">Monto.</param>
        /// <param name="imputacion">Imputacion ("M":suma, "m":resta).</param>
        /// <param name="califMonto">Calif monto.</param>
        void GeneralDiscount(string texto, float monto, string imputacion, string califMonto);

        /// <summary>
        /// Devolucion de envases, Bonificaciones y Recargos
        /// </summary>
        /// <param name="texto">Texto.</param>
        /// <param name="monto">Monto.</param>
        /// <param name="IVA">IV.</param>
        /// <param name="imputacion">Imputacion ("M":suma, "m":resta).</param>
        /// <param name="coefImp">Coeficiente de impuestos internos.</param>
        /// <param name="total">Total.</param>
        /// <param name="califOper">Calificador de operacion ("B": descuento/recargo, "otro caracter": devolucion de envases).</param>
        void ReturnRecharge(string texto, float monto, float IVA, string imputacion, float coefImp, string total, string califOper);

        /// <summary>
        /// Recargo IVA a Responsable no Inscripto
        /// </summary>
        /// <param name="monto">Monto.</param>
        void ChargeNonRegisteredTax(float monto);

        /// <summary>
        ///  Percepciones
        /// </summary>
        /// <param name="IVA">IV.</param>
        /// <param name="texto">Texto.</param>
        /// <param name="monto">Monto.</param>
        void Perceptions(float IVA, string texto, float monto);

        /// <summary>
        /// Subtotal
        /// </summary>
        /// <param name="impresion">Impresion ("P":imprime texto y monto, "otro": no imprime).</param>
        Subtotal Subtotal(string impresion);

        /// <summary>
        /// Total / Pago
        /// </summary>
        /// <param name="texto">Texto.</param>
        /// <param name="monto">Monto pagado.</param>
        /// <param name="vuelto">Vuelto/cancelacion.</param>
        TotalTender TotalTender(string texto, float monto, string vuelto);

        /// <summary>
        /// Cerrar comprobante fiscal
        /// </summary>
        CloseFiscalReceipt CloseFiscalReceipt();

        #endregion

        #region Comandos de comprobante no-fiscal

        /// <summary>
        /// Abrir comprobante no-fiscal
        /// </summary>
        void OpenNonFiscalRecipt();

        /// <summary>
        /// Abrir comprobante no-fiscal en impresora slip
        /// </summary>
        void OpenNonFiscalSlip();

        /// <summary>
        /// Imprimir texto no-fiscal
        /// </summary>
        /// <param name="texto">Texto(40 caracteres).</param>
        void PrintNonFiscalText(string texto);

        /// <summary>
        /// Cerrar comprobante no-fiscal
        /// </summary>
        void CloseNonFiscalReceipt();

        #endregion

        #region Comandos de comprobante no-fiscal homologado

        /// <summary>
        /// Abrir documento no fiscal homologado
        /// </summary>
        /// <param name="tipoDocumento">Tipo documento ("R": nota de credito A; "S": nota de credito B o C).</param>
        /// <param name="identificacionNroDocumento">Identificacion nro documento (llenar con un numero cualquiera).</param>
        OpenDNFH OpenDNFH(string tipoDocumento, string identificacionNroDocumento);

        /// <summary>
        /// Imprimir línea de información en DNFH
        /// </summary>
        /// <param name="texto">Texto.</param>
        /// <param name="parametroDisplay">Parametro display.</param>
        /// <param name="cantidadUnidades">Cantidad unidades.</param>
        void PrintDNFHInfo(string texto, float parametroDisplay, string cantidadUnidades);

        /// <summary>
        /// Impresión de firma y aclaración en DNFH
        /// </summary>
        void PrintSignDNFH(string firmaAclaracionOtrasLeyendas);

        /// <summary>
        /// Texto de líneas de recibos
        /// </summary>
        void ReceiptText(string texto);

        /// <summary>
        /// Cerrar documento no fiscal homologado
        /// </summary>
        CloseDNFH CloseDNFH();

        /// <summary>
        /// Documento no fiscal homologado farmacias
        /// </summary>
        /// <param name="cantidadEjemplares">Cantidad ejemplares a imprimir maximo 2.</param>
        void DNFHFarmacias(int cantidadEjemplares);

        /// <summary>
        /// Documento no fiscal homologado reparto
        /// </summary>
        /// <param name="cantidadEjemplares">Cantidad ejemplares a imprimir maximo 3.</param>
        void DNFHReparto(int cantidadEjemplares);

        /// <summary>
        /// Datos del voucher de tarjeta de credito 1
        /// </summary>
        /// <param name="nombreCliente">Nombre cliente (30 caracteres).</param>
        /// <param name="nombreTarjetaCredito">Nombre tarjeta credito (20 caracteres).</param>
        /// <param name="califOperacion">Calificador de operacion.</param>
        /// <param name="nroTarjeta">Nro tarjeta (16 digitos).</param>
        /// <param name="fechaVencimientoTarjeta">Fecha vencimiento tarjeta (AAMM).</param>
        /// <param name="tipoTarjetaUsada">Tipo tarjeta usada ("D":debito, "C": credito).</param>
        /// <param name="cantidadCuotas">Cantidad cuotas (2 digitos).</param>
        void SetVoucherData1(string nombreCliente, string nombreTarjetaCredito, string califOperacion, string nroTarjeta, DateTime fechaVencimientoTarjeta, string tipoTarjetaUsada, float cantidadCuotas);

        /// <summary>
        /// Datos del voucher de tarjeta de credito 2
        /// </summary>
        /// <param name="codigoComercio">Codigo comercio (15 digitos).</param>
        /// <param name="nroTerminal">Nro terminal (8 digitos).</param>
        /// <param name="nroLote">Nro lote (3 digitos).</param>
        /// <param name="nroCupon">Nro cupon (4 digitos).</param>
        /// <param name="ingresoDatosTarjeta">Ingreso datos tarjeta ("*":manual, "<SP>":automatica).</param>
        /// <param name="tipoOperacion">Tipo operacion("N":on line, "F": off line).</param>
        /// <param name="nroAutorizacion">Nro autorizacion (6 digitos).</param>
        /// <param name="importe">Importe (15 caracteres incluyendo signo monetario).</param>
        /// <param name="nroComprobanteFiscal">Nro comprobante fiscal (8 digitos).</param>
        /// <param name="tipoOperacion"></param>
        /// <param name="nroAutorizacion"></param>
        /// <param name="importe"></param>
        /// <param name="nroComprobanteFiscal"></param>
        void SetVoucherData2(float codigoComercio, float nroTerminal, float nroLote, float nroCupon, string ingresoDatosTarjeta, string tipoOperacion, float nroAutorizacion, string importe, float nroComprobanteFiscal);

        /// <summary>
        /// Cargar opciones de voucher
        /// </summary>
        /// <param name="espacio">Espacio.</param>
        /// <param name="lineas">Lineas.</param>
        /// <param name="nroDocumento">Nro documento.</param>
        /// <param name="nroTelefono">Nro telefono.</param>
        void SetVaucherOptions(string espacio, float lineas, string nroDocumento, string nroTelefono);

        /// <summary>
        /// Imprimir voucher
        /// </summary>
        /// <param name="cantidad">Cantidad de ejemplares a imprimir(maximo 3).</param>
        void PrintVoucher(float cantidad);

        #endregion

        #region Comandos comunes a varios tipos de documentos

        /// <summary>
        /// Cancelación
        /// </summary>
        void Cancel();

        /// <summary>
        /// Reimpresión del último comprobante emitido
        /// </summary>
        void Reprint();

        /// <summary>
        /// Código de barras
        /// </summary>
        /// <param name="tipo">Tipo de codigo("1": EAN 13, "2": EAN 8, "3": UPCA).</param>
        /// <param name="dato">Dato.</param>
        /// <param name="numerico">Numerico ("N":imprime numeros, "otro": no imprime numeros).</param>
        /// <param name="momento">Momento ("P":imprime en el momento, "otro": imprime al final del comprobante).</param>
        void BarCode(float tipo, float dato, string numerico, string momento);

        #endregion

        #region Comandos de control de impresora

        /// <summary>
        /// Avanzar papel de ticket
        /// </summary>
        /// <param name="cantidad">Cantidad de lineas a avanzar.</param>
        void FeedReceipt(float cantidad);

        /// <summary>
        /// Avanzar papel cintal de auditoria
        /// </summary>
        /// <param name="cantidad">Cantidad de lineas a avanzar.</param>
        void FeedJournal(float cantidad);

        /// <summary>
        /// Avanzar papeles de ticket y cinta de auditoria
        /// </summary>
        /// <param name="cantidad">Cantidad de lineas a avanzar.</param>
        void FeedReceiptJournal(float cantidad);

        #endregion

        #region Comandos de fecha y hora

        /// <summary>
        /// Ingresar fecha y hora
        /// </summary>
        /// <param name="fecha">Fecha(AAMMDD).</param>
        /// <param name="hora">Hora (HHMMSS).</param>
        void SetDateTime(DateTime fecha, DateTime hora);

        /// <summary>
        /// Consultar fecha y hora
        /// </summary>
        GetDateTime GetDateTime();

        /// <summary>
        /// Programar tecto de encabezado y cola de ticket y documentos no fiscales
        /// </summary>
        /// <param name="linea">numero de linea de encabezamiento (1-10) o cola (11-20).</param>
        /// <param name="texto">Texto (40 caracteres).</param>
        void SetHeaderTrailer(int linea, string texto);

        /// <summary>
        /// Consultar txto de encabezado y cola de ticket
        /// </summary>
        /// <param name="linea">numero de linea de encabezamiento (1-10) o cola (11-20).</param>
        GetHeaderTrailer GetHeaderTrailer(int linea);

        /// <summary>
        /// Datos de comprobador ticket-factura
        /// </summary>
        /// <param name="nombre">Nombre del comprador.</param>
        /// <param name="cuit">Cuit.</param>
        /// <param name="responsabilidadIVA">Responsabilidad IVA.</param>
        /// <param name="tipoDocumento">Tipo documento.</param>
        /// <param name="domicilio">Domicilio(40 caracteres).</param>
        CustomerData CustomerData(string nombre, string cuit, string responsabilidadIVA, string tipoDocumento, string domicilio);

        /// <summary>
        /// Programar texto del nombre fantasia del propietario
        /// </summary>
        /// <param name="linea">numero de linea del nombre de fantasia (1-2).</param>
        /// <param name="texto">Texto (40 caracteres).</param>
        void SetFantasyName(int linea, string texto);

        /// <summary>
        /// Reportar texto del nombre fantasia del propietario
        /// </summary>
        /// <param name="linea">Linea.</param>
        GetFantasyName GetFantasyName(int linea);

        /// <summary>
        /// Carga informacion remito / comprobante original
        /// </summary>
        /// <param name="linea">Linea (1-2).</param>
        /// <param name="texto">Texto(20 caracteres).</param>
        void SetEmbarkNumber(float linea, string texto);

        /// <summary>
        /// Reporta informacion remito / comprobante original
        /// </summary>
        /// <param name="linea">Linea (1-2).</param>
        GetEmbarkNumber GetEmbarkNumber(int linea);

        /// <summary>
        /// Cambiar fecha de inicio de actividad
        /// </summary>
        /// <param name="fecha">Fecha (AAMMDD).</param>
        void ChangeBussinessStartupDate(DateTime fecha);

        /// <summary>
        /// Carga de lineas de usuario por zona
        /// </summary>
        void SetUserLinesByZone();

        /// <summary>
        /// Obtener lineas de usuario por zona
        /// </summary>
        void GetUserLinesByZone();

        #endregion

        #region Comandos cajon de dinero

        /// <summary>
        /// Abrir cajon de dinero
        /// </summary>
        void OpenDrawer();

        #endregion

        #region Comandos de display

        /// <summary>
        /// Escribe en el display
        /// </summary>
        /// <param name="campo">Campo.</param>
        /// <param name="mensaje">Mensaje.</param>
        void WriteDisplay(string campo, string mensaje);

        #endregion

        #region Comandos de comunicacion por red (solo SMH/P-451F)

        /// <summary>
        /// Cargar parametros de red
        /// </summary>
        /// <param name="ip">Ip.</param>
        /// <param name="mascaraDeRed">Mascara de red.</param>
        /// <param name="gateway">Gateway.</param>
        void SetNetworkParameters(string ip, string mascaraDeRed, string gateway);

        /// <summary>
        /// Obtener parametros de red
        /// </summary>
        void GetNetworkParameters();

        /// <summary>
        /// Cargar configuracion de email
        /// </summary>
        /// <param name="ip">Ip.</param>
        /// <param name="puerto">Puerto.</param>
        /// <param name="email">Email.</param>
        void SetMailServerConfiguration(string ip, string puerto, string email);

        /// <summary>
        /// Leer configuracion del servidor de email
        /// </summary>
        void GetMailServerConfiguration();

        /// <summary>
        /// Enviar documento por email
        /// </summary>
        void SendDocByEmail();

        #endregion
    }
}