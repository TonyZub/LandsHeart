using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace LandsHeart
{
	public sealed class ItemCanvasModel : MonoBehaviour
	{
		#region Fields

		[SerializeField] private Image _image;
		[SerializeField] private TMP_Text _itemName;
		[SerializeField] private TMP_Text _description;

		#endregion


		#region Methods

		public void SetData(Item item)
		{
			_image.sprite = item.ItemData.ItemSprite;
			_itemName.text = item.ItemData.ItemNameLocalized.GetLocalizedValue();
			_description.text = item.ItemData.ItemDescription.GetLocalizedValue();
		}

		#endregion
	}
}