using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

namespace Project
{
	public class BasicCameraController : MonoBehaviour
	{
		[SerializeField] private string _moveActionName = "Player/Move";
		[SerializeField] private string _lookActionName = "Player/Look";
		[Space]
		[SerializeField] private Camera _camera;
		[SerializeField] private GameObject _focusObject;
		[Header("Move Settings")]
		[SerializeField] private float _maxMoveSpeed = 5;
		[Min(0)]
		[SerializeField] private float _moveSmoothTime = .5f;
		[Header("Look Settings")]
		[Range(0, 3)]
		[SerializeField] private float _rotationSensitivity = .5f;
		[Range(0, 10)]
		[SerializeField] private float _rotationSmoothTime = 5f;
		[Range(-180, 180)]
		[SerializeField] private float _minPitchAngle = 10;
		[Range(-180, 180)]
		[SerializeField] private float _maxPitchAngle = 90;

		private InputAction _moveAction;
		private InputAction _lookAction;

		private Vector2 _moveInput;
		private Vector2 _lookInput;

		private Vector3 _currentVelocity;
		private Vector3 _targetRotation;

		private void Reset()
		{
			_camera = Camera.main;
			_focusObject = gameObject;
		}

		private void Awake()
		{
			_moveAction = InputSystem.actions.FindAction(_moveActionName);
			_lookAction = InputSystem.actions.FindAction(_lookActionName);

			_targetRotation = _focusObject.transform.rotation.eulerAngles;
		}

		private void Start()
		{
			ValidateParentConstraint();
		}

		private void OnEnable()
		{
			if (_moveAction != null)
			{
				_moveAction.performed += MoveAction_performed;
				_moveAction.canceled += MoveAction_canceled;
			}
			if (_lookAction != null)
			{
				_lookAction.performed += LookAction_performed;
				_lookAction.canceled += LookAction_canceled;
			}
		}

		private void OnDisable()
		{
			if (_moveAction != null)
			{
				_moveAction.performed -= MoveAction_performed;
				_moveAction.canceled -= MoveAction_canceled;
			}
			if (_lookAction != null)
			{
				_lookAction.performed -= LookAction_performed;
				_lookAction.canceled -= LookAction_canceled;
			}
		}

		private void Update()
		{
			Move();
			Look();
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
				parentConstraint.SetTranslationOffset(index, _camera.transform.position - _focusObject.transform.position);
				parentConstraint.SetRotationOffset(index, _camera.transform.rotation.eulerAngles - _focusObject.transform.rotation.eulerAngles);
				parentConstraint.constraintActive = true;
			}
		}

		private void MoveAction_performed(InputAction.CallbackContext context) => CacheInput(context, ref _moveInput);
		private void MoveAction_canceled(InputAction.CallbackContext context) => CacheInput(context, ref _moveInput);

		private void LookAction_performed(InputAction.CallbackContext context) => CacheInput(context, ref _lookInput);
		private void LookAction_canceled(InputAction.CallbackContext context) => CacheInput(context, ref _lookInput);

		private void CacheInput<T>(InputAction.CallbackContext context, ref T cache) where T : struct
		{
			cache = context.ReadValue<T>();
		}

		private void Move()
		{
			var forward = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up).normalized;
			var right = Vector3.ProjectOnPlane(_camera.transform.right, Vector3.up).normalized;
			Debug.DrawLine(_focusObject.transform.position, _focusObject.transform.position + forward, Color.blue, .1f);
			Debug.DrawLine(_focusObject.transform.position, _focusObject.transform.position + right, Color.red, .1f);

			Vector3 current = _focusObject.transform.position;
			Vector3 delta = (forward * _moveInput.y + right * _moveInput.x) * _maxMoveSpeed;
			Vector3 target = current + delta;
			_focusObject.transform.position = Vector3.SmoothDamp(current, target, ref _currentVelocity, _moveSmoothTime, _maxMoveSpeed, Time.deltaTime);
		}

		private void Look()
		{
			_targetRotation += new Vector3(_lookInput.y, _lookInput.x,0) * _rotationSensitivity;
			_targetRotation.x = Mathf.Clamp(_targetRotation.x, _minPitchAngle, _maxPitchAngle);
			_focusObject.transform.rotation = Quaternion.Slerp(_focusObject.transform.rotation, Quaternion.Euler(_targetRotation), Time.deltaTime * _rotationSmoothTime);
		}

	}
}
