using UnityEngine;

namespace Project.AI.GOAP
{
	[System.Obsolete("Marked to be deprecated. Use ResourceObject instead.")]
	public class Cubicle : MonoBehaviour
	{
		public static readonly Resource Resource = new("Cubicle", "FreeCubicle");

		private void Awake()
		{
			GWorld.AddResource(Resource, gameObject);
		}
	}
}
