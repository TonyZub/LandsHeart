using System;
using DG.Tweening;


namespace LandsHeart
{
	public sealed class SceneLoadingCanvasController
	{
        #region Constants

        public const float CANVAS_SHOWING_TIME = 0.5f;

        private const float MIN_CANVAS_ALPHA = 0f;
        private const float MAX_CANVAS_ALPHA = 1f;
        private const float MIN_TIME_TO_LOAD_SCENE = 1f;

        #endregion


        #region Events

        public event Action MinTimeToLoadSceneEnded;

        #endregion


        #region Fields

        private readonly SceneStateMachine _sceneStateMachine;
        private readonly AudioMixerService _audioMixerService;
        private readonly AudioService _audioService;
        private SceneLoadingCanvasModel _sceneLoadingCanvasModel;
        private Tween _minTimeToLoadSceneCounter;
        private Tween _smoothProgressUpdaterShower;
        private float _actualSceneLoadingProgress;

        #endregion


        #region Constructor

        public SceneLoadingCanvasController(SceneStateMachine sceneStateMachine)
        {
            _sceneStateMachine = sceneStateMachine;
            _audioMixerService = GlobalContext.Instance.GlobalServices.AudioMixerService;
            _audioService = GlobalContext.Instance.GlobalServices.AudioService;
            CreateSceneLoadingCanvasModel();
            SubscribeEvents();
        }

        #endregion


        #region Methods

        private void CreateSceneLoadingCanvasModel()
        {
            _sceneLoadingCanvasModel = (SceneLoadingCanvasModel)PrimitiveFactory.
                Instantiate(Data.UIData.SceneLoadingCanvasModel);
        }

        private void SubscribeEvents()
        {
            _sceneStateMachine.OnSceneStartLoadingAsync += ActivateSceneLoadingProgressUpdater;
            GlobalController.Instance.OnDispose += UnsubscribeEvents;
        }

        private void UnsubscribeEvents()
        {
            _sceneStateMachine.OnSceneStartLoadingAsync -= ActivateSceneLoadingProgressUpdater;
            GlobalController.Instance.OnDispose -= UnsubscribeEvents;
        }

        private void ActivateSceneLoadingProgressUpdater()
        {
            SetZeroSceneLoadingProgressValue();
            SubscriveSceneLoadingProgressUpdater();
            SmoothShowSceneLoadingProgressUpdater(CallEventMinTimeToLoadScene);
        }

        private void CallEventMinTimeToLoadScene()
        {
            _minTimeToLoadSceneCounter = DOVirtual.Float(0, MIN_TIME_TO_LOAD_SCENE, MIN_TIME_TO_LOAD_SCENE,
                SetLoadingProgress).SetEase(Ease.InOutQuad).OnComplete(() => MinTimeToLoadSceneEnded?.Invoke());
        }

        private void SubscriveSceneLoadingProgressUpdater()
        {
            GlobalController.Instance.OnUpdate += UpdateActualSceneLoaingProgress;
            _sceneStateMachine.CurrentSceneState.OnSceneLoadingEnded += UnsubscribeSceneLoadingProgressUpdater;
            _sceneStateMachine.CurrentSceneState.OnSceneLoadingEnded += SmoothHideSceneLoadingProgressUpdater;
        }

        private void UnsubscribeSceneLoadingProgressUpdater()
        {
            GlobalController.Instance.OnUpdate -= UpdateActualSceneLoaingProgress;
            _sceneStateMachine.CurrentSceneState.OnSceneLoadingEnded -= SmoothHideSceneLoadingProgressUpdater;
            _sceneStateMachine.CurrentSceneState.OnSceneLoadingEnded -= UnsubscribeSceneLoadingProgressUpdater;
        }

        private void SetZeroSceneLoadingProgressValue()
        {
            SetActualSceneLoadingProgress(0f);
            _sceneLoadingCanvasModel.SetProgress(0f);
        }

        private void ShowSceneLoadingProgressUpdater()
        {
            _sceneLoadingCanvasModel.Show();
            //_audioService.ChangeAmbience(AmbientSoundNames.SceneSwitchAmbience, CANVAS_SHOWING_TIME);
            //_audioMixerService.DisableOnSceneSound(CANVAS_SHOWING_TIME);
        }

        private void SmoothShowSceneLoadingProgressUpdater(TweenCallback doAfter)
        {
            SetCanvasAlpha(0f);
            ShowSceneLoadingProgressUpdater();
            _smoothProgressUpdaterShower = DOVirtual.Float(MIN_CANVAS_ALPHA, MAX_CANVAS_ALPHA, CANVAS_SHOWING_TIME,
                SetCanvasAlpha).OnComplete(doAfter);
        }

        private void HideSceneLoadingProgressUpdater()
        {
            _sceneLoadingCanvasModel.Hide();
        }

        private void SmoothHideSceneLoadingProgressUpdater()
        {
            //_audioService.ChangeAmbience(AmbientSoundNames.None, CANVAS_SHOWING_TIME);
            //_audioMixerService.EnableOnSceneSound(CANVAS_SHOWING_TIME);
            _smoothProgressUpdaterShower = DOVirtual.Float(MAX_CANVAS_ALPHA, MIN_CANVAS_ALPHA, CANVAS_SHOWING_TIME,
                SetCanvasAlpha).OnComplete(HideSceneLoadingProgressUpdater);
        }

        private void SetCanvasAlpha(float alpha)
        {
            _sceneLoadingCanvasModel.SetCanvasAlpha(alpha);
        }

        private void SetLoadingProgress(float progress)
        {
            _sceneLoadingCanvasModel.SetProgress(progress * _actualSceneLoadingProgress);
        }

        private void UpdateActualSceneLoaingProgress()
        {
            SetActualSceneLoadingProgress(_sceneStateMachine.CurrentSceneState.SceneLoadingProgress);
        }

        private void SetActualSceneLoadingProgress(float progress)
        {
            _actualSceneLoadingProgress = progress;
        }

        #endregion
    }
}