using UnityEngine;

namespace Project.AI.GOAP
{
	public class GetPatient : GAction
	{
		private GameObject _resource;

		public override bool PrePerform()
		{
			Target = GWorld.RemovePatient();
			if (!Target) return false;

			_resource = GWorld.RemoveCubicle();
			if (!_resource)
			{
				// Give back the patient
				GWorld.AddPatient(Target);
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
