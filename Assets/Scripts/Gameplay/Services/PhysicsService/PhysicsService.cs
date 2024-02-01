using UnityEngine;


namespace LandsHeart
{
	public sealed class PhysicsService
	{
        #region Fields

        private readonly PhysicsServiceData _data;

        #endregion


        #region Properties

        public PhysicsServiceData Data => _data;

        #endregion


        #region Constructor

        public PhysicsService(PhysicsServiceData serviceData)
        {
            _data = serviceData;
        }

        #endregion


        #region Methods

        public bool DoRaycast(Vector3 from, Vector3 direction, out RaycastHit rayHit, float distance, LayerMask layerMask)
        {
            return Physics.Raycast(from, direction, out rayHit, distance, layerMask);
        }

        public bool DoRaycast(Ray ray, out RaycastHit rayHit, float distance, LayerMask layerMask)
        {
            return Physics.Raycast(ray, out rayHit, distance, layerMask);
        }

        public bool TryGetGroundedPosition(Vector3 checkingPosition, LayerMask groundLayer, out Vector3 groundedPosition)
        {
            var isHitted = DoRaycast(new Vector3(checkingPosition.x, checkingPosition.y, checkingPosition.z), Vector3.down,
                out RaycastHit hit, float.MaxValue, groundLayer);
            groundedPosition = isHitted ? hit.point : checkingPosition;
            return isHitted;
        }

        public Vector3 GetLocalGroundedPosition(Vector3 position, LayerMask groundLayer, float distanceToSearch)
        {
            if (DoRaycast(position, Vector3.down * distanceToSearch,
                out RaycastHit hit, distanceToSearch, groundLayer))
            {
                return hit.point;
            }
            else
            {
                return position;
            }
        }

        #endregion
    }
}