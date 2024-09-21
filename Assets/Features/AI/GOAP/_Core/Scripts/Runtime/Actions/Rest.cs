using UnityEngine;

namespace Project.AI.GOAP
{
	public class Rest : GAction
	{
		public override bool PrePerform()
		{
			return true;
		}

		public override bool PostPerform()
		{
			Beliefs.RemoveState("exhausted");
			return true;
		}
	}
}
