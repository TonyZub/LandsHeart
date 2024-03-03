using System;
using UnityEngine;


namespace LandsHeart
{
	public sealed class GameCycleController : IDisposable
	{
        #region Fields

        private CycleEndObjectModel[] _cycleEndObjects;


        #endregion


        #region Properties

        #endregion


        #region Constructor

        public GameCycleController()
        {
            _cycleEndObjects = ObjectFinder.FindObjectsOfType<CycleEndObjectModel>(true);
            SubscribeEvents();
        }

        #endregion


        #region IDisposable

        public void Dispose()
        {
            UnsubscribeEvents();
        }

        #endregion


        #region Methods

        private void SubscribeEvents()
        {
            foreach (var obj in _cycleEndObjects)
            {
                obj.MouseUpAsButton += OnCycleEndObjectMouseDownAsButton;
            }
        }

        private void UnsubscribeEvents()
        {
            foreach (var obj in _cycleEndObjects)
            {
                obj.MouseUpAsButton -= OnCycleEndObjectMouseDownAsButton;
            }
        }

        private void OnCycleEndObjectMouseDownAsButton(InteractiveObjectModel obj)
        {
            MessageLogger.Log("Cycle End");
        }

        #endregion
    }
}