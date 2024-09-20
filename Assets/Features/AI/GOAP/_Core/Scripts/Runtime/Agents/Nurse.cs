using UnityEngine;

namespace Project.AI.GOAP
{
	public class Nurse : GAgent
	{
		protected override void Start()
		{
			base.Start();
			_goals.Add(new SubGoal("treatPatient", 1, false), 3);
		}
	}
}
