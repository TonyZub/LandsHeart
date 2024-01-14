namespace LandsHeart
{
	public sealed class GoldResource : ResourceBase
	{
        #region Properties

        public override ResourcesTypes ResourceType => ResourcesTypes.Wood;

        #endregion


        #region Contructor

        public GoldResource(int amount)
        {
            Preset(amount);
        }

        #endregion
    }
}