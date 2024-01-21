namespace LandsHeart
{
	public sealed class FabricResource : ResourceBase
	{
        #region Properties

        public override ResourcesTypes ResourceType => ResourcesTypes.Fabric;

        #endregion


        #region Contructor

        public FabricResource(int amount)
        {
            Preset(amount);
        }

        #endregion
    }
}