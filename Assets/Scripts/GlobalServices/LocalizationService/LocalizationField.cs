using UnityEngine;
using TMPro;
using NaughtyAttributes;


namespace LandsHeart
{
	[RequireComponent(typeof(TMP_Text))]
	public class LocalizationField : MonoBehaviour
	{
		#region Fields

		[SerializeField] private LocalizationDataHolder _localizationData;

		private LocalizationService _localizationService;
		private TMP_Text _textField;

		#endregion


		#region Properties

		public LocalizationDataHolder LocalizationData => _localizationData;
		
		#endregion


		#region UnityMethods

#if UNITY_EDITOR
		private void OnValidate()
        {
			UpdateTextFieldEditor();
		}
#endif

		private void Awake()
        {
			_textField = GetComponent<TMP_Text>();
			_localizationService = GlobalContext.Instance.GlobalServices.LocalizationService;
			SubscribeEvents();
			UpdateTextFieldGame();
		}

        private void OnDestroy()
        {
			UnsubscribeEvents();
		}

		#endregion


		#region Methods

		private void SubscribeEvents()
        {
			_localizationService.LanguageChanged += UpdateTextFieldGame;
		}

		private void UnsubscribeEvents()
        {
			_localizationService.LanguageChanged -= UpdateTextFieldGame;
		}

		[Button("Preset value")]
		private void UpdateTextFieldEditor()
        {
			if (_localizationData.LocalePart == null || _localizationData.LocalePart.Equals(string.Empty) ||
				_localizationData.LocaleKey == null || _localizationData.LocaleKey.Equals(string.Empty)) return;
			GetComponent<TMP_Text>().text = GetLocalizedText();
		}
 
		private void UpdateTextFieldGame()
        {
			_textField.text = GetLocalizedText();
		}

		protected virtual string GetLocalizedText()
        {
			return _localizationData.GetLocalizedValue();
		}

		#endregion
	}
}