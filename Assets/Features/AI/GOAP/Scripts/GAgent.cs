using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.AI.GOAP
{
	public class GAgent : MonoBehaviour
    {
        [SerializeField] private List<GAction> _actions = new();
        [SerializeField] private Dictionary<SubGoal, int> _goals = new();

        private GPlanner _planner;
        private Queue<GAction> _actionQueue;

        [ReadOnly, SerializeField] private GAction _currentAction;
        private SubGoal _currentGoal;

		private void Start()
		{
			Init();
		}

		private void LateUpdate()
		{
			
		}

		private void Init()
		{
			var actions = GetComponentsInChildren<GAction>();
			if (actions == null) return;
			foreach (var action in actions)
				_actions.Add(action);
		}
	}
}
