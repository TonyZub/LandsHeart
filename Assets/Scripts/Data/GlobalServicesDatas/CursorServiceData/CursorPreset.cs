using UnityEngine;
using System;


namespace LandsHeart
{
	[Serializable]
	public struct CursorPreset
	{
		#region Fields

		[SerializeField] private CursorTypes _type;
		[SerializeField] private Sprite _sprite;
		[SerializeField] private Vector2 _pointOffset;

		#endregion


		#region Properties

		public CursorTypes Type => _type;
		public Sprite Sprite => _sprite;
		public Vector2 PointOffset => _pointOffset;

		#endregion
	}
}