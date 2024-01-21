using System;
using UnityEngine;


namespace LandsHeart
{
	public sealed class MovableByMouseObject : MonoBehaviour
	{
        #region Events

        public event Action MouseEntered;
        public event Action MouseExited;
        public event Action MouseDown;
        public event Action MouseUp;

        #endregion


        #region Fields


        #endregion


        #region Properties

        #endregion


        #region UnityMethods

        private void OnMouseEnter()
        {
            MouseEntered?.Invoke();
        }

        private void OnMouseDown()
        {
            MouseExited?.Invoke();
        }

        private void OnMouseUp()
        {
            MouseUp?.Invoke();
        }

        private void OnMouseExit()
        {
            MouseDown?.Invoke();
        }

        #endregion


        #region Methods

        #endregion
    }
}