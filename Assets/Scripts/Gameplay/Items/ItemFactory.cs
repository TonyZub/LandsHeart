using System;
using UnityEngine;


namespace LandsHeart
{
	public sealed class ItemFactory
	{
        #region Methods

        public Item CreateItem(ItemsNames itemName) => itemName switch
        {
            ItemsNames.None => null,
            _ => throw new ArgumentException($"There is no item with name {itemName}")
        };

        public ItemCanvasModel CreateItemCanvasModel(Item item, Transform parent)
        {
            var newItemCanvasModel = GameObject.Instantiate(Data.UIData.ItemCanvasModel, parent);
            newItemCanvasModel.SetData(item);
            return newItemCanvasModel;
        }

        #endregion
    }
}