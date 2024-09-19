using UnityEngine;

namespace Project.AI.GOAP
{
    public class Cubicle : MonoBehaviour
    {
		private void Awake()
		{
			GWorld.AddCubicle(gameObject);
		}
	}
}
