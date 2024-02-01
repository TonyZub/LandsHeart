using UnityEngine;


namespace LandsHeart
{
	public sealed class Profession
	{
		#region Fields

		[SerializeField] private ProfessionsNames _professionName;

		#endregion


		#region Properties

		public ProfessionsNames ProfessionName => _professionName;

		#endregion
	}
}