using UnityEngine;

namespace Project.AI.GOAP
{
    [RequireComponent(typeof(GAgent))]
    public class GStateMonitor : MonoBehaviour
    {
        [SerializeField] private string _stateToLookFor; // state
        [Min(0)]
        [SerializeField] private float _stateStrength; // initial state
        [Min(0)]
        [SerializeField] private float _stateDecayRate; // how much the state strength decay over time
        [SerializeField] private WorldStates _beliefs;
        [SerializeField] private GameObject _resourcePrefab; // what to instantiate when the state strength comes to zero
        [SerializeField] private Vector3 _instantiationOffset;
        // [SerializeField] private string _queueName; // queue of resources to put the instantiated gameobject (already done by _resource)
        // [SerializeField] private string _worldState; // world state that will be triggered upon the instantiation of the gameobject (already done by _resource)
        [SerializeField] private Resource _resource; // resource to put the instantiated gameobject
        [SerializeField] private GAction _actionThatSatisfies; // action to monitor if it's already trying to satisfy the same state

        private GAgent _agent;
        private bool _stateFound = false;
        private float _initialStrength;

		private void Awake()
		{
            _agent = GetComponent<GAgent>();
            _beliefs = _agent.Beliefs;
            _initialStrength = _stateStrength;
		}

		private void FixedUpdate()
		{
			if (_actionThatSatisfies.Running)
			{
				_stateFound = false;
				ResetStateStrength();
			}

			if (!_stateFound && _beliefs.HasState(_stateToLookFor))
                _stateFound = true;

            if (_stateFound)
            {
                _stateStrength -= _stateDecayRate * Time.fixedDeltaTime;
                if (_stateStrength < 0)
                {
                    Vector3 location = new(transform.position.x, transform.position.y, transform.position.z);
                    location += _instantiationOffset;

					GameObject item = Instantiate(_resourcePrefab, location, _resourcePrefab.transform.rotation);
                    _stateFound = false;
                    ResetStateStrength();
                    _agent.RemoveState(_stateToLookFor);
                    GWorld.AddResource(_resource, item);
				}
            }
		}

		private void ResetStateStrength()
		{
			_stateStrength = _initialStrength;
		}
	}
}
