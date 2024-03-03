using echo17.EndlessBook;
using System;


namespace LandsHeart
{
	public sealed class BuildingsBookController : IDisposable
	{
        #region Events

        public event Action<bool> BookActivityChanged;

        #endregion


        #region Fields

        private readonly EndlessBook _bookOnScene;
        private readonly InteractiveExtendableObjectModel _bookInteractor;
        private readonly BuildingsBookPagesController _pagesController;
        private readonly CameraService _cameraService;
        private readonly InputController _input;

        private bool _isActivated;

        #endregion


        #region Properties

        public bool IsActivated
        {
            get => _isActivated;
            private set
            {
                if(_isActivated != value)
                {
                    _isActivated = value;
                    BookActivityChanged?.Invoke(IsActivated);
                }
            }
        }

        #endregion


        #region Constructor

        public BuildingsBookController(EndlessBook bookOnScene)
        {
            _bookOnScene = bookOnScene;
            _bookInteractor = _bookOnScene.GetComponent<InteractiveExtendableObjectModel>();
            _pagesController = new BuildingsBookPagesController(this, _bookOnScene, _bookInteractor);
            _cameraService = GlobalContext.Instance.GetDependency<GameplayServices>().CameraService;
            _input = InputController.Instance;
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
            _bookInteractor.MouseEntered += OnMouseEnter;
            _bookInteractor.MouseExited += OnMouseExit;
            _bookInteractor.MouseUpAsButton += OnMouseClick;
        }

        private void UnsubscribeEvents()
        {
            _bookInteractor.MouseEntered -= OnMouseEnter;
            _bookInteractor.MouseExited -= OnMouseExit;
            _bookInteractor.MouseUpAsButton -= OnMouseClick;
        }

        private void OnMouseClick(InteractiveObjectModel interactor)
        {
            if (_bookOnScene.CurrentState == EndlessBook.StateEnum.OpenMiddle)
                return;

            _bookOnScene.SetState(EndlessBook.StateEnum.OpenMiddle, 1f, OnBookOpen, false);
            _cameraService.SetActiveCamera(CameraNames.BuildingBook);
            _bookInteractor.Outlinable.enabled = false;
            IsActivated = true;
        }

        private void OnBookOpen(EndlessBook.StateEnum fromState, EndlessBook.StateEnum toState, int pageNumber)
        {
            _input.EscapePressed += OnEscapePressed;
        }

        private void OnEscapePressed()
        {
            if (_bookOnScene.IsDraggingPage)
                return;

            _input.EscapePressed -= OnEscapePressed;
            _bookOnScene.SetState(EndlessBook.StateEnum.ClosedFront);
            _cameraService.SetActiveCamera(CameraNames.TableDown);
            IsActivated = false;

            if (_bookInteractor.IsMouseInside) 
                _bookInteractor.Outlinable.enabled = true;
        }

        private void OnMouseEnter(InteractiveObjectModel interactor)
        {
            if(!IsActivated)
            {
                _bookInteractor.Outlinable.enabled = true;
            }
        }

        private void OnMouseExit(InteractiveObjectModel interactor)
        {
            if (!IsActivated)
            {
                _bookInteractor.Outlinable.enabled = false;
            }
        }

        #endregion
    }
}