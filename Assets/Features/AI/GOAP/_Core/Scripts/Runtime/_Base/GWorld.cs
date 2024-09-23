using System.Collections.Generic;
using UnityEngine;

namespace Project.AI.GOAP
{
	public sealed class GWorld
	{
		private static GWorld _instance;
		private static WorldStates _world;
		private static Queue<GameObject> _patients;
		private static Queue<GameObject> _cubicles;
		private static Queue<GameObject> _offices;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Reset()
		{
			_patients = null;
			_cubicles = null;
			_offices = null;

			_instance = new();
			_world = new();
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

		#region Patients
		public static void AddPatient(GameObject patient)
		{
			_patients ??= new();
			_patients.Enqueue(patient);
			World.ModifyState("Waiting", 1);
		}

		public static GameObject RemovePatient()
		{
			if (_patients == null || _patients.Count == 0) return null;
			World.ModifyState("Waiting", -1);
			return _patients.Dequeue();
		}
		#endregion

		#region Cubicles
		public static void AddCubicle(GameObject cubicle)
		{
			_cubicles ??= new();
			_cubicles.Enqueue(cubicle);
			World.ModifyState("FreeCubicle", 1);
		}

		public static GameObject RemoveCubicle()
		{
			if (_cubicles == null || _cubicles.Count == 0) return null;
			World.ModifyState("FreeCubicle", -1);
			return _cubicles.Dequeue();
		}
		#endregion

		#region Offices
		public static void AddOffice(GameObject office)
		{
			_offices ??= new();
			_offices.Enqueue(office);
			World.ModifyState("FreeOffice", 1);
		}

		public static GameObject RemoveOffice()
		{
			if (_offices == null || _offices.Count == 0) return null;
			World.ModifyState("FreeOffice", -1);
			return _offices.Dequeue();
		}
		#endregion
	}
}
