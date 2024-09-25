using UnityEngine;

namespace Project.AI.GOAP
{
	public class Research : GAction
	{
		public override bool PrePerform()
		{
			//Target = GWorld.RemoveOffice();
			Target = GWorld.RemoveResource(Office.Resource);
			if (!Target) return false;
			Inventory.AddItem(Target);
			return true;
		}

		public override bool PostPerform()
		{
			//GWorld.AddOffice(Target);
			GWorld.AddResource(Office.Resource, Target);
			Inventory.RemoveItem(Target);
			Target = null;
			return true;
		}
	}
}
