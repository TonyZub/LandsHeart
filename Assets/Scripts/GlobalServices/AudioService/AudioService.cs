using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using PixelCrushers.DialogueSystem;


namespace LandsHeart
{
    public sealed class AudioService : IDispose
    {
        #region Constants

        private const string DISABLE_EFFECTS_SOUNDS_FUNC_NAME = "DisableEffectsSounds";
        private const string ENABLE_EFFECTS_SOUNDS_FUNC_NAME = "EnableEffectsSounds";

        #endregion


        #region Fields

        private readonly AudioServiceData _serviceData;
        private readonly AudioSourcePool _sourcesPool;

        private Queue<SoundQueueElement> _musicQueue;
        private Queue<SoundQueueElement> _ambienceQueue;

        private AudioSource _mainMusicSource;
        private AudioSource _blendingMusicSource;

        private AudioSource _mainAmbienceSource;
        private AudioSource _blendindAmbienceSource;

        private Sequence _blendingMusicSequence;
        private Sequence _blendingAmbienceSequence;

        private MusicSound _cachedMusicSound;
        private AmbientSound _cachedAmbienceSound;
        private EffectSound _cachedEffectSound;

        private bool _areEffectsSoundsEnabled = true;

        #endregion


        #region Constructor

        public AudioService(AudioServiceData serviceData, AudioSourcePool audioSourcePool)
        {
            _serviceData = serviceData;
            _sourcesPool = audioSourcePool;
            _mainMusicSource = _sourcesPool.FirstMusicSource;
            _blendingMusicSource = _sourcesPool.SecondMusicSource;
            _mainAmbienceSource = _sourcesPool.FirstAmbienceSource;
            _blendindAmbienceSource = _sourcesPool.SecondAmbienceSource;
            _musicQueue = new Queue<SoundQueueElement>();
            _ambienceQueue = new Queue<SoundQueueElement>();
            RegisterLuaFunctions();
        }

        #endregion


        #region IDispose

        public void OnDispose()
        {
            UnregisterLuaFunctions();
        }

        #endregion


        #region Methods

        private void RegisterLuaFunctions()
        {
            Lua.RegisterFunction(DISABLE_EFFECTS_SOUNDS_FUNC_NAME, this, SymbolExtensions.GetMethodInfo(() => DisableEffectsSounds()));
            Lua.RegisterFunction(ENABLE_EFFECTS_SOUNDS_FUNC_NAME, this, SymbolExtensions.GetMethodInfo(() => EnableEffectsSounds()));
        }

        private void UnregisterLuaFunctions()
        {
            Lua.UnregisterFunction(DISABLE_EFFECTS_SOUNDS_FUNC_NAME);
            Lua.UnregisterFunction(ENABLE_EFFECTS_SOUNDS_FUNC_NAME);
        }

        public void ChangeMusic(MusicSoundNames soundName, float blendTime = 0f, bool doOverlap = false)
        {
            _cachedMusicSound = _serviceData.GetMusicSoundByName(soundName);
            if (_cachedMusicSound.SoundClip != _mainMusicSource.clip)
            {
                _musicQueue.Enqueue(new SoundQueueElement(_cachedMusicSound, blendTime, doOverlap));
                PlayNextMusicInList();
            }
        }

        private void PlayNextMusicInList()
        {
            if (_blendingMusicSequence == null || (!_blendingMusicSequence.IsPlaying() && _musicQueue.Count > 0))
            {
                BlendMusicFromQueue();
            }
        }

        private void BlendMusicFromQueue()
        {
            SoundQueueElement nextMusicElement = _musicQueue.Peek();
            _blendingMusicSource.clip = nextMusicElement.Sound.SoundClip;
            _blendingMusicSource.pitch = nextMusicElement.Sound.Pitch;
            _blendingMusicSource.loop = true;
            _blendingMusicSource.Play();

            _blendingMusicSequence = DOTween.Sequence();
            _blendingMusicSequence.Append(_mainMusicSource.DOFade(0f, nextMusicElement.BlendTIme));

            if (nextMusicElement.DoOverlap)
            {
                _blendingMusicSequence.Join(_blendingMusicSource.DOFade(nextMusicElement.Sound.Volume,
                    nextMusicElement.BlendTIme));
            }
            else
            {
                _blendingMusicSequence.Append(_blendingMusicSource.DOFade(nextMusicElement.Sound.Volume,
                    nextMusicElement.BlendTIme));
            }

            _blendingMusicSequence.OnComplete(SwitchMusicSources);
        }

