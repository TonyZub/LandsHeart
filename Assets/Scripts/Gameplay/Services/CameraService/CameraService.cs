using UnityEngine;


namespace LandsHeart
{
	public sealed class CameraService
	{
        #region Fields

        private readonly CameraHolder _holder;

        #endregion


        #region Properties

        public Camera MainCamera => _holder.MainCamera;

        #endregion


        #region Constructor

        public CameraService(CameraHolder holder)
        {
            _holder = holder;
        }

        #endregion
    }
}