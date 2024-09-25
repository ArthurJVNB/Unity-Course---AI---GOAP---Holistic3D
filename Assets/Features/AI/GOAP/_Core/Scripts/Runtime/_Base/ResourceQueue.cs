using System.Collections.Generic;
using UnityEngine;

namespace Project.AI.GOAP
{
	public class ResourceQueue
	{
		private readonly Queue<GameObject> _queue;
		private readonly string _tag;
		private readonly string _modifyState;
		private readonly WorldStates _worldStates;

		public ResourceQueue(string tag, string modifyState, WorldStates worldStates)
		{
			_queue = new();
			_tag = tag;
			_modifyState = modifyState;
			_worldStates = worldStates;
		}

		public void AddResource(GameObject resource)
		{
			_queue.Enqueue(resource);
			ModifyWorldState(1);
		}

		public GameObject RemoveResource()
		{
			if (_queue.Count == 0) return null;
			ModifyWorldState(-1);
			return _queue.Dequeue();
		}

		private void ModifyWorldState(int value)
		{
			if (string.IsNullOrEmpty(_modifyState)) return;
			_worldStates.ModifyState(_modifyState, value);
		}
	}
}
