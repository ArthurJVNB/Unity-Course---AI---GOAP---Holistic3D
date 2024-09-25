using UnityEngine;

namespace Project.AI.GOAP
{
    public class Toilet : MonoBehaviour
    {
		public static readonly Resource Resource = new("Toilet", "FreeToilet");

		private void Awake()
		{
			//GWorld.AddToilet(gameObject);
			GWorld.AddResource(Resource, gameObject);
		}
	}
}
