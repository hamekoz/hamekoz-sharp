
using System;

namespace POS.Fiscal
{
	
	
	public enum ResponsabilidaFrenteIVA
	{
		/// <summary>
		/// /
		/// </summary>
		ResponsableInscripto,// = "I",
		
		/// <summary>
		/// no válido en los modelos SMH/P-715F, SMH/P-PR5F y SMH/P-441F
		/// </summary>
		ResponsableNoInscripto,// = "N",
		
		/// <summary>
		/// no válido en los modelos SMH/P-715F, SMH/P-PR5F y SMH/P-441F
		/// </summary>
		Exento,// = "E",
		
		/// <summary>
		/// 
		/// </summary>
		NoResponsable,// = "A",
		
		/// <summary>
		/// no disponible en el modelo SMH/P-PR4F
		/// </summary>
		Monotributista,// = "M",
		
		/// <summary>
		/// sólo disponible en los modelos SMH/P-715F, SMH/P-PR5F y SMH/P-441F
		/// </summary>
		MonotributistaSocial,// = "S",
	}
}