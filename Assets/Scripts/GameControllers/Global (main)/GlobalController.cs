using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace LandsHeart
{
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

        #endregion


        #region Properties

        public LoggingTypes LoggingTypesEnabled => _loggingTypesEnabled;
        public EventSystem EventSystem => _eventSystem;
        public GlobalContext GlobalContext => _globalContext;

        #endregion


        #region MonoSingleton

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
            //TODO - manually dispose something if needed
            base.Dispose();
        }

        #endregion


        #region UnityMethods

        private void Start()
        {
            OnStart?.Invoke();
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }

        #endregion


        #region Methods

        private void InitDoTween()
        {
            DG.Tweening.DOTween.Init(false, false);
        }

        private void KeepAliveDuringGame()
        {
            DontDestroyOnLoad(this);
        }

        private void GetEventSystem()
        {
            _eventSystem = GetComponent<EventSystem>();
        }

        private void CreateGlobalContext()
        {
            _globalContext = new GlobalContext();
        }

#if UNITY_EDITOR

        [ContextMenu(nameof(ClearSave))]
        [NaughtyAttributes.Button("Clear saves", NaughtyAttributes.EButtonEnableMode.Editor)]
        private void ClearSave()
        {
            SavingSystem.ClearSaveData();
            Debug.Log("Saves were cleared");
        }

#endif

        #endregion
    }
}
