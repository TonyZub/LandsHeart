using NaughtyAttributes;
using UnityEngine;
using System;


namespace LandsHeart
{
	[Serializable]
	public sealed class LocalizationDataHolder
	{
		#region Fields

		[Dropdown("LocaleParts")]
		[OnValueChanged("UpdateKeysArrayInInspector")]
		[SerializeField] private string _localePart = "";

		[HideIf(nameof(isLocalePartEmpty))]
		[Dropdown("LocaleKeys")]
		[OnValueChanged("UpdateLocaleDescription")]
		[SerializeField] private string _localeKey = "";

		[SerializeField] private string _localeDescription = "";

		#endregion


		#region Properties

		public string[] LocaleParts => Data.LocalizationData.GetLocaleParts();

		public string[] LocaleKeys => _localePart.Equals(string.Empty) ? new string[1] { "" } :
			Data.LocalizationData.GetLocaleKeys(_localePart);

		public string LocalePart => _localePart;
		public string LocaleKey => _localeKey;
		public string LocaleDescription => _localeDescription;

		private bool isLocalePartEmpty => _localePart.Equals(string.Empty);

        #endregion


		#region Methods

		public string GetLocalizedValue()
		{
			return Data.LocalizationData.GetLocaleByKey(_localeKey).LocalesArray[LocalesHelper.SelectedLocaleIndex];
		}

		private void UpdateKeysArrayInInspector()
		{
			if (_localePart.Equals(string.Empty)) _localePart = LocaleParts[0];
            if (LocaleKey.Equals(string.Empty))
            {
				_localeKey = LocaleKeys[0];
			}		
			UpdateLocaleDescription();
		}

		private void UpdateLocaleDescription()
		{
			var locale = Data.LocalizationData.GetLocaleByKey(_localeKey);
			_localeDescription = locale.Info;
		}

		#endregion
	}
}