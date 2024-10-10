using System;
using UnityEngine;
using UnityEngine.Events;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
#endif
#if ENABLE_LEGACY_INPUT_MANAGER && !ENABLE_INPUT_SYSTEM
#endif

namespace Project
{
	public class SpawnOnClick : MonoBehaviour
	{
		public event Action<GameObject> OnSpawned;
		public event Action<GameObject> OnEndPositioning;

		[SerializeField] private GameObject _prefabToSpawn;
		[SerializeField] private Transform _parent;
		[Header("Events")]
		[Space, SerializeField] private UnityEvent<GameObject> _onSpawned;
		[Space, SerializeField] private UnityEvent<GameObject> _onEndPositioning;

		private GameObject _instantiatedObject;
		private LayerMask _originalObjectLayer;

		#region New Input System
#if ENABLE_INPUT_SYSTEM
		private InputAction _clickAction;

		private void OnEnable()
		{
			_clickAction = InputSystem.actions.FindAction("Click");
			_clickAction.performed += ClickAction_performed;
		}

		private void OnDisable()
		{
			if (_clickAction != null) _clickAction.performed -= ClickAction_performed;
		}

		private void Update()
		{
			if (_clickAction.IsPressed())
				MoveInstantiatedObject();
		}

		private void ClickAction_performed(InputAction.CallbackContext context)
		{
			if (context.action.WasPressedThisFrame())
			{
				Ray ray = GetPointerRay();
				//Debug.DrawRay(Camera.main.transform.position, ray.direction * 1000, Color.red, 10);

				if (Physics.Raycast(ray, out RaycastHit hit))
				{
					Vector3 spawnPosition = hit.point;
					Debug.Log($"spawn at position {spawnPosition} (hit object {hit.transform.name})");
					Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 10);
					_instantiatedObject = Instantiate(_prefabToSpawn, spawnPosition, _prefabToSpawn.transform.rotation, _parent);
					_originalObjectLayer = _instantiatedObject.layer;
					_instantiatedObject.layer = LayerMask.NameToLayer("Ignore Raycast");
					Notify_OnSpawned();
				}
				return;
			}

			if (context.action.WasReleasedThisFrame())
			{
				MoveInstantiatedObject();
				_instantiatedObject.layer = _originalObjectLayer;
				Debug.DrawLine(Camera.main.transform.position, _instantiatedObject.transform.position, Color.green, 10);
				Notify_OnEndPositiong();
			}
		}

		private void MoveInstantiatedObject()
		{
			if (!_instantiatedObject) return;
			if (Physics.Raycast(GetPointerRay(), out RaycastHit hit))
			{
				Vector3 newPosition = hit.point;
				Debug.DrawLine(Camera.main.transform.position, hit.point, Color.blue, 1);
				_instantiatedObject.transform.position = newPosition;
			}
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

		private void Notify_OnSpawned()
		{
			_onSpawned?.Invoke(_instantiatedObject);
			OnSpawned?.Invoke(_instantiatedObject);
		}

		private void Notify_OnEndPositiong()
		{
			_onEndPositioning?.Invoke(_instantiatedObject);
			OnEndPositioning?.Invoke(_instantiatedObject);
		}
#endif
		#endregion

		#region Legacy Input System (not implemented)
#if ENABLE_LEGACY_INPUT_MANAGER && !ENABLE_INPUT_SYSTEM
		
#endif
		#endregion
	}
}
