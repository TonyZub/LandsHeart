using System;
using System.Collections.Generic;


namespace LandsHeart
{
	public sealed class GlobalContext : NonMonoSingleton<GlobalContext>
	{
        #region Fields

        public enum DisposableTypes
        {
            None    = 1, // not disposing, only manual
            Local   = 2, // auto disposing at the scene end
            Global  = 3, // auto disposing at the game end
        }

        private SceneStateMachine _sceneStateMachine;
        private InputController _inputController;
        private GlobalServices _globalServices;
        private Dictionary<Type, object> _diContainer;
        private Dictionary<Type, IDisposable> _localDisposables;
        private Dictionary<Type, IDisposable> _globalDisposables;

        #endregion


        #region Properties

        public SceneStateMachine SceneStateMachine => _sceneStateMachine;
        public InputController InputController => _inputController;
        public GlobalServices GlobalServices => _globalServices;

        #endregion


        #region Constuctor

        public GlobalContext() : base()
        {
            CreateDIContainer();
            CreateDisposables();
            CreateSceneStateMachine();
            CreateInputController();
            CreateGlobalServices();
            SubscribeEvents();
        }

        #endregion


        #region Methods

        private void CreateDIContainer()
        {
            _diContainer = new Dictionary<Type, object>();
        }

        private void CreateDisposables()
        {
            _localDisposables = new Dictionary<Type, IDisposable>();
            _globalDisposables = new Dictionary<Type, IDisposable>();
        }

        private void CreateSceneStateMachine()
        {
            _sceneStateMachine = new SceneStateMachine();
        }

        private void SubscribeEvents()
        {
            _sceneStateMachine.OnSceneStateChanged += MakeLocalDispose;
        }

        private void UnsubscribeEvents()
        {
            _sceneStateMachine.OnSceneStateChanged -= MakeLocalDispose;
        }

        private void CreateInputController()
        {
            _inputController = new InputController();
        }

        private void CreateGlobalServices()
        {
            _globalServices = new GlobalServices();
        }

        public void RegisterDependency<T>(T obj, DisposableTypes disposingType = DisposableTypes.None)
        {
            if (_diContainer.ContainsKey(typeof(T)))
            {
                throw new ArgumentException($"type {typeof(T)} is already in DI container");
            }
            _diContainer.Add(typeof(T), obj);
            AddDependencyToDisposables(obj, disposingType);
        }

        private void AddDependencyToDisposables<T>(T obj, DisposableTypes disposingType = DisposableTypes.None)
        {
            if (obj is IDisposable)
            {
                switch (disposingType)
                {
                    case DisposableTypes.None:
                        break;
                    case DisposableTypes.Local:
                        if (_localDisposables.ContainsKey(typeof(T)))
                        {
                            throw new ArgumentException($"type {typeof(T)} is already in local disposables container");
                        }
                        _localDisposables.Add(typeof(T), obj as IDisposable);
                        break;
                    case DisposableTypes.Global:
                        if (_globalDisposables.ContainsKey(typeof(T)))
                        {
                            throw new ArgumentException($"type {typeof(T)} is already in global disposables container");
                        }
                        _globalDisposables.Add(typeof(T), obj as IDisposable);
                        break;
                    default:
                        break;
                }
            }
        }

        public void UnregisterDependency<T>()
        {
            if (!_diContainer.ContainsKey(typeof(T)))
            {
                throw new ArgumentException($"type {typeof(T)} is missing in DI container");
            }
            _diContainer.Remove(typeof(T));
        }

        public T GetDependency<T>()
        {
            if(_diContainer.TryGetValue(typeof(T), out object dependecy))
            {
                return (T)dependecy;
            }
            throw new ArgumentException($"type {typeof(T)} is missing in DI container");
        }

        public bool TryGetDependency<T>(out T dependency)
        {
            dependency = default;
            if (_diContainer.TryGetValue(typeof(T), out object dependecy))
            {
                dependency = (T)dependecy;
                return true;
            }
            return false;
        }

        public void DisposeAndUnregisterDependency<T>()
        {
            var dependency = GetDependency<T>();
            if(dependency is IDispose)
            {
                (dependency as IDispose).OnDispose();
            }
            UnregisterDependency<T>();
        }

        private void MakeLocalDispose()
        {
            foreach (var item in _localDisposables.Values)
            {
                item.Dispose();
            }
            _localDisposables.Clear();
            GC.Collect();
        }

        protected override void Dispose()
        {
            UnsubscribeEvents();
            foreach (var item in _globalDisposables.Values)
            {
                item.Dispose();
            }
            _diContainer.Clear();
            _localDisposables.Clear();
            _globalDisposables.Clear();
            GC.Collect();
            base.Dispose();
        }

        #endregion
    }
}