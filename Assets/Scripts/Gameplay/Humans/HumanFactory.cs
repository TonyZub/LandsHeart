namespace LandsHeart
{
	public sealed class HumanFactory
	{
		#region Methods

		public static Human CreateHuman()
		{
			return new Human(HumanGenders.None, string.Empty, string.Empty, string.Empty, new Profession(), new System.Collections.Generic.List<Feature>(),
				new System.Collections.Generic.List<Item>(), null, 0, 0); // TO REFACTOR
		}

		#endregion
	}
}