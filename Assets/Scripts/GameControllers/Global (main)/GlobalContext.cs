using System;
using System.Collections.Generic;


namespace LandsHeart
{
	public sealed class GlobalContext : NonMonoSingleton<GlobalContext>
	{
        #region Fields

        private SceneStateMachine _sceneStateMachine;
        private InputController _inputController;
        private GlobalServices _globalServices;
        private Dictionary<Type, object> _diContainer;

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
            CreateSceneStateMachine();
            CreateInputController();
            CreateGlobalServices();
        }

        #endregion


        #region Methods

        private void CreateDIContainer()
        {
            _diContainer = new Dictionary<Type, object>();
        }

        private void CreateSceneStateMachine()
        {
            _sceneStateMachine = new SceneStateMachine();
        }

        private void CreateInputController()
        {
            _inputController = new InputController();
        }

        private void CreateGlobalServices()
        {
            _globalServices = new GlobalServices();
        }

        public void RegisterDependency<T>(T obj)
        {
            if (_diContainer.ContainsKey(typeof(T)))
            {
                throw new ArgumentException($"type {typeof(T)} is already in DI container");
            }
            _diContainer.Add(typeof(T), obj);
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

        protected override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}