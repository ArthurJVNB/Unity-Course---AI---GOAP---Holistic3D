namespace Project.AI.GOAP
{
	public class GoToCubicle : GAction
	{
		public override bool PrePerform()
		{
			Target = Inventory.FindItemWithTag("Cubicle");
			return Target;
		}

		public override bool PostPerform()
		{
			GWorld.World.ModifyState("TreatingPatient", 1);
			GWorld.AddResource(Cubicle.Resource, Target);
			Inventory.RemoveItem(Target);
			return true;
		}
	}
}
