namespace LandsHeart
{
	public sealed class OreResource : ResourceBase
	{
        #region Properties

        public override ResourcesTypes ResourceType => ResourcesTypes.Ore;

        #endregion


        #region Contructor

        public OreResource(int amount)
        {
            Preset(amount);
        }

        #endregion
    }
}