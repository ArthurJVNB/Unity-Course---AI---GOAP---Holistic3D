using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Project.AI.GOAP
{
	[RequireComponent(typeof(GAgent))]
	public abstract class GAction : MonoBehaviour
	{
		[SerializeField] private string _actionName = "Action";
		[SerializeField] private float _cost = 1.0f;
		[SerializeField] private GameObject _target;
		[TagSelector]
		[SerializeField] private string _targetTag;
		[SerializeField] private float _duration = 0;
		[SerializeField] private WorldState[] _preConditions;
		[SerializeField] private WorldState[] _afterEffects;
		[SerializeField] private NavMeshAgent _agent;

		[SerializeField] private Dictionary<string, int> _preConditionsDict;
		[SerializeField] private Dictionary<string, int> _effects;

		[SerializeField] private WorldStates _agentBeliefs;

		[SerializeField] private GInventory _inventory;
		[SerializeField] private WorldStates _beliefs;

		[SerializeField] private bool _running = false;

		#region Public Properties
		public string ActionName => _actionName;
		public Dictionary<string, int> Effects => _effects;
		public float Cost => _cost;

		public bool Running
		{
			get => _running;
			set => _running = value;
		}

		public NavMeshAgent Agent => _agent;
		public float Duration => _duration;

		public GameObject Target
		{
			get => _target;
			set => _target = value;
		}

		public string TargetTag => _targetTag;
		public Dictionary<string, int> PreConditions => _preConditionsDict;

		public GInventory Inventory
		{
			get => _inventory;
			set => _inventory = value;
		}

		protected WorldStates Beliefs
		{
			get => _beliefs;
			set => _beliefs = value;
		}
		#endregion

		protected virtual void Reset()
		{
			_actionName = GetType().Name;
		}

		protected virtual void Awake()
		{
			Init();
		}

		private void Init()
		{
			_preConditionsDict = new();
			_effects = new();
			
			if (!_agent) _agent = GetComponentInChildren<NavMeshAgent>();

			if (_preConditions != null)
				foreach (var condition in _preConditions)
					_preConditionsDict.Add(condition.Key, condition.Value);

			if (_afterEffects != null)
				foreach (var afterEffect in _afterEffects)
					_effects.Add(afterEffect.Key, afterEffect.Value);

			GAgent agent = GetComponent<GAgent>();
			_inventory = agent.Inventory;
			_beliefs = agent.Beliefs;
		}

		public bool IsAchievable()
		{
			return true;
		}

		public bool IsAchievableGiven(Dictionary<string, int> conditions)
		{
			if (conditions == null)
				return _preConditionsDict == null || _preConditionsDict.Count == 0;

			foreach (var preCondition in _preConditionsDict)
			{
				if (!conditions.ContainsKey(preCondition.Key))
					return false;
			}

			return true;
		}

		public abstract bool PrePerform();
		public abstract bool PostPerform();
	}
}
