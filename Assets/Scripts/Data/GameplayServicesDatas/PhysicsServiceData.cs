using UnityEngine;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "PhysicsServiceData", 
		menuName = "Data/GameplayServicesDatas/PhysicsServiceData")]
	public sealed class PhysicsServiceData : ScriptableObject
	{
		#region Fields

		[SerializeField] private LayerMask _tableLayer;
		[SerializeField] private LayerMask _mobavleObjectsLayers;

		#endregion


		#region Properties

		public LayerMask TableLayer => _tableLayer;
		public LayerMask MovableObjectsLayers => _mobavleObjectsLayers;

		#endregion
	}
}