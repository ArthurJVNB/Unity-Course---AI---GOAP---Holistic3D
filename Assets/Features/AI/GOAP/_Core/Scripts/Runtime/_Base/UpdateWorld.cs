using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.AI.GOAP
{
	public class UpdateWorld : MonoBehaviour
	{
		[SerializeField] private Text _states;

		private Dictionary<string, int> _worldStates;

		private void Reset() => _states = GetComponentInChildren<Text>();
		private void Start() => _worldStates = GWorld.World.States;
		private void LateUpdate() => UpdateUI();

		private void UpdateUI()
		{
			string text = string.Empty;
			foreach (var state in _worldStates)
				text += $"{state.Key}, {state.Value}\n";
			_states.text = text;
		}
	}
}
