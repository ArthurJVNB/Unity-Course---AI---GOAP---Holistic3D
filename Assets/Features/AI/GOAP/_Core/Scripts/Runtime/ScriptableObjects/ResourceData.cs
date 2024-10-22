using UnityEngine;

namespace Project.AI.GOAP
{
	[CreateAssetMenu(fileName = "ResourceData", menuName = "Scriptable Objects/ResourceData")]
	public class ResourceData : ScriptableObject
	{
		public string Tag;
		public string ModifyState;
	}
}
