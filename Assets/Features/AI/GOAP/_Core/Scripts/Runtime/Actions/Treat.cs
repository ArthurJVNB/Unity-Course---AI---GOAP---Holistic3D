namespace Project.AI.GOAP
{
	public class Treat : GAction
	{
		public override bool PrePerform()
		{
			Target = Inventory.FindItemWithTag("Cubicle");
			return Target;
		}

		public override bool PostPerform()
		{
			Inventory.RemoveItem(Target);
			return true;
		}
	}
}
