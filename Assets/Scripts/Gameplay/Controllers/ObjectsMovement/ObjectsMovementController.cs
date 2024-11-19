using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace LandsHeart
{
	public sealed class ObjectsMovementController : IDisposable
	{
		#region Constants

		private const float OBJECTS_MOVING_HEIGHT = 0.1f;

        #endregion


        #region Fields

        private readonly GameplayServices _gameplayServices;
		private List<MovableByMouseObject> _movableObjects;

		private Ray _cameraRay;

		#endregion


		#region Properties

		public MovableByMouseObject CurrentHoveringObject { get; private set; }
		public MovableByMouseObject CurrentHoldingObject { get; private set; }

		public bool IsHoveringObject => CurrentHoveringObject != null;
		public bool IsHoldingObject => CurrentHoldingObject != null;

        #endregion


        #region Constructor

		public ObjectsMovementController()
		{
            _gameplayServices = GlobalContext.Instance.GetDependency<GameplayServices>();
            _movableObjects = ObjectFinder.FindObjectsOfType<MovableByMouseObject>().ToList();
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
			foreach (var item in _movableObjects)
			{
				SubscribeObject(item);
            }
            GlobalController.Instance.OnUpdate += OnUpdate;
        }

		private void UnsubscribeEvents()
		{
            foreach (var item in _movableObjects)
            {
                UnsubscribeObject(item);
            }
            GlobalController.Instance.OnUpdate -= OnUpdate;
        }

		private void SubscribeObject(InteractiveObjectModel obj)
		{
			obj.MouseEntered += OnObjectMouseEnter;
			obj.MouseExited += OnObjectMouseExit;
            obj.MouseDown += OnObjectMouseDown;
            obj.MouseUp += OnObjectMouseUp;
			obj.Destroyed += OnObjectDestroyed;
		}

		private void UnsubscribeObject(InteractiveObjectModel obj)
		{
            obj.MouseEntered -= OnObjectMouseEnter;
            obj.MouseExited -= OnObjectMouseExit;
            obj.MouseDown -= OnObjectMouseDown;
            obj.MouseUp -= OnObjectMouseUp;
            obj.Destroyed -= OnObjectDestroyed;
        }

		private void OnObjectMouseEnter(InteractiveObjectModel obj)
		{
			if (IsHoldingObject) return;
			obj.Outlinable.enabled = true;
		}

		private void OnObjectMouseExit(InteractiveObjectModel obj)
		{
            if (IsHoldingObject) return;
            obj.Outlinable.enabled = false;
        }

		private void OnObjectMouseDown(InteractiveObjectModel obj)
		{
			CurrentHoldingObject = (MovableByMouseObject)obj;
			CurrentHoldingObject.transform.position += Vector3.up * OBJECTS_MOVING_HEIGHT;
		}

		private void OnObjectMouseUp(InteractiveObjectModel obj)
		{
			PlaceMovableObject();
            CurrentHoldingObject = null;
        }

		private void OnObjectDestroyed(InteractiveObjectModel obj)
		{
			UnsubscribeObject(obj);
		}

		private void OnUpdate()
		{
			if (!IsHoldingObject) return;
			MoveHoldingItem();
        }

		private void MoveHoldingItem()
		{
			_cameraRay = _gameplayServices.CameraService.MainCamera.
				ScreenPointToRay(InputController.Instance.MousePosition);

			if(_gameplayServices.PhysicsService.DoRaycast(_cameraRay, out RaycastHit hit, float.MaxValue,
				_gameplayServices.PhysicsService.Data.TableLayer))
			{
				CurrentHoldingObject.transform.position = new Vector3(hit.point.x,
					CurrentHoldingObject.transform.position.y, hit.point.z);
			}
			else
			{
				return;
			}	

            if (_gameplayServices.PhysicsService.TryGetGroundedPosition(CurrentHoldingObject.transform.position,
				_gameplayServices.PhysicsService.Data.TableLayer, out Vector3 positionOnTable))
			{
				CurrentHoldingObject.transform.position = positionOnTable + Vector3.up * OBJECTS_MOVING_HEIGHT;
            }
		}

		private void PlaceMovableObject()
        {
            if (_gameplayServices.PhysicsService.TryGetGroundedPosition(CurrentHoldingObject.transform.position,
                _gameplayServices.PhysicsService.Data.TableLayer, out Vector3 positionOnTable))
            {
                CurrentHoldingObject.transform.position = positionOnTable;
            }
        }

        #endregion
    }
}