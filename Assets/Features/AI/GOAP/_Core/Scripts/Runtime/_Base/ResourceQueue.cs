using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.AI.GOAP
{
	public class ResourceQueue
	{
		private Queue<GameObject> _queue;
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

		public GameObject RemoveResource(GameObject resource)
		{
			if (_queue.Count == 0) return null;
			if (!_queue.Contains(resource)) return null;
			_queue = new(_queue.Where(v => v != resource));
			ModifyWorldState(-1);
			return resource;
		}

		private void ModifyWorldState(int value)
		{
			if (string.IsNullOrEmpty(_modifyState)) return;
			_worldStates.ModifyState(_modifyState, value);
		}
	}
}
