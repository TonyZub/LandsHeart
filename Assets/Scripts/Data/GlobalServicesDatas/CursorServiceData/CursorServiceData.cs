using UnityEngine;
using Extensions;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "CursorServiceData", menuName = "Data/GlobalServiceDatas/CursorServiceData")]
	public sealed class CursorServiceData : ScriptableObject
	{
		#region Fields

		[SerializeField] private ParticleSystem _clickParticles;
		[SerializeField] private float _cursorSizePartOfScreen;
#if UNITY_EDITOR
		[ArrayElementTitle("_type")]
#endif
		[SerializeField] private CursorPreset[] _cursorPresets;

		#endregion


		#region Properties

		public ParticleSystem ClickParticles => _clickParticles;
		public float CursorSizePartOfScreen => _cursorSizePartOfScreen;
		public CursorPreset[] CursorPresets => _cursorPresets;

        #endregion


        #region Methods

		public CursorPreset GetPresetByType(CursorTypes type)
        {
			return CursorPresets.FirstWhich(x => x.Type == type);
        }

        #endregion
    }
}