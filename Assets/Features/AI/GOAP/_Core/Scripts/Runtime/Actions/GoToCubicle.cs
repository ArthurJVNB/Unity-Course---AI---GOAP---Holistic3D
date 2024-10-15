using UnityEngine;

namespace Project.AI.GOAP
{
	public class GoToCubicle : GAction
	{
		private const string StateTreatingPatient = "TreatingPatient";

		[Header("Resource")]
		[SerializeField] private ResourceData _resourceCubicle;

		public override bool PrePerform()
		{
			//Target = Inventory.FindItemWithTag("Cubicle");
			Target = Inventory.FindItemWithTag(_resourceCubicle.Tag);
			if (!Target) return false;

			GWorld.World.ModifyState(StateTreatingPatient, 1);
			return true;
		}

		public override bool PostPerform()
		{
			//GWorld.World.ModifyState("TreatingPatient", 1);
			//GWorld.AddResource(Cubicle.Resource, Target);
			GWorld.World.ModifyState(StateTreatingPatient, -1);
			GWorld.AddResource(_resourceCubicle, Target);
			Inventory.RemoveItem(Target);
			return true;
		}
	}
}
