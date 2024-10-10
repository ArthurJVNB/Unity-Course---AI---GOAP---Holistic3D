using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
#endif
#if ENABLE_LEGACY_INPUT_MANAGER && !ENABLE_INPUT_SYSTEM
#endif

namespace Project
{
	public class SpawnOnClick : MonoBehaviour
	{
		[SerializeField] private GameObject _prefabToSpawn;

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

		private void ClickAction_performed(InputAction.CallbackContext context)
		{
			if (!context.action.WasReleasedThisFrame()) return;

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

			if (!isScreenPositionDefined) return;

			ray = Camera.main.ScreenPointToRay(screenPosition);
			//Debug.DrawRay(Camera.main.transform.position, ray.direction * 1000, Color.red, 10);

			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				Vector3 spawnPosition = hit.point;
				Debug.Log($"spawn at position {spawnPosition} (hit object {hit.transform.name})");
				Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 10);
				Instantiate(_prefabToSpawn, spawnPosition, _prefabToSpawn.transform.rotation);
			}
		}
#endif
		#endregion

		#region Legacy Input System
#if ENABLE_LEGACY_INPUT_MANAGER && !ENABLE_INPUT_SYSTEM
		
#endif
		#endregion
	}
}
