using System.Collections.Generic;

namespace Project.AI.GOAP
{
	public class Node
	{
		public Node Parent { get; }
		public float Cost { get; }
		public Dictionary<string, int> State { get; }
		public GAction Action { get; }

		public Node(Node parent, float cost, Dictionary<string, int> allStates, GAction action)
		{
			Parent = parent;
			Cost = cost;
			State = new(allStates); // copy of the dictionary
			Action = action;
		}
	}
}
