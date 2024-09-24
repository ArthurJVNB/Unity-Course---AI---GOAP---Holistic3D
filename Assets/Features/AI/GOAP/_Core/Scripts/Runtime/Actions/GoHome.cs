using UnityEngine;

namespace Project.AI.GOAP
{
	public class GoHome : GAction
	{
		public override bool PrePerform()
		{
			Beliefs.RemoveState("atHospital");
			return true;
		}

		public override bool PostPerform()
		{
			//Destroy(gameObject);
			return true;
		}
	}
}
