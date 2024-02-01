namespace LandsHeart
{
	public abstract class Item : ICycleEnd
	{
		#region Fields

		private readonly ItemData _itemData;

		#endregion


		#region Properties

		public ItemData ItemData => _itemData;

		#endregion


		#region Constructor

		public Item(ItemData itemData)
		{
			_itemData = itemData;
		}

        #endregion


        #region ICycleEnd

		public virtual void OnCycleEnd()
		{
            MessageLogger.Log($"Item {ItemData.ItemName} ended cycle");
            //TODO
        }

        #endregion
    }
}