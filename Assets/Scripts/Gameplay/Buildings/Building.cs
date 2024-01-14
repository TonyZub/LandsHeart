namespace LandsHeart
{
	public abstract class Building
	{
        #region Properties

        public virtual BuildingsNames BuildingName { get; private set; } = BuildingsNames.None;

        #endregion


        #region Methods

        public virtual void OnCycleEnd()
        {
            //TODO
        }

        #endregion
    }
}