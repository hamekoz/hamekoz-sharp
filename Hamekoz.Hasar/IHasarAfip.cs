//
//  IHasarAfip.cs
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

namespace Hamekoz.Hasar
{
	public interface IHasarAfip
	{	
		#region Comandos de inicializacion, baja y configuración

		/// <summary>
		/// Inicializacion de controlador fiscal
		/// </summary>
		void InitEpromFiscal();

		/// <summary>
		/// Baja de controlador fiscal
		/// </summary>
		/// <param name="numero">Numero de registro de equipo (3 letras + 7 numeros).</param>
		void KillEpromFiscal(string numero);

		/// <summary>
		/// Configuracion del controlador en bloque
		/// </summary>
		/// <param name="limiteDatosConsumidor">Limite datos consumidor.</param>
		/// <param name="limiteTicket">Limite ticket.</param>
		/// <param name="porcentajeIVA">Porcentaje IVA.</param>
		/// <param name="reservado">Reservado (siempre "1").</param>
		/// <param name="cambio">Cambio ("P":imprime, "otro": no imprime).</param>
		/// <param name="leyenda">Leyenda ("P":imprime, "otro": no imprime).</param>
		/// <param name="tipoCorte">Tipo corte ("F":corte completo, "P": corte parcial,"N": no corta).</param>
		void ConfigureControllerByBlock(string limiteDatosConsumidor, float limiteTicket,float porcentajeIVA, float reservado, string cambio, string leyenda , string tipoCorte);

		/// <summary>
		/// Configuración general del controlador
		/// </summary>
		/// <param name="limiteDatos">Limite ingreso datos consumidor final.</param>
		/// <param name="limiteTicket">Limite ticket - factura.</param>
		/// <param name="porcentajeIVA">Porcentaje IVA.</param>
		/// <param name="cantidad">Cantidad.</param>
		/// <param name="cambio">Cambio.</param>
		/// <param name="leyendas">Leyendas.</param>
		/// <param name="tipoCorte">Tipo corte.</param>
		void GeneralConfiguration(string limiteDatos,float limiteTicket, float porcentajeIVA, float cantidad, string cambio, string leyendas, string tipoCorte);

		/// <summary>
		/// Configuracion del controlador fiscal por parámetros
		/// </summary>
		/// <param name="parametro">Parametro.</param>
		/// <param name="valor">Valor.</param>
		void ConfigureControllerByOne(string parametro, string valor);

		/// <summary>
		/// Cambio de responsabilidad frente al IVA
		/// </summary>
		/// <param name="IVA">Responsabilidad frente al IVA.</param>
		void ChangeIVAResponsability(string IVA);

		/// <summary>
		/// Cambio número de Ingresos Brutos
		/// </summary>
		/// <param name="nroIB">Nro de Ingresos Brutos (20 caracteres)</param>
		void ChangeIBNumber(string nroIB);

		/// <summary>
		/// Cambio fecha de inicio de actividades
		/// </summary>
		/// <param name="fecha">Fecha.</param>
		void ChangeStarDate(DateTime fecha);

		/// <summary>
		/// Seteo de velocidad de comunicación
		/// </summary>
		/// <param name="velocidad">Velocidad.</param>
		void SetComSpeed(float velocidad);

		#endregion

	}
}

