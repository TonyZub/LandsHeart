namespace LandsHeart
{
	public sealed class RockResource : ResourceBase
	{
        #region Properties

        public override ResourcesTypes ResourceType => ResourcesTypes.Rock;

        #endregion


        #region Contructor

        public RockResource(int amount)
        {
            Preset(amount);
        }

        #endregion
    }
}