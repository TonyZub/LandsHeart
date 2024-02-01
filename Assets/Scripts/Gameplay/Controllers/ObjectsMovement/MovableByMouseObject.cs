using UnityEngine;


namespace LandsHeart
{
	public sealed class MovableByMouseObject : InteractiveObjectModel
    {
        #region Fields

        [SerializeField] private MovableObjectTypes _movableType;

        #endregion


        #region Properties

        public MovableObjectTypes MovableType => _movableType;

        #endregion
    }
}