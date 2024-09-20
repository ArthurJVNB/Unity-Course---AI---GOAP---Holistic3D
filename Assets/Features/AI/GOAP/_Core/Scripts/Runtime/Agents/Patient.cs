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
			_goals.Add(new SubGoal("isWaiting", 1, true), 3);
			_goals.Add(new SubGoal("isTreated", 1, true), 5);
		}
	}
}
