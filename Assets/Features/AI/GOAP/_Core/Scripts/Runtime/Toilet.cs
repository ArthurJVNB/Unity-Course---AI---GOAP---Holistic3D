using UnityEngine;

namespace Project.AI.GOAP
{
    public class Toilet : MonoBehaviour
    {
		private void Awake()
		{
			GWorld.AddToilet(gameObject);
		}
	}
}
