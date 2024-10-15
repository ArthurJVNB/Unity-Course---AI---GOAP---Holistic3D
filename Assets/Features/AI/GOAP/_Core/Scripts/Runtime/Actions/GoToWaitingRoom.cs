using UnityEngine;

namespace Project.AI.GOAP
{
	public class GoToWaitingRoom : GAction
	{
		[Header("Resource")]
		[SerializeField] private ResourceData _resourceToGo;

		public override bool PrePerform()
		{
			return true;
		}

		public override bool PostPerform()
		{
			//GWorld.AddResource(Patient.Resource, gameObject);
			GWorld.AddResource(_resourceToGo, gameObject);
			//Beliefs.ModifyState("atHospital", 1);
			return true;
		}
	}
}
