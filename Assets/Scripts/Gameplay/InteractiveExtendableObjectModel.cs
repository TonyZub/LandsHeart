using System;


namespace LandsHeart
{
	public class InteractiveExtendableObjectModel : InteractiveObjectModel
	{
        #region Events

        private event Action<InteractiveObjectModel> OnMouseRightClick;

        #endregion


        #region Properties

        public bool IsRightMouseOnHold { get ; private set; }

        #endregion


        #region Methods

        private void SubscribeRightMouseClick()
        {
            InputController.Instance.RightMouseStateChanged += OnRightMouseStateChanged;
        }

        private void UnsubscribeRightMouseClick()
        {
            InputController.Instance.RightMouseStateChanged -= OnRightMouseStateChanged;
        }

        protected override void OnMouseEntered()
        {
            base.OnMouseEntered();
            SubscribeRightMouseClick();
        }

        protected override void OnMouseExited()
        {
            base.OnMouseExited();
            UnsubscribeRightMouseClick();
            IsRightMouseOnHold = false;
        }

        protected virtual void OnRightMouseStateChanged(bool isPressed)
        {
            if (isPressed)
            {
                OnRightMouseDown();
            }
            else
            {
                OnRightMouseUp();
            }
        }

        protected virtual void OnRightMouseDown()
        {
            IsRightMouseOnHold = true;
        }

        protected virtual void OnRightMouseUp()
        {
            if (IsRightMouseOnHold) OnMouseRightClick?.Invoke(this);
            IsRightMouseOnHold = false;
        }

        protected override void OnDispose()
        {
            UnsubscribeRightMouseClick();
            base.OnDispose();
        }

        #endregion
    }
}