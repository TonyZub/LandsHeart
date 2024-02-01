using System;
using UnityEngine;


namespace LandsHeart
{
	[Serializable]
	public struct ItemData
	{
		#region Fields

		[SerializeField] private ItemsNames _itemName;
		[SerializeField] private Sprite _itemSprite;
		[SerializeField] private LocalizationDataHolder _itemNameLocalized;
		[SerializeField] private LocalizationDataHolder _itemDescription;

		#endregion


		#region Properties

		public ItemsNames ItemName => _itemName;
		public Sprite ItemSprite => _itemSprite;
		public LocalizationDataHolder ItemNameLocalized => _itemNameLocalized;
		public LocalizationDataHolder ItemDescription => _itemDescription;

		#endregion
	}
}