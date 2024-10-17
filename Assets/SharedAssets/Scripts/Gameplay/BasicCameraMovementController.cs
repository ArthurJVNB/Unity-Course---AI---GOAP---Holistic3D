using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

namespace Project
{
	public class BasicCameraMovementController : MonoBehaviour
	{
		[SerializeField] private string _moveActionName = "Player/Move";
		[SerializeField] private string _lookActionName = "Player/Look";
		[Space]
		[SerializeField] private Camera _camera;
		[SerializeField] private GameObject _focusObject;
		[SerializeField] private float _maxSpeed = 5;
		[Min(0)]
		[SerializeField] private float _smoothTime = .5f;

		private InputAction _moveAction;
		private InputAction _lookAction;

		private Vector3 _currentVelocity;

		private void Reset()
		{
			_camera = Camera.main;
			_focusObject = gameObject;
		}

		private void Awake()
		{
			_moveAction = InputSystem.actions.FindAction(_moveActionName);
			_lookAction = InputSystem.actions.FindAction(_lookActionName);
			
		}

		private void Start()
		{
			ValidateParentConstraint();
		}

		private void ValidateParentConstraint()
		{
			var source = new ConstraintSource()
			{
				sourceTransform = _focusObject.transform,
				weight = 1,
			};
			bool addSource;

			if (!_camera.TryGetComponent(out ParentConstraint parentConstraint))
				addSource = true;
			else
			{
				if (parentConstraint.sourceCount == 0)
					addSource = true;
				else
				{
					for (int i = 0; i < parentConstraint.sourceCount; i++)
					{
						List<ConstraintSource> sources = new();
						parentConstraint.GetSources(sources);
						for (int j = 0; j < sources.Count; j++)
							if (sources[i].sourceTransform.gameObject == _focusObject) return;
					}
					addSource = true;
				}
			}
			
			if (addSource)
			{
				parentConstraint = _camera.gameObject.AddComponent<ParentConstraint>();
				int index = parentConstraint.AddSource(source);
				parentConstraint.SetTranslationOffset(index, _camera.transform.position -  _focusObject.transform.position);
				parentConstraint.SetRotationOffset(index, _camera.transform.rotation.eulerAngles - _focusObject.transform.rotation.eulerAngles);
				parentConstraint.constraintActive = true;
			}
		}

		private void OnEnable()
		{
			//if (_moveAction != null) _moveAction.performed += MoveAction_performed;
			if (_lookAction != null) _lookAction.performed += LookAction_performed;
		}

		private void OnDisable()
		{
			//if (_moveAction != null) _moveAction.performed -= MoveAction_performed;
			if (_lookAction != null) _lookAction.performed -= LookAction_performed;
		}

		private void Update()
		{
			Move();
		}

		private void Move()
		{
			var input = _moveAction.ReadValue<Vector2>();
			var forward = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up).normalized;
			var right = Vector3.ProjectOnPlane(_camera.transform.right, Vector3.up).normalized;
			Debug.DrawLine(_focusObject.transform.position, _focusObject.transform.position + forward, Color.blue, .1f);
			Debug.DrawLine(_focusObject.transform.position, _focusObject.transform.position + right, Color.red, .1f);

			Vector3 current = _focusObject.transform.position;
			Vector3 delta = (forward * input.y + right * input.x) * _maxSpeed;
			Vector3 target = current + delta;
			_focusObject.transform.position = Vector3.SmoothDamp(current, target, ref _currentVelocity, _smoothTime, _maxSpeed, Time.deltaTime);
		}

		//private void MoveAction_performed(InputAction.CallbackContext context)
		//{
		//	Debug.Log("Move");
		//	Vector2 input = context.ReadValue<Vector2>();
		//	_object.transform.position += new Vector3(input.x, 0, input.y) * Time.deltaTime;
		//}

		private void LookAction_performed(InputAction.CallbackContext context)
		{
			Debug.Log("Look");
		}

	}
}
