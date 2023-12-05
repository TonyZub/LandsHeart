using System;
using UnityEngine;

namespace LandsHeart
{
	public struct SaveData
	{
        #region Fields

        [SerializeField] private SceneStateNames _sceneStateName;
        [SerializeField] private DateTime _time;

        #endregion


        #region Properties

        public SceneStateNames SceneStateName => _sceneStateName;
        public DateTime Time => _time;

        #endregion


        #region Constructor

        /// <summary>
        /// In scene save
        /// </summary>
        public SaveData(SceneStateNames sceneStateName)
        {
            _sceneStateName = sceneStateName;
            _time = DateTime.Now;
        }

        #endregion
    }
}