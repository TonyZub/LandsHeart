namespace LandsHeart
{
	public sealed class GlobalServices : NonMonoSingleton<GlobalServices>
	{
        #region Fields

        //private readonly CursorService _cursorService;
        private readonly LocalizationService _localizationService;
        private readonly AudioService _audioService;
        private readonly AudioMixerService _audioMixerService;

        #endregion


        #region Properties

        //public CursorService CursorService => _cursorService;
        public LocalizationService LocalizationService => _localizationService;
        public AudioService AudioService => _audioService;
        public AudioMixerService AudioMixerService => _audioMixerService;

        #endregion


        #region Constructor

        public GlobalServices()
        {
            //_cursorService = new CursorService(Data.CursorServiceData);
            _localizationService = new LocalizationService();
            _audioService = new AudioService(Data.AudioServiceData, ObjectFinder.FindObjectOfType<AudioSourcePool>(true));
            _audioMixerService = new AudioMixerService(Data.AudioServiceData.MainMixer);
        }

        #endregion


        #region IDispose

        public void OnDispose()
        {
            _audioService.OnDispose();
        }

        #endregion
    }
}