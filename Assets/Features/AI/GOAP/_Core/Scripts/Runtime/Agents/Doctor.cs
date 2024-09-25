using UnityEngine;

namespace Project.AI.GOAP
{
	public class Doctor : GAgent
	{
		protected override void Start()
		{
			base.Start();
			_goals.Add(new SubGoal("research", 1, false), 1);
			_goals.Add(new SubGoal("rested", 1, false), 3);
			_goals.Add(new SubGoal("relief", 1, false), 3);

			InvokeGetTired();
			InvokeNeedRelief();
		}

		private void InvokeGetTired()
		{
			Invoke(nameof(GetTired), Random.Range(50, 60));
		}

		private void GetTired()
		{
			Beliefs.AddState("exhausted", 0);
			InvokeGetTired();
		}

		private void InvokeNeedRelief()
		{
			Invoke(nameof(NeedRelief), Random.Range(60, 120));
		}

		private void NeedRelief()
		{
			Beliefs.AddState("needRelief", 0);
			InvokeNeedRelief();
		}
	}
}
