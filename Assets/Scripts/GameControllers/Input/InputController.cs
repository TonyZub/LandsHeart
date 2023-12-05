using UnityEngine;


namespace LandsHeart
{
    public sealed class InputController : NonMonoSingleton<InputController>
    {
        #region Properties

        public Vector2 MousePosition => Input.mousePosition;
        public bool IsDisabled { get; private set; }

        #endregion


        #region Constructor

        public InputController() : base()
        {
            SubscribeEvents();
        }

        #endregion


        #region Methods

        private void SubscribeEvents()
        {
            GlobalController.Instance.OnUpdate += GetInput;
        }

        private void UnsubscribeEvents()
        {
            GlobalController.Instance.OnUpdate -= GetInput;
        }

        private void GetInput()
        {
            if (IsDisabled) return;
            //TODO
        }

        public void DisableInput()
        {
            IsDisabled = true;
        }

        public void EnableInput()
        {
            IsDisabled = false;
        }

        protected override void Dispose()
        {
            UnsubscribeEvents();
            base.Dispose();
        }

        #endregion
    }
}

