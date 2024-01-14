namespace LandsHeart
{
	public sealed class WoodResource : ResourceBase
    {
        #region Properties

        public override ResourcesTypes ResourceType => ResourcesTypes.Wood;

        #endregion


        #region Contructor

        public WoodResource(int amount)
        {
            Preset(amount);
        }

        #endregion
    }
}