using UnityEngine;

namespace Project.AI.GOAP
{
	public class ResourceObject : MonoBehaviour
	{
		[SerializeField] private ResourceData _resource;

		public ResourceData Resource => _resource;

		private void Awake()
		{
			GWorld.AddResource(_resource, gameObject);
		}

		private void OnDestroy()
		{
			GWorld.RemoveResource(_resource, gameObject);
		}
	}
}
