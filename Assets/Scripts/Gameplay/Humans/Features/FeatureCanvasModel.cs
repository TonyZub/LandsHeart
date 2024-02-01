using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace LandsHeart
{
	public sealed class FeatureCanvasModel : MonoBehaviour
	{
		#region Fields

		[SerializeField] private Image _image;
		[SerializeField] private TMP_Text _featureName;
		[SerializeField] private TMP_Text _description;

		#endregion


		#region Methods

		public void SetData(Feature feature)
		{
			_image.sprite = feature.FeatureData.FeatureSprite;
			_featureName.text = feature.FeatureData.FeatureNameLocalized.GetLocalizedValue();
			_description.text = feature.FeatureData.FeatureDescription.GetLocalizedValue();
		}

		#endregion
	}
}