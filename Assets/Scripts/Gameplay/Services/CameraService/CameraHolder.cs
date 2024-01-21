using UnityEngine;


namespace LandsHeart
{
	public sealed class CameraHolder : MonoBehaviour
	{
		#region Fields

		[SerializeField] private Camera _mainCamera;

		#endregion


		#region Properties

		public Camera MainCamera => _mainCamera;

		#endregion
	}
}