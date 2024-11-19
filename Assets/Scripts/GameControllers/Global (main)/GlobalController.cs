using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;


namespace LandsHeart
{
    [RequireComponent(typeof(EventSystem))]
    public sealed class GlobalController : MonoSingleton<GlobalController>
    {
        #region Events

        public event Action OnStart;
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;

        #endregion


        #region Fields

        [SerializeField] private LoggingTypes _loggingTypesEnabled;

        private EventSystem _eventSystem;
        private GlobalContext _globalContext;
        private DiContainer _diContainer;

        #endregion


        #region Properties

        public LoggingTypes LoggingTypesEnabled => _loggingTypesEnabled;
        public EventSystem EventSystem => _eventSystem;
        public GlobalContext GlobalContext => _globalContext;

        #endregion


        #region MonoSingleton

        [Inject]
        public void Construct(DiContainer container) => _diContainer = container;

        protected override void Initialize()
        {
            base.Initialize();
            InitDoTween();
            KeepAliveDuringGame();
            GetEventSystem();
            CreateGlobalContext();
        }

        protected override void Dispose()
        {
            base.Dispose();
            _globalContext.Dispose();
        }

        #endregion


        #region UnityMethods

        private void Start() => OnStart?.Invoke();
        private void Update() => OnUpdate?.Invoke();
        private void FixedUpdate() => OnFixedUpdate?.Invoke();
        private void LateUpdate() => OnLateUpdate?.Invoke();

        #endregion


        #region Methods

        private void InitDoTween() => DG.Tweening.DOTween.Init(false, false);
        private void KeepAliveDuringGame() => DontDestroyOnLoad(gameObject);
        private void GetEventSystem() => _eventSystem = GetComponent<EventSystem>();

        private void CreateGlobalContext()
        {
            _globalContext = new GlobalContext();
            _globalContext.Construct(_diContainer);
        }

        #endregion
    }
}
