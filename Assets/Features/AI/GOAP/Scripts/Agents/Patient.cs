using System;
using UnityEngine;

namespace Project.AI.GOAP
{
    public class Patient : GAgent
    {
		protected override void Start()
		{
			base.Start();
			InitGoals();
		}

		private void InitGoals()
		{
			SubGoal subGoal1 = new("isWaiting", 1, true);
			_goals.Add(subGoal1, 3);
		}
	}
}
