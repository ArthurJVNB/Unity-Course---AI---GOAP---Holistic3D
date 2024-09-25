using UnityEngine;

namespace Project.AI.GOAP
{
	public class GoToToilet : GAction
	{
		public override bool PrePerform()
		{
			Target = GWorld.RemoveResource(Toilet.Resource);
			if (!Target) return false;
			Inventory.AddItem(Target);
			return true;
		}

		public override bool PostPerform()
		{
			GWorld.AddResource(Toilet.Resource, Target);
			Inventory.RemoveItem(Target);
			Target = null;
			Beliefs.RemoveState("needRelief");
			return true;
		}
	}
}
