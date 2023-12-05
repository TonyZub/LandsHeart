using UnityEngine;
using Extensions;
using UnityEngine.Audio;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "AudioServiceData", menuName = "Data/GlobalServiceDatas/AudioServiceData")]
	public sealed class AudioServiceData : ScriptableObject
	{
        #region Fields

        [SerializeField] private AudioMixer _mainMixer;

#if UNITY_EDITOR
        [ArrayElementTitle("_soundName")]
#endif
        [SerializeField] private MusicSound[] _musicSounds;
#if UNITY_EDITOR
        [ArrayElementTitle("_soundName")]
#endif
        [SerializeField] private AmbientSound[] _ambientSounds;
#if UNITY_EDITOR
        [ArrayElementTitle("_soundName")]
#endif
        [SerializeField] private EffectSound[] _effectSounds;

        #endregion


        #region Properties

        public AudioMixer MainMixer => _mainMixer;
        public MusicSound[] MusicSounds => _musicSounds;
        public AmbientSound[] AmbienceSounds => _ambientSounds;
        public EffectSound[] EffectsSounds => _effectSounds;

        #endregion


        #region Public Methods

        public MusicSound GetMusicSoundByName(MusicSoundNames soundName)
        {
            return MusicSounds.FirstWhich(x => x.SoundName == soundName);
        }

        public AmbientSound GetAmbienceSoundByName(AmbientSoundNames soundName)
        {
            return AmbienceSounds.FirstWhich(x => x.SoundName == soundName);
        }

        public EffectSound GetEffectSoundByName(EffectSoundNames soundName)
        {
            return EffectsSounds.FirstWhich(x => x.SoundName == soundName);
        }

        #endregion
    }
}
