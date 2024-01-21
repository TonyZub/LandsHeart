using UnityEngine;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "HumanGeneratingData", menuName = "Data/Gameplay/HumanGeneratingData")]
	public class HumanGeneratingData : ScriptableObject
	{
		#region Fields

		[SerializeField] private string[] _nameKeys;
		[SerializeField] private string[] _surnamePrefixKeys;
		[SerializeField] private string[] _surnamePostfixKeys;


		#endregion


		#region Properties

		public string[] NameKeys => _nameKeys;
		public string[] SurnamePrefixKeys => _surnamePrefixKeys;
		public string[] SurnamePostfixKeys => _surnamePostfixKeys;

		#endregion
	}
}