        private void SwitchMusicSources()
        {
            _musicQueue.Dequeue();
            (_mainMusicSource, _blendingMusicSource) = (_blendingMusicSource, _mainMusicSource);
            _blendingMusicSource.Stop();
            PlayNextMusicInList();
        }

        public void ChangeAmbience(AmbientSoundNames soundName, float blendTime = 0f, bool doOverlap = false)
        {
            _cachedAmbienceSound = _serviceData.GetAmbienceSoundByName(soundName);
            if (_cachedAmbienceSound.SoundClip != _mainAmbienceSource.clip)
            {
                _ambienceQueue.Enqueue(new SoundQueueElement(_cachedAmbienceSound, blendTime, doOverlap));
                PlayNextAmbienceList();
            }
        }

        private void PlayNextAmbienceList()
        {
            if (_blendingAmbienceSequence == null || (!_blendingAmbienceSequence.IsPlaying() && _ambienceQueue.Count > 0))
            {
                BlendAmbienceFromQueue();
            }
        }

        private void BlendAmbienceFromQueue()
        {
            SoundQueueElement nextSFXElement = _ambienceQueue.Peek();
            _blendindAmbienceSource.clip = nextSFXElement.Sound.SoundClip;
            _blendindAmbienceSource.pitch = nextSFXElement.Sound.Pitch;
            _blendindAmbienceSource.loop = nextSFXElement.Sound.IsLooped;
            _blendindAmbienceSource.Play();

            _blendingAmbienceSequence = DOTween.Sequence();
            _blendingAmbienceSequence.Append(_mainAmbienceSource.DOFade(0f, nextSFXElement.BlendTIme));

            if (nextSFXElement.DoOverlap)
            {
                _blendingAmbienceSequence.Join(_blendindAmbienceSource.DOFade(nextSFXElement.Sound.Volume,
                    nextSFXElement.BlendTIme));
            }
            else
            {
                _blendingAmbienceSequence.Append(_blendindAmbienceSource.DOFade(nextSFXElement.Sound.Volume,
                    nextSFXElement.BlendTIme));
            }

            _blendingAmbienceSequence.OnComplete(SwitchAmbienceSources);
        }

        private void SwitchAmbienceSources()
        {
            _ambienceQueue.Dequeue();
            (_mainAmbienceSource, _blendindAmbienceSource) = (_blendindAmbienceSource, _mainAmbienceSource);
            _blendindAmbienceSource.Stop();
            PlayNextAmbienceList();
        }

        public void PlayEffectSoundInLeftEar(EffectSoundNames soundName)
        {
            PlayEffectSound(_sourcesPool.LeftEffectSources, _serviceData.GetEffectSoundByName(soundName));
        }

        public void PlayEffectSoundInCenter(EffectSoundNames soundName)
        {
            PlayEffectSound(_sourcesPool.CenterEffectSources, _serviceData.GetEffectSoundByName(soundName));
        }

        public void PlayEffectSoundInRightEar(EffectSoundNames soundName)
        {
            PlayEffectSound(_sourcesPool.RightEffectSources, _serviceData.GetEffectSoundByName(soundName));
        }

        private void PlayEffectSound(AudioSource[] sources, Sound sound)
        {
            if (!_areEffectsSoundsEnabled) return;
            for (int i = 0; i < sources.Length; i++)
            {
                if (!sources[i].isPlaying)
                {
                    sources[i].clip = sound.SoundClip;
                    sources[i].volume = sound.Volume;
                    sources[i].pitch = sound.Pitch;
                    sources[i].loop = sound.IsLooped;
                    sources[i].Play();
                    return;
                }
            }
        }

        public void DisableEffectsSounds()
        {
            _areEffectsSoundsEnabled = false;
        }

        public void EnableEffectsSounds()
        {
            _areEffectsSoundsEnabled = true;
        }

        #endregion
    }
}
