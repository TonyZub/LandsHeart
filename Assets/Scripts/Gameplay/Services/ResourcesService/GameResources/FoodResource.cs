namespace LandsHeart
{
	public sealed class FoodResource : ResourceBase
	{
        #region Properties

        public override ResourcesTypes ResourceType => ResourcesTypes.Food;

        #endregion


        #region Contructor

        public FoodResource(int amount)
		{
			Preset(amount);
		}

        #endregion
	}
}