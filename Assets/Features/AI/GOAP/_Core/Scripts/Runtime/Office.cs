using UnityEngine;

namespace Project.AI.GOAP
{
    public class Office : MonoBehaviour
    {
		public static readonly Resource Resource = new("Office", "FreeOffice");

		private void Awake()
		{
			GWorld.AddResource(Resource, gameObject);
		}
	}
}
