using System.Collections.Generic;
using UnityEngine;

namespace Project.AI.GOAP
{
	public sealed class GWorld
	{
		private static GWorld _instance;
		private static WorldStates _world;
		private static Queue<GameObject> _patients;

		[UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Reset()
		{
			_instance = new();
			_world = new();
			_patients = new();
		}

		static GWorld()
		{
			_world = new();
			_patients = new();
		}

		private GWorld() { }

		public static GWorld Instance => _instance;

		public static WorldStates World => Instance.GetWorld();

		public WorldStates GetWorld() => _world;

		public static void AddPatient(GameObject patient)
		{
			_patients ??= new();
			_patients.Enqueue(patient);
		}

		public static GameObject RemovePatient()
		{
			if (_patients == null || _patients.Count == 0) return null;
			return _patients.Dequeue();
		}
	}
}
