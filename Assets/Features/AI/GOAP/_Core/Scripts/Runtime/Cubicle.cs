using UnityEngine;

namespace Project.AI.GOAP
{
	public class Cubicle : MonoBehaviour
    {
		public static readonly Resource Resource = new("Cubicle", "FreeCubicle");

		private void Awake()
		{
			//GWorld.AddCubicle(gameObject);
			GWorld.AddResource(Resource, gameObject);
		}
	}
}
