﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Project.AI.GOAP
{
	public abstract class GAction : MonoBehaviour
    {
        [SerializeField] private string _actionName = "Action";
        [SerializeField] private float _cost = 1.0f;
        [SerializeField] private GameObject _target;
        [SerializeField] private GameObject _targetTag;
        [SerializeField] private float _duration = 0;
        [SerializeField] private WorldState[] _preConditions;
        [SerializeField] private WorldState[] _afterEffects;
        [SerializeField] private NavMeshAgent _agent;

        [SerializeField] private Dictionary<string, int> _preConditionsDict;
        [SerializeField] private Dictionary<string, int> _effects;

        [SerializeField] private WorldStates _agentBeliefs;

        [SerializeField] private bool _running = false;

        public string ActionName => _actionName;

		private void Awake()
		{
			Init();
		}

		private void Init()
        {
            _preConditionsDict = new();
            _effects = new();

            if (_preConditions != null)
                foreach (var condition in _preConditions)
                    _preConditionsDict.Add(condition.Key, condition.Value);

            if (_afterEffects != null)
                foreach (var afterEffect in _afterEffects)
                    _effects.Add(afterEffect.Key, afterEffect.Value);
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
