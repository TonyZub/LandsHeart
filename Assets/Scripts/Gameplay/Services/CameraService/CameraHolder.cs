using Cinemachine;
using UnityEngine;


namespace LandsHeart
{
	public sealed class CameraHolder : MonoBehaviour
	{
		#region Fields

		[SerializeField] private Camera _mainCamera;
		[Space]
		[SerializeField] private CinemachineVirtualCamera _wallCamera;
		[SerializeField] private CinemachineVirtualCamera _tableDownCamera;
		[SerializeField] private CinemachineVirtualCamera _buildingBookCamera;

		#endregion


		#region Properties

		public Camera MainCamera => _mainCamera;

		public CinemachineVirtualCamera WallCamera => _wallCamera;
		public CinemachineVirtualCamera TableDownCamera => _tableDownCamera;
		public CinemachineVirtualCamera BuildingBookCamera => _buildingBookCamera;

		#endregion
    }
}