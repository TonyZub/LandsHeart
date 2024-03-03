using Cinemachine;
using UnityEngine;


namespace LandsHeart
{
	public sealed class CameraService
	{
        #region Constants

        private const int ACTIVE_CAMERA_PRIORITY = 10;

        #endregion


        #region Fields

        private readonly CameraHolder _holder;
        private CinemachineVirtualCamera[] _cameras;

        #endregion


        #region Properties

        public Camera MainCamera => _holder.MainCamera;

        #endregion


        #region Constructor

        public CameraService(CameraHolder holder)
        {
            _holder = holder;
            CreateCamerasArray();
            SetActiveCamera(CameraNames.TableDown);
        }

        #endregion


        #region Methods

        private void CreateCamerasArray()
        {
            _cameras = new CinemachineVirtualCamera[3]
            {
                _holder.WallCamera,
                _holder.TableDownCamera,
                _holder.BuildingBookCamera,
            };
        }

        public void SetActiveCamera(CameraNames cameraName)
        {
            SetAllCamerasSamePriority();
            _cameras[(int)cameraName].Priority = ACTIVE_CAMERA_PRIORITY;
        }

        private void SetAllCamerasSamePriority()
        {
            foreach (var camera in _cameras)
            {
                camera.Priority = 0;
            }
        }

        #endregion
    }
}