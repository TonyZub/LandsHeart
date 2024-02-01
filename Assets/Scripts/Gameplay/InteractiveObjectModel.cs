using EPOOutline;
using System;
using UnityEngine;


namespace LandsHeart
{
    [RequireComponent(typeof(Outlinable))]
    public abstract class InteractiveObjectModel : MonoBehaviour
	{
        #region Events

        public event Action<InteractiveObjectModel> MouseEntered;
        public event Action<InteractiveObjectModel> MouseExited;
        public event Action<InteractiveObjectModel> MouseDown;
        public event Action<InteractiveObjectModel> MouseUp;
        public event Action<InteractiveObjectModel> MouseUpAsButton;
        public event Action<InteractiveObjectModel> Destroyed;

        #endregion


        #region Fields

        [SerializeField] private bool _isOutlinableControlledByModel;

        #endregion


        #region Properties

        public Outlinable Outlinable { get; private set; }
        public bool IsOutlinableControlledByModel => _isOutlinableControlledByModel;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            Outlinable = GetComponent<Outlinable>();
        }

        private void OnMouseEnter()
        {
            OnMouseEntered();
        }

        private void OnMouseDown()
        {
            OnMousePressed();
        }

        private void OnMouseUp()
        {
            OnMouseReleased();
        }

        private void OnMouseUpAsButton()
        {
            OnMouseClicked();
        }

        private void OnMouseExit()
        {
            OnMouseExited();
        }

        private void OnDestroy()
        {
            OnDispose();
        }

        #endregion


        #region Methods

        protected virtual void OnMouseEntered()
        {
            if (IsOutlinableControlledByModel) Outlinable.enabled = true;
            MouseEntered?.Invoke(this);
        }

        protected virtual void OnMousePressed()
        {
            MouseDown?.Invoke(this);
        }

        protected virtual void OnMouseReleased()
        {
            MouseUp?.Invoke(this);
        }

        protected virtual void OnMouseClicked()
        {
            MouseUpAsButton?.Invoke(this);
        }

        protected virtual void OnMouseExited()
        {
            if (IsOutlinableControlledByModel) Outlinable.enabled = false;
            MouseExited?.Invoke(this);
        }

        protected virtual void OnDispose()
        {
            Destroyed?.Invoke(this);
        }

        #endregion
    }
}