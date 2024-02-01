using System;
using UnityEngine;


namespace LandsHeart
{
    public sealed class InputController : NonMonoSingleton<InputController>
    {
        #region Events

        public event Action LeftMouseDown;
        public event Action LeftMouseUp;
        public event Action RightMouseDown;
        public event Action RightMouseUp;

        #endregion


        #region Fields

        private bool _isLeftMouseDown;
        private bool _isLeftMouseUp;
        private bool _isRightMouseDown;
        private bool _isRightMouseUp;

        #endregion


        #region Properties

        public Vector2 MousePosition => Input.mousePosition;
        public bool IsDisabled { get; private set; }
        public bool IsLeftMouseDown
        {
            get => _isLeftMouseDown;
            private set
            {
                if(_isLeftMouseDown != value)
                {
                    _isLeftMouseDown = value;
                    LeftMouseDown?.Invoke();
                }
            }
        }
        public bool IsLeftMouseUp
        {
            get => _isLeftMouseUp;
            private set
            {
                if(_isLeftMouseUp != value)
                {
                    _isLeftMouseUp = value;
                    LeftMouseUp?.Invoke();
                }
            }
        }  
        public bool IsRightMouseDown
        {
            get => _isRightMouseDown;
            private set
            {
                if(_isRightMouseDown != value)
                {
                    _isRightMouseDown = value;
                    RightMouseDown?.Invoke();
                }
            }
        }
        public bool IsRightMouseUp
        {
            get => _isRightMouseUp;
            private set
            {
                if(_isRightMouseUp != value)
                {
                    _isRightMouseUp = value;
                    RightMouseUp?.Invoke();
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
            IsLeftMouseDown = Input.GetMouseButtonDown(0);
            IsLeftMouseUp = Input.GetMouseButtonUp(0);
            IsRightMouseDown = Input.GetMouseButtonDown(1);
            IsRightMouseUp = Input.GetMouseButtonUp(1);
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

