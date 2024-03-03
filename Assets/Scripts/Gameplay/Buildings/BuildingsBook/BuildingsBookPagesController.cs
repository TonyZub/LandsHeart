using echo17.EndlessBook;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace LandsHeart
{
	public sealed class BuildingsBookPagesController : IDisposable
	{
        #region Constants

        private const float TURN_STOP_SPEED = 1f;

        #endregion


        #region Fields

        private readonly BuildingsBookController _bookController;
        private readonly EndlessBook _bookOnScene;
        private readonly InteractiveExtendableObjectModel _bookInteractor;
        private readonly BoxCollider _bookCollider;
        private readonly CameraService _cameraService;
        private readonly EventSystem _eventSystem;
        private readonly InputController _input;

        private bool _doReversePageIfNotMidway;

        #endregion


        #region Constructor

        public BuildingsBookPagesController(BuildingsBookController bookController, EndlessBook bookOnScene, 
            InteractiveExtendableObjectModel bookInteractor)
        {
            _bookController = bookController;
            _bookOnScene = bookOnScene;
            _bookInteractor = bookInteractor;
            _bookCollider = _bookOnScene.GetComponent<BoxCollider>();
            _eventSystem = GlobalController.Instance.EventSystem;
            _cameraService = GlobalContext.Instance.GetDependency<GameplayServices>().CameraService;
            _input = InputController.Instance;
            _doReversePageIfNotMidway = true;
            SubscribeEvents();
        }

        #endregion


        #region IDisposable

        public void Dispose()
        {
            UnsubscribeEvents();
        }

        #endregion


        #region Methods

        private void SubscribeEvents()
        {
            _bookController.BookActivityChanged += OnBookActivityChanged;
        }

        private void UnsubscribeEvents()
        {
            _bookController.BookActivityChanged -= OnBookActivityChanged;
        }

        private void OnBookActivityChanged(bool isActivated)
        {
            if (isActivated)
            {
                _bookInteractor.MouseDown += OnMouseDown;
                _bookInteractor.MouseUp += OnMouseUp;
                _bookInteractor.MouseMove += OnMouseDrag;
            }
            else
            {
                _bookInteractor.MouseDown -= OnMouseDown;
                _bookInteractor.MouseUp -= OnMouseUp;
                _bookInteractor.MouseMove -= OnMouseDrag;
            }
        }

        private void OnMouseDown(InteractiveObjectModel interactor)
        {
            if (_bookOnScene.IsTurningPages || _bookOnScene.IsDraggingPage || _eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            var normalizedTime = GetNormalizedTime();
            var direction = normalizedTime > 0.5f ? Page.TurnDirectionEnum.TurnForward : Page.TurnDirectionEnum.TurnBackward;

            _bookOnScene.TurnPageDragStart(direction);
        }

        private void OnMouseDrag(InteractiveObjectModel interactor)
        {
            if (_bookOnScene.IsTurningPages || !_bookOnScene.IsDraggingPage || !_input.IsLeftMousePressed || 
                _eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            var normalizedTime = GetNormalizedTime();
            _bookOnScene.TurnPageDrag(normalizedTime);
        }

        private void OnMouseUp(InteractiveObjectModel interactor)
        {
            if (_bookOnScene.IsTurningPages || !_bookOnScene.IsDraggingPage || _eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            _bookOnScene.TurnPageDragStop(TURN_STOP_SPEED, PageTurnCompleted, reverse: _doReversePageIfNotMidway ?
                (_bookOnScene.TurnPageDragNormalizedTime < 0.5f) : false);
        }

        private float GetNormalizedTime()
        {
            var ray = _cameraService.MainCamera.ScreenPointToRay(_input.MousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var vector = _bookCollider.transform.forward;
                var delta = (hit.point - _bookCollider.transform.position).normalized;
                var cross = Vector3.Cross(delta, vector);
                var part = -cross.y;

                return 0.5f + part / 2f; // 0.5f + (hit.point.x - _bookCollider.transform.position.x) / _bookCollider.size.x;
            }

            var viewportPoint = _cameraService.MainCamera.ScreenToViewportPoint(_input.MousePosition);
            return (viewportPoint.x >= 0.5f) ? 1 : 0;
        }

        private void PageTurnCompleted(int leftPageNumber, int rightPageNumber)
        {
        }

        #endregion
    }
}