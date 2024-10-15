using UnityEngine;

namespace Project.AI.GOAP
{
	[System.Obsolete("Marked to be deprecated. Use ResourceObject instead.")]
	public class Office : MonoBehaviour
	{
		public static readonly Resource Resource = new("Office", "FreeOffice");

		private void Awake()
		{
			GWorld.AddResource(Resource, gameObject);
		}
	}
}
