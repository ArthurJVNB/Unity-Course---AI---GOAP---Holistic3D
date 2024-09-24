using UnityEngine;

namespace Project.AI.GOAP
{
	public class GoToToilet : GAction
	{
		public override bool PrePerform()
		{
			Target = GWorld.RemoveToilet();
			if (!Target) return false;
			Inventory.AddItem(Target);
			return true;
		}

		public override bool PostPerform()
		{
			GWorld.AddToilet(Target);
			Inventory.RemoveItem(Target);
			Target = null;
			return true;
		}
	}
}
