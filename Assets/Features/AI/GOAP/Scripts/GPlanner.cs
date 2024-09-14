using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.AI.GOAP
{
	public class GPlanner
	{
		public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates states)
		{
			List<GAction> usableActions = new();
			foreach (GAction action in actions)
			{
				if (action.IsAchievable())
					usableActions.Add(action);
			}

			List<Node> leaves = new();
			Node start = new(null, 0, GWorld.Instance.GetWorld().GetStates(), null);

			bool success = BuildGraph(start, leaves, usableActions, goal);

			if (!success)
			{
				Debug.Log("NO PLAN");
				return null;
			}

			Node cheapest = null;
			foreach (Node leaf in leaves)
			{
				if (cheapest == null)
					cheapest = leaf;
				else if (leaf.Cost < cheapest.Cost)
					cheapest = leaf;
			}

			// Create linked list of nodes
			List<GAction> result = new();
			Node node = cheapest;
			while (node != null)
			{
				if (node.Action != null)
					result.Insert(0, node.Action);
				node = node.Parent;
			}

			Queue<GAction> queue = new();
			foreach (GAction action in result)
				queue.Enqueue(action);

			Debug.Log("The Plan is: ");
			foreach (GAction action in queue)
				Debug.Log("Q: " + action.ActionName);

			return queue;
		}

		private bool BuildGraph(Node start, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
		{
			throw new NotImplementedException();
		}
	}
}
