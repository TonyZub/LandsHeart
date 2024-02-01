using Extensions;
using UnityEngine;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "Items", menuName = "Data/Gameplay/Items")]
	public sealed class Items : ScriptableObject
	{
        #region Fields

#if UNITY_EDITOR
        [ArrayElementTitle("_itemName")]
#endif
        [SerializeField] private ItemData[] _items;

        #endregion


        #region Methods

        public ItemData GetItemByName(ItemsNames itemName)
        {
            return _items.FirstWhich(x => x.ItemName == itemName);
        }

        #endregion
    }
}