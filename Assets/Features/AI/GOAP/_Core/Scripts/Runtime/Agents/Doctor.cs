using UnityEngine;

namespace Project.AI.GOAP
{
	public class Doctor : GAgent
	{
		protected override void Start()
		{
			base.Start();
			_goals.Add(new SubGoal("rested", 1, false), 3);

			InvokeGetTired();
		}

		private void InvokeGetTired()
		{
			Invoke(nameof(GetTired), Random.Range(10, 20));
		}

		private void GetTired()
		{
			Beliefs.ModifyState("exhausted", 0);
			InvokeGetTired();
		}
	}
}
