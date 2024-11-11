using UnityEngine;


namespace LandsHeart
{
	public sealed class BuildingBookCanvasModel : MonoBehaviour
	{
        #region Fields

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _scrollContent;

		#endregion


		#region Properties

		public CanvasGroup CanvasGroup => _canvasGroup;
		public Transform ScrollContent => _scrollContent;

		#endregion
	}
}