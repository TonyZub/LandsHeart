using System;
using Zenject;


namespace LandsHeart
{
	public sealed class GlobalContext : NonMonoSingleton<GlobalContext>, IDisposable
	{
        #region Fields

        private SceneStateMachine _sceneStateMachine;
        private InputController _inputController;
        private GlobalServices _globalServices;
        private DiContainer _diContainer;

        #endregion


        #region Properties

        public SceneStateMachine SceneStateMachine => _sceneStateMachine;
        public InputController InputController => _inputController;
        public GlobalServices GlobalServices => _globalServices;
        public DiContainer DiContainer => _diContainer;

        #endregion


        #region Constructor

        public GlobalContext() : base(doSubscribeGlobalDispose: false)
        {
        }

        public void Construct(DiContainer container)
        {
            _diContainer = container;
            CreateSceneStateMachine();
            CreateInputController();
            CreateGlobalServices();
        }

        public new void Dispose() => base.Dispose();

        #endregion


        #region Methods

        private void CreateSceneStateMachine() => _sceneStateMachine = new SceneStateMachine();
        private void CreateInputController() => _inputController = new InputController();
        private void CreateGlobalServices() => _globalServices = new GlobalServices();

        public void RegisterDependency<T>(T obj)
        {
            if (_diContainer.HasBinding<T>())
            {
                throw new ArgumentException($"type {typeof(T)} is already in DI container");
            }
            _diContainer.Bind<T>().FromInstance(obj).AsSingle();
        }

        public T GetDependency<T>()
        {
            if(!_diContainer.HasBinding<T>())
            {
                throw new ArgumentException($"type {typeof(T)} is missing in DI container");
            }
            return _diContainer.Resolve<T>();
        }

        public void UnregisterDependency<T>()
        {
            if (!_diContainer.HasBinding<T>())
            {
                throw new ArgumentException($"type {typeof(T)} is missing in DI container");
            }
            _diContainer.Unbind<T>();
        }

        #endregion
    }
}