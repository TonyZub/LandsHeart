namespace LandsHeart
{
	public sealed class PaperResource : ResourceBase
	{
        #region Properties

        public override ResourcesTypes ResourceType => ResourcesTypes.Paper;

        #endregion


        #region Contructor

        public PaperResource(int amount)
        {
            Preset(amount);
        }

        #endregion
    }
}