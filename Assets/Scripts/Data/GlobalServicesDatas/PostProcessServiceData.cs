using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "PostProcessServiceData", menuName = "Data/GlobalServiceDatas/PostProcessServiceData")]
	public sealed class PostProcessServiceData : ScriptableObject
	{
		#region Fields

		[SerializeField] private PostProcessProfile _lvl_1_profile;
		[SerializeField] private float _defaultVignetteIntencity = 0.6f;
		[SerializeField] private float _maxVignetteIntencity = 0.8f;

		#endregion


		#region Properties

		public PostProcessProfile Lvl_1_Profile => _lvl_1_profile;
		public float DefaultVignetteIntencity => _defaultVignetteIntencity;
		public float MaxVignetteIntencity => _maxVignetteIntencity;

		#endregion
	}
}
