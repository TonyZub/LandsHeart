using UnityEngine;


namespace LandsHeart
{
	public sealed class HumanFactory
	{
		#region Methods

		public static Human MakeHuman()
		{
			return new Human(string.Empty, string.Empty, string.Empty, new Profession(), new System.Collections.Generic.List<Feature>(),
				new System.Collections.Generic.List<Item>(), null, 0); // TO REFACTOR
		}

		#endregion
	}
}