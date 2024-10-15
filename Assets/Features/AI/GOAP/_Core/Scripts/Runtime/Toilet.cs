using UnityEngine;

namespace Project.AI.GOAP
{
    public class Toilet : MonoBehaviour
    {
		public static readonly Resource Resource = new("Toilet", "FreeToilet");

		private void Awake()
		{
			GWorld.AddResource(Resource, gameObject);
		}

		private void OnDestroy()
		{
			GWorld.RemoveResource(Resource, gameObject);
		}
	}
}
