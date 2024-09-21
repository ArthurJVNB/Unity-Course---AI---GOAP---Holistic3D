using System.Collections.Generic;

namespace Project.AI.GOAP
{
	public class WorldStates
	{
		public Dictionary<string, int> States { get; private set; }

		// Constructor
		public WorldStates()
		{
			States = new();
		}

		// HasState
		public bool HasState(string key)
		{
			return States.ContainsKey(key);
		}

		// AddState
		public void AddState(string key, int value)
		{
			States.Add(key, value);
		}

		// ModifyState
		public void ModifyState(string key, int value)
		{
			if (HasState(key))
			{
				States[key] += value;
				if (States[key] <= 0)
					RemoveState(key);
			}
			else
				AddState(key, value);
		}

		// RemoveState
		public void RemoveState(string key)
		{
			States.Remove(key);
		}

		public void SetState(string key, int value)
		{
			if (HasState(key))
				States[key] = value;
			else
				AddState(key, value);
		}

		public Dictionary<string, int> GetStates()
		{
			return States;
		}
	}
}
