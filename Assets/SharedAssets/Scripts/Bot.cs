using UnityEngine;
using UnityEngine.AI;

namespace Project
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Bot : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private NavMeshAgent Agent
        {
            get
            {
                if (!_agent)
                    _agent = GetComponent<NavMeshAgent>();
                return _agent;
            }
        }

        public void Move()
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Agent.destination = hitInfo.point;
            }
        }
    }
}
