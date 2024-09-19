namespace Project.AI.GOAP
{
	public class GoToWaitingRoom : GAction
	{
		public override bool PrePerform()
		{
			return true;
		}

		public override bool PostPerform()
		{
			GWorld.World.ModifyStates("Waiting", 1);
			GWorld.AddPatient(gameObject);
			return true;
		}
	}
}
