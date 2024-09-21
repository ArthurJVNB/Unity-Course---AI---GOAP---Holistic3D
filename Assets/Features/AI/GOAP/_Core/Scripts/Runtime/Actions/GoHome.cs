using UnityEngine;

namespace Project.AI.GOAP
{
	public class GoHome : GAction
	{
		public override bool PrePerform()
		{
			return true;
		}

		public override bool PostPerform()
		{
			Destroy(gameObject);
			return true;
		}
	}
}
