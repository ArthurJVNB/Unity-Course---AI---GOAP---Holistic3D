using System.Collections.Generic;

namespace Project.AI.GOAP
{
	public class SubGoal
    {
        /// <summary>
        /// SubGoals
        /// </summary>
        public Dictionary<string, int> SGoals;
        /// <summary>
        /// When the subgoals are complete, should this subgoal be removed?
        /// </summary>
        public bool Remove;

		public SubGoal(string key, int value, bool remove)
		{
            SGoals = new() { {  key, value } };
            Remove = remove;
		}
	}
}
