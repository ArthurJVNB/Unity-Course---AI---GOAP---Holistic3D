using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
#endif
#if ENABLE_LEGACY_INPUT_MANAGER && !ENABLE_INPUT_SYSTEM
#endif

namespace Project
{
	public class GameplayController : MonoBehaviour
	{
		public event Action<GameObject> OnSpawned;
		public event Action<GameObject> OnEndPositioning;

		[Header("Ray Settings")]
		[Tooltip("Only applicable to instantiate or move object, not when trying to pick a Pickable Object.")]
		[SerializeField] private LayerMask _layerMask = 1 << 0;
		[Min(100)]
		[SerializeField] private float _rayMaxDistance = 10000;
		[Space]
		[SerializeField] private GameObject _prefabToSpawn;
		[SerializeField] private Transform _parent;
		[Space]
		[ContextMenuItem("Find Prefab Selectors Buttons", nameof(FindButtonPrefabSelectors))]
		[SerializeField] private ButtonPrefabSelector[] _selectorButtons;
		[Header("Events")]
		[Space, SerializeField] private UnityEvent<GameObject> _onSpawned;
		[Space, SerializeField] private UnityEvent<GameObject> _onEndPositioning;

		private GameObject _currentObject;
		private Vector3 _cachePosition;
		private Quaternion _cacheRotation;
		private Vector3 _pointerOffset;
		private Dictionary<GameObject, LayerMask> _cachedLayerMasks;
		private bool _shouldDeleteObject;
		private bool _isPointerOverUI;

		private void Reset()
		{
			FindButtonPrefabSelectors();
		}

		private void OnEnable()
		{
			OnEnableInternal_Input();
			SubscribeAll();
		}

		private void OnDisable()
		{
			OnDisableInternal_Input();
			UnsubscribeAll();
		}

		private void Update()
		{
			_isPointerOverUI = EventSystem.current.IsPointerOverGameObject();
			UpdateInternal_Input();
		}

		public void PointerEnterTrash()
		{
			_shouldDeleteObject = true;
		}

		public void PointerExitTrash()
		{
			_shouldDeleteObject = false;
		}

		public void SetPrefabToSpawn(GameObject gameObject)
		{
			_prefabToSpawn = gameObject;
		}

		private void SubscribeAll()
		{
			if (_selectorButtons == null) return;
			foreach (var button in _selectorButtons)
				button.OnClick += ButtonPrefabSelector_OnClick;
		}

		private void UnsubscribeAll()
		{
			if (_selectorButtons == null) return;
			foreach (var button in _selectorButtons)
				button.OnClick -= ButtonPrefabSelector_OnClick;
		}

		private void ButtonPrefabSelector_OnClick(GameObject prefab)
		{
			SetPrefabToSpawn(prefab);
		}

		[ContextMenu("Find Prefab Selector Buttons")]
		private void FindButtonPrefabSelectors()
		{
			_selectorButtons = FindObjectsByType<ButtonPrefabSelector>(FindObjectsSortMode.None);
		}

		private void CacheCurrentObject()
		{
			if (!_currentObject) return;
			_cachePosition = _currentObject.transform.position;
			_cacheRotation = _currentObject.transform.rotation;
		}

		private void ApplyCacheOnCurrentObject()
		{
			if (!_currentObject) return;
			_currentObject.transform.position = _cachePosition;
			_currentObject.transform.rotation = _cacheRotation;
		}

		#region Input
		#region Input: New Input System
#if ENABLE_INPUT_SYSTEM
		private InputAction _clickAction;
		private InputAction _previousAction;
		private InputAction _nextAction;
		private InputAction _deleteAction;

		private void OnEnableInternal_Input()
		{
			_clickAction = InputSystem.actions.FindAction("Click");
			_clickAction.performed += ClickAction_performed;

			_previousAction = InputSystem.actions.FindAction("Previous");
			_previousAction.performed += PreviousAction_performed;

			_nextAction = InputSystem.actions.FindAction("Next");
			_nextAction.performed += NextAction_performed;

			_deleteAction = InputSystem.actions.FindAction("Interact");
			_deleteAction.performed += DeleteAction_performed;
		}

		private void OnDisableInternal_Input()
		{
			if (_clickAction != null) _clickAction.performed -= ClickAction_performed;
			if (_previousAction != null) _previousAction.performed -= PreviousAction_performed;
			if (_nextAction != null) _nextAction.performed -= NextAction_performed;
			if (_deleteAction != null) _deleteAction.performed -= DeleteAction_performed;
		}

		private void UpdateInternal_Input()
		{
			if (_clickAction.IsPressed() && !_isPointerOverUI)
				MoveCurrentObject(GetPointerRay());
		}

		private void ClickAction_performed(InputAction.CallbackContext context)
		{
			if (context.action.WasPressedThisFrame())
			{
				Ray ray = GetPointerRay();

				if (TryPickObject(ray))
					return;

				InstantiateObject(ray);
				return;
			}

			if (context.action.WasReleasedThisFrame())
			{
				if (_shouldDeleteObject)
				{
					DeleteCurrentObject();
					_shouldDeleteObject = false;
				}
				else
				{
					DropCurrentObject(GetPointerRay());
					ApplyCachedLayerMasks();
				}

				if (_currentObject) Debug.DrawLine(Camera.main.transform.position, _currentObject.transform.position, Color.green, 10);
				Notify_OnEndPositiong();
			}
		}

