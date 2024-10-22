using UnityEngine;

namespace Project.AI.GOAP
{
	public class GetPatient : GAction
	{
		[Header("Resources Needed")]
		[SerializeField] private ResourceData _resourcePatient;
		[SerializeField] private ResourceData _resourceCubicle;

		private GameObject _resource;

		public override bool PrePerform()
		{
			//Target = GWorld.RemoveResource(Patient.Resource);
			Target = GWorld.RemoveResource(_resourcePatient);
			if (!Target) return false;

			//_resource = GWorld.RemoveResource(Cubicle.Resource);
			_resource = GWorld.RemoveResource(_resourceCubicle);
			if (!_resource)
			{
				// Give back the patient
				//GWorld.AddResource(Patient.Resource, Target);
				GWorld.AddResource(_resourcePatient, Target);
				Target = null;
				return false;
			}

			Inventory.AddItem(_resource);

			return Target;
		}

		public override bool PostPerform()
		{
			if (Target) Target.GetComponent<GAgent>().Inventory.AddItem(_resource);
			return true;
		}
	}
}
