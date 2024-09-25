using UnityEngine;

namespace Project.AI.GOAP
{
    public class Patient : GAgent
    {
		protected override void Start()
		{
			base.Start();

			_goals.Add(new SubGoal("isWaiting", 1, true), 1);
			_goals.Add(new SubGoal("isTreated", 1, true), 5);
			_goals.Add(new SubGoal("relief", 1, false), 3);
			_goals.Add(new SubGoal("isHome", 1, true), 3);

			InvokeNeedRelief();
		}

		private void InvokeNeedRelief()
		{
			Invoke(nameof(NeedRelief), Random.Range(20, 30));
		}

		private void NeedRelief()
		{
			Beliefs.AddState("needRelief", 0);
			InvokeNeedRelief();
		}
	}
}
