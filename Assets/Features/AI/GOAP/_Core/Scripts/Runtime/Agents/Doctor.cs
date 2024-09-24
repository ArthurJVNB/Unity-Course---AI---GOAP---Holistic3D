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
			_goals.Add(new SubGoal("toilet", 1, false), 3);

			InvokeGetTired();
			InvokeNeedToPiss();
		}

		private void InvokeGetTired()
		{
			Invoke(nameof(GetTired), Random.Range(20, 30));
		}

		private void GetTired()
		{
			Beliefs.ModifyState("exhausted", 0);
			InvokeGetTired();
		}

		private void InvokeNeedToPiss()
		{
			Invoke(nameof(NeedToPiss), Random.Range(20, 30));
		}

		private void NeedToPiss()
		{
			Debug.Log("Need To Piss");
			Beliefs.ModifyState("needToPiss", 0);
			InvokeNeedToPiss();
		}
	}
}
