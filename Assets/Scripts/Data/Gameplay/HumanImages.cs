using UnityEngine;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "HumanImages", menuName = "Data/Gameplay/HumanImages")]
	public sealed class HumanImages : ScriptableObject
	{
		#region Fields

		[SerializeField] private Sprite[] _menSprites;
		[SerializeField] private Sprite[] _womenSprites;

		#endregion


		#region Properties

		public Sprite[] MenSprites => _menSprites;
		public Sprite[] WomenSprites => _womenSprites;

		#endregion
	}
}