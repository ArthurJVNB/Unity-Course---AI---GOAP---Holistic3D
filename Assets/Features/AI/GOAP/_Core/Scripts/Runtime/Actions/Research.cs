using UnityEngine;

namespace Project.AI.GOAP
{
	public class Research : GAction
	{
		public override bool PrePerform()
		{
			Target = GWorld.RemoveOffice();
			if (!Target) return false;
			Inventory.AddItem(Target);
			return true;
		}

		public override bool PostPerform()
		{
			GWorld.AddOffice(Target);
			Inventory.RemoveItem(Target);
			Target = null;
			return true;
		}
	}
}
