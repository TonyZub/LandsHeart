namespace LandsHeart
{
	public sealed class MetalResource : ResourceBase
	{
        #region Properties

        public override ResourcesTypes ResourceType => ResourcesTypes.Metal;

        #endregion


        #region Contructor

        public MetalResource(int amount)
        {
            Preset(amount);
        }

        #endregion
    }
}