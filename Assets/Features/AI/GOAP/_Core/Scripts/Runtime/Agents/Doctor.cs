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
			const int Min = 50;
			const int Max = 60;
			Invoke(nameof(GetTired), Random.Range(Min, Max));
		}

		private void GetTired()
		{
			Beliefs.AddState("exhausted", 0);
			InvokeGetTired();
		}

		private void InvokeNeedRelief()
		{
			const int Min = 60;
			const int Max = 120;
			Invoke(nameof(NeedRelief), Random.Range(Min, Max));
		}

		private void NeedRelief()
		{
			Beliefs.AddState("needRelief", 0);
			InvokeNeedRelief();
		}
	}
}
