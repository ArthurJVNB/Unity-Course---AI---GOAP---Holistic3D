using UnityEngine;

namespace Project.AI.GOAP
{
	public class Research : GAction
	{
		[Header("Resource")]
		[SerializeField] private ResourceData _resourceOffice;

		public override bool PrePerform()
		{
			//Target = GWorld.RemoveResource(Office.Resource);
			Target = GWorld.RemoveResource(_resourceOffice);
			if (!Target) return false;
			Inventory.AddItem(Target);
			return true;
		}

		public override bool PostPerform()
		{
			//GWorld.AddResource(Office.Resource, Target);
			GWorld.AddResource(_resourceOffice, Target);
			Inventory.RemoveItem(Target);
			Target = null;
			return true;
		}
	}
}
