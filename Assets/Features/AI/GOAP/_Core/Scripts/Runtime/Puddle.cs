using UnityEngine;

namespace Project.AI.GOAP
{
    public class Puddle : MonoBehaviour
    {
        public static readonly Resource Resource = new("Puddle", "Puddle");

		private void Awake()
		{
			GWorld.AddResource(Resource, gameObject);
		}
	}
}
