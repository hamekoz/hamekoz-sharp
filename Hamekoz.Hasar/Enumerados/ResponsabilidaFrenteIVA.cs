using System;

namespace POS.Fiscal
{


	public enum ResponsabilidaFrenteIVA
	{
		/// <summary>
		/// I
		/// </summary>
		ResponsableInscripto,

		/// <summary>
		///N. No válido en los modelos SMH/P-715F, SMH/P-PR5F y SMH/P-441F
		/// </summary>
		ResponsableNoInscripto,

		/// <summary>
		/// E. No válido en los modelos SMH/P-715F, SMH/P-PR5F y SMH/P-441F
		/// </summary>
		Exento,

		/// <summary>
		/// A
		/// </summary>
		NoResponsable,

		/// <summary>
		/// M. No disponible en el modelo SMH/P-PR4F
		/// </summary>
		Monotributista,

		/// <summary>
		/// S. Sólo disponible en los modelos SMH/P-715F, SMH/P-PR5F y SMH/P-441F
		/// </summary>
		MonotributistaSocial,
	}
}