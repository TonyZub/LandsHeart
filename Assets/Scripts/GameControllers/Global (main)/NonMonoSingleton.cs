using System;


namespace LandsHeart
{
	public abstract class NonMonoSingleton<T> where T : NonMonoSingleton<T>, new()
	{
        #region Fields

        private readonly bool _doSubscribeGlobalDispose;
        private bool _isInstance;

        #endregion


        #region Properties

        public static T Instance { get; private set; }

        #endregion


        #region Constructor

        protected NonMonoSingleton(bool doSubscribeGlobalDispose = true)
        {
            _doSubscribeGlobalDispose = doSubscribeGlobalDispose;

            if (Instance == null)
            {
                Instance = this as T;
                _isInstance = true;

                if(_doSubscribeGlobalDispose)
                    SubscribeDispose();
            }
            else
            {
                throw new InvalidOperationException($"Constructing a { typeof(T).Name}" +
                    $" is already done, there must be not more than 1 class on session");
            }
        }

        #endregion


        #region Methods

        private void SubscribeDispose() => GlobalController.Instance.OnDispose += Dispose;
        private void UnsubscribeDispose() => GlobalController.Instance.OnDispose -= Dispose;

        protected virtual void Dispose()
        {
            if(_doSubscribeGlobalDispose)
                UnsubscribeDispose();

            if (_isInstance)
                Instance = null;
        }

        #endregion
    }
}