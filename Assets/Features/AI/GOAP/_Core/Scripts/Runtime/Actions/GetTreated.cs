using UnityEngine;

namespace Project.AI.GOAP
{
	public class GetTreated : GAction
	{
		public override bool PrePerform()
		{
			Target = Inventory.FindItemWithTag("Cubicle");
			return Target;
		}

		public override bool PostPerform()
		{
			GWorld.World.ModifyState("isTreated", 1);
			Inventory.RemoveItem(Target);
			return true;
		}
	}
}
