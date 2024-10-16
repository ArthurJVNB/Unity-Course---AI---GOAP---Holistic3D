using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Project
{
	public class ButtonPrefabSelector : MonoBehaviour
	{
		public event Action<GameObject> OnClick;

		[SerializeField] private Button _button;
		[SerializeField] private GameObject _prefab;
		[Header("Events")]
		[Space, SerializeField] private UnityEvent<GameObject> _onClick;

		private void Reset() => _button = GetComponentInChildren<Button>();

		private void OnEnable() => _button.onClick.AddListener(Button_OnClick);
		private void OnDisable() => _button.onClick.RemoveListener(Button_OnClick);

		private void Button_OnClick()
		{
			Notify_OnClick();
		}

		private void Notify_OnClick()
		{
			_onClick?.Invoke(_prefab);
			OnClick?.Invoke(_prefab);
		}
	}
}
