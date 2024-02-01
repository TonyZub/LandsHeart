namespace LandsHeart
{
	public abstract class Building : ICycleEnd
	{
        #region Fields

        private readonly BuildingData _buildingData;

        #endregion


        #region Properties

        public BuildingData BuildingData => _buildingData;

        #endregion


        #region Constructor

        public Building(BuildingData buildingData)
        {
            _buildingData = buildingData;
        }

        #endregion


        #region ICycleEnd

        public virtual void OnCycleEnd()
        {
            MessageLogger.Log($"Building {BuildingData.BuildingName} ended cycle");
            //TODO
        }

        #endregion
    }
}