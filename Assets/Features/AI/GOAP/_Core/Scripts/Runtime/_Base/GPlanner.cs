using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.AI.GOAP
{
	public class GPlanner
	{
		public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates beliefStates)
		{
			List<GAction> usableActions = new();
			foreach (GAction action in actions)
			{
				if (action.IsAchievable())
					usableActions.Add(action);
			}

			List<Node> leaves = new();
			Node start = new(null, 0, GWorld.World.States, beliefStates.States, null);

			bool success = BuildGraph(start, leaves, usableActions, goal);

			if (!success)
			{
				//Debug.Log("NO PLAN");
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

		private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
		{
			bool foundPath = false;
			
			foreach (GAction action in usableActions)
			{
				if (action.IsAchievableGiven(parent.State))
				{
					Dictionary<string, int> currentState = new();
					foreach (var effect in action.Effects)
					{
						if (currentState.ContainsKey(effect.Key)) continue;
						currentState.Add(effect.Key, effect.Value);
					}

					Node node = new(parent, parent.Cost + action.Cost, currentState, action); // Next node

					if (GoalAchieved(goal, currentState))
					{
						leaves.Add(node);
						foundPath = true;
					}
					else
					{
						List<GAction> subset = ActionSubset(usableActions, action);
						bool found = BuildGraph(node, leaves, subset, goal);
						if (found)
							foundPath = true;
					}
				}
			}

			return foundPath;
		}

		private bool GoalAchieved(Dictionary<string, int> goals, Dictionary<string, int> state)
		{
			foreach (var goal in goals)
			{
				if (!state.ContainsKey(goal.Key))
					return false;
			}
			return true;
		}

		private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe)
		{
			List<GAction> subset = new();
			foreach (var action in actions)
			{
				if (action.Equals(removeMe)) continue;
				subset.Add(action);
			}
			return subset;
		}
	}
}
