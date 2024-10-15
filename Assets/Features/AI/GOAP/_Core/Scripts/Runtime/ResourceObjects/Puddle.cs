using UnityEngine;

namespace Project.AI.GOAP
{
	[System.Obsolete("Marked to be deprecated. Use ResourceObject instead.")]
	public class Puddle : MonoBehaviour
	{
		public static readonly Resource Resource = new("Puddle", "Puddle");

		private void Awake()
		{
			GWorld.AddResource(Resource, gameObject);
		}
	}
}
