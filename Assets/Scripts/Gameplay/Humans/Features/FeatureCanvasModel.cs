using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace LandsHeart
{
	public sealed class FeatureCanvasModel : MonoBehaviour
	{
		#region Fields

		[SerializeField] private Image _image;
		[SerializeField] private TMP_Text _description;

		#endregion


		#region Methods

		public void SetData(Feature feature)
		{
			
		}

		#endregion
	}
}