using UnityEngine;

namespace Project.AI.GOAP
{
	public class CleanUpPuddle : GAction
	{
		[Header("Resource")]
		[SerializeField] private ResourceData _resourcePuddle;

		public override bool PrePerform()
		{
			//Target = GWorld.RemoveResource(Puddle.Resource);
			Target = GWorld.RemoveResource(_resourcePuddle);
			if (!Target) return false;
			Inventory.AddItem(Target);
			return true;
		}

		public override bool PostPerform()
		{
			Inventory.RemoveItem(Target);
			Destroy(Target);
			Target = null;
			return true;
		}
	}
}
