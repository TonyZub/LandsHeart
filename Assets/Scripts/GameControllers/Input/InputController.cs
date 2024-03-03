using System;
using UnityEngine;


namespace LandsHeart
{
    public sealed class InputController : NonMonoSingleton<InputController>
    {
        #region Events

        public event Action<bool> LeftMouseStateChanged;
        public event Action<bool> RightMouseStateChanged;
        public event Action EscapePressed;

        #endregion


        #region Fields

        private bool _isLeftMousePressed;
        private bool _isRightMousePressed;

        #endregion


        #region Properties

        public Vector2 MousePosition => Input.mousePosition;
        public bool IsDisabled { get; private set; }
        public bool IsLeftMousePressed
        {
            get => _isLeftMousePressed;
            private set
            {
                if(value !=  _isLeftMousePressed)
                {
                    _isLeftMousePressed = value;
                    LeftMouseStateChanged?.Invoke(IsLeftMousePressed);
                }
            }
        }
        public bool IsRightMousePressed
        {
            get => _isRightMousePressed;
            private set
            {
                if (value != _isRightMousePressed)
                {
                    _isRightMousePressed = value;
                    RightMouseStateChanged?.Invoke(IsRightMousePressed);
                }
            }
        }

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
            IsLeftMousePressed = Input.GetMouseButton(0);
            IsRightMousePressed = Input.GetMouseButton(1);
            if (Input.GetKeyDown(KeyCode.Escape)) EscapePressed?.Invoke();
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

