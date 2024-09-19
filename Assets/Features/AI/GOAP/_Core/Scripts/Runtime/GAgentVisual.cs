using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.AI.GOAP
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(GAgent))]
	public class GAgentVisual : MonoBehaviour
	{
		private GAgent _agent;
		public GAgent Agent => _agent;

		// Start is called before the first frame update
		void Start()
		{
			_agent = GetComponent<GAgent>();
		}

	}
}
