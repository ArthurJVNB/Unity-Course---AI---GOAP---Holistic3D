using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.AI.GOAP
{
	public class GAgent : MonoBehaviour
	{
		[SerializeField] private List<GAction> _actions = new();
		[SerializeField] protected Dictionary<SubGoal, int> _goals = new();

		private GPlanner _planner;
		private Queue<GAction> _actionQueue;

		[ReadOnly, SerializeField] private GAction _currentAction;
		private SubGoal _currentGoal;
		private bool _invoked = false;

		public List<GAction> Actions => _actions;
		public Queue<GAction> ActionQueue => _actionQueue;
		public GAction CurrentAction => _currentAction;
		public Dictionary<SubGoal, int> Goals => _goals;

		protected virtual void Start()
		{
			Init();
		}

		protected virtual void LateUpdate()
		{
			// If agent does have a plan
			if (_currentAction != null && _currentAction.Running)
			{
				// If agent has a path and reached the goal
				const float MIN_REACHED_DISTANCE = 1f;
				if (_currentAction.Agent.hasPath && _currentAction.Agent.remainingDistance < MIN_REACHED_DISTANCE)
				{
					if (!_invoked)
					{
						Invoke(nameof(CompleteAction), _currentAction.Duration);
						_invoked = true;
					}
				}

				return;
			}

			// If agent does not have a plan
			if (_planner == null || _actionQueue == null)
			{
				_planner = new GPlanner();
				//var sortedGoals = from entry in _goals orderby entry.Value descending select entry; // Teacher's code
				var sortedGoals = _goals?.OrderByDescending(v => v.Value); // My code. Same as commented line above
				foreach (var goal in sortedGoals)
				{
					_actionQueue = _planner.Plan(_actions, goal.Key.SGoals, null);
					if (_actionQueue == null) continue;
					_currentGoal = goal.Key;
					break;
				}
			}

			if (_actionQueue != null)
			{
				// If the agent runned out of things to do
				if (_actionQueue.Count == 0)
				{
					if (_currentGoal.Remove)
						_goals.Remove(_currentGoal);
					_planner = null; // Clear planner to start again another plan
				}
				// If the agent still have actions to do
				else if (_actionQueue.Count > 0)
				{
					_currentAction = _actionQueue.Dequeue();
					if (_currentAction.PrePerform())
					{
						if (_currentAction.Target == null && _currentAction.TargetTag != string.Empty)
							_currentAction.Target = GameObject.FindWithTag(_currentAction.TargetTag);
						if (_currentAction.Target != null)
						{
							_currentAction.Running = true;
							_currentAction.Agent.SetDestination(_currentAction.Target.transform.position);
						}
					}
					else
					{
						// Force to get a new plan
						_actionQueue = null;
					}
				}
			}
		}

		private void Init()
		{
			var actions = GetComponentsInChildren<GAction>();
			if (actions == null) return;
			foreach (var action in actions)
				_actions.Add(action);
		}

		private void CompleteAction()
		{
			_currentAction.Running = false;
			_currentAction.PostPerform();
			_invoked = false;
		}
	}
}
