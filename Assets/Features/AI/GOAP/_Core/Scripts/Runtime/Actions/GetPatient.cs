using UnityEngine;

namespace Project.AI.GOAP
{
	public class GetPatient : GAction
	{
		private GameObject _resource;

		public override bool PrePerform()
		{
			Target = GWorld.RemoveResource(Patient.Resource);
			if (!Target) return false;

			_resource = GWorld.RemoveResource(Cubicle.Resource);
			if (!_resource)
			{
				// Give back the patient
				GWorld.AddResource(Patient.Resource, Target);
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
