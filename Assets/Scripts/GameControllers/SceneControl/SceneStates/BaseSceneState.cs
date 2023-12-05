using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


namespace LandsHeart
{
	public abstract class BaseSceneState
	{
		#region Constants

		private const float SCENE_MAX_PROGRESS = 0.9f;
		protected const float SCENE_MUSIC_SWITCH_TIME = 1f;

        #endregion


        #region Events

        public event Action OnSceneLoadingStarted;
		public event Action OnSceneLoadingEnded;

		#endregion


		#region Fields

		private AsyncOperation _sceneLoadingOperation;
		private readonly SceneStateNames _stateName;
		private readonly SceneNames _sceneName;
		private Tween _sceneLoadingTween;

		#endregion


		#region Properties

		public SceneStateNames StateName => _stateName;
		public SceneNames SceneName => _sceneName;
		public float SceneLoadingProgress => _sceneLoadingOperation == null ? 0f : 
			_sceneLoadingOperation.progress / SCENE_MAX_PROGRESS;
		public bool IsActiveState { get; private set; }

        #endregion


        #region Constuctor

		public BaseSceneState(SceneStateNames stateName, SceneNames sceneName)
        {
			_stateName = stateName;
			_sceneName = sceneName;
        }

        #endregion


        #region Methods

		private void SubscribeEvents()
        {
			SceneStateMachine.Instance.SceneLoadingCanvasController.MinTimeToLoadSceneEnded += OnMinTimeToLoadSceneEnded;
		}

		private void UnsubscribeEvents()
        {
			SceneStateMachine.Instance.SceneLoadingCanvasController.MinTimeToLoadSceneEnded -= OnMinTimeToLoadSceneEnded;
		}

        private void LoadSceneAsync(float delayBeforeSceneLoadingStart)
        {
			OnSceneLoadingStarted?.Invoke();
			_sceneLoadingTween = DOVirtual.DelayedCall(delayBeforeSceneLoadingStart, StartSceneLoading);
		}

		private void LoadScene()
		{
            OnSceneLoadingStarted?.Invoke();
            SceneManager.LoadScene((int)SceneName);
			GlobalController.Instance.OnUpdate += WaitForDirectSceneLoad;
        }

		private void WaitForDirectSceneLoad()
		{
            GlobalController.Instance.OnUpdate -= WaitForDirectSceneLoad;
            OnSceneLoadingComplete();
        }

		private void StartSceneLoading()
        {
			_sceneLoadingOperation = SceneManager.LoadSceneAsync((int)SceneName);
			_sceneLoadingOperation.allowSceneActivation = false;
			_sceneLoadingOperation.completed += OnAfterSceneLoadingEnded;
		}

		private void OnMinTimeToLoadSceneEnded()
        {
			if(IsActiveState) _sceneLoadingOperation.allowSceneActivation = true;
		} 

		private void OnAfterSceneLoadingEnded(AsyncOperation operation)
        {
			operation.completed -= OnAfterSceneLoadingEnded;
			OnSceneLoadingComplete();
		}

		protected virtual void OnSceneLoadingComplete()
        {
			OnSceneLoadingEnded?.Invoke();
        }

		public virtual void EnterState(bool doLoadScene = true)
        {
    //        GlobalContext.Instance.GlobalServices.AudioService.ChangeMusic(MusicSoundNames.None,
				//SCENE_MUSIC_SWITCH_TIME);
            IsActiveState = true;
			if (doLoadScene)
			{
                SubscribeEvents();
                LoadSceneAsync(SceneLoadingCanvasController.CANVAS_SHOWING_TIME);
            }
        }

		public virtual void EnterStateWithoutLoading()
		{
            //GlobalContext.Instance.GlobalServices.AudioService.ChangeMusic(MusicSoundNames.None, 0f);
            IsActiveState = true;
			LoadScene();
        }

		public virtual void ExitState()
        {
			UnsubscribeEvents();
			IsActiveState = false;
        }

		#endregion
	}
}