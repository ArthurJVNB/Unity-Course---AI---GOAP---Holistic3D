namespace Project.AI.GOAP
{
	public class GetPatient : GAction
	{
		public override bool PrePerform()
		{
			Target = GWorld.RemovePatient();
			return Target;
		}

		public override bool PostPerform()
		{
			return true;
		}
	}
}
