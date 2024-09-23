using UnityEngine;

namespace Project.AI.GOAP
{
    public class Office : MonoBehaviour
    {
		private void Awake()
		{
			GWorld.AddOffice(gameObject);
		}
	}
}
