using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

namespace Project
{
	public class BasicCameraController : MonoBehaviour
	{
		[Header("Input Settings")]
		[SerializeField] private string _moveActionName = "Player/Move";
		[SerializeField] private string _lookActionName = "Player/Look";
		[SerializeField] private string _zoomActionName = "Player/Zoom";

		[Header("Basic Settings")]
		[SerializeField] private Camera _camera;
		[SerializeField] private GameObject _focusObject;
		[Range(0.1f, 50)]
		[SerializeField] private float _desiredDistance = 10;

		[Header("Move Settings")]
		[SerializeField] private float _maxMoveSpeed = 10;
		[Min(0)]
		[SerializeField] private float _moveSmoothTime = 1;

		[Header("Look Settings")]
		[SerializeField] private bool _invertLookHorizontal;
		[SerializeField] private bool _invertLookVertical = true;
		[Range(0, 3)]
		[SerializeField] private float _rotationSensitivity = .5f;
		[Range(0, 10)]
		[SerializeField] private float _rotationSmoothTime = 5f;
		[Range(-180, 180)]
		[SerializeField] private float _minPitchAngle = 10;
		[Range(-180, 180)]
		[SerializeField] private float _maxPitchAngle = 90;

		[Header("Zoom Settings")]
		[SerializeField] private bool _invertZoomInput = true;
		[Min(0)]
		[SerializeField] private float _zoomSpeed = 5;
		[SerializeField] private float _maxZoomSmoothSpeed = 10;
		[SerializeField] private float _zoomSmoothTime = 1;
		[Min(0)]
		[SerializeField] private float _minDistance = 10;
		[Min(0)]
		[SerializeField] private float _maxDistance = 40;

		private InputAction _moveAction;
		private InputAction _lookAction;
		private InputAction _zoomAction;

		private Vector2 _moveInput;
		private Vector2 _lookInput;
		private Vector2 _zoomInput;

		private Vector3 _currentVelocity;
		private Vector3 _targetRotation;

		private float _currentDistance;
		private float _currentDistanceVelocity;
		private float _targetDistance;

		private Vector3 DesiredDistanceOffset => _focusObject.transform.forward * _desiredDistance;

		private void Reset()
		{
			_camera = Camera.main;
			_focusObject = gameObject;
		}

		private void Awake()
		{
			_moveAction = InputSystem.actions.FindAction(_moveActionName);
			_lookAction = InputSystem.actions.FindAction(_lookActionName);
			_zoomAction = InputSystem.actions.FindAction(_zoomActionName);

			_currentDistance = _desiredDistance;
			_targetDistance = _desiredDistance;
			_targetRotation = _camera.transform.rotation.eulerAngles;
			_focusObject.transform.rotation = _camera.transform.rotation;
		}

		private void Start()
		{
			//ValidateParentConstraint();
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

			if (_zoomAction != null)
			{
				_zoomAction.performed += ZoomAction_performed;
				_zoomAction.canceled += ZoomAction_canceled;
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
			Zoom();
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

		private void ZoomAction_performed(InputAction.CallbackContext context) => CacheInput(context, ref _zoomInput);
		private void ZoomAction_canceled(InputAction.CallbackContext context) => CacheInput(context, ref _zoomInput);

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
			_camera.transform.position = _focusObject.transform.position - DesiredDistanceOffset;
		}


		private void Look()
		{
			if (_invertLookHorizontal) _lookInput.x *= -1f;
			if (_invertLookVertical) _lookInput.y *= -1f;
			_targetRotation += new Vector3(_lookInput.y, _lookInput.x, 0) * _rotationSensitivity;
			_targetRotation.x = Mathf.Clamp(_targetRotation.x, _minPitchAngle, _maxPitchAngle);
			_focusObject.transform.rotation = Quaternion.Slerp(_focusObject.transform.rotation, Quaternion.Euler(_targetRotation), Time.deltaTime * _rotationSmoothTime);
			_camera.transform.LookAt(_focusObject.transform);
		}

		private void Zoom()
		{
			if (_zoomInput.y != 0)
			{
				Debug.Log("Zoom");
				float delta = _zoomInput.y * (_invertZoomInput ? -1 : 1) * _zoomSpeed;
				_targetDistance = Mathf.Clamp(_desiredDistance + delta, _minDistance, _maxDistance);
			}
			_currentDistance = Mathf.SmoothDamp(_currentDistance, _targetDistance, ref _currentDistanceVelocity, _zoomSmoothTime, _maxZoomSmoothSpeed, Time.deltaTime);
			_desiredDistance = _currentDistance;
		}
	}
}