		private void PreviousAction_performed(InputAction.CallbackContext context)
		{
			if (!context.action.WasPressedThisFrame()) return;
			RotateObject(false);
		}

		private void NextAction_performed(InputAction.CallbackContext context)
		{
			if (!context.action.WasPressedThisFrame()) return;
			RotateObject(true);
		}

		private void DeleteAction_performed(InputAction.CallbackContext context)
		{
			DeleteCurrentObject();
		}

		private static Ray GetPointerRay()
		{
			bool isScreenPositionDefined = false;
			Vector2 screenPosition = Vector2.zero;
			Ray ray = default;
			if (EnhancedTouchSupport.enabled)
			{
				if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
				{
					screenPosition = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].screenPosition;
					isScreenPositionDefined = true;
				}
			}

			if (!isScreenPositionDefined)
			{
				Pen pen = Pen.current;
				if (pen != null && pen.IsPressed())
				{
					screenPosition = pen.position.value;
					isScreenPositionDefined = true;
				}
			}

			if (!isScreenPositionDefined)
			{
				Mouse mouse = Mouse.current;
				if (mouse != null)
				{
					screenPosition = mouse.position.value;
					isScreenPositionDefined = true;
				}
			}

			if (isScreenPositionDefined)
				ray = Camera.main.ScreenPointToRay(screenPosition);

			return ray;
		}

#endif
		#endregion

		#region Input: Legacy Input System (not implemented)
#if ENABLE_LEGACY_INPUT_MANAGER && !ENABLE_INPUT_SYSTEM
		
#endif
		#endregion
		#endregion

		#region Layer Masks
		private void CacheLayerMasks(Collider[] colliders)
		{
			if (colliders == null) return;

			_cachedLayerMasks ??= new();
			_cachedLayerMasks.Clear();

			foreach (Collider collider in colliders)
				_cachedLayerMasks.TryAdd(collider.gameObject, collider.gameObject.layer);
		}

		private void ApplyCachedLayerMasks()
		{
			if (_cachedLayerMasks == null) return;
			foreach (var item in _cachedLayerMasks)
				if (item.Key) item.Key.layer = item.Value;
		}

		private void ApplyIgnoreLayerMask(Collider[] colliders)
		{
			if (colliders == null) return;
			foreach (var collider in colliders)
				collider.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		}
		#endregion

		#region Pickable Object
		private bool TryPickObject(Ray ray)
		{
			if (_isPointerOverUI) return false;
			if (Physics.Raycast(ray, out RaycastHit hit, _rayMaxDistance))
			{
				PickableObject pickedObject = hit.transform.GetComponentInChildren<PickableObject>();
				if (!pickedObject) pickedObject = hit.transform.GetComponentInParent<PickableObject>();
				if (!pickedObject) return false;

				_currentObject = pickedObject.gameObject;
				Collider[] colliders = _currentObject.GetComponentsInChildren<Collider>();
				CacheLayerMasks(colliders);
				ApplyIgnoreLayerMask(colliders);

				if (Physics.Raycast(ray, out hit))
					_pointerOffset = _currentObject.transform.position - hit.point;

				CacheCurrentObject();
				return true;
			}

			return false;
		}

		private void InstantiateObject(Ray ray)
		{
			if (_isPointerOverUI) return;
			if (!_prefabToSpawn) return;
			if (Physics.Raycast(ray, out RaycastHit hit, _rayMaxDistance, _layerMask))
			{
				Vector3 spawnPosition = hit.point;
				Debug.Log($"spawn {_prefabToSpawn.name} at position {spawnPosition} (hit object {hit.transform.name})");
				Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 10);
				_currentObject = Instantiate(_prefabToSpawn, spawnPosition, _prefabToSpawn.transform.rotation, _parent);
				_pointerOffset = Vector3.zero;
				Collider[] colliders = _currentObject.GetComponentsInChildren<Collider>();
				CacheLayerMasks(colliders);
				CacheCurrentObject();
				ApplyIgnoreLayerMask(colliders);
				Notify_OnSpawned();
			}
		}

		private void MoveCurrentObject(Ray ray)
		{
			if (!_currentObject) return;
			if (Physics.Raycast(ray, out RaycastHit hit, _rayMaxDistance, _layerMask))
			{
				Vector3 newPosition = hit.point + _pointerOffset;
				Debug.DrawLine(Camera.main.transform.position, hit.point, Color.blue, 1);
				_currentObject.transform.position = newPosition;
			}
		}

		private void RotateObject(bool clockwise)
		{
			if (!_currentObject) return;
			_currentObject.transform.Rotate(new Vector3(0, clockwise ? 90 : -90, 0));
		}

		private void DropCurrentObject(Ray ray)
		{
			if (!_currentObject) return;
			if (_isPointerOverUI)
				ApplyCacheOnCurrentObject();
			else
			{
				MoveCurrentObject(ray);
				CacheCurrentObject();
			}
		}

		private void DeleteCurrentObject()
		{
			if (!_currentObject) return;
			Destroy(_currentObject);
		}
		#endregion

		#region Notifiers
		private void Notify_OnSpawned()
		{
			_onSpawned?.Invoke(_currentObject);
			OnSpawned?.Invoke(_currentObject);
		}

		private void Notify_OnEndPositiong()
		{
			_onEndPositioning?.Invoke(_currentObject);
			OnEndPositioning?.Invoke(_currentObject);
		}
		#endregion
	}
}
