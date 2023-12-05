using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

namespace LandsHeart
{
	public sealed class CursorService
	{
        #region Constants

        private const string CURSOR_CANVAS_NAME = "[CursorCanvas]";
        private const string CURSOR_GAMEOBJECT_NAME = "[CursorObject]";
        private const int SORTING_ORDER = 100;

        #endregion


        #region Events

        public event Action CursorEnabled;
        public event Action CursorDisabled;

        #endregion


        #region Fields

        private readonly CursorServiceData _serviceData;
        private AudioService _audioService;
        private CursorPreset _currentPreset;
        private Camera _mainCamera;
        private InputController _inputController;
        private Canvas _cursorCanvas;
        private Image _cursorImage;
        private ParticleSystem _clickParticles;
        private Vector2 _cursorPosition;
        private Vector2 _particlesPosition;
        private List<RaycastResult> raycastResults;
        private bool _isCursorEnabled;

        #endregion


        #region Properties

        public CursorPreset CurrentCursorPreset => _currentPreset;
        public bool IsCursorEnabled
        {
            get => _isCursorEnabled;
            private set
            {
                if(value != _isCursorEnabled)
                {
                    _isCursorEnabled = value;
                    if (_isCursorEnabled)
                    {
                        EnableCursor();
                    }
                    else
                    {
                        DisableCursor();
                    }
                }
            }
        }
        public bool IsCursorTypeLocked { get; private set; }

        #endregion


        #region Constructor

        public CursorService(CursorServiceData serviceData)
        {
            _serviceData = serviceData;
            _inputController = InputController.Instance;
            _isCursorEnabled = true;
            CreateWorldCursor();
            SetCursorScale();
            SetCursorLockMode(CursorLockMode.None);
            SubscribeEvents();
        }

        #endregion


        #region Methods

        private void OnStart()
        {
            GlobalController.Instance.OnStart -= OnStart;
            _audioService = GlobalContext.Instance.GlobalServices.AudioService;
        }

        private void CreateWorldCursor()
        {
            var newCanvas = PrimitiveFactory.CreateEmptyGameObject();
            newCanvas.name = CURSOR_CANVAS_NAME;
            newCanvas.layer = (int)LayerManager.UI;
            _cursorCanvas = newCanvas.AddComponent<Canvas>();
            _cursorCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _cursorCanvas.sortingOrder = SORTING_ORDER;
            GameObject.DontDestroyOnLoad(_cursorCanvas);

            var newCursor = PrimitiveFactory.CreateEmptyGameObject();
            newCursor.name = CURSOR_GAMEOBJECT_NAME;
            newCursor.layer = (int)LayerManager.UI;
            newCursor.transform.SetParent(_cursorCanvas.transform);
            _cursorImage = newCursor.AddComponent<Image>();
            _cursorImage.preserveAspect = true;
            _cursorImage.raycastTarget = false;
            _cursorImage.rectTransform.pivot = Vector2.up;

            //_clickParticles = PrimitiveFactory.Instantiate(_serviceData.ClickParticles, _cursorImage.transform);
        }

        private void GetMainCamera()
        {
            _mainCamera = Camera.main;
        }

        private void SetCursorScale()
        {
            _cursorImage.rectTransform.sizeDelta = new Vector2(Screen.width * _serviceData.CursorSizePartOfScreen,
                Screen.height * _serviceData.CursorSizePartOfScreen);
        }

        private void DisableCursor()
        {
            _cursorImage.gameObject.SetActive(false);
            GlobalController.Instance.OnUpdate -= MoveWorldCursor;
        }

        private void EnableCursor()
        {
            GetMainCamera();
            _cursorImage.gameObject.SetActive(true);
            GlobalController.Instance.OnUpdate += MoveWorldCursor;
        }

        private void SubscribeEvents()
        {
            //InputController.Instance.LeftMousePressed += OnClick;
            SceneStateMachine.Instance.OnSceneStateChanged += OnSceneStateChange;
            GlobalController.Instance.OnStart += OnStart;
            GlobalController.Instance.OnDispose += UnsubscribeEvents;
        }

        private void OnSceneStateChange()
        {
            DisableCursor();
            SceneStateMachine.Instance.CurrentSceneState.OnSceneLoadingEnded += OnSceneLoadEnded;
        }

        private void OnSceneLoadEnded()
        {
            SceneStateMachine.Instance.CurrentSceneState.OnSceneLoadingEnded -= OnSceneLoadEnded;
            EnableCursor();
        }

        private void UnsubscribeEvents()
        {
            //InputController.Instance.LeftMousePressed -= OnClick;
            SceneStateMachine.Instance.OnSceneStateChanged -= OnSceneStateChange;
            GlobalController.Instance.OnDispose -= UnsubscribeEvents;
        }

        private void MoveWorldCursor()
        {
            Cursor.visible = false;
            _cursorPosition.Set(_inputController.MousePosition.x + _currentPreset.PointOffset.x, 
                _inputController.MousePosition.y + +_currentPreset.PointOffset.y);
            _cursorImage.transform.position = _cursorPosition;
            _particlesPosition = _mainCamera.ScreenToWorldPoint(_inputController.MousePosition);
            //_clickParticles.transform.position = _particlesPosition;
        }

        private void OnClick(bool isPressed)
        {
            if (isPressed || EventSystem.current.IsPointerOverGameObject()) return;
            switch (_currentPreset.Type)
            {
                case CursorTypes.None:
                    break;
                case CursorTypes.GameplayHoverPickable:
                    _audioService.PlayEffectSoundInCenter(EffectSoundNames.ClickOnPickableObject);
                    break;
                case CursorTypes.GameplayHoverLookable:
                    _audioService.PlayEffectSoundInCenter(EffectSoundNames.ClickOnLookableObject);
                    break;
                default:
                    _audioService.PlayEffectSoundInCenter(EffectSoundNames.ClickOnNothing);
                    break;
            }
            //if(!isPressed) _clickParticles.Play();
        }

        public void SetCursorLockMode(CursorLockMode lockMode)
        {
            Cursor.lockState = lockMode;
            switch (lockMode)
            {
                case CursorLockMode.None:
                    IsCursorEnabled = true;
                    CursorEnabled?.Invoke();
                    break;
                case CursorLockMode.Locked:
                    IsCursorEnabled = false;
                    CursorDisabled?.Invoke();
                    break;
                case CursorLockMode.Confined:
                    IsCursorEnabled = false;
                    CursorDisabled?.Invoke();
                    break;
                default:
                    break;
            }
        }

        public void SetCursorType(CursorTypes type)
        {
            if (!IsCursorTypeLocked)
            {
                _currentPreset = _serviceData.GetPresetByType(type);
                _cursorImage.sprite = _currentPreset.Sprite;
                //Cursor.SetCursor(_currentPreset.Texture, _currentPreset.PointOffset, CursorMode.Auto);
            }
        }

        public void LockCursorType()
        {
            IsCursorTypeLocked = true;
        }

        public void UnlockCursorType()
        {
            IsCursorTypeLocked = false;
        }

        public bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(_inputController.MousePosition.x, _inputController.MousePosition.y);
            raycastResults = new List<RaycastResult>();
            EventSystem.current?.RaycastAll(eventDataCurrentPosition, raycastResults);
            return raycastResults.Count > 0;
        }

        #endregion
    }
}