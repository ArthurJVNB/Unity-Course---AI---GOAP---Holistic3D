using UnityEngine;

namespace Project.AI.GOAP
{
    public class Patient : GAgent
    {
		protected override void Start()
		{
			base.Start();
			InitGoals();
			InvokeNeedRelief();
		}

		private void InitGoals()
		{
			_goals.Add(new SubGoal("isWaiting", 1, true), 3);
			_goals.Add(new SubGoal("isTreated", 1, true), 5);
			_goals.Add(new SubGoal("isHome", 1, true), 3);
			_goals.Add(new SubGoal("needRelief", 1, true), 3);
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
