using UnityEngine;

namespace Project.AI.GOAP
{
	public class Nurse : GAgent
	{
		protected override void Start()
		{
			base.Start();
			_goals.Add(new SubGoal("treatPatient", 1, false), 3);
			_goals.Add(new SubGoal("rested", 1, false), 1);
			_goals.Add(new SubGoal("relief", 1, false), 1);

			InvokeGetTired();
			InvokeNeedRelief();
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

		private void InvokeNeedRelief()
		{
			Invoke(nameof(NeedRelief), Random.Range(20, 30));
		}

		private void NeedRelief()
		{
			Beliefs.ModifyState("needRelief", 0);
			InvokeNeedRelief();
		}
	}
}
