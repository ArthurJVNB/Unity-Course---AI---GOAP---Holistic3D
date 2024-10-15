using UnityEngine;

namespace Project.AI.GOAP
{
	public class GoToToilet : GAction
	{
		[Header("Resource")]
		[SerializeField] private ResourceData _resourceToilet;

		public override bool PrePerform()
		{
			//Target = GWorld.RemoveResource(Toilet.Resource);
			Target = GWorld.RemoveResource(_resourceToilet);
			if (!Target) return false;
			Inventory.AddItem(Target);
			return true;
		}

		public override bool PostPerform()
		{
			//GWorld.AddResource(Toilet.Resource, Target);
			GWorld.AddResource(_resourceToilet, Target);
			Inventory.RemoveItem(Target);
			Target = null;
			Beliefs.RemoveState("needRelief");
			return true;
		}
	}
}
