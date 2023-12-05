using UnityEngine.Rendering.PostProcessing;


namespace LandsHeart
{
    public sealed class PostProcessService : IDispose
	{
        #region Fields

        private readonly PostProcessServiceData _serviceData;
        private readonly PostProcessVolume _volume;

        private AutoExposure _volumeAutoExposure;
        private Vignette _vignette;

        private float _baseExposureCompensation;
        private float _vignetteDifference;

        #endregion


        #region Properties

        public float BaseExposureCompensation => _baseExposureCompensation;

        #endregion


        #region Constructor

        public PostProcessService(PostProcessServiceData serviceData)
        {
            _serviceData = serviceData;
            _volume = ObjectFinder.FindObjectOfType<PostProcessVolume>();
            SetSceneVolumeProfile();
            GetDependencies();
        }

        #endregion


        #region IDispose

        public void OnDispose()
        {
        }

        #endregion


        #region Private Methods

        private void SetSceneVolumeProfile()
        {
            switch (SceneStateMachine.Instance.CurrentSceneState.SceneName)
            {
                case SceneNames.Bootstrap:
                    break;
                case SceneNames.MainScene:
                    _volume.profile = _serviceData.Lvl_1_Profile;
                    break;
                default:
                    break;
            }
        }

        private void GetDependencies()
        {
            _volumeAutoExposure = _volume.sharedProfile.GetSetting<AutoExposure>();
            _baseExposureCompensation = _volumeAutoExposure.keyValue.value;
            _vignette = _volume.sharedProfile.GetSetting<Vignette>();
            _vignetteDifference = _serviceData.MaxVignetteIntencity - _serviceData.DefaultVignetteIntencity;
        }

        public void SetExposureCompensation(float exposureCompensation)
        {
            _volumeAutoExposure.keyValue.value = exposureCompensation;
        }

        public void ResetExposureCompensation()
        {
            _volumeAutoExposure.keyValue.value = _baseExposureCompensation;
        }
        
        public void SetVignetteIntencity(float ratio)
        {
            _vignette.intensity.value = _serviceData.MaxVignetteIntencity - _vignetteDifference * ratio;
        }

        public void ResetVignetteIntencity()
        {
            _vignette.intensity.value = _serviceData.DefaultVignetteIntencity;
        }

        #endregion
    }
}
