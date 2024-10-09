namespace Project.AI.GOAP
{
	public class CleanUpPuddle : GAction
	{
		public override bool PrePerform()
		{
			Target = GWorld.RemoveResource(Puddle.Resource);
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
