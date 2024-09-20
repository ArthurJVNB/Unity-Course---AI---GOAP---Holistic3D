using UnityEngine;

namespace Project.AI.GOAP
{
	public class Nurse : GAgent
	{
		protected override void Start()
		{
			base.Start();
			_goals.Add(new SubGoal("treatPatient", 1, true), 3);
			_goals.Add(new SubGoal("Waiting", 1, true), 1);
		}
	}
}
