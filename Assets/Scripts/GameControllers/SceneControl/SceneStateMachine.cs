using System;
using Extensions;


namespace LandsHeart
{
	public sealed class SceneStateMachine : NonMonoSingleton<SceneStateMachine>
    {
        #region Events

        public event Action OnSceneStateChanged;
        public event Action OnSceneStartLoadingAsync;

        #endregion


        #region Fields

        private SceneLoadingCanvasController _sceneLoadingCanvasController;
        private BaseSceneState[] _sceneStates;

        #endregion


        #region Properties

        public BaseSceneState CurrentSceneState { get; private set; }
        public SceneLoadingCanvasController SceneLoadingCanvasController => _sceneLoadingCanvasController;
        public bool IsSceneStatesArrayComplete => _sceneStates != null && _sceneStates.Length > 0;

        #endregion


        #region Constructor

        public SceneStateMachine() : base()
        {
            CreateSceneStates();
            GlobalController.Instance.OnStart += OnStart;
        }

        #endregion


        #region Public Methods

        public void SetState(SceneStateNames stateName)
        {
            SetNextState(stateName, true);
        }

        public void SetStateWithoutLoading(SceneStateNames stateName)
        {
            SetNextState(stateName, false);
        }

        private void SetNextState(SceneStateNames stateName, bool isAsync)
        {
            CheckIfStatesArrayComplete();
            CheckIfCurrentStateIsNotNull();
            CurrentSceneState.ExitState();
            InitSceneSwitch(stateName, isAsync);
        }

        #endregion


        #region Private Methods

        private void OnStart()
        {
            GlobalController.Instance.OnStart -= OnStart;
            CreateSceneLoadingCanvasController();
            StartBootstrap();
        }

        private void CreateSceneStates()
        {
            _sceneStates = new BaseSceneState[2]
            {
                new BootstrapSceneState(SceneStateNames.Bootstrap, SceneNames.Bootstrap),
                new MainSceneState(SceneStateNames.MainScene, SceneNames.MainScene),
            };
        }

        private void CreateSceneLoadingCanvasController()
        {
            _sceneLoadingCanvasController = new SceneLoadingCanvasController(this);
        }

        private void StartBootstrap()
        {
            CurrentSceneState = GetSceneStateFromArrayByName(SceneStateNames.Bootstrap);
            CurrentSceneState.EnterState(false);
        }

        private void InitSceneSwitch(SceneStateNames stateName, bool isAsync)
        {
            CurrentSceneState = GetSceneStateFromArrayByName(stateName);
            OnSceneStateChanged?.Invoke();
            if (isAsync)
            {
                OnSceneStartLoadingAsync?.Invoke();
                CurrentSceneState.EnterState();
            }
            else
            {
                CurrentSceneState.EnterStateWithoutLoading();
            }
        }

        private void CheckIfStatesArrayComplete()
        {
            if (!IsSceneStatesArrayComplete) throw new NullReferenceException("Scene states array is not complete");
        }

        private void CheckIfCurrentStateIsNotNull()
        {
            if (CurrentSceneState == null) throw new NullReferenceException("Current scene state is null");
        }

        private BaseSceneState GetSceneStateFromArrayByName(SceneStateNames stateName)
        {
            return _sceneStates.FirstWhich(x => x.StateName == stateName);
        }

        protected override void Dispose()
        {
            CurrentSceneState?.ExitState();
            base.Dispose();
        }

#endregion
    }
}