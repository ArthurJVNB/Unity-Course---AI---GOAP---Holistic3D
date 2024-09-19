using UnityEngine;

namespace Project.AI.GOAP
{
	public class Nurse : GAgent
	{
		protected override void Start()
		{
			base.Start();
			SubGoal subGoal = new("treatPatient", 1, true);
			_goals.Add(subGoal, 3);
		}
	}
}
