using UnityEngine;

namespace Project.AI.GOAP
{
    public class Janitor : GAgent
    {
		protected override void Start()
		{
			base.Start();
			_goals.Add(new SubGoal("clean", 1, false), 3);
		}
	}
}
