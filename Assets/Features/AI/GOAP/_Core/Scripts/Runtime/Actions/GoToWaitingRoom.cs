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
			GWorld.AddPatient(gameObject);
			//Beliefs.ModifyState("atHospital", 1);
			return true;
		}
	}
}
