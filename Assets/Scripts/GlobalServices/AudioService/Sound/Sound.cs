using UnityEngine;
using System;


namespace LandsHeart
{
    [Serializable]
    public class Sound
    {
        #region Fields

        [SerializeField] private AudioClip _soundClip;
        [SerializeField] [Range(0f, 1f)] private float _volume;
        [SerializeField] [Range(-3f, 3f)] private float _pitch;
        [SerializeField] private bool _isLooped;

        #endregion


        #region Properties

        public AudioClip SoundClip => _soundClip;
        public float Volume => _volume;
        public float Pitch => _pitch;
        public bool IsLooped => _isLooped;

        #endregion
    }
}

