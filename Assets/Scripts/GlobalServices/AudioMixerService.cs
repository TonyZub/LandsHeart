using DG.Tweening;
using UnityEngine.Audio;


namespace LandsHeart
{
	public sealed class AudioMixerService
	{
        #region Constants

        private const string ON_SCENE_VOLUME_PAR_NAME = "OnSceneVolume";
        private const string TOTAL_VOLUME_PAR_NAME = "TotalVolume";
        private const string MUSIC_VOLUME_PAR_NAME = "MusicVolume";
        private const string MICROPHONE_VOLUME_PAR_NAME = "MicrophoneVolume";
        private const string MICROPHONE_PITCH_PAR_NAME = "MicrophonePitch";
        private const string MICROPHONE_EFFECT_PAR_NAME = "MicrophoneEffect";

        private const float MIN_VOLUME_VALUE = -80f;
        private const float NORMAL_VOLUME_VALUE = 0f;

        #endregion


        #region Fields

        private readonly AudioMixer _audioMixer;

        private Tween _onSceneAudioVolumeTween;

        #endregion


        #region Constructor

        public AudioMixerService(AudioMixer mixer)
        {
            _audioMixer = mixer;
        }

        #endregion


        #region Methods

        public void DisableOnSceneSound(float duration)
        {
            if (_onSceneAudioVolumeTween != null && _onSceneAudioVolumeTween.IsPlaying()) _onSceneAudioVolumeTween.Complete();
            _onSceneAudioVolumeTween = _audioMixer.DOSetFloat(ON_SCENE_VOLUME_PAR_NAME, MIN_VOLUME_VALUE, duration);
        }

        public void EnableOnSceneSound(float duration)
        {
            if (_onSceneAudioVolumeTween != null && _onSceneAudioVolumeTween.IsPlaying()) _onSceneAudioVolumeTween.Complete();
            _onSceneAudioVolumeTween = _audioMixer.DOSetFloat(ON_SCENE_VOLUME_PAR_NAME, NORMAL_VOLUME_VALUE, duration);
        }

        public void SetTotalVolume(float volume)
        {
            _audioMixer.SetFloat(TOTAL_VOLUME_PAR_NAME, (1 - volume) * MIN_VOLUME_VALUE);
        }

        public void SetMusicVolume(float volume)
        {
            _audioMixer.SetFloat(MUSIC_VOLUME_PAR_NAME, (1 - volume) * MIN_VOLUME_VALUE);
        }

        public void SetMicrophoneLayerParameters(float volume, float pitch, float effect)
        {
            _audioMixer.SetFloat(MICROPHONE_VOLUME_PAR_NAME, volume);
            _audioMixer.SetFloat(MICROPHONE_PITCH_PAR_NAME, pitch);
            _audioMixer.SetFloat(MICROPHONE_EFFECT_PAR_NAME, effect);
        }

        #endregion
    }
}