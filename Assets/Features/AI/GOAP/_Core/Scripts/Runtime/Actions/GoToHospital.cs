namespace Project.AI.GOAP
{
	public class GoToHospital : GAction
	{
		public override bool PrePerform()
		{
			return true;
		}

		public override bool PostPerform()
		{
			Beliefs.ModifyState("atHospital", 1);
			return true;
		}
	}
}
