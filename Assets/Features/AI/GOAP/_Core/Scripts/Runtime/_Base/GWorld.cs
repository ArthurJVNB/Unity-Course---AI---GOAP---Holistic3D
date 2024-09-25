using System.Collections.Generic;
using UnityEngine;

namespace Project.AI.GOAP
{
	public sealed class GWorld
	{
		private static WorldStates _world;
		private static Dictionary<Resource, ResourceQueue> _resources;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			_world = new();
			_resources = new();
		}

		public static WorldStates World => _world;

		public static void AddResource(Resource resource, GameObject gameObject)
		{
			if (!_resources.ContainsKey(resource))
				_resources.Add(resource, new(resource.Tag, resource.ModifyState, World));
			_resources[resource].AddResource(gameObject);
		}

		public static GameObject RemoveResource(Resource resource)
		{
			if (!_resources.ContainsKey(resource)) return null;
			return _resources[resource]?.RemoveResource();
		}
	}
}
