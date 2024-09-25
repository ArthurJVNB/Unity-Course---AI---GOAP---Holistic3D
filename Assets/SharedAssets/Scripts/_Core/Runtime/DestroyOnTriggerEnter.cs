using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Collider))]
    public class DestroyOnTriggerEnter : MonoBehaviour
    {
		[TagSelector]
		[SerializeField] private List<string> _tags;

		private void OnTriggerEnter(Collider other)
		{
			if (_tags.Contains(other.tag))
				Destroy(other.gameObject);
		}
	}
}
