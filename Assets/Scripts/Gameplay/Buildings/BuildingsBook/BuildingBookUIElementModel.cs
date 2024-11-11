using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace LandsHeart
{
	public sealed class BuildingBookUIElementModel : MonoBehaviour
	{
		#region Fields

		[SerializeField] private Image _buildingImage;
		[SerializeField] private TMP_Text _buildingName;
		[SerializeField] private TMP_Text _buildingPrice;

		#endregion


		#region Properties

		public Image BuildingImage => _buildingImage;
		public TMP_Text BuildingName => _buildingName;
		public TMP_Text BuildingPrice => _buildingPrice;

		#endregion
	}
